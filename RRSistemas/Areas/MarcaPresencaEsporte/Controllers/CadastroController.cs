using RRSistemas.Areas.MarcaPresencaEsporte.Entity;
using RRSistemas.Areas.MarcaPresencaEsporte.Models;
using RRSistemas.Entity;
using RRSistemas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace RRSistemas.Areas.MarcaPresencaEsporte.Controllers
{
    public class CadastroController : Controller
    {
        // GET: MarcaPresencaEsporte/Cadastro
        int id_usuario_session = Convert.ToInt32(System.Web.HttpContext.Current.Session["id_usuario"]);
        int id_empresa_session = Convert.ToInt32(System.Web.HttpContext.Current.Session["id_empresa"]);
        // GET: MarcaPresenca/Cadastro
        
        AulaDAO _aulaDAO;
        UsuarioDAO _usuarioDAO;
        TurmaDAO _turmaDAO;

        //****************************************************************************************************************
        // ALUNOS
        //****************************************************************************************************************
        AlunoDAO _alunoDAO;
        public ActionResult AlunoCadastro(int? id_aluno)
        {
            ViewBag.id_perfil = Session["id_perfil"];
            if (Session["id_usuario"] != null)
            {
                if (id_aluno != null)
                {
                    _alunoDAO = new AlunoDAO();
                    var Aluno = _alunoDAO.ObterAlunoPeloId(id_aluno.Value);

                    ViewBag.id_aluno = id_aluno.Value;
                    ViewBag.nm_aluno = Aluno.nm_aluno;
                    ViewBag.nr_matricula = Aluno.nr_matricula;
                    ViewBag.cd_responsavel = Aluno.cd_responsavel;
                    ViewBag.nm_responsavel = Aluno.nm_responsavel;
                    ViewBag.ComboSexo = new SelectList(new DropDownListDAO().CarregaComboSexo(), "id_sexo", "nm_sexo", Aluno.id_sexo);
                    ViewBag.ComboTipoSanguineo = new SelectList(new DropDownListDAO().CarregaComboTipoSanguineo(), "id_tipo_sanguineo", "nm_tipo_sanguineo", Aluno.id_tipo_sanguineo);
                    ViewBag.ComboDiabetes = new SelectList(new DropDownListDAO().CarregaComboSimNao(), "id_selecao", "nm_selecao", Aluno.dv_diabetes);
                    ViewBag.ComboPressaoAlta = new SelectList(new DropDownListDAO().CarregaComboSimNao(), "id_selecao", "nm_selecao", Aluno.dv_pressao_alta);
                    ViewBag.ComboAtividadeFisica = new SelectList(new DropDownListDAO().CarregaComboSimNao(), "id_selecao", "nm_selecao", Aluno.dv_atividade_fisica);
                    ViewBag.ComboCirurgia = new SelectList(new DropDownListDAO().CarregaComboSimNao(), "id_selecao", "nm_selecao", Aluno.dv_cirurgia);
                    ViewBag.ComboDoencaCronica = new SelectList(new DropDownListDAO().CarregaComboSimNao(), "id_selecao", "nm_selecao", Aluno.dv_doenca_cronica);
                    ViewBag.ComboAlergia = new SelectList(new DropDownListDAO().CarregaComboSimNao(), "id_selecao", "nm_selecao", Aluno.dv_alergia);
                    ViewBag.ComboAlergiaMedicamento = new SelectList(new DropDownListDAO().CarregaComboSimNao(), "id_selecao", "nm_selecao", Aluno.dv_alergia_medicamento);
                    ViewBag.ComboMedicamento = new SelectList(new DropDownListDAO().CarregaComboSimNao(), "id_selecao", "nm_selecao", Aluno.dv_medicamento);


                    ViewBag.ComboTurmaAlunoDisp = new SelectList(new TurmaDAO().CarregaComboTurmaAlunoDisp(Convert.ToInt32(id_aluno)), "id_turma", "nm_turma", Aluno.id_aluno);

                    ViewBag.ListaAlunoTurma = _alunoDAO.ListarAlunoTurma(id_aluno.Value);
                    //var AlunoTurma = _alunoDAO.ObterTurmaAlunoLista(id_aluno.Value);

                    return View("AlunoCadastro", Aluno);
                }
                else
                {
                    ViewBag.id_aluno = 0;
                    ViewBag.ComboSexo = new SelectList(new DropDownListDAO().CarregaComboSexo(), "id_sexo", "nm_sexo");
                    ViewBag.ComboTipoSanguineo = new SelectList(new DropDownListDAO().CarregaComboTipoSanguineo(), "id_tipo_sanguineo", "nm_tipo_sanguineo");
                    ViewBag.ComboDiabetes = new SelectList(new DropDownListDAO().CarregaComboSimNao(), "id_selecao", "nm_selecao");
                    ViewBag.ComboPressaoAlta = new SelectList(new DropDownListDAO().CarregaComboSimNao(), "id_selecao", "nm_selecao");
                    ViewBag.ComboAtividadeFisica = new SelectList(new DropDownListDAO().CarregaComboSimNao(), "id_selecao", "nm_selecao");
                    ViewBag.ComboCirurgia = new SelectList(new DropDownListDAO().CarregaComboSimNao(), "id_selecao", "nm_selecao");
                    ViewBag.ComboDoencaCronica = new SelectList(new DropDownListDAO().CarregaComboSimNao(), "id_selecao", "nm_selecao");
                    ViewBag.ComboAlergia = new SelectList(new DropDownListDAO().CarregaComboSimNao(), "id_selecao", "nm_selecao");
                    ViewBag.ComboAlergiaMedicamento = new SelectList(new DropDownListDAO().CarregaComboSimNao(), "id_selecao", "nm_selecao");
                    ViewBag.ComboMedicamento = new SelectList(new DropDownListDAO().CarregaComboSimNao(), "id_selecao", "nm_selecao");

                    return View();
                }
            }
            else
            {
                return RedirectToAction("Logout", "Authentication", new { area = "" });
            }
        }

        [HttpPost]
        public ActionResult AlunoCadastro(AlunoENT aluno)
        {
            int id_aluno = aluno.id_aluno;
            _alunoDAO = new AlunoDAO();
            if (ModelState.IsValid)
            {
                _alunoDAO.InserirAlterarAluno(aluno);
                return RedirectToAction("AlunoCadastro", "Cadastro", new { id_aluno = id_aluno });
            }
            else
            {
                return View("AlunoCadastro", id_aluno);
            }
        }

        public ActionResult AlunoTurmaCadastro(int id_aluno)
        {
            ViewBag.id_perfil = Session["id_perfil"];
            if (Session["id_usuario"] != null)
            {
                _alunoDAO = new AlunoDAO();
                var Aluno = _alunoDAO.ObterAlunoPeloId(id_aluno);

                ViewBag.id_aluno = id_aluno;
                ViewBag.ComboTurma = new SelectList(new DropDownListEspDAO().ObterTodosOsProfessores(id_empresa_session, 8), "id_usuario", "nm_usuario", id_aluno);

                return View("AlunoCadastro", Aluno);
            }
            else
            {
                return RedirectToAction("Logout", "Authentication", new { area = "" });
            }
        }



        //****************************************************************************************************************
        // AULA
        //****************************************************************************************************************
        public ActionResult AulaCadastro()
        {
            if (Session["id_usuario"] != null)
            {
                ViewBag.ComboProfessor = new SelectList(new DropDownListEspDAO().ObterTodosOsProfessores(id_empresa_session, 8), "id_usuario", "nm_usuario");
                ViewBag.ComboTurma = new SelectList(new DropDownListEspDAO().ObterTodasAsTurmas(id_usuario_session), "id_turma", "nm_turma");
                return View();
            }
            else
            {
                return RedirectToAction("Logout", "Authentication", new { area = "" });
            }
        }

        //[HttpPost]
        //public ActionResult AulaCadastro(AulaENT aula)
        //{
        //    int id_aula = aula.id_aula;
        //    _aulaDAO = new AulaDAO();
        //    if (ModelState.IsValid)
        //    {
        //        _aulaDAO.InserirAlterarAula(aula);
        //        return RedirectToAction("AulaEdit", id_aula);
        //    }
        //    else
        //    {
        //        return View("AulaCadastro", id_aula);
        //    }
        //}
        //public ActionResult AulaEdit(int id_aula)
        //{
        //    _aulaDAO = new AulaDAO();
        //    var Aula = _aulaDAO.ObterAulaPeloId(id_aula);
        //    int id_professor = Aula.id_professor;
        //    int id_turma = Aula.id_turma;

        //    ViewBag.ComboProfessor = new SelectList(new DropDownListEspDAO().ObterTodosOsProfessores(id_empresa_session, 8), "id_usuario", "nm_usuario", id_professor);
        //    ViewBag.ComboTurma = new SelectList(new DropDownListEspDAO().ObterTodasAsTurmas(id_usuario_session), "id_turma", "nm_turma", id_turma);

        //    return View("AulaCadastro", Aula);
            
        //}

        // POST: AULA/Edit/5
        //[HttpPost]
        //public ActionResult AulaEdit(AulaENT aula)
        //{
        //    int id_aula = aula.id_aula;
        //    _aulaDAO = new AulaDAO();

        //    if (ModelState.IsValid)
        //    {
        //        _aulaDAO.InserirAlterarAula(aula);
        //        return RedirectToAction("AulaEdit", id_aula);
        //    }
        //    else
        //    {
        //        return View("AulaEdit", id_aula);
        //    }
        //}

        //****************************************************************************************************************
        // CLIENTE
        //****************************************************************************************************************
        public ActionResult ClienteCadastro(int? id_usuario)
        {
            if (Session["id_usuario"] != null)
            {
                if (id_usuario != null)
                {
                    _usuarioDAO = new UsuarioDAO();
                    var Cliente = _usuarioDAO.ObterUsuarioPeloId(id_usuario.Value);
                    int id_sexo = Cliente.id_sexo;
                    int id_estado = Cliente.id_estado;
                    ViewBag.id_usuario = id_usuario.Value;

                    ViewBag.ComboSexo = new SelectList(new DropDownListDAO().CarregaComboSexo(), "id_sexo", "nm_sexo", id_sexo);
                    ViewBag.ComboEstado = new SelectList(new DropDownListDAO().CarregaComboEstado(), "id_estado", "nm_estado", id_estado);

                    return View("ClienteCadastro", Cliente);
                }
                else
                {
                    ViewBag.id_usuario = 0;
                    ViewBag.ComboSexo = new SelectList(new DropDownListDAO().CarregaComboSexo(), "id_sexo", "nm_sexo");
                    ViewBag.ComboEstado = new SelectList(new DropDownListDAO().CarregaComboEstado(), "id_estado", "nm_estado");
                    return View();
                }

                    //int id_cliente = Convert.ToInt32(Request["id_cliente"]);

            }
            else
            {
                return RedirectToAction("Logout", "Authentication", new { area = "" });
            }
        }

        // POST: AULA/Edit/5
        [HttpPost]
        public ActionResult ClienteCadastro(UsuarioENT usuario)
        {
            int id_usuario = usuario.id_usuario;
            _usuarioDAO = new UsuarioDAO();

            if (ModelState.IsValid)
            {
                usuario.id_empresa = id_empresa_session;
                usuario.id_perfil = 10; //CLIENTE
                usuario.id_cargo = 6; //CLIENTE
                usuario.id_departamento = 16; //CLIENTE
                usuario.id_perfil_jornada = 1;
                usuario.id_usuario_cadastro = id_usuario_session;

                _usuarioDAO.InserirAlterarUsuario(usuario);
                return RedirectToAction("ClienteCadastro", "Cadastro", new { id_usuario = id_usuario });
                //return RedirectToAction("ClienteCadastro", id_usuario);
            }
            else
            {
                return View("ClienteCadastro", id_usuario);
            }
        }

        public ActionResult ClienteListaAluno(int id_cliente)
        {
            if (Session["id_usuario"] != null)
            {
                _alunoDAO = new AlunoDAO();
                ViewBag.ListarAlunosCliente = _alunoDAO.ListarAlunoCliente(id_cliente);
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
        public ActionResult TurmaCadastro(int? id_turma)
        {
            ViewBag.id_perfil = Session["id_perfil"];
            if (Session["id_usuario"] != null)
            {
                
                if (id_turma != null)
                {
                    _turmaDAO = new TurmaDAO();
                    var Turma = _turmaDAO.ObterTurmaPeloId(id_turma.Value);
                    int id_professor = Turma.id_professor;

                    ViewBag.ComboProfessor = new SelectList(new DropDownListEspDAO().ObterTodosOsProfessores(id_empresa_session, 8), "id_usuario", "nm_usuario", id_professor);

                    return View("TurmaCadastro", Turma);
                }
                else
                {
                    ViewBag.ComboProfessor = new SelectList(new DropDownListEspDAO().ObterTodosOsProfessores(id_empresa_session, 8), "id_usuario", "nm_usuario");
                    return View();
                }
            }
            else
            {
                return RedirectToAction("Logout", "Authentication", new { area = "" });
            }
        }
        [HttpPost]
        public ActionResult TurmaCadastro(TurmaENT turma)
        {
            int id_turma = turma.id_turma;
            _turmaDAO = new TurmaDAO();

            if (ModelState.IsValid)
            {
                _turmaDAO.InserirEditarTurma(turma);
                ViewBag.Message = "Registro Inserido com Sucesso!";
                // DEFINIR COMO FAREI O RETORNO.
                //return View();
                return RedirectToAction("TurmaBusca","Busca");
                //return RedirectToAction("TurmaCadastro", "Cadastro", new { id_turma = id_turma });
            }
            else
            {
                return View("TurmaCadastro",id_turma);
            }
        }


        //****************************************************************************************************************
        // ALUNO/TURMA
        //****************************************************************************************************************

        [HttpGet]
        [Route("CadastroAlunoTurma/{id_aluno}/{id_turma}")]

        public void CadastroAlunoTurma(int id_aluno, int id_turma)
        {
            if (Session["id_usuario"] != null)
            {
                _turmaDAO = new TurmaDAO();
                _turmaDAO.TurmaAlunoAdd(id_aluno,id_turma);

                var routeValues = new RouteValueDictionary( new { Controller = "Cadastro", action = "AlunoCadastro", id_aluno = id_aluno });

                //return RedirectToAction("AlunoCadastro", "Cadastro", new { area = "MarcaPresencaEsporte", routeValues });
                //return RedirectToAction("AlunoCadastro", routeValues);
                //return View("AlunoCadastro", id_aluno);
                //Response.Redirect("~/MarcaPresencaEsporte/Cadastro/AlunoCadastro?id_aluno=" + id_aluno);
                //return View();
            }
            //else
            //{
            //    return RedirectToAction("Logout", "Authentication", new { area = "" });

            //}
        }

        public ActionResult AlunoTurmaDel(int id_turma_aluno, int id_aluno)
        {
            if (Session["id_usuario"] != null)
            {
                _turmaDAO = new TurmaDAO();
                _turmaDAO.TurmaAlunoDel(id_turma_aluno);

                
                return RedirectToAction("AlunoCadastro", "Cadastro", new { area = "MarcaPresencaEsporte", id_aluno = id_aluno });
                //return View("AlunoCadastro", id_aluno);
            }
            else
            {
                return RedirectToAction("Logout", "Authentication", new { area = "" });

            }
        }

        //****************************************************************************************************************
        // PROFESSOR
        //****************************************************************************************************************
        public ActionResult ProfessorCadastro(int? id_usuario)
        {
            if (Session["id_usuario"] != null)
            {
                if (id_usuario != null)
                {
                    _usuarioDAO = new UsuarioDAO();
                    var Professor = _usuarioDAO.ObterUsuarioPeloId(id_usuario.Value);
                    int id_sexo = Professor.id_sexo;
                    int id_estado = Professor.id_estado;
                    ViewBag.id_usuario = id_usuario.Value;

                    ViewBag.ComboSexo = new SelectList(new DropDownListDAO().CarregaComboSexo(), "id_sexo", "nm_sexo", id_sexo);
                    ViewBag.ComboEstado = new SelectList(new DropDownListDAO().CarregaComboEstado(), "id_estado", "nm_estado", id_estado);

                    return View("ProfessorCadastro", Professor);
                }
                else
                {
                    ViewBag.id_usuario = 0;
                    ViewBag.ComboSexo = new SelectList(new DropDownListDAO().CarregaComboSexo(), "id_sexo", "nm_sexo");
                    ViewBag.ComboEstado = new SelectList(new DropDownListDAO().CarregaComboEstado(), "id_estado", "nm_estado");
                    return View();
                }
            }
            else
            {
                return RedirectToAction("Logout", "Authentication", new { area = "" });
            }
        }

        [HttpPost]
        public ActionResult ProfessorCadastro(UsuarioENT usuario)
        {
            int id_usuario = usuario.id_usuario;
            _usuarioDAO = new UsuarioDAO();

            if (ModelState.IsValid)
            {
                usuario.id_empresa = id_empresa_session;
                usuario.id_perfil = 8; //PROFESSOR
                usuario.id_cargo = 7; //PROFESSOR
                usuario.id_departamento = 17; //ENSINO
                usuario.id_perfil_jornada = 1;

                _usuarioDAO.InserirAlterarUsuario(usuario);
                ViewBag.Message = "Registro Inserido com Sucesso!";
                // DEFINIR COMO FAREI O RETORNO.
                //return RedirectToAction("ProfessorCadastro", "Cadastro", new { id_usuario = id_usuario });
                return RedirectToAction("ProfessorBusca", "Busca");
            }
            else
            {
                return View("ProfessorCadastro", id_usuario);
            }
        }
    }
}