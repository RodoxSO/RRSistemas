using RRSistemas.Entity;
using RRSistemas.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RRSistemas.Controllers
{
    public class FileManagerController : Controller
    {
        //string nm_diretorio = System.Web.HttpContext.Current.Session["nm_diretorio"].ToString();
        // GET: FileManager
        //int id_usuario = Convert.ToInt32(System.Web.HttpContext.Current.Session["id_usuario"]);
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ViewUpload()
        {
            FileManager fileManager = new FileManager();

            if (Session["id_usuario"] != null)
            {
                ViewBag.ListFilesUser = fileManager.ListFilesUser(Convert.ToInt32(Session["id_usuario"]));
                return View();
            }
            else
            {
                return RedirectToAction("Logout", "Authentication", new { area = "" });
            }
        }

        [HttpPost]
        public ActionResult ViewUpload(HttpPostedFileBase file)
        {
            FileManager fileManager = new FileManager();

            ViewBag.Message = fileManager.UploadFile(file, Convert.ToInt32(Session["id_usuario"])); ;
            ViewBag.ListaArquivos = fileManager.ListFilesUser(Convert.ToInt32(Session["id_usuario"]));
            return View();
        }

        public ActionResult DeleteFile(string FileName, int IdUser)
        {
            string path_core = "/UploadedFiles/";
            int id_empresa = Convert.ToInt32(HttpContext.Session["id_empresa"]);
            int id_usuario = Convert.ToInt32(HttpContext.Session["id_usuario"]);

            string path_empresa = HttpContext.Server.MapPath(path_core + id_empresa + "/");
            string path_usuario = path_empresa + IdUser + "\\";
            string path_url_download = HttpContext.Request.Url.Authority + path_core + id_empresa + "/" + IdUser + "/";
            string PathFile = path_usuario + FileName;

            FileManager fileManager = new FileManager();
            fileManager.DeleteFile(PathFile);

            return RedirectToAction("ViewDownloadUser?id_usuario="+ IdUser, "FileManager", new { area = "" });
        }

        public ActionResult DownloadFile(string FileName, int IdUser)
        {

            string path_core = "/UploadedFiles/";
            int id_empresa = Convert.ToInt32(HttpContext.Session["id_empresa"]);
            int id_usuario = Convert.ToInt32(HttpContext.Session["id_usuario"]);

            string path_empresa = HttpContext.Server.MapPath(path_core + id_empresa + "/");
            string path_usuario = path_empresa + IdUser + "\\";
            string path_url_download = HttpContext.Request.Url.Authority + path_core + id_empresa + "/" + IdUser + "/";

            string PathFile = path_usuario + FileName;

            //int id_empresa = Convert.ToInt32(HttpContext.Current.Session["id_empresa"]);
            //int id_usuario = Convert.ToInt32(HttpContext.Current.Session["id_usuario"]);

            //string path_core = "/UploadedFiles/15/14";

            //string path_empresa = Server.MapPath(path_core + id_empresa + "/");
            //string path_usuario = path_empresa + id_usuario;

            FileManager fileManager = new FileManager();

            //fileManager.DownloadFile(@Server.MapPath(path_core) + @"\" + FileName);
            fileManager.DownloadFile(PathFile);

            return RedirectToAction("ViewDownload", "FileManager", new { area = "" });
        }

        public ActionResult ViewDownload()
        {
            FileManager fileManager = new FileManager();

            if (Session["id_usuario"] != null)
            {
                ViewBag.ListQtdFilesUser = fileManager.ListQtdFilesUser();
                return View();
            }
            else
            {
                return RedirectToAction("Logout", "Authentication", new { area = "" });
            }
        }

        public ActionResult ViewDownloadUser(int id_usuario)
        {
            FileManager fileManager = new FileManager();

            if (Session["id_usuario"] != null)
            {
                ViewBag.ListFilesUser = fileManager.ListFilesUser(id_usuario);
                return View();
            }
            else
            {
                return RedirectToAction("Logout", "Authentication", new { area = "" });
            }
        }

    }
}