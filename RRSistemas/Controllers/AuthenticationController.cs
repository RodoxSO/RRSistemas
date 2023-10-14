using RRSistemas.Entity;
using RRSistemas.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace RRSistemas.Controllers
{
    public class AuthenticationController : Controller
    {
        public static Conexao conexao = new Conexao();
        // GET: Authentication
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(UsuarioENT objUser)
        {
            if (ModelState.IsValid)
            {
                SqlConnection cn = null;
                SqlParameter par = null;
                DbDataReader rd = null;

                try
                {
                    cn = conexao.Conectar();
                    SqlCommand cmd = cn.CreateCommand();
                    cmd.CommandType = System.Data.CommandType.Text;

                    cmd.CommandText = @"EXEC adm.SP_LOGIN_USUARIO @login = @login, @senha = @senha";
                    par = cmd.CreateParameter();
                    par.DbType = System.Data.DbType.String;
                    par.ParameterName = "@login";
                    par.Value = objUser.nm_login;
                    cmd.Parameters.Add(par);

                    par = cmd.CreateParameter();
                    par.DbType = System.Data.DbType.String;
                    par.ParameterName = "@senha";
                    par.Value = objUser.pw_senha;
                    cmd.Parameters.Add(par);

                    rd = cmd.ExecuteReader();

                    while (rd.Read())
                    {
                        Session["id_usuario"] = Convert.ToInt32(rd["id_usuario"]);
                        Session["nm_usuario"] = rd["nm_usuario"].ToString();
                        Session["id_empresa"] = Convert.ToInt32(rd["id_empresa"]);
                        Session["id_perfil"] = Convert.ToInt32(rd["id_perfil"]);
                        Session["id_sexo"] = Convert.ToInt32(rd["id_sexo"]);
                        Session["nm_perfil"] = rd["nm_perfil"].ToString();
                        Session["nm_diretorio"] = rd["nm_diretorio"].ToString();


                        if (rd["nm_arquivo_foto"].ToString() == null || rd["nm_arquivo_foto"].ToString() == "")
                        {
                            if (Convert.ToInt32(rd["id_sexo"]) == 1)
                            {
                                Session["nm_foto_usuario"] = "avatarFem.jpg";
                            }
                            else
                            {
                                Session["nm_foto_usuario"] = "avatarMasc.jpg";
                            }
                        }
                        else
                        {
                            Session["nm_foto_usuario"] = rd["nm_arquivo_foto"].ToString();
                        }

                        return RedirectToAction("Redirecionar");


                    }
                }

                catch (Exception ex)
                {
                    ViewBag.Message = ex;
                    throw ex;
                }

                finally
                {
                    if (rd != null)
                        rd.Close();
                    if (cn != null)
                        cn.Close();
                }
            }

            ViewBag.Message = "Usuario ou senha incorretos!";
            return View(objUser);
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Clear();
            Session.Abandon();
            FormsAuthentication.RedirectToLoginPage();

            return RedirectToAction("Redirecionar");
        }

        public ActionResult Redirecionar()
        {
            if (Session["id_usuario"] != null)
            {
                //return View();
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
    }
}