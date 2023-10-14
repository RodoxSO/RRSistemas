using RRSistemas.Entity;
using RRSistemas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RRSistemas.Controllers
{
    public class FollowupController : Controller
    {
        int id_usuario_logado = Convert.ToInt32(System.Web.HttpContext.Current.Session["id_usuario"]);
        int id_empresa = Convert.ToInt32(System.Web.HttpContext.Current.Session["id_empresa"]);

        // GET: Followup
        public ActionResult Index()
        {
            return View();
        }

        FollowupDAO _followupDAO;
        FollowupENT followup;
        public ActionResult FollowUpBusca(int id_followup_categoria, int id_parametro)
        {
            if (Session["id_usuario"] != null)
            {
                //string fupname;

                if (id_followup_categoria == 1)
                {
                    ViewBag.fupname = "Empresa";
                }
                else if (id_followup_categoria == 2)
                {
                    ViewBag.fupname = "Usuário";
                }
                else if (id_followup_categoria == 3)
                {
                    ViewBag.fupname = "Aluno";
                }

                ViewBag.id_followup_categoria = id_followup_categoria;
                ViewBag.id_parametro = id_parametro;
                _followupDAO = new FollowupDAO();
                ViewBag.ListaFollowup = _followupDAO.ListarFollowup(id_followup_categoria, id_parametro);
                return View();
            }
            else
            {
                return RedirectToAction("Logout", "Authentication", new { area = "" });
            }
        }

        public ActionResult FollowUpCadastro(int id_followup_categoria, int id_parametro)
        {
            if (Session["id_usuario"] != null)
            {
                followup = new FollowupENT();
                followup.id_followup_categoria = id_followup_categoria;
                followup.id_parametro = id_parametro;

                ViewBag.ComboTipoFollowup = new SelectList(new DropDownListDAO().CarregaComboTipoFollowup(), "id_followup_tipo", "nm_followup_tipo");
                return View();
            }
            else
            {
                return RedirectToAction("Logout", "Authentication", new { area = "" });
            }
        }

        [HttpPost]
        public ActionResult FollowUpCadastro(FollowupENT followup)
        {
            followup.id_empresa = id_empresa;
            followup.id_usuario = followup.id_parametro;
            followup.id_usuario_sistema = id_usuario_logado;

            _followupDAO = new FollowupDAO();
            if (ModelState.IsValid)
            {
                _followupDAO.InserirFollowup(followup);
                return RedirectToAction("FollowUpBusca", "FollowUp", new { id_followup_categoria = followup.id_followup_categoria, id_parametro = followup.id_parametro });
            }
            else
            {
                return View("FollowUpBusca", new { id_followup_categoria = followup.id_followup_categoria, id_parametro = followup.id_parametro });
            }
        }
    }
}