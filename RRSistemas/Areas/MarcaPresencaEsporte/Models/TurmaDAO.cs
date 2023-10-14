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
    public class TurmaDAO
    {
        public static Conexao conexao = new Conexao();
        public List<TurmaENT> ObterTurmaLista(int id_usuario)
        {
            SqlConnection cn = null;
            SqlParameter par = null;
            DbDataReader rd = null;
            TurmaENT turma = null;
            int cont = 1;

            try
            {
                cn = conexao.Conectar();
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                cmd.CommandText = @"EXEC esp.SP_TURMA_LISTA @id_usuario = @id_usuario";
                par = cmd.CreateParameter();
                par.DbType = System.Data.DbType.Int32;
                par.ParameterName = "@id_usuario";
                par.Value = id_usuario;
                cmd.Parameters.Add(par);

                rd = cmd.ExecuteReader();

                List<TurmaENT> TurmaLista = new List<TurmaENT>();

                while (rd.Read())
                {
                    turma = new TurmaENT();

                    turma.nr_linha = cont;
                    turma.id_turma = Convert.ToInt32(rd["id_turma"]);
                    turma.id_aula = Convert.ToInt32(rd["id_aula"]);
                    turma.nm_turma = rd["nm_turma"].ToString();
                    turma.nm_usuario_professor = rd["nm_professor"].ToString();
                    turma.nm_dia_semana = rd["nm_dia_semana"].ToString();
                    turma.hr_inicio_aula = rd["hr_inicio_aula"].ToString();
                    turma.hr_final_aula = rd["hr_final_aula"].ToString();
                    turma.nr_vagas_total = Convert.ToInt32(rd["nr_vagas_total"]);
                    turma.nr_vagas_disponiveis = Convert.ToInt32(rd["nr_vagas_disponiveis"]);
                    turma.nr_qtd_aluno = Convert.ToInt32(rd["nr_qtd_aluno"]);
                    turma.dv_aula_liberada = Convert.ToInt32(rd["dv_aula_liberada"]);

                    TurmaLista.Add(turma);
                    cont++;

                }
                    
                return TurmaLista;
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

        public String ListarTurma(int id_usuario)
        {
            int id_perfil = 0;

            if (HttpContext.Current.Session["id_perfil"] != null)
                id_perfil = Convert.ToInt32(HttpContext.Current.Session["id_perfil"]);

            String sHTML = String.Empty;

            List<TurmaENT> turmaLista = ObterTurmaLista(id_usuario);

            foreach (TurmaENT itemTurmaLista in turmaLista)
            {
                sHTML += String.Format("<tr>");
                sHTML += String.Format("        <td>{0}</td>", itemTurmaLista.nr_linha);
                sHTML += String.Format("        <td>{0}</td>", itemTurmaLista.nm_usuario_professor);
                sHTML += String.Format("        <td>{0}</td>", itemTurmaLista.nm_turma);
                sHTML += String.Format("        <td>{0}</td>", itemTurmaLista.nm_dia_semana);
                sHTML += String.Format("        <td>{0}-{1}</td>", itemTurmaLista.hr_inicio_aula, itemTurmaLista.hr_final_aula);

                if (id_perfil == 8)
                {
                    sHTML += String.Format("        <td align='center'>{0}</td>", itemTurmaLista.nr_qtd_aluno);
                }
                else
                {
                    sHTML += String.Format("        <td>{0}/{1}</td>", itemTurmaLista.nr_vagas_disponiveis, itemTurmaLista.nr_vagas_total);
                }
                
                //sHTML += String.Format("        <td>{0}</td>", itemTurmaLista.dv_ativo);

                if (id_perfil != 8)
                { 
                    sHTML += String.Format("        <td><a href=/MarcaPresencaEsporte/Cadastro/TurmaCadastro?id_turma={0}><i class=\"fa fa-fw fa-edit\"title=\"Editar Turma\"></i></a></td>", itemTurmaLista.id_turma);
                }

                sHTML += String.Format("</tr>");
            }

            return sHTML;
        }

        public int InserirEditarTurma(TurmaENT turma)
        {
            int id_usuario = 0;
            int id_empresa = 0;

            if (HttpContext.Current.Session["id_usuario"] != null)
                id_usuario = Convert.ToInt32(HttpContext.Current.Session["id_usuario"]);

            if (HttpContext.Current.Session["id_empresa"] != null)
                id_empresa = Convert.ToInt32(HttpContext.Current.Session["id_empresa"]);

            SqlConnection cn = null;

            try
            {
                cn = conexao.Conectar();
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                cmd.CommandText = @"EXEC esp.SP_TURMA_INS_ALT    @id_turma				   = @id_turma				
                                                               ,@id_empresa			       = @id_empresa			
                                                               ,@id_professor	        = @id_professor	
                                                               ,@nm_turma				   = @nm_turma				
                                                               ,@ds_turma				   = @ds_turma				
                                                               ,@hr_inicio_aula		       = @hr_inicio_aula		
                                                               ,@hr_final_aula			   = @hr_final_aula			
                                                               ,@dv_domingo			       = @dv_domingo			
                                                               ,@dv_segunda			       = @dv_segunda			
                                                               ,@dv_terca				   = @dv_terca				
                                                               ,@dv_quarta				   = @dv_quarta				
                                                               ,@dv_quinta				   = @dv_quinta				
                                                               ,@dv_sexta				   = @dv_sexta				
                                                               ,@dv_sabado				   = @dv_sabado				
                                                               ,@nr_vagas_total		       = @nr_vagas_total		
                                                               ,@dv_ativo				   = @dv_ativo				
                                                               ,@id_usuario_sistema	       = @id_usuario_sistema	           		
                                            ";

                cmd.Parameters.AddWithValue("@id_turma", turma.id_turma);
                cmd.Parameters.AddWithValue("@id_empresa", id_empresa);
                cmd.Parameters.AddWithValue("@id_professor", turma.id_professor);
                cmd.Parameters.AddWithValue("@nm_turma", turma.nm_turma);
                cmd.Parameters.AddWithValue("@ds_turma", turma.ds_turma);
                cmd.Parameters.AddWithValue("@hr_inicio_aula", turma.hr_inicio_aula);
                cmd.Parameters.AddWithValue("@hr_final_aula", turma.hr_final_aula);
                cmd.Parameters.AddWithValue("@dv_domingo", turma.dv_domingo);
                cmd.Parameters.AddWithValue("@dv_segunda", turma.dv_segunda);
                cmd.Parameters.AddWithValue("@dv_terca", turma.dv_terca);
                cmd.Parameters.AddWithValue("@dv_quarta", turma.dv_quarta);
                cmd.Parameters.AddWithValue("@dv_quinta", turma.dv_quinta);
                cmd.Parameters.AddWithValue("@dv_sexta", turma.dv_sexta);
                cmd.Parameters.AddWithValue("@dv_sabado", turma.dv_sabado);
                cmd.Parameters.AddWithValue("@nr_vagas_total", turma.nr_vagas_total);
                cmd.Parameters.AddWithValue("@dv_ativo", turma.dv_ativo);
                cmd.Parameters.AddWithValue("@id_usuario_sistema", id_usuario);

                return cmd.ExecuteNonQuery();
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

        public TurmaENT ObterTurmaPeloId(int id_turma)
        {
            SqlConnection cn = null;
            SqlParameter par = null;
            DbDataReader rd = null;
            TurmaENT turma = null;

            try
            {
                cn = conexao.Conectar();
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                cmd.CommandText = "EXEC esp.SP_TURMA_CARREGA @id_turma = @id_turma";
                par = cmd.CreateParameter();
                par.DbType = System.Data.DbType.Int32;
                par.ParameterName = "@id_turma";
                par.Value = id_turma;
                cmd.Parameters.Add(par);

                rd = cmd.ExecuteReader();

                while (rd.Read())
                {
                    turma = new TurmaENT();

                    turma.id_turma = Convert.ToInt32(rd["id_turma"]);
                    turma.id_professor = Convert.ToInt32(rd["id_professor"]);
                    turma.nm_turma = rd["nm_turma"].ToString();
                    turma.ds_turma = rd["ds_turma"].ToString();
                    turma.nr_vagas_total = Convert.ToInt32(rd["nr_vagas_total"]);
                    turma.hr_inicio_aula = rd["hr_inicio_aula"].ToString();
                    turma.hr_final_aula = rd["hr_final_aula"].ToString();
                    turma.dv_domingo = Convert.ToBoolean(rd["dv_domingo"]);
                    turma.dv_segunda = Convert.ToBoolean(rd["dv_segunda"]);
                    turma.dv_terca = Convert.ToBoolean(rd["dv_terca"]);
                    turma.dv_quarta = Convert.ToBoolean(rd["dv_quarta"]);
                    turma.dv_quinta = Convert.ToBoolean(rd["dv_quinta"]);
                    turma.dv_sexta = Convert.ToBoolean(rd["dv_sexta"]);
                    turma.dv_sabado = Convert.ToBoolean(rd["dv_sabado"]);
                    turma.dv_ativo = Convert.ToBoolean(rd["dv_ativo"]);
                }

                return turma;
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

        public List<TurmaENT> CarregaComboTurmaAlunoDisp(int id_aluno)
        {
            SqlConnection cn = null;
            DbDataReader rd = null;
            SqlParameter par = null;
            TurmaENT turma = null;



            try
            {
                cn = conexao.Conectar();
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                cmd.CommandText = "esp.SP_COMBO_TURMA_ALUNO_DISP @id_aluno = @id_aluno";
                par = cmd.CreateParameter();
                par.DbType = System.Data.DbType.Int32;
                par.ParameterName = "@id_aluno";
                par.Value = id_aluno;
                cmd.Parameters.Add(par);

                rd = cmd.ExecuteReader();

                var ComboTurmas = new List<TurmaENT>();
                //ComboTurmas.Insert(0, new TurmaENT() { id_turma = 0, nm_turma = "Selecione a turma" });

                while (rd.Read())
                {
                    var ComboTurma = new TurmaENT();

                    ComboTurma.id_turma = Convert.ToInt32(rd["id_turma"]);
                    ComboTurma.nm_turma = rd["nm_turma"].ToString();


                    ComboTurmas.Add(ComboTurma);
                }

                return ComboTurmas;
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

        public int TurmaAlunoAdd(int id_aluno, int id_turma)
        {
            int id_usuario = 0;
            int id_empresa = 0;

            if (HttpContext.Current.Session["id_usuario"] != null)
                id_usuario = Convert.ToInt32(HttpContext.Current.Session["id_usuario"]);

            if (HttpContext.Current.Session["id_empresa"] != null)
                id_empresa = Convert.ToInt32(HttpContext.Current.Session["id_empresa"]);

            SqlConnection cn = null;

            try
            {
                cn = conexao.Conectar();
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                cmd.CommandText = @"EXEC esp.SP_TURMA_ALUNO_ADD @id_turma = @id_turma				
                                                               ,@id_aluno = @id_aluno	           		
                                            ";

                cmd.Parameters.AddWithValue("@id_turma", id_turma);
                cmd.Parameters.AddWithValue("@id_aluno", id_aluno);


                return cmd.ExecuteNonQuery();
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

        public int TurmaAlunoDel(int id_turma_aluno)
        {
            SqlConnection cn = null;

            try
            {
                cn = conexao.Conectar();
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                cmd.CommandText = @"delete from esp.tb_turma_aluno where id_turma_aluno = @id_turma_aluno";

                cmd.Parameters.AddWithValue("@id_turma_aluno", id_turma_aluno);

                return cmd.ExecuteNonQuery();
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

    }
}