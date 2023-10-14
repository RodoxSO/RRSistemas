using RRSistemas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RRSistemas.Controllers
{
    public class UserController : Controller
    {
        int id_usuario_logado = Convert.ToInt32(System.Web.HttpContext.Current.Session["id_usuario"]);
        int id_empresa = Convert.ToInt32(System.Web.HttpContext.Current.Session["id_empresa"]);
        UsuarioDAO _usuario;
        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult UsuarioBusca(int id_perfil)
        {
            if (Session["id_usuario"] != null)
            {
                _usuario = new UsuarioDAO();
                ViewBag.ListaUsuarios = _usuario.ListarUsuarios(id_empresa, id_perfil);
                return View();
            }
            else
            {
                return RedirectToAction("Logout", "Authentication", new { area = "" });
            }
        }
    }
}