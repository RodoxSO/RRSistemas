using RRSistemas.Areas.MarcaPresencaEsporte.Entity;
using RRSistemas.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace RRSistemas.Areas.MarcaPresencaEsporte.Models
{
    public class PresencaDAO
    {

        int id_usuario = HttpContext.Current.Session["id_usuario"] != null ? Convert.ToInt32(HttpContext.Current.Session["id_usuario"]) : 0;
        int id_empresa = HttpContext.Current.Session["id_empresa"] != null ? Convert.ToInt32(HttpContext.Current.Session["id_empresa"]) : 0;
        
        public static Conexao conexao = new Conexao();
        SqlConnection con = null;
        SqlCommand cmd = null;
        SqlParameter par = null;
        DbDataReader rd = null;

        PresencaENT presencaENT = null;
        public List<PresencaENT> ObterChamadaLista(int id_aula)
        {
            try
            {
                con = conexao.Conectar();
                cmd = con.CreateCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                cmd.CommandText = @"EXEC esp.SP_CHAMADA_LISTA @id_aula = @id_aula";
                par = cmd.CreateParameter();
                par.DbType = System.Data.DbType.Int32;
                par.ParameterName = "@id_aula";
                par.Value = id_aula;
                cmd.Parameters.Add(par);

                rd = cmd.ExecuteReader();

                List<PresencaENT> ListaChamada = new List<PresencaENT>();

                while (rd.Read())
                {
                    presencaENT = new PresencaENT();

                    presencaENT.id_turma_aluno = Convert.ToInt32(rd["id_turma_aluno"]);
                    presencaENT.id_turma = Convert.ToInt32(rd["id_turma"]);
                    presencaENT.id_aula = Convert.ToInt32(rd["id_aula"]);
                    presencaENT.id_aluno = Convert.ToInt32(rd["id_aluno"]);
                    presencaENT.nr_aluno = Convert.ToInt32(rd["nr_aluno"]);
                    presencaENT.nm_aluno = rd["nm_aluno"].ToString();
                    presencaENT.nm_foto_aluno = (String.IsNullOrEmpty(rd["nm_foto_aluno"].ToString())) ? null : rd["nm_foto_aluno"].ToString();
                    presencaENT.dv_presenca = Convert.ToBoolean(rd["dv_presenca"]);

                    ListaChamada.Add(presencaENT);
                }

                return ListaChamada;
            }

            catch (Exception Ex)
            {
                throw  Ex;
            }

            finally
            {
                if (rd != null)
                    rd.Close();
                if (con != null)
                    con.Close();
            }
        }

        public String ListarChamada(int id_aula)
        {
            //Monta menu pai
            String sHTML = String.Empty;
            String str_checked = String.Empty;

            List<PresencaENT> chamada = ObterChamadaLista(id_aula);

            foreach (PresencaENT itemChamada in chamada)
            {
                if (itemChamada.dv_presenca == true) {
                    str_checked = "checked = 'checked' disabled = 'disabled'";
                }else
                {
                    str_checked = "";
                }

                sHTML += String.Format("<tr>");
                //sHTML += String.Format("        <td>{0}</td>", itemPresenca.nr_aluno);
                sHTML += String.Format("        <td>{0}</td>", itemChamada.nm_aluno);
                sHTML += String.Format("        <td align=\"center\"><input type=\"checkbox\" name=\"CheckPresenca\" value=\"{0}\" {1}/></td>", itemChamada.id_aluno, str_checked);
                //sHTML += String.Format("        <td>{0}</td>", itemPresenca.dv_presenca);
                //sHTML += String.Format("        <td>{0}</td>", itemTurmaLista.dv_ativo);
                //sHTML += String.Format("        <td><a href=/MarcaPresencaEsporte/Cadastro/TurmaCadastro?id_turma={0}><i class=\"fa fa-fw fa-edit\"title=\"Editar Turma\"></i></a></td>", itemTurmaLista.id_turma);
                //sHTML += String.Format("        <td align=\"center\"><a href=\"/MarcaPresencaEsporte/ListaPresenca/ListaPresenca\"><i class=\"fa fa-play\" data-toggle=\"tooltip\" data-placement=\"top\" title=\"Iniciar Aula\"></i></a></td>");
                sHTML += String.Format("</tr>");
            }

            return sHTML;
        }

        public int InserirPresenca(int id_aula, int id_aluno)
        {
            /*
            int id_usuario = 0;
            int id_empresa = 0;

            if (HttpContext.Current.Session["id_usuario"] != null)
                id_usuario = Convert.ToInt32(HttpContext.Current.Session["id_usuario"]);

            if (HttpContext.Current.Session["id_empresa"] != null)
                id_empresa = Convert.ToInt32(HttpContext.Current.Session["id_empresa"]);
            */

            try
            {
                con = conexao.Conectar();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                cmd.CommandText = @"EXEC esp.SP_PRESENCA_INS @id_aula = @id_aula, @id_aluno = @id_aluno";

                cmd.Parameters.AddWithValue("@id_aula", id_aula);
                cmd.Parameters.AddWithValue("@id_aluno", id_aluno);

                return cmd.ExecuteNonQuery();
            }

            catch (Exception Ex)
            {
                throw;
            }

            finally
            {
                if (con != null)
                    con.Close();
            }
        }

        public List<PresencaENT> ObterPresencaLista()
        {
            try
            {
                con = conexao.Conectar();
                cmd = con.CreateCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                cmd.CommandText = @"EXEC esp.SP_PRESENCA_LISTA @id_usuario = @id_usuario";
                par = cmd.CreateParameter();
                par.DbType = System.Data.DbType.Int32;
                par.ParameterName = "@id_usuario";
                par.Value = id_usuario;
                cmd.Parameters.Add(par);

                rd = cmd.ExecuteReader();

                List<PresencaENT> ListaPresenca = new List<PresencaENT>();

                while (rd.Read())
                {
                    presencaENT = new PresencaENT();

                    presencaENT.id_aula = Convert.ToInt32(rd["id_aula"]);
                    presencaENT.id_turma = Convert.ToInt32(rd["id_turma"]);
                    presencaENT.nm_turma = rd["nm_turma"].ToString();
                    presencaENT.nm_professor = rd["nm_professor"].ToString();
                    presencaENT.dt_aula = rd["dt_aula"].ToString();
                    presencaENT.hr_aula = rd["hr_aula"].ToString();
                    presencaENT.dt_aula_semana = rd["dt_aula_semana"].ToString();

                    ListaPresenca.Add(presencaENT);
                }

                return ListaPresenca;
            }

            catch (Exception Ex)
            {
                throw Ex;
            }

            finally
            {
                if (rd != null)
                    rd.Close();
                if (con != null)
                    con.Close();
            }
        }

        public String ListarPresenca()
        {
            //Monta menu pai
            String sHTML = String.Empty;
            String str_checked = String.Empty;

            List<PresencaENT> presenca = ObterPresencaLista();

            foreach (PresencaENT itemPresenca in presenca)
            {
                if (itemPresenca.dv_presenca == true)
                {
                    str_checked = "checked = 'checked' disabled = 'disabled'";
                }
                else
                {
                    str_checked = "";
                }

                sHTML += String.Format("<tr>");
                //sHTML += String.Format("        <td>{0}</td>", itemPresenca.nr_aluno);
                sHTML += String.Format("        <td>{0}</td>", itemPresenca.nm_turma);
                sHTML += String.Format("        <td>{0}</td>", itemPresenca.dt_aula);
                sHTML += String.Format("        <td>{0}</td>", itemPresenca.hr_aula);
                sHTML += String.Format("        <td>{0}</td>", itemPresenca.dt_aula_semana);
                sHTML += String.Format("        <td align=\"center\"><a href=\"/MarcaPresencaEsporte/Presenca/ListaPresenca?&id_aula={0}\"><i class=\"fa fa-paste\" data-toggle=\"tooltip\" data-placement=\"top\" title=\"Lista de Presença\"></i></a></td>", itemPresenca.id_aula);
                //sHTML += String.Format("        <td align=\"center\"><input type=\"checkbox\" name=\"CheckPresenca\" value=\"{0}\" {1}/></td>", itemPresenca.id_aluno, str_checked);
                //sHTML += String.Format("        <td>{0}</td>", itemPresenca.dv_presenca);
                //sHTML += String.Format("        <td>{0}</td>", itemTurmaLista.dv_ativo);
                //sHTML += String.Format("        <td><a href=/MarcaPresencaEsporte/Cadastro/TurmaCadastro?id_turma={0}><i class=\"fa fa-fw fa-edit\"title=\"Editar Turma\"></i></a></td>", itemTurmaLista.id_turma);
                //sHTML += String.Format("        <td align=\"center\"><a href=\"/MarcaPresencaEsporte/ListaPresenca/ListaPresenca\"><i class=\"fa fa-play\" data-toggle=\"tooltip\" data-placement=\"top\" title=\"Iniciar Aula\"></i></a></td>");
                sHTML += String.Format("</tr>");
            }

            return sHTML;
        }

        public String ListarPresencaAula(int id_aula)
        {
            //Monta menu pai
            String sHTML = String.Empty;
            String dv_presenca = String.Empty;

            List<PresencaENT> chamada = ObterChamadaLista(id_aula);

            foreach (PresencaENT itemChamada in chamada)
            {
                if (itemChamada.dv_presenca == true)
                {
                    dv_presenca = "Presente";
                }
                else
                {
                    dv_presenca = "Falta";
                }

                sHTML += String.Format("<tr>");
                //sHTML += String.Format("        <td>{0}</td>", itemPresenca.nr_aluno);
                sHTML += String.Format("        <td>{0}</td>", itemChamada.nm_aluno);
                sHTML += String.Format("        <td align=\"center\">{0}</td>", dv_presenca);
                //sHTML += String.Format("        <td>{0}</td>", itemPresenca.dv_presenca);
                //sHTML += String.Format("        <td>{0}</td>", itemTurmaLista.dv_ativo);
                //sHTML += String.Format("        <td><a href=/MarcaPresencaEsporte/Cadastro/TurmaCadastro?id_turma={0}><i class=\"fa fa-fw fa-edit\"title=\"Editar Turma\"></i></a></td>", itemTurmaLista.id_turma);
                //sHTML += String.Format("        <td align=\"center\"><a href=\"/MarcaPresencaEsporte/ListaPresenca/ListaPresenca\"><i class=\"fa fa-play\" data-toggle=\"tooltip\" data-placement=\"top\" title=\"Iniciar Aula\"></i></a></td>");
                sHTML += String.Format("</tr>");
            }

            return sHTML;
        }
    }
}