using RRSistemas.Areas.MarcaPonto.Entity;
using RRSistemas.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace RRSistemas.Areas.MarcaPonto.Models
{
    public class MarcaPontoDAO
    {
        public static Conexao conexao = new Conexao();

        public List<MarcaPontoENT> ObterMarcacaoDiaria(int id_usuario)
        {
            SqlConnection cn = null;
            SqlParameter par = null;
            DbDataReader rd = null;
            MarcaPontoENT marcaponto = null;
            int cont = 1;

            try
            {
                cn = conexao.Conectar();
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                cmd.CommandText = @"EXEC mpo.SP_MOSTRA_MARCACAO_USUARIO_DIARIO @id_usuario = @id_usuario";
                par = cmd.CreateParameter();
                par.DbType = System.Data.DbType.Int32;
                par.ParameterName = "@id_usuario";
                par.Value = id_usuario;
                cmd.Parameters.Add(par);

                rd = cmd.ExecuteReader();

                List<MarcaPontoENT> MarcacaoLista = new List<MarcaPontoENT>();

                while (rd.Read())
                {
                    marcaponto = new MarcaPontoENT();

                    marcaponto.id_ponto_eletronico = Convert.ToInt32(rd["id_ponto_eletronico"]);
                    marcaponto.nr_matricula = rd["nr_matricula"].ToString();
                    marcaponto.nm_usuario = rd["nm_usuario"].ToString();
                    marcaponto.nm_dia_semana = rd["nm_dia_semana"].ToString();
                    marcaponto.dt_marcacao = rd["dt_marcacao"].ToString();
                    marcaponto.hr_marcacao = rd["hr_marcacao"].ToString();
                    marcaponto.nr_endereco_ip_local = rd["nr_endereco_ip_local"].ToString();
                    marcaponto.nm_tipo_acesso = rd["nm_tipo_acesso"].ToString();
                    marcaponto.nm_tipo_marcacao = rd["nm_tipo_marcacao"].ToString();
                    marcaponto.nm_endereco_reg = rd["nm_endereco_reg"].ToString();


                    MarcacaoLista.Add(marcaponto);

                }

                return MarcacaoLista;
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

        public String ListarMarcacaoDiaria()
        {
            int id_usuario = 0;

            if (HttpContext.Current.Session["id_usuario"] != null)
                id_usuario = Convert.ToInt32(HttpContext.Current.Session["id_usuario"]);

            String sHTML = String.Empty;

            List<MarcaPontoENT> marcacaoLista = ObterMarcacaoDiaria(id_usuario);

            foreach (MarcaPontoENT itemMarcacaoLista in marcacaoLista)
            {
                sHTML += String.Format("<tr>");
                sHTML += String.Format("        <td>{0}</td>", itemMarcacaoLista.nm_tipo_marcacao);
                sHTML += String.Format("        <td>{0}</td>", itemMarcacaoLista.dt_marcacao + ' ' + itemMarcacaoLista.hr_marcacao);
                sHTML += String.Format("        <td>{0}</td>", itemMarcacaoLista.nm_dia_semana);
                sHTML += String.Format("</tr>");
            }

            return sHTML;
        }
        public int MarcaPonto()
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

                cmd.CommandText = @"EXEC mpo.SP_MARCA_PONTO @id_usuario = @id_usuario";

                cmd.Parameters.AddWithValue("@id_usuario", id_usuario);

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