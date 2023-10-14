using RRSistemas.Entity;
using RRSistemas.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace RRSistemas.Models
{
    public class GetMenu
    {
        public static Conexao conexao = new Conexao();
        public static List<MenuENT> ObterDadosDoMenu(int id_menu, int id_sistema)
        {
            int id_usuario = 0;

            if (HttpContext.Current.Session["id_usuario"] != null)
                id_usuario = Convert.ToInt32(HttpContext.Current.Session["id_usuario"]);

            SqlConnection cn = null;
            SqlParameter par = null;
            DbDataReader rd = null;
            MenuENT menu = null;

            try
            {
                cn = conexao.Conectar();
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                cmd.CommandText = @"EXEC adm.SP_MONTA_MENU @id_usuario = @id_usuario, @id_menu = @id_menu";
                par = cmd.CreateParameter();
                par.DbType = System.Data.DbType.Int32;
                par.ParameterName = "@id_usuario";
                par.Value = id_usuario;
                cmd.Parameters.Add(par);

                par = cmd.CreateParameter();
                par.DbType = System.Data.DbType.Int32;
                par.ParameterName = "@id_menu";
                par.Value = id_menu;
                cmd.Parameters.Add(par);

                rd = cmd.ExecuteReader();

                List<MenuENT> ListaMenu = new List<MenuENT>();

                while (rd.Read())
                {
                    menu = new MenuENT();

                    menu.id_menu = Convert.ToInt32(rd["id_menu"]);
                    menu.id_menu_pai = Convert.ToInt32(rd["id_menu_pai"]);
                    menu.nm_menu = rd["nm_menu"].ToString();
                    menu.ds_menu = rd["ds_menu"].ToString();
                    menu.link_menu = rd["link_menu"].ToString();
                    menu.nm_AreaName = rd["nm_AreaName"].ToString();
                    menu.nm_ActionName = rd["nm_ActionName"].ToString();
                    menu.nm_ControllerName = rd["nm_ControllerName"].ToString();
                    menu.parametro_icone_menu = rd["parametro_icone_menu"].ToString();
                    menu.parametro_sub_menu = rd["parametro_sub_menu"].ToString();

                    ListaMenu.Add(menu);
                 }
                 

                return ListaMenu;
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

        public static String MontaMenu(int id_menu)
        {
            //Monta menu pai
            String sHTML = String.Empty;
            String nm_AreaName = String.Empty;
            List<MenuENT> listMenu = ObterDadosDoMenu(id_menu, 3);

            sHTML += String.Format("<ul class=\"sidebar navbar-nav\">");

            foreach (MenuENT itemMenu in listMenu)
            {
                nm_AreaName = String.Empty;
                if (itemMenu.nm_AreaName != string.Empty)
                {
                    nm_AreaName = itemMenu.nm_AreaName + "/";
                }

                List<MenuENT> listMenuFilho = ObterDadosDoMenu(itemMenu.id_menu, 3);
                //sHTML += String.Format("<li class=\"nav-item\" data-toggle=\"tooltip\" data-placement=\"right\" title=\"{0}\">", itemMenu.nm_menu);

                if (listMenuFilho != null && listMenuFilho.Count > 0)
                {
                    sHTML += String.Format("<li class=\"nav-item dropdown\">");
                    sHTML += String.Format("    <a class=\"nav-link dropdown-toggle\" href=\"#\" id=\"{0}Dropdown\" role=\"button\" data-toggle=\"dropdown\" aria-haspopup=\"true\" aria-expanded=\"false\"><i class=\"fas fa-fw {1}\"></i><span> {0}</span></a>", itemMenu.nm_menu, itemMenu.parametro_icone_menu);
                    sHTML += String.Format("        <div class=\"dropdown-menu\" aria-labelledby=\"{0}Dropdown\">", itemMenu.nm_menu);
                }
                else
                {
                    sHTML += String.Format("<li class=\"nav-item\">");
                    sHTML += String.Format("<a class=\"nav-link\" href=/" + nm_AreaName + itemMenu.nm_ControllerName +"/"+ itemMenu.nm_ActionName + "><i class=\"fas fa-fw {0}\"></i><span> {1}</span></a>", itemMenu.parametro_icone_menu, itemMenu.nm_menu);
                }

                ////sHTML += String.Format("    <a class=\"nav-link\" href=\"{0}\">", itemMenu.link_menu);
                //sHTML += String.Format("        <i class=\"fa fa-fw {0}\"></i>", itemMenu.parametro_icone_menu);
                //sHTML += String.Format("        <span class=\"nav-link-text link\">{0}</span>", itemMenu.nm_menu);
                //sHTML += String.Format("    </a>");

                if (listMenuFilho != null && listMenuFilho.Count > 0)
                {
                    foreach (MenuENT itemMenuFilho in listMenuFilho)
                    {
                        nm_AreaName = String.Empty;
                        if (itemMenuFilho.nm_AreaName != string.Empty)
                        {
                            nm_AreaName = itemMenuFilho.nm_AreaName + "/";
                        }


                        sHTML += String.Format("<a class=\"dropdown-item\" href=/" + nm_AreaName  + itemMenuFilho.nm_ControllerName + "/" + itemMenuFilho.nm_ActionName + "><i class=\"fas fa-fw {0}\"></i> {1}</a>", itemMenuFilho.parametro_icone_menu, itemMenuFilho.nm_menu);
                    }
                    sHTML += String.Format("    </div>");
                }

                sHTML += String.Format("</li>");
            }
            sHTML += String.Format("</ul>");
            return sHTML;
        }
    }
}
