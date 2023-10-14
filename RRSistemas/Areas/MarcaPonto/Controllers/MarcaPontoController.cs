using RRSistemas.Areas.MarcaPonto.Entity;
using RRSistemas.Areas.MarcaPonto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace RRSistemas.Areas.MarcaPonto.Controllers
{
    public class MarcaPontoController : Controller
    {
        int id_usuario_session = Convert.ToInt32(System.Web.HttpContext.Current.Session["id_usuario"]);
        int id_empresa_session = Convert.ToInt32(System.Web.HttpContext.Current.Session["id_empresa"]);

        DateTime data_hora;

        MarcaPontoDAO _marcaPontoDAO;
        MarcaPontoENT marcaPonto;
        // GET: MarcaPonto/MarcaPonto
        public ActionResult Index()
        {
            marcaPonto = new MarcaPontoENT();
            if (Session["id_usuario"] != null)
            {
                _marcaPontoDAO = new MarcaPontoDAO();

                data_hora = DateTime.Now;
                ViewBag.lbl_hora_atual = data_hora.ToShortDateString() + ' ' + data_hora.ToShortTimeString();
                //ViewBag.lbl_hora_atual = data_hora.ToString();
                ViewBag.ListaMarcacaoDiaria = _marcaPontoDAO.ListarMarcacaoDiaria();
                return View("MarcaPonto");
            }
            else
            {
                return RedirectToAction("Logout", "Authentication", new { area = "" });
            }
        }

        public ActionResult MarcaPonto()
        {
            _marcaPontoDAO = new MarcaPontoDAO();

            _marcaPontoDAO.MarcaPonto();
            return RedirectToAction("Index", "MarcaPonto");
        }
    }
}