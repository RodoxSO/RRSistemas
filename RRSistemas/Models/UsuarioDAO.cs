using RRSistemas.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace RRSistemas.Models
{
    public class UsuarioDAO
    {
        public static Conexao conexao = new Conexao();
        UsuarioENT usuario = null;

        public List<UsuarioENT> ObterListaUsuario(int id_empresa, int id_perfil)
        {
            SqlConnection cn = null;
            DbDataReader rd = null;
            SqlParameter par = null;

            try
            {
                cn = conexao.Conectar();
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                cmd.CommandText = @"EXEC adm.SP_USUARIO_LISTA  @id_empresa = @id_empresa, @id_perfil = @id_perfil";
                par = cmd.CreateParameter();
                par.DbType = System.Data.DbType.Int32;
                par.ParameterName = "@id_empresa";
                par.Value = id_empresa;
                cmd.Parameters.Add(par);

                par = cmd.CreateParameter();
                par.DbType = System.Data.DbType.Int32;
                par.ParameterName = "@id_perfil";
                par.Value = id_perfil;
                cmd.Parameters.Add(par);

                rd = cmd.ExecuteReader();

                List<UsuarioENT> ListaUsuario = new List<UsuarioENT>();

                while (rd.Read())
                {
                    usuario = new UsuarioENT();

                    usuario.nr_linha = Convert.ToInt32(rd["nr_linha"]);
                    usuario.id_usuario = Convert.ToInt32(rd["id_usuario"]);
                    usuario.nm_usuario = rd["nm_usuario"].ToString();
                    usuario.cd_usuario = rd["cd_usuario"].ToString();
                    usuario.nr_rg = rd["nr_rg"].ToString();
                    usuario.nr_cpf = rd["nr_cpf"].ToString();
                    usuario.dt_nascimento = Convert.ToDateTime(rd["dt_nascimento"]);
                    usuario.dv_ativo = Convert.ToBoolean(rd["dv_ativo"]);
                    //usuario.dt_cadastro = Convert.ToDateTime(rd["dt_cadastro"]);

                    ListaUsuario.Add(usuario);

                }
                    
                return ListaUsuario;
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
        public String ListarUsuarios(int id_empresa, int id_perfil)
        {
            //Monta menu pai
            string sHTML = string.Empty;
            string statusUsuario = string.Empty;

            List<UsuarioENT> listaUsuario = ObterListaUsuario(id_empresa, id_perfil);

            foreach (UsuarioENT itemUsuario in listaUsuario)
            {
                if (itemUsuario.dv_ativo == true)
                {
                    statusUsuario = "<font style=\"color:#006400;\">Ativo</font>";
                }
                else
                {
                    statusUsuario = "<font style=\"color:#FF0000;\">Inativo</font>";
                }

                sHTML += string.Format("<tr>");
                sHTML += string.Format("        <td>{0}</td>", itemUsuario.nr_linha);
                sHTML += string.Format("        <td>{0}</td>", itemUsuario.nm_usuario);
                sHTML += string.Format("        <td>{0}</td>", itemUsuario.cd_usuario);
                sHTML += string.Format("        <td>{0}</td>", itemUsuario.nr_rg);
                sHTML += string.Format("        <td>{0}</td>", itemUsuario.nr_cpf);
                sHTML += string.Format("        <td>{0}</td>", statusUsuario);
                sHTML += String.Format("        <td><a href=/MarcaPresencaEsporte/Cadastro/UsuarioCadastro?id_usuario={0}><i class=\"fa fa-fw fa-edit\"></i></a></td>", itemUsuario.id_usuario);
                sHTML += string.Format("</tr>");
            }

            return sHTML;
        }

        public UsuarioENT ObterUsuarioPeloId(int id_usuario)
        {
            SqlConnection cn = null;
            DbDataReader rd = null;
            SqlParameter par = null;

            try
            {
                cn = conexao.Conectar();

                cn = conexao.Conectar();
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                cmd.CommandText = "EXEC adm.SP_USUARIO_CARREGA @id_usuario = @id_usuario";
                par = cmd.CreateParameter();
                par.DbType = System.Data.DbType.Int32;
                par.ParameterName = "@id_usuario";
                par.Value = id_usuario;
                cmd.Parameters.Add(par);

                rd = cmd.ExecuteReader();

                while (rd.Read())
                {
                    usuario = new UsuarioENT();

                    usuario.id_usuario = Convert.ToInt32(rd["id_usuario"]);
                    usuario.id_empresa = Convert.ToInt32(rd["id_empresa"]);
                    usuario.id_perfil = Convert.ToInt32(rd["id_perfil"]);
                    usuario.id_cargo = Convert.ToInt32(rd["id_cargo"]);
                    usuario.id_departamento = Convert.ToInt32(rd["id_departamento"]);
                    usuario.id_perfil_jornada = Convert.ToInt32(rd["id_perfil_jornada"]);
                    usuario.id_sexo = Convert.ToInt32(rd["id_sexo"]);
                    usuario.id_estado = Convert.ToInt32(rd["id_estado"]);
                    usuario.cd_usuario = rd["cd_usuario"].ToString();
                    usuario.nm_usuario = rd["nm_usuario"].ToString();
                    usuario.dt_nascimento = Convert.ToDateTime(rd["dt_nascimento"]);
                    usuario.nm_endereco = rd["nm_endereco"].ToString();
                    usuario.nr_endereco = rd["nr_endereco"].ToString();
                    usuario.nm_complemento = (String.IsNullOrEmpty(rd["nm_complemento"].ToString())) ? null : rd["nm_complemento"].ToString();
                    usuario.nm_bairro = rd["nm_bairro"].ToString();
                    usuario.nm_cidade = rd["nm_cidade"].ToString();
                    usuario.nr_cep = rd["nr_cep"].ToString();
                    usuario.nr_rg = rd["nr_rg"].ToString();
                    usuario.nm_rg_orgao_emissor = rd["nm_rg_orgao_emissor"].ToString();
                    usuario.dt_rg_expedicao = Convert.ToDateTime(rd["dt_rg_expedicao"]);
                    usuario.nr_cpf = rd["nr_cpf"].ToString();
                    usuario.nr_telefone = rd["nr_telefone"].ToString();
                    usuario.nr_celular = rd["nr_celular"].ToString();
                    usuario.nm_email = rd["nm_email"].ToString();
                    usuario.nm_login = rd["nm_login"].ToString();
                    usuario.nm_diretorio = rd["nm_diretorio"].ToString();
                    usuario.nm_arquivo_foto = rd["nm_arquivo_foto"].ToString();
                    usuario.dv_primeiro_acesso = Convert.ToBoolean(rd["dv_primeiro_acesso"]);
                    usuario.dv_reset_senha = Convert.ToBoolean(rd["dv_reset_senha"]);
                    usuario.dv_ponto_mobile = Convert.ToBoolean(rd["dv_ponto_mobile"]);
                    usuario.dt_admissao = Convert.ToDateTime(rd["dt_admissao"]);
                    usuario.dt_cadastro = Convert.ToDateTime(rd["dt_cadastro"]);
                    usuario.id_usuario_cadastro = Convert.ToInt32(rd["id_usuario_cadastro"]);
                    usuario.dv_ativo = Convert.ToBoolean(rd["dv_ativo"]);
                }

                return usuario;
            }

            catch (Exception ex)
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


        public String ListarClientes(int id_empresa)
        {
            string sHTML = string.Empty;
            string statusUsuario = string.Empty;

            List<UsuarioENT> listaCliente = ObterListaUsuario(id_empresa, 10); //id_perfil = 10 - PERFIL CLIENTE

            foreach (UsuarioENT itemUsuario in listaCliente)
            {
                if (itemUsuario.dv_ativo == true)
                {
                    statusUsuario = "<font style=\"color:#006400;\">Ativo</font>";
                }
                else
                {
                    statusUsuario = "<font style=\"color:#FF0000;\">Inativo</font>";
                }

                sHTML += string.Format("<tr>");
                sHTML += string.Format("        <td>{0}</td>", itemUsuario.nr_linha);
                sHTML += string.Format("        <td>{0}</td>", itemUsuario.nm_usuario);
                sHTML += string.Format("        <td>{0}</td>", itemUsuario.cd_usuario);
                sHTML += string.Format("        <td>{0}</td>", itemUsuario.nr_rg);
                sHTML += string.Format("        <td>{0}</td>", itemUsuario.nr_cpf);
                sHTML += string.Format("        <td>{0}</td>", statusUsuario);
                sHTML += string.Format("        <td><a href=\"/MarcaPresencaEsporte/Cadastro/ClienteCadastro?id_usuario={0}\"><i class=\"fa fa-edit\" data-toggle=\"tooltip\" data-placement=\"top\" title=\"Editar Cliente\"></i></td>", itemUsuario.id_usuario);
                sHTML += string.Format("</tr>");
            }

            return sHTML;
        }

        public String ListarProfessores(int id_empresa)
        {
            string sHTML = string.Empty;
            string statusUsuario = string.Empty;

            List<UsuarioENT> listaProfessor = ObterListaUsuario(id_empresa, 8); //id_perfil = 8 - PROFESSOR

            foreach (UsuarioENT itemUsuario in listaProfessor)
            {
                if (itemUsuario.dv_ativo == true)
                {
                    statusUsuario = "<font style=\"color:#006400;\">Ativo</font>";
                }
                else
                {
                    statusUsuario = "<font style=\"color:#FF0000;\">Inativo</font>";
                }

                sHTML += string.Format("<tr>");
                sHTML += string.Format("        <td>{0}</td>", itemUsuario.nr_linha);
                sHTML += string.Format("        <td>{0}</td>", itemUsuario.nm_usuario);
                sHTML += string.Format("        <td>{0}</td>", itemUsuario.cd_usuario);
                sHTML += string.Format("        <td>{0}</td>", itemUsuario.nr_rg);
                sHTML += string.Format("        <td>{0}</td>", itemUsuario.nr_cpf);
                sHTML += string.Format("        <td>{0}</td>", statusUsuario);
                sHTML += string.Format("        <td><a href=\"/MarcaPresencaEsporte/Cadastro/ProfessorCadastro?id_usuario={0}\"><i class=\"fa fa-edit\" data-toggle=\"tooltip\" data-placement=\"top\" title=\"Editar Cliente\"></i></td>", itemUsuario.id_usuario);
                sHTML += string.Format("</tr>");
            }

            return sHTML;
        }

        public int InserirAlterarUsuario(UsuarioENT usuario)
        {
            SqlConnection cn = null;
            SqlParameter par = null;
            int id_usuario_ret;

            try
            {
                cn = conexao.Conectar();
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                cmd.CommandText = @"EXEC adm.SP_USUARIO_INS_ALT     @id_usuario			    = @id_usuario			
                                                                   ,@id_empresa			    = @id_empresa			
                                                                   ,@id_perfil				= @id_perfil				
                                                                   ,@id_cargo				= @id_cargo				
                                                                   ,@id_departamento		= @id_departamento		
                                                                   ,@id_perfil_jornada		= @id_perfil_jornada		
                                                                   ,@id_sexo				= @id_sexo				
                                                                   ,@id_estado				= @id_estado			
                                                                   ,@nm_usuario			    = @nm_usuario			
                                                                   ,@dt_nascimento			= @dt_nascimento			
                                                                   ,@nm_endereco			= @nm_endereco			
                                                                   ,@nr_endereco			= @nr_endereco			
                                                                   ,@nm_complemento		    = @nm_complemento		
                                                                   ,@nm_bairro				= @nm_bairro				
                                                                   ,@nm_cidade				= @nm_cidade				
                                                                   ,@nr_cep				    = @nr_cep				
                                                                   ,@nr_rg					= @nr_rg					
                                                                   ,@nm_rg_orgao_emissor	= @nm_rg_orgao_emissor	
                                                                   ,@dt_rg_expedicao		= @dt_rg_expedicao		
                                                                   ,@nr_cpf				    = @nr_cpf				
                                                                   ,@nr_telefone			= @nr_telefone			
                                                                   ,@nr_celular			    = @nr_celular			
                                                                   ,@nm_email				= @nm_email				
                                                                   ,@nm_arquivo_foto		= @nm_arquivo_foto		
                                                                   ,@dv_ponto_mobile		= @dv_ponto_mobile		
                                                                   ,@id_usuario_cadastro	= @id_usuario_cadastro	
                                                                   ,@dv_ativo				= @dv_ativo				 
                                                                   ,@id_usuario_ret	        = @id_usuario_ret	output";


                cmd.Parameters.AddWithValue("@id_usuario", usuario.id_usuario);
                cmd.Parameters.AddWithValue("@id_empresa", usuario.id_empresa);
                cmd.Parameters.AddWithValue("@id_perfil", usuario.id_perfil);
                cmd.Parameters.AddWithValue("@id_cargo", usuario.id_cargo);
                cmd.Parameters.AddWithValue("@id_departamento", usuario.id_departamento);
                cmd.Parameters.AddWithValue("@id_perfil_jornada", usuario.id_perfil_jornada);
                cmd.Parameters.AddWithValue("@id_sexo", usuario.id_sexo);
                cmd.Parameters.AddWithValue("@id_estado", usuario.id_estado);
                cmd.Parameters.AddWithValue("@nm_usuario", usuario.nm_usuario);
                cmd.Parameters.AddWithValue("@dt_nascimento", usuario.dt_nascimento);
                cmd.Parameters.AddWithValue("@nm_endereco", usuario.nm_endereco);
                cmd.Parameters.AddWithValue("@nr_endereco", usuario.nr_endereco);
                cmd.Parameters.AddWithValue("@nm_complemento", usuario.nm_complemento);
                cmd.Parameters.AddWithValue("@nm_bairro", usuario.nm_bairro);
                cmd.Parameters.AddWithValue("@nm_cidade", usuario.nm_cidade);
                cmd.Parameters.AddWithValue("@nr_cep", usuario.nr_cep);
                cmd.Parameters.AddWithValue("@nr_rg", usuario.nr_rg);
                cmd.Parameters.AddWithValue("@nm_rg_orgao_emissor", usuario.nm_rg_orgao_emissor);
                cmd.Parameters.AddWithValue("@dt_rg_expedicao", usuario.dt_rg_expedicao);
                cmd.Parameters.AddWithValue("@nr_cpf", usuario.nr_cpf);
                cmd.Parameters.AddWithValue("@nr_telefone", usuario.nr_telefone);
                cmd.Parameters.AddWithValue("@nr_celular", usuario.nr_celular);
                cmd.Parameters.AddWithValue("@nm_email", usuario.nm_email);
                cmd.Parameters.AddWithValue("@nm_arquivo_foto", "teste.jpg"); //usuario.nm_arquivo_foto
                cmd.Parameters.AddWithValue("@dv_ponto_mobile", usuario.dv_ponto_mobile);
                cmd.Parameters.AddWithValue("@id_usuario_cadastro", usuario.id_usuario_cadastro);
                cmd.Parameters.AddWithValue("@dv_ativo", usuario.dv_ativo);

                //par = cmd.CreateParameter();
                //par.DbType = System.Data.DbType.Int32;
                //par.ParameterName = "@id_usuario_ret";
                //par.Direction = ParameterDirection.Output;

                cmd.Parameters.Add("@id_usuario_ret", SqlDbType.Int, 4);
                cmd.Parameters["@id_usuario_ret"].Direction = ParameterDirection.Output;

//                cmd.Parameters.Add(par);

                cmd.ExecuteNonQuery();

                id_usuario_ret = Convert.ToInt32(cmd.Parameters["@id_usuario_ret"].Value);
                return id_usuario_ret;
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