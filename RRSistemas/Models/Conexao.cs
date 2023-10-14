using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace RRSistemas.Models
{
    public class Conexao
    {
        public static string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["RRSistemas"].ConnectionString;
        public static SqlConnection cn = new SqlConnection(connectionString);
        //public static SqlConnection cn = new SqlConnection(Settings.Default.SistemaEscola);

        public SqlConnection Conectar()
        {
            if (cn.State == System.Data.ConnectionState.Closed)
            {
                cn.Open();
            }
            return cn;
        }

        public SqlConnection Desconectar()
        {
            if (cn.State == System.Data.ConnectionState.Open)
            {
                cn.Close();
            }
            return cn;
        }
    }
}