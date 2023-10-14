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
    public class AlunoDAO
    {
        public static Conexao conexao = new Conexao();
        int id_empresa = 0;
        int cont = 1;

        public List<AlunoENT> ObterListaAluno(int id_usuario)
        {
            SqlConnection cn = null;
            DbDataReader rd = null;
            SqlParameter par = null;
            AlunoENT aluno = null;

            try
            {
                cn = conexao.Conectar();
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                cmd.CommandText = @"esp.SP_ALUNO_LISTA @id_usuario = @id_usuario";
                par = cmd.CreateParameter();
                par.DbType = System.Data.DbType.Int32;
                par.ParameterName = "@id_usuario";
                par.Value = id_usuario;
                cmd.Parameters.Add(par);

                rd = cmd.ExecuteReader();

                List<AlunoENT> ListaAluno = new List<AlunoENT>();

                while (rd.Read())
                {
                    aluno = new AlunoENT();

                    aluno.nr_linha = cont;
                    aluno.id_usuario_responsavel = Convert.ToInt32(rd["id_usuario_responsavel"]);
                    aluno.id_aluno = Convert.ToInt32(rd["id_aluno"]);
                    aluno.nr_matricula = rd["nr_matricula"].ToString();
                    aluno.nm_aluno = rd["nm_aluno"].ToString();
                    aluno.dt_nascimento = rd["dt_nascimento"].ToString();
                    aluno.nm_sexo = rd["nm_sexo"].ToString();
                    aluno.nm_foto_aluno = rd["nm_foto_aluno"].ToString();
                    aluno.nm_usuario = rd["nm_usuario"].ToString();
                    aluno.dv_ativo = Convert.ToBoolean(rd["dv_ativo"]);

                    ListaAluno.Add(aluno);
                    cont++;
                }

                return ListaAluno;
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

        public AlunoENT ObterAlunoPeloId(int id_aluno)
        {
            SqlConnection cn = null;
            DbDataReader rd = null;
            SqlParameter par = null;
            AlunoENT aluno = null;

            try
            {
                cn = conexao.Conectar();
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                cmd.CommandText = "EXEC esp.SP_ALUNO_CARREGA @id_aluno = @id_aluno";
                par = cmd.CreateParameter();
                par.DbType = System.Data.DbType.Int32;
                par.ParameterName = "@id_aluno";
                par.Value = id_aluno;
                cmd.Parameters.Add(par);

                rd = cmd.ExecuteReader();

                //List<AlunoENT> ListaAluno = new List<AlunoENT>();

                while (rd.Read())
                {
                    aluno = new AlunoENT();

                    aluno.id_aluno = Convert.ToInt32(rd["id_aluno"]);
                    aluno.id_usuario_responsavel = Convert.ToInt32(rd["id_usuario_responsavel"]);
                    aluno.id_tipo_sanguineo = Convert.ToInt32(rd["id_tipo_sanguineo"]);
                    aluno.id_sexo = Convert.ToInt32(rd["id_sexo"]);
                    aluno.nm_responsavel = rd["nm_responsavel"].ToString();
                    aluno.cd_responsavel = rd["cd_responsavel"].ToString();
                    aluno.nr_matricula = rd["nr_matricula"].ToString();
                    aluno.nm_aluno = rd["nm_aluno"].ToString();
                    aluno.dt_nascimento = (String.IsNullOrEmpty(rd["dt_nascimento"].ToString())) ? null : rd["dt_nascimento"].ToString();  //Convert.ToDateTime(rd["dt_nascimento"]);
                    aluno.nr_rg = rd["nr_rg"].ToString();
                    aluno.nm_rg_orgao_emissor = rd["nm_rg_orgao_emissor"].ToString();
                    aluno.dt_rg_expedicao = (String.IsNullOrEmpty(rd["dt_rg_expedicao"].ToString())) ? null : rd["dt_rg_expedicao"].ToString();  //Convert.ToDateTime(rd["dt_rg_expedicao"]);
                    aluno.nr_cpf = rd["nr_cpf"].ToString();
                    aluno.nm_foto_aluno = (String.IsNullOrEmpty(rd["nm_foto_aluno"].ToString())) ? null : rd["nm_foto_aluno"].ToString();
                    aluno.nr_telefone = rd["nr_telefone"].ToString();
                    aluno.nr_celular = rd["nr_celular"].ToString();
                    aluno.nm_email = rd["nm_email"].ToString();
                    aluno.nr_altura = Convert.ToInt32(rd["nr_altura"]);
                    aluno.nr_peso = Convert.ToInt32(rd["nr_peso"]);
                    aluno.nr_pressao = rd["nr_pressao"].ToString();
                    aluno.dv_atividade_fisica = Convert.ToInt32(rd["dv_atividade_fisica"]);
                    aluno.ds_atividade_fisica = rd["ds_atividade_fisica"].ToString();
                    aluno.dv_alergia = Convert.ToInt32(rd["dv_alergia"]);
                    aluno.ds_alergia = rd["ds_alergia"].ToString();
                    aluno.dv_alergia_medicamento = Convert.ToInt32(rd["dv_alergia_medicamento"]);
                    aluno.ds_alergia_medicamento = rd["ds_alergia_medicamento"].ToString();
                    aluno.dv_medicamento = Convert.ToInt32(rd["dv_medicamento"]);
                    aluno.ds_medicamento = rd["ds_medicamento"].ToString();
                    aluno.dv_cirurgia = Convert.ToInt32(rd["dv_cirurgia"]);
                    aluno.ds_cirurgia = rd["ds_cirurgia"].ToString();
                    aluno.dv_doenca_cronica = Convert.ToInt32(rd["dv_doenca_cronica"]);
                    aluno.ds_doenca_cronica = rd["ds_doenca_cronica"].ToString();
                    aluno.dv_diabetes = Convert.ToInt32(rd["dv_diabetes"]);
                    aluno.dv_pressao_alta = Convert.ToInt32(rd["dv_pressao_alta"]);
                    aluno.dt_cadastro = Convert.ToDateTime(rd["dt_cadastro"]);
                    aluno.id_usuario_cadastro = Convert.ToInt32(rd["id_usuario_cadastro"]);
                    aluno.dv_ativo = Convert.ToBoolean(rd["dv_ativo"]);

                    //ListaAluno.Add(aluno);
                }

                return aluno;
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

        public int InserirAlterarAluno(AlunoENT aluno)
        {
            SqlConnection cn = null;
            SqlParameter par = null;
            
            try
            {
                cn = conexao.Conectar();
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                cmd.CommandText = @"EXEC esp.SP_ALUNO_INS_ALT     @id_aluno				          = @id_aluno				
                                                                 ,@id_usuario_responsavel         = @id_usuario_responsavel
                                                                 ,@id_tipo_sanguineo		      = @id_tipo_sanguineo		
                                                                 ,@id_sexo				          = @id_sexo				
                                                                 ,@nm_aluno				          = @nm_aluno				
                                                                 ,@dt_nascimento			      = @dt_nascimento			
                                                                 ,@nr_rg					      = @nr_rg					
                                                                 ,@nm_rg_orgao_emissor	          = @nm_rg_orgao_emissor	
                                                                 ,@dt_rg_expedicao		          = @dt_rg_expedicao		
                                                                 ,@nr_cpf				          = @nr_cpf				
                                                                 ,@nm_foto_aluno			      = @nm_foto_aluno			
                                                                 ,@nr_telefone			          = @nr_telefone			
                                                                 ,@nr_celular			          = @nr_celular			
                                                                 ,@nm_email				          = @nm_email				
                                                                 ,@nr_altura				      = @nr_altura				
                                                                 ,@nr_peso				          = @nr_peso				
                                                                 ,@nr_pressao			          = @nr_pressao			
                                                                 ,@dv_atividade_fisica	          = @dv_atividade_fisica	
                                                                 ,@ds_atividade_fisica	          = @ds_atividade_fisica	
                                                                 ,@dv_alergia			          = @dv_alergia			
                                                                 ,@ds_alergia			          = @ds_alergia			
                                                                 ,@dv_alergia_medicamento         = @dv_alergia_medicamento
                                                                 ,@ds_alergia_medicamento         = @ds_alergia_medicamento
                                                                 ,@dv_medicamento		          = @dv_medicamento		
                                                                 ,@ds_medicamento		          = @ds_medicamento		
                                                                 ,@dv_cirurgia			          = @dv_cirurgia			
                                                                 ,@ds_cirurgia			          = @ds_cirurgia			
                                                                 ,@dv_doenca_cronica		      = @dv_doenca_cronica		
                                                                 ,@ds_doenca_cronica		      = @ds_doenca_cronica		
                                                                 ,@dv_diabetes			          = @dv_diabetes			
                                                                 ,@dv_pressao_alta		          = @dv_pressao_alta		
                                                                 ,@id_usuario_cadastro	          = @id_usuario_cadastro	
                                                                 ,@dv_ativo				          = @dv_ativo ";


                cmd.Parameters.AddWithValue("@id_aluno", aluno.id_aluno);
                cmd.Parameters.AddWithValue("@id_usuario_responsavel", aluno.id_usuario_responsavel);
                cmd.Parameters.AddWithValue("@id_tipo_sanguineo", aluno.id_tipo_sanguineo);
                cmd.Parameters.AddWithValue("@id_sexo", aluno.id_sexo);
                cmd.Parameters.AddWithValue("@nm_aluno", aluno.nm_aluno);
                cmd.Parameters.AddWithValue("@dt_nascimento", aluno.dt_nascimento);
                cmd.Parameters.AddWithValue("@nr_rg", aluno.nr_rg);
                cmd.Parameters.AddWithValue("@nm_rg_orgao_emissor", aluno.nm_rg_orgao_emissor);
                cmd.Parameters.AddWithValue("@dt_rg_expedicao", aluno.dt_rg_expedicao);
                cmd.Parameters.AddWithValue("@nr_cpf", aluno.nr_cpf);
                cmd.Parameters.AddWithValue("@nm_foto_aluno", "teste.jpg"); //aluno.nm_foto_aluno);
                cmd.Parameters.AddWithValue("@nr_telefone", aluno.nr_telefone);
                cmd.Parameters.AddWithValue("@nr_celular", aluno.nr_celular);
                cmd.Parameters.AddWithValue("@nm_email", aluno.nm_email);
                cmd.Parameters.AddWithValue("@nr_altura", aluno.nr_altura);
                cmd.Parameters.AddWithValue("@nr_peso", aluno.nr_peso);
                cmd.Parameters.AddWithValue("@nr_pressao", aluno.nr_pressao);
                cmd.Parameters.AddWithValue("@dv_atividade_fisica", aluno.dv_atividade_fisica);
                cmd.Parameters.AddWithValue("@ds_atividade_fisica", aluno.ds_atividade_fisica);
                cmd.Parameters.AddWithValue("@dv_alergia", aluno.dv_alergia);
                cmd.Parameters.AddWithValue("@ds_alergia", aluno.ds_alergia);
                cmd.Parameters.AddWithValue("@dv_alergia_medicamento", aluno.dv_alergia_medicamento);
                cmd.Parameters.AddWithValue("@ds_alergia_medicamento", aluno.ds_alergia_medicamento);
                cmd.Parameters.AddWithValue("@dv_medicamento", aluno.dv_medicamento);
                cmd.Parameters.AddWithValue("@ds_medicamento", aluno.ds_medicamento);
                cmd.Parameters.AddWithValue("@dv_cirurgia", aluno.dv_cirurgia);
                cmd.Parameters.AddWithValue("@ds_cirurgia", aluno.ds_cirurgia);
                cmd.Parameters.AddWithValue("@dv_doenca_cronica", aluno.dv_doenca_cronica);
                cmd.Parameters.AddWithValue("@ds_doenca_cronica", aluno.ds_doenca_cronica);
                cmd.Parameters.AddWithValue("@dv_diabetes", aluno.dv_diabetes);
                cmd.Parameters.AddWithValue("@dv_pressao_alta", aluno.dv_pressao_alta);
                cmd.Parameters.AddWithValue("@id_usuario_cadastro", aluno.id_usuario_cadastro);
                cmd.Parameters.AddWithValue("@dv_ativo", aluno.dv_ativo);

                //par = cmd.CreateParameter();
                //par.DbType = System.Data.DbType.Int32;
                //par.ParameterName = "@id_usuario_ret";
                //par.Direction = System.Data.ParameterDirection.Output;
                //cmd.Parameters.Add(par);

                return cmd.ExecuteNonQuery();

                //id_usuario_ret = Convert.ToInt32(cmd.Parameters["@id_usuario_ret"].Value);
                //return id_usuario_ret;
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

        //****************************************************************************************************************
        // ALUNO (Lista TODOS os alunos para do cliente logado)
        //****************************************************************************************************************
        public String ListarAluno(int id_usuario)
        {
            int nr_linha = 1;
            //if (Convert.ToInt32(HttpContext.Current.Session["id_usuario"]) != null)
            //if (HttpContext.Current.Session["id_usuario"] != null)
            //    id_usuario = Convert.ToInt32(HttpContext.Current.Session["id_usuario"]);

            if (HttpContext.Current.Session["id_empresa"] != null)
                id_empresa = Convert.ToInt32(HttpContext.Current.Session["id_empresa"]);

            String sHTML = String.Empty;

            List<AlunoENT> alunoLista = ObterListaAluno(id_usuario);

            foreach (AlunoENT itemAlunoLista in alunoLista)
            {
                sHTML += String.Format("<tr>");
                sHTML += String.Format("        <td>{0}</td>", nr_linha);
                sHTML += String.Format("        <td>{0}</td>", itemAlunoLista.nr_matricula);
                sHTML += String.Format("        <td>{0}</td>", itemAlunoLista.nm_aluno);
                sHTML += String.Format("        <td>{0}</td>", itemAlunoLista.dt_nascimento);
                sHTML += String.Format("        <td>{0}</td>", itemAlunoLista.nm_usuario);
                //sHTML += String.Format("        <td>{0}</td>", itemAlunoLista.dv_ativo);
                sHTML += String.Format("        <td><a href=/MarcaPresencaEsporte/Cadastro/AlunoCadastro?id_aluno={0}><i class=\"fa fa-fw fa-edit\" data-toggle=\"tooltip\" data-placement=\"top\" title=\"Editar Aluno\"></i></td>", itemAlunoLista.id_aluno);
                sHTML += String.Format("</tr>");

                nr_linha += 1;
            }

            return sHTML;
        }

        //****************************************************************************************************************
        // ALUNO (Lista apenas os alunos para do cliente selecionado)
        //****************************************************************************************************************
        public String ListarAlunoCliente(int id_usuario)
        {
            int nr_linha = 1;
            //if (Convert.ToInt32(HttpContext.Current.Session["id_usuario"]) != null)
            //if (HttpContext.Current.Session["id_usuario"] != null)
            //    id_usuario = Convert.ToInt32(HttpContext.Current.Session["id_usuario"]);

            if (HttpContext.Current.Session["id_empresa"] != null)
                id_empresa = Convert.ToInt32(HttpContext.Current.Session["id_empresa"]);

            String sHTML = String.Empty;

            List<AlunoENT> alunoLista = ObterListaAluno(id_usuario);

            foreach (AlunoENT itemAlunoLista in alunoLista)
            {
                sHTML += String.Format("<tr>");
                sHTML += String.Format("        <td>{0}</td>", nr_linha);
                sHTML += String.Format("        <td>{0}</td>", itemAlunoLista.nm_aluno);
                sHTML += String.Format("        <td>{0}</td>", itemAlunoLista.dt_nascimento);
                sHTML += String.Format("        <td>{0}</td>", itemAlunoLista.nm_sexo);
                sHTML += String.Format("        <td>{0}</td>", itemAlunoLista.nm_turma);
                //sHTML += String.Format("        <td>{0}</td>", itemAlunoLista.dv_ativo);
                sHTML += String.Format("        <td><a href=/MarcaPresencaEsporte/Cadastro/AlunoCadastro?id_aluno={0}><i class=\"fa fa-fw fa-edit\"></i></td>", itemAlunoLista.id_aluno);
                sHTML += String.Format("</tr>");

                nr_linha += 1;
            }

            return sHTML;
        }


        //****************************************************************************************************************
        // ALUNO (Carrega as turmas do aluno selecionado no cadastro)
        //****************************************************************************************************************
        public List<AlunoENT> ObterTurmaAlunoLista(int id_aluno)
        {
            SqlConnection cn = null;
            SqlParameter par = null;
            DbDataReader rd = null;
            AlunoENT turmaAluno = null;
            int cont = 1;

            try
            {
                cn = conexao.Conectar();
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                cmd.CommandText = @"EXEC esp.SP_ALUNO_TURMA_LISTA @id_aluno = @id_aluno";
                par = cmd.CreateParameter();
                par.DbType = System.Data.DbType.Int32;
                par.ParameterName = "@id_aluno";
                par.Value = id_aluno;
                cmd.Parameters.Add(par);

                rd = cmd.ExecuteReader();

                List<AlunoENT> TurmaAlunoLista = new List<AlunoENT>();

                while (rd.Read())
                {
                    turmaAluno = new AlunoENT();

                    turmaAluno.nr_linha = cont;
                    turmaAluno.id_turma_aluno = Convert.ToInt32(rd["id_turma_aluno"]);
                    turmaAluno.id_turma = Convert.ToInt32(rd["id_turma"]);
                    turmaAluno.nm_turma = rd["nm_turma"].ToString();
                    turmaAluno.nm_professor = rd["nm_professor"].ToString();
                    turmaAluno.nm_dia_semana = rd["nm_dia_semana"].ToString();
                    turmaAluno.hr_inicio_aula = rd["hr_inicio_aula"].ToString();
                    turmaAluno.hr_final_aula = rd["hr_final_aula"].ToString();

                    TurmaAlunoLista.Add(turmaAluno);
                    cont++;

                }

                return TurmaAlunoLista;
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

        public String ListarAlunoTurma(int id_aluno)
        {
            String sHTML = String.Empty;

            List<AlunoENT> turmaAlunoLista = ObterTurmaAlunoLista(id_aluno);

            foreach (AlunoENT itemTurmaAlunoLista in turmaAlunoLista)
            {
                sHTML += String.Format("<tr>");
                sHTML += String.Format("        <td>{0}</td>", itemTurmaAlunoLista.nr_linha);
                sHTML += String.Format("        <td>{0}</td>", itemTurmaAlunoLista.nm_turma);
                sHTML += String.Format("        <td>{0}</td>", itemTurmaAlunoLista.nm_professor);
                sHTML += String.Format("        <td>{0}</td>", itemTurmaAlunoLista.nm_dia_semana);
                sHTML += String.Format("        <td>{0}-{1}</td>", itemTurmaAlunoLista.hr_inicio_aula, itemTurmaAlunoLista.hr_final_aula);
                
                //if (id_perfil != 8)
                //{
                    sHTML += String.Format("        <td><a href=/MarcaPresencaEsporte/Cadastro/AlunoTurmaDel?id_turma_aluno={0}&id_aluno={1}><i class=\"fa fa-fw fa-trash\"title=\"Dessassociar Turma\"></i></a></td>", itemTurmaAlunoLista.id_turma_aluno, id_aluno);
                //}

                sHTML += String.Format("</tr>");
            }

            return sHTML;
        }
    }
}