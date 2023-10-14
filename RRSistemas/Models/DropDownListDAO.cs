using RRSistemas.Entity;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace RRSistemas.Models
{
    public class DropDownListDAO
    {
        public static Conexao conexao = new Conexao();
        public List<DropdownSexoENT> CarregaComboSexo()
        {
            SqlConnection cn = null;
            DbDataReader rd = null;

            try
            {
                cn = conexao.Conectar();
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                cmd.CommandText = "EXEC adm.SP_COMBO_SEXO";

                rd = cmd.ExecuteReader();

                var ComboSexo = new List<DropdownSexoENT>();
                //ComboSexo.Insert(0, new SexoENT() { id_sexo = 0, nm_sexo = "Selecione o Sexo" });

                while (rd.Read())
                {
                    var itensComboSexo = new DropdownSexoENT();

                    itensComboSexo.id_sexo = Convert.ToInt32(rd["id_sexo"]);
                    itensComboSexo.sg_sexo = rd["sg_sexo"].ToString();
                    itensComboSexo.nm_sexo = rd["nm_sexo"].ToString();


                    ComboSexo.Add(itensComboSexo);
                }

                return ComboSexo;
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

        public List<DropdownUFEstadoENT> CarregaComboEstado()
        {
            SqlConnection cn = null;
            DbDataReader rd = null;

            try
            {
                cn = conexao.Conectar();
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                cmd.CommandText = "EXEC adm.SP_COMBO_ESTADO";

                rd = cmd.ExecuteReader();

                var ComboEstados = new List<DropdownUFEstadoENT>();
                //ComboEstados.Insert(0, new UFEstadoENT() { id_estado = 0, nm_estado = "Selecione um Estado" });

                while (rd.Read())
                {
                    var ComboEstado = new DropdownUFEstadoENT();

                    ComboEstado.id_estado = Convert.ToInt32(rd["id_estado"]);
                    ComboEstado.nm_estado = rd["sg_estado"].ToString() + "-" + rd["nm_estado"].ToString();


                    ComboEstados.Add(ComboEstado);
                }

                return ComboEstados;
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

        public List<DropdownTipoSanguineoENT> CarregaComboTipoSanguineo()
        {
            SqlConnection cn = null;
            DbDataReader rd = null;

            try
            {
                cn = conexao.Conectar();
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                cmd.CommandText = "EXEC adm.SP_COMBO_TIPO_SANGUINEO";

                rd = cmd.ExecuteReader();

                var ComboTipoSanguineo = new List<DropdownTipoSanguineoENT>();
                //ComboSexo.Insert(0, new SexoENT() { id_sexo = 0, nm_sexo = "Selecione o Sexo" });

                while (rd.Read())
                {
                    var itensComboTipoSanguineo = new DropdownTipoSanguineoENT();

                    itensComboTipoSanguineo.id_tipo_sanguineo = Convert.ToInt32(rd["id_tipo_sanguineo"]);
                    itensComboTipoSanguineo.nm_tipo_sanguineo = rd["nm_tipo_sanguineo"].ToString();

                    ComboTipoSanguineo.Add(itensComboTipoSanguineo);
                }

                return ComboTipoSanguineo;
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

        public List<DropdownSimNaoENT> CarregaComboSimNao()
        {
            try
            {
                var ComboSimNao = new List<DropdownSimNaoENT>();
                //ComboSimNao.Insert(0, new DropdownSimNaoENT() { id_selecao = 0, nm_selecao = " " });
                ComboSimNao.Insert(0, new DropdownSimNaoENT() { id_selecao = 1, nm_selecao = "Sim" });
                ComboSimNao.Insert(1, new DropdownSimNaoENT() { id_selecao = 0, nm_selecao = "Não" });

                return ComboSimNao;
            }

            catch (Exception)
            {

                throw;
            }

            finally
            {

            }
        }

        public List<DropdownFollowupTipoENT> CarregaComboTipoFollowup()
        {
            SqlConnection cn = null;
            DbDataReader rd = null;

            try
            {
                cn = conexao.Conectar();
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                cmd.CommandText = "EXEC adm.SP_COMBO_FOLLOWUP";

                rd = cmd.ExecuteReader();

                var ComboTipoFollowup = new List<DropdownFollowupTipoENT>();
                //ComboSexo.Insert(0, new SexoENT() { id_sexo = 0, nm_sexo = "Selecione o Sexo" });

                while (rd.Read())
                {
                    var itensComboTipoFollowup = new DropdownFollowupTipoENT();

                    itensComboTipoFollowup.id_followup_tipo = Convert.ToInt32(rd["id_followup_tipo"]);
                    itensComboTipoFollowup.nm_followup_tipo = rd["nm_followup_tipo"].ToString();

                    ComboTipoFollowup.Add(itensComboTipoFollowup);
                }

                return ComboTipoFollowup;
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
    }
}