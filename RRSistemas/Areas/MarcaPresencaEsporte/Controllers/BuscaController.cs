using RRSistemas.Areas.MarcaPresencaEsporte.Models;
using RRSistemas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RRSistemas.Areas.MarcaPresencaEsporte.Controllers
{
    public class BuscaController : Controller
    {
        // GET: MarcaPresencaEsporte/Busca
        int id_usuario = Convert.ToInt32(System.Web.HttpContext.Current.Session["id_usuario"]);
        int id_empresa = Convert.ToInt32(System.Web.HttpContext.Current.Session["id_empresa"]);

        UsuarioDAO _usuarioDAO;
        // GET: MarcaPresenca/Busca
        //****************************************************************************************************************
        // ALUNOS
        //****************************************************************************************************************
        AlunoDAO _aluno;
        public ActionResult AlunoBusca()
        {
            if (Session["id_usuario"] != null)
            {
                _aluno = new AlunoDAO();
                ViewBag.ListaAlunos = _aluno.ListarAluno(id_usuario);

                return View();
            }
            else
            {
                return RedirectToAction("Logout", "Authentication", new { area = "" });
            }
        }

        public ActionResult AlunoClienteBusca(int id_cliente)
        {
            if (Session["id_usuario"] != null)
            {
                _aluno = new AlunoDAO();
                ViewBag.ListarAlunosCliente = _aluno.ListarAlunoCliente(id_cliente);
                return View();
            }
            else
            {
                return RedirectToAction("Logout", "Authentication", new { area = "" });
            }
        }

        //****************************************************************************************************************
        // ALUNO -> TURMA
        //****************************************************************************************************************
        public ActionResult AlunoTurmaBusca(int id_aluno)
        {
            if (Session["id_usuario"] != null)
            {
                _aluno = new AlunoDAO();
                ViewBag.ListarAlunosCliente = _aluno.ListarAlunoCliente(id_aluno);
                return View();
            }
            else
            {
                return RedirectToAction("Logout", "Authentication", new { area = "" });
            }
        }


//****************************************************************************************************************
// AULA
//****************************************************************************************************************
AulaDAO _aula;
        public ActionResult AulaBusca()
        {
            if (Session["id_usuario"] != null)
            {
                _aula = new AulaDAO();
                ViewBag.ListaAula = _aula.ListarAula(id_usuario);
                return View();
            }
            else
            {
                return RedirectToAction("Logout", "Authentication", new { area = "" });
            }
        }

        //****************************************************************************************************************
        // RESPONSAVEL
        //****************************************************************************************************************
        
        public ActionResult ClienteBusca()
        {
            if (Session["id_usuario"] != null)
            {
                _usuarioDAO = new UsuarioDAO();
                ViewBag.ListarClientes = _usuarioDAO.ListarClientes(id_empresa); //ID_PERFIL = 10 - CLIENTE
                return View();
            }
            else
            {
                return RedirectToAction("Logout", "Authentication", new { area = "" });
            }
        }

        //****************************************************************************************************************
        // TURMA
        //****************************************************************************************************************
        TurmaDAO _turma;

        public ActionResult TurmaBusca()
        {
            if (Session["id_usuario"] != null)
            {
                _turma = new TurmaDAO();
                ViewBag.id_perfil = Session["id_perfil"];
                ViewBag.ListaTurma = _turma.ListarTurma(id_usuario);
                return View();
            }
            else
            {
                return RedirectToAction("Logout", "Authentication", new { area = "" });
            }
        }

        //****************************************************************************************************************
        // PROFESSOR
        //****************************************************************************************************************
        public ActionResult ProfessorBusca()
        {
            if (Session["id_usuario"] != null)
            {
                _usuarioDAO = new UsuarioDAO();
                ViewBag.ListaProfessor = _usuarioDAO.ListarProfessores(id_empresa); //ID_PERFIL = 8 - PROFESSOR
                return View();
            }
            else
            {
                return RedirectToAction("Logout", "Authentication", new { area = "" });
            }
        }
    }
}