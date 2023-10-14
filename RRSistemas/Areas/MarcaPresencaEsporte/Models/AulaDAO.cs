using RRSistemas.Areas.MarcaPresencaEsporte.Entity;
using RRSistemas.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace RRSistemas.Areas.MarcaPresencaEsporte.Models
{
    public class AulaDAO
    {
        public static Conexao conexao = new Conexao();
        public List<AulaENT> ObterAulaLista(int id_usuario)
        {
            SqlConnection cn = null;
            SqlParameter par = null;
            DbDataReader rd = null;
            TurmaENT turma = null;
            AulaENT aula = null;
            int cont = 1;

            try
            {
                cn = conexao.Conectar();
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                cmd.CommandText = @"EXEC esp.SP_AULA_LISTA_USUARIO @id_usuario = @id_usuario";
                par = cmd.CreateParameter();
                par.DbType = System.Data.DbType.Int32;
                par.ParameterName = "@id_usuario";
                par.Value = id_usuario;
                cmd.Parameters.Add(par);

                rd = cmd.ExecuteReader();

                List<AulaENT> AulaLista = new List<AulaENT>();

                while (rd.Read())
                {
                    aula = new AulaENT();

                    aula.nr_linha = cont;
                    aula.id_turma = Convert.ToInt32(rd["id_turma"]);
                    aula.id_aula = Convert.ToInt32(rd["id_aula"]);
                    aula.nm_turma = rd["nm_turma"].ToString();
                    aula.nm_professor = rd["nm_professor"].ToString();
                    aula.nm_dia_semana = rd["nm_dia_semana"].ToString();
                    aula.dt_aula = rd["dt_aula"].ToString();
                    aula.hr_inicio_aula = rd["hr_inicio_aula"].ToString();
                    aula.hr_final_aula = rd["hr_final_aula"].ToString();
                    aula.nr_qtd_alunos = Convert.ToInt32(rd["nr_qtd_alunos"]);

                    AulaLista.Add(aula);
                    cont++;

                }

                return AulaLista;
            }

            catch (Exception)
            {
                throw;
            }

            finally
            {
                if (rd != null)
                    rd.Close();
                if (cn != null)
                    cn.Close();
            }
        }

        public String ListarAula(int id_usuario)
        {
            int id_perfil = 0;

            if (HttpContext.Current.Session["id_perfil"] != null)
                id_perfil = Convert.ToInt32(HttpContext.Current.Session["id_perfil"]);

            //Monta menu pai
            String sHTML = String.Empty;

            List<AulaENT> aulaLista = ObterAulaLista(id_usuario);

            foreach (AulaENT itemAulaLista in aulaLista)
            {
                sHTML += String.Format("<tr>");
                sHTML += String.Format("        <td>{0}</td>", itemAulaLista.nr_linha);
                sHTML += String.Format("        <td>{0}</td>", itemAulaLista.nm_turma);
                sHTML += String.Format("        <td>{0}</td>", itemAulaLista.dt_aula);
                sHTML += String.Format("        <td>{0}-{1}</td>", itemAulaLista.hr_inicio_aula, itemAulaLista.hr_final_aula);
                sHTML += String.Format("        <td>{0}</td>", itemAulaLista.nr_qtd_alunos);
                //sHTML += String.Format("        <td>{0}</td>", itemTurmaLista.dv_ativo);
                //if (itemTurmaLista.id_aula != 0)
                //{
                    sHTML += String.Format("        <td align=\"center\"><a href=\"/MarcaPresencaEsporte/Presenca/ListaChamada?id_turma={0}&id_aula={1}\"><i class=\"fa fa-play\" data-toggle=\"tooltip\" data-placement=\"top\" title=\"Iniciar Aula\"></i></a></td>", itemAulaLista.id_turma, itemAulaLista.id_aula);
                //}
                //else
                //{
                //    sHTML += String.Format("        <td align=\"center\"><a href=\"/MarcaPresencaEsporte/Presenca/ListaChamada?id_turma={0}\"><i class=\"fa fa-play\" data-toggle=\"tooltip\" data-placement=\"top\" title=\"Iniciar Aula\"></i></a></td>", itemTurmaLista.id_turma);
                //}

                sHTML += String.Format("</tr>");
            }

            return sHTML;
        }

        public int InserirAula(int id_turma)
        {
            SqlConnection cn = null;
            SqlParameter par = null;

            int id_usuario = 0;
            int id_empresa = 0;

            if (HttpContext.Current.Session["id_usuario"] != null)
                id_usuario = Convert.ToInt32(HttpContext.Current.Session["id_usuario"]);

            try
            {
                cn = conexao.Conectar();

                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                cmd.CommandText = @"esp.SP_AULA_INS @id_turma = @id_turma, @id_aula = @id_aula OUTPUT";

                par = cmd.CreateParameter();
                par.DbType = System.Data.DbType.Int32;
                par.ParameterName = "@id_turma";
                par.Value = id_turma;
                cmd.Parameters.Add(par);

                par = cmd.CreateParameter();
                par.DbType = System.Data.DbType.Int32;
                par.ParameterName = "@id_aula";
                par.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(par);

                cmd.ExecuteNonQuery();

                return Convert.ToInt32(cmd.Parameters["@id_aula"].Value);
            }

            catch (Exception)
            {
                throw;
            }

            finally
            {
                if (cn != null)
                    cn.Close();
            }
        }

        public AulaENT ObterAulaPeloId(int id_turma, int id_aula)
        {
            SqlConnection cn = null;
            DbDataReader rd = null;
            SqlParameter par = null;
            AulaENT aula = null;

            try
            {
                cn = conexao.Conectar();

                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                cmd.CommandText = "EXEC esp.SP_AULA_CARREGA @id_turma = @id_turma, @id_aula = @id_aula";
                par = cmd.CreateParameter();
                par.DbType = System.Data.DbType.Int32;
                par.ParameterName = "@id_turma";
                par.Value = id_turma;
                cmd.Parameters.Add(par);

                par = cmd.CreateParameter();
                par.DbType = System.Data.DbType.Int32;
                par.ParameterName = "@id_aula";
                par.Value = id_aula;
                cmd.Parameters.Add(par);

                rd = cmd.ExecuteReader();

                while (rd.Read())
                {
                    aula = new AulaENT();

                    aula.id_aula = Convert.ToInt32(rd["id_aula"]);
                    aula.id_turma = Convert.ToInt32(rd["id_turma"]);
                    aula.nm_turma = rd["nm_turma"].ToString();
                    aula.dt_aula = rd["dt_aula"].ToString();
                    aula.hr_inicio_aula = rd["hr_inicio_aula"].ToString();
                    aula.hr_final_aula = rd["hr_final_aula"].ToString();
                    aula.id_professor = Convert.ToInt32(rd["id_professor"]);
                    aula.nm_professor = rd["nm_professor"].ToString();
                    //aula.dv_aula_dada = Convert.ToBoolean(reader["dv_aula_dada"]);
                    aula.dv_ativo = Convert.ToBoolean(rd["dv_ativo"]);
                }

                return aula;
            }

            catch (Exception)
            {

                throw;
            }

            finally
            {
                if (rd != null)
                    rd.Close();
                if (cn != null)
                    cn.Close();
            }
        }
 

        //MÉTODOS QUE CARREGAM AS AULAS PARA A LISTA DE CHAMADA

        public List<AulaListaENT> ObterAulaChamada(int id_empresa, int id_usuario)
        {
            SqlConnection cn = null;
            DbDataReader rd = null;
            SqlParameter par = null;
            AulaListaENT aula = null;

            try
            {
                cn = conexao.Conectar();

                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                cmd.CommandText = @"EXEC esp.SP_CHAMADA_LISTA_AULA @id_empresa = @id_empresa, @id_usuario = @id_usuario";
                par = cmd.CreateParameter();
                par.DbType = System.Data.DbType.Int32;
                par.ParameterName = "@id_empresa";
                par.Value = id_empresa;
                cmd.Parameters.Add(par);

                par = cmd.CreateParameter();
                par.DbType = System.Data.DbType.Int32;
                par.ParameterName = "@id_usuario";
                par.Value = id_usuario;
                cmd.Parameters.Add(par);

                rd = cmd.ExecuteReader();

                List<AulaListaENT> ListaAula = new List<AulaListaENT>();


                if (rd.Read())
                {
                    do
                    {
                        aula = new AulaListaENT();

                        aula.nr_linha = Convert.ToInt32(rd["nr_linha"]);
                        aula.id_aula = Convert.ToInt32(rd["id_aula"]);
                        aula.nm_turma = rd["nm_turma"].ToString();
                        aula.nm_professor = rd["nm_usuario_professor"].ToString();
                        aula.dt_aula = rd["dt_aula"].ToString();
                        aula.hr_aula = rd["hr_aula"].ToString();
                        aula.dv_ativo = Convert.ToBoolean(rd["dv_ativo"]);

                        ListaAula.Add(aula);

                    }
                    while (rd.Read());
                }

                return ListaAula;
            }

            catch (Exception)
            {
                throw;
            }

            finally
            {
                if (rd != null)
                    rd.Close();
                if (cn != null)
                    cn.Close();
            }
        }

        public String ListaAulaChamada(int id_empresa, int id_usuario)
        {
            //Monta menu pai
            String sHTML = String.Empty;
            List<AulaListaENT> listaAula = ObterAulaChamada(id_empresa, id_usuario);

            foreach (AulaListaENT itemAula in listaAula)
            {
                sHTML += String.Format("<tr>");
                sHTML += String.Format("        <td>{0}</td>", itemAula.nr_linha);
                sHTML += String.Format("        <td>{0}</td>", itemAula.nm_turma);
                sHTML += String.Format("        <td>{0}</td>", itemAula.nm_professor);
                sHTML += String.Format("        <td>{0}</td>", itemAula.dt_aula);
                sHTML += String.Format("        <td>{0}</td>", itemAula.hr_aula);
                sHTML += String.Format("        <td><a href=\"ChamadaLista.aspx?id_aula={0}\">Iniciar aula</a></td>", itemAula.id_aula);
                sHTML += String.Format("</tr>");
            }

            return sHTML;
        }
    }
}