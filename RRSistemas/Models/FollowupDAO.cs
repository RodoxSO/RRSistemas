using RRSistemas.Entity;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace RRSistemas.Models
{
    public class FollowupDAO
    {
        public static Conexao conexao = new Conexao();

        SqlConnection cn = null;
        DbDataReader rd = null;
        SqlParameter par = null;
        FollowupENT followup = null;

        public List<FollowupENT> ObterListaFollowup(int id_followup_categoria, int id_parametro)
        {
            int cont = 1;

            try
            {
                cn = conexao.Conectar();
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                cmd.CommandText = @"EXEC adm.SP_FOLLOWUP_LISTA @id_followup_categoria = @id_followup_categoria, @id_parametro = @id_parametro";

                par = cmd.CreateParameter();
                par.DbType = System.Data.DbType.Int32;
                par.ParameterName = "@id_followup_categoria";
                par.Value = id_followup_categoria;
                cmd.Parameters.Add(par);

                par = cmd.CreateParameter();
                par.DbType = System.Data.DbType.Int32;
                par.ParameterName = "@id_parametro";
                par.Value = id_parametro;
                cmd.Parameters.Add(par);

                rd = cmd.ExecuteReader();

                List<FollowupENT> ListaFollowup = new List<FollowupENT>();

                while (rd.Read())
                {
                    followup = new FollowupENT();

                    followup.nr_linha = cont;
                    followup.dt_followup = Convert.ToDateTime(rd["dt_followup"]);
                    followup.ds_followup = rd["ds_followup"].ToString();

                    ListaFollowup.Add(followup);

                    cont += 1;

                }
                //}

                return ListaFollowup;
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

        public String ListarFollowup(int id_followup_categoria, int @id_parametro)
        {
            string sHTML = string.Empty;
            string statusUsuario = string.Empty;

            List<FollowupENT> listaFollowup = ObterListaFollowup(id_followup_categoria, @id_parametro);

            foreach (FollowupENT itemFollowup in listaFollowup)
            {
                sHTML += string.Format("<tr>");
                sHTML += string.Format("        <td>{0}</td>", itemFollowup.nr_linha);
                sHTML += string.Format("        <td>{0}</td>", itemFollowup.dt_followup);
                sHTML += string.Format("        <td>{0}</td>", itemFollowup.ds_followup);
                sHTML += string.Format("</tr>");
            }

            return sHTML;
        }

        public int InserirFollowup(FollowupENT followup)
        {
            SqlConnection cn = null;

            try
            {
                cn = conexao.Conectar();
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                cmd.CommandText = @"EXEC adm.SP_FOLLOWUP_INSERE  @id_followup_categoria  = @id_followup_categoria 
                                                                ,@id_empresa			 = @id_empresa			
                                                                ,@id_usuario			 = @id_usuario			
                                                                ,@ds_followup			 = @ds_followup			
                                                                ,@id_usuario_sistema	 = @id_usuario_sistema	";

                cmd.Parameters.AddWithValue("@id_followup_categoria", followup.id_followup_categoria);
                cmd.Parameters.AddWithValue("@id_empresa", followup.id_empresa);
                cmd.Parameters.AddWithValue("@id_usuario", followup.id_usuario);
                cmd.Parameters.AddWithValue("@ds_followup", followup.ds_followup);
                cmd.Parameters.AddWithValue("@id_usuario_sistema", followup.id_usuario_sistema);

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