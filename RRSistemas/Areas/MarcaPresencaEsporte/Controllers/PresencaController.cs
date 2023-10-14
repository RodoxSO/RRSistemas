using RRSistemas.Areas.MarcaPresencaEsporte.Entity;
using RRSistemas.Areas.MarcaPresencaEsporte.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RRSistemas.Areas.MarcaPresencaEsporte.Controllers
{
    public class PresencaController : Controller
    {
        // GET: MarcaPresencaEsporte/Presenca
        public ActionResult Index()
        {
            return View();
        }

        //****************************************************************************************************************
        // PRESENÇA
        //****************************************************************************************************************
        AulaENT aulaENT;
        PresencaENT presencaENT;

        AulaDAO _aulaDAO;
        PresencaDAO _presencaDAO;

        public ActionResult ListaChamada(int id_turma, int id_aula)
        {
            if (Session["id_usuario"] != null)
            {
                _presencaDAO = new PresencaDAO();
                _aulaDAO = new AulaDAO();
                presencaENT = new PresencaENT();

                if (id_aula == 0)
                {
                    id_aula = _aulaDAO.InserirAula(id_turma);
                }

                var Aula = _aulaDAO.ObterAulaPeloId(id_turma, id_aula);

                ViewBag.nm_turma = Aula.nm_turma;
                ViewBag.nm_professor = Aula.nm_professor;
                ViewBag.dt_aula = Aula.dt_aula + " " + Aula.hr_inicio_aula + "-" + Aula.hr_final_aula;
                ViewBag.id_aula = Aula.id_aula;
                ViewBag.ListarChamada = _presencaDAO.ListarChamada(Aula.id_aula);

                return View();
            }
            else
            {
                return RedirectToAction("Logout", "Authentication", new { area = "" });
            }
        }

        [HttpPost]
        public ActionResult ListaChamada(PresencaENT presenca, IList<int> CheckPresenca)
        {
            _presencaDAO = new PresencaDAO();

            if (ModelState.IsValid)
            {
                if (CheckPresenca == null)
                {
                    _presencaDAO.InserirPresenca(presenca.id_aula, 0);
                }else
                {
                    foreach (int itemPresenca in CheckPresenca)
                    {
                        _presencaDAO.InserirPresenca(presenca.id_aula, itemPresenca);
                    }
                }
                

                //return RedirectToAction("ListaChamada", "Presenca", new { id_turma = presenca.id_turma, id_aula = presenca.id_aula });
            //}
            //else
            //{
                
            }

            return RedirectToAction("ListaChamada", "Presenca", new { id_turma = presenca.id_turma, id_aula = presenca.id_aula });
        }

        public ActionResult BuscaPresenca()
        {
            if (Session["id_usuario"] != null)
            {
                _presencaDAO = new PresencaDAO();
                presencaENT = new PresencaENT();

                //var Aula = _aulaDAO.ObterAulaPeloId(id_turma, id_aula);

                //ViewBag.BuscaPresenca
                //ViewBag.nm_turma = Aula.nm_turma;
                //ViewBag.nm_professor = Aula.nm_professor;
                //ViewBag.dt_aula = Aula.dt_aula + " " + Aula.hr_inicio_aula + "-" + Aula.hr_final_aula;
                //ViewBag.id_aula = Aula.id_aula;
                ViewBag.BuscaPresenca = _presencaDAO.ListarPresenca();

                return View();
            }
            else
            {
                return RedirectToAction("Logout", "Authentication", new { area = "" });
            }
        }

        public ActionResult ListaPresenca(int id_aula)
        {
            if (Session["id_usuario"] != null)
            {
                _aulaDAO = new AulaDAO();
                _presencaDAO = new PresencaDAO();
                presencaENT = new PresencaENT();

                int id_turma = 0;


                var Aula = _aulaDAO.ObterAulaPeloId(id_turma, id_aula);

                ViewBag.nm_turma = Aula.nm_turma;
                ViewBag.nm_professor = Aula.nm_professor;
                ViewBag.dt_aula = Aula.dt_aula + " " + Aula.hr_inicio_aula + "-" + Aula.hr_final_aula;
                ViewBag.id_aula = Aula.id_aula;
                ViewBag.ListarPresenca = _presencaDAO.ListarPresencaAula(id_aula);

                return View();
            }
            else
            {
                return RedirectToAction("Logout", "Authentication", new { area = "" });
            }
        }
    }
}