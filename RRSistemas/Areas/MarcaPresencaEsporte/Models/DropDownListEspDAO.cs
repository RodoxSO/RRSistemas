using RRSistemas.Areas.MarcaPresencaEsporte.Entity;
using RRSistemas.Entity;
using RRSistemas.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace RRSistemas.Areas.MarcaPresencaEsporte.Models
{
    public class DropDownListEspDAO
    {
        public static Conexao conexao = new Conexao();
        public List<UsuarioENT> ObterTodosOsProfessores(int id_empresa, int id_perfil)
        {
            SqlConnection cn = null;
            SqlParameter par = null;
            DbDataReader rd = null;

            try
            {
                cn = conexao.Conectar();
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                cmd.CommandText = @"EXEC adm.SP_COMBO_USUARIO @id_empresa = @id_empresa, @id_perfil = @id_perfil";

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

                var ComboProfessores = new List<UsuarioENT>();
                //ComboProfessores.Insert(0, new UsuarioENT() { id_usuario = 0, nm_usuario = "Selecione um Professor" });

                while (rd.Read())
                {
                    var ComboProfessor = new UsuarioENT();

                    ComboProfessor.id_usuario = Convert.ToInt32(rd["id_usuario"]);
                    ComboProfessor.nm_usuario = rd["nm_usuario"].ToString();
                    
                    ComboProfessores.Add(ComboProfessor);
                }

                return ComboProfessores;
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

        public List<TurmaENT> ObterTodasAsTurmas(int id_usuario)
        {
            SqlConnection cn = null;
            SqlParameter par = null;
            DbDataReader rd = null;

            try
            {
                cn = conexao.Conectar();
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                cmd.CommandText = "EXEC esp.SP_COMBO_TURMA @id_usuario = @id_usuario";

                par = cmd.CreateParameter();
                par.DbType = System.Data.DbType.Int32;
                par.ParameterName = "@id_usuario";
                par.Value = id_usuario;
                cmd.Parameters.Add(par);

                rd = cmd.ExecuteReader();

                var ComboTurmas = new List<TurmaENT>();
                //ComboTurmas.Insert(0, new TurmaENT() { id_turma = 0, nm_turma = "Selecione uma Turma" });

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
    }
}