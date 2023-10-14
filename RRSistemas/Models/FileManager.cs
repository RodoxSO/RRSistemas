using RRSistemas.Entity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace RRSistemas.Models
{
    public class FileManager
    {
        int count = 1;
        //string nm_diretorio = HttpContext.Current.Session["nm_diretorio"].ToString();
        int id_empresa = Convert.ToInt32(HttpContext.Current.Session["id_empresa"]);
        int id_usuario = Convert.ToInt32(HttpContext.Current.Session["id_usuario"]);

        string path_core = "/UploadedFiles/";
        
        public void CreateDirectory()
        {
            string path_empresa = HttpContext.Current.Server.MapPath(path_core + id_empresa + "/");
            string path_usuario = path_empresa + id_usuario;

            try
            {
                if (!Directory.Exists(path_empresa))
                {
                    //Directory.CreateDirectory(path_empresa);
                    Directory.CreateDirectory(path_usuario);
                }
                else
                {
                    if (!Directory.Exists(path_usuario))
                    {
                        Directory.CreateDirectory(path_empresa + id_usuario);
                    }
                }
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<FileManagerENT> ObterArquivosDiretorio(string path_usuario)
        {
            try
            {
                List<FileManagerENT> FileList = new List<FileManagerENT>();

                FileManagerENT files = null;

                if (Directory.Exists(path_usuario))
                {
                    string[] arquivos = Directory.GetFiles(path_usuario);

                    foreach (string arq in arquivos)
                    {
                        files = new FileManagerENT();

                        FileInfo infoArquivo = new FileInfo(arq);

                        files.FileName = infoArquivo.Name;
                        files.FileExtension = infoArquivo.Extension;
                        files.FileSize = Convert.ToInt32(infoArquivo.Length);
                        files.FileDateCreation = infoArquivo.CreationTime.ToString();
                        files.DirectoryName = infoArquivo.DirectoryName;

                        FileList.Add(files);
                    }
                }
                
                return FileList;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public String ListFilesUser(int id_user)
        {
            string path_company = HttpContext.Current.Server.MapPath(path_core + id_empresa + "/");
            string path_user = path_company + id_user;
            string path_url_download = HttpContext.Current.Request.Url.Authority + path_core + id_empresa + "/" + id_user;

            try
            {
                String sHTML = String.Empty;
                

                List<FileManagerENT> arquivoLista = ObterArquivosDiretorio(path_user);

                foreach (FileManagerENT itemArquivoLista in arquivoLista)
                {
                    sHTML += String.Format("<tr>");
                    sHTML += String.Format("        <td>{0}</td>", count);
                    sHTML += String.Format("        <td>{0}</td>", itemArquivoLista.FileName);
                    sHTML += String.Format("        <td>{0}</td>", itemArquivoLista.FileExtension);
                    sHTML += String.Format("        <td>{0}</td>", itemArquivoLista.FileSize);
                    sHTML += String.Format("        <td>{0}</td>", itemArquivoLista.FileDateCreation);

                    //if (Convert.ToInt32(HttpContext.Current.Session["id_perfil"]) != 8)
                    //{
                    //sHTML += String.Format("        <td align=\"center\"><a href=//" + path_url_download +"/{0}><i class=\"fa fa-download\" data-toggle=\"tooltip\" data-placement=\"top\" title=\"Download\"></i></a></td>", itemArquivoLista.FileName);
                    sHTML += String.Format("        <td align=\"center\"><a href=\"/FileManager/DownloadFile?FileName={0}&IdUser={1}\"><i class=\"fa fa-download\" data-toggle=\"tooltip\" data-placement=\"top\" title=\"Download\"></i></a></td>", itemArquivoLista.FileName, id_user);
                    //}

                    sHTML += String.Format("        <td align=\"center\"><a href=\"/FileManager/DeleteFile?FileName={0}&IdUser={1}\"><i class=\"fa fa-trash\" data-toggle=\"tooltip\" data-placement=\"top\" title=\"Deletar\"></i></a></td>", itemArquivoLista.FileName, id_user);
                    sHTML += String.Format("</tr>");
                    count++;
                }

                return sHTML;
            }
            

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DownloadInfo(string PathFile, int IdUser)
        {
            string path_empresa = HttpContext.Current.Server.MapPath(path_core + id_empresa + "/");
            string path_usuario = path_empresa + id_usuario;
            string path_url_download = HttpContext.Current.Request.Url.Authority + path_core + id_empresa + "/" + IdUser;
        }

        public void DownloadFile(string PathFile)
        {
            FileInfo fInfo = new FileInfo(PathFile);
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ContentType = "application/octet-stream";
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=\"" + fInfo.Name + "\"");
            HttpContext.Current.Response.AddHeader("Content-Length", fInfo.Length.ToString());
            HttpContext.Current.Response.WriteFile(fInfo.FullName);
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.Close();
            fInfo = null;
        }

        public void DeleteFile(string PathFile)
        {
            FileInfo fInfo = new FileInfo(PathFile);
            try
            {
                fInfo.Delete();
            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message);
            }






            //string Message = null;
            //string path_empresa = HttpContext.Current.Server.MapPath(path_core + id_empresa + "/");
            //string path_usuario = path_empresa + id_usuario;

            //try
            //{

            //    if (File.Exists(path_usuario + "/" + FileName))
            //    {
            //        File.Delete(path_usuario + "/" + FileName);
            //        if (File.Exists(path_usuario + "/" + FileName) == false)
            //            Message = "<div class=\"p-3 mb-2 bg-success text-white\">Arquivo deletado com sucesso!</div>";
            //    }
            //    else
            //    {
            //        Message = "<div class=\"p-3 mb-2 bg-danger text-white\">O arquivo" + FileName + " nao existe!</div>";
            //        //return Message;
            //    }
            //}

            //catch (Exception ex)
            //{
            //    throw ex;
            //    Message = "<div class=\"p-3 mb-2 bg-danger text-white\">" + ex + "</div>";
            //    // return Message;
            //}

            //return Message;

        }

        public String UploadFile(HttpPostedFileBase file, int id_user)
        {
            string path_empresa = HttpContext.Current.Server.MapPath(path_core + id_empresa + "/");
            string path_usuario = path_empresa + id_user;

            try
            {
                if (!Directory.Exists(path_usuario))
                {
                    CreateDirectory();
                }

                if ((file.ContentLength > 0) && (file.ContentLength < 15728640))
                {
                    string _FileName = Path.GetFileName(file.FileName);
                    string _path = Path.Combine(path_usuario, _FileName);
                    file.SaveAs(_path);
                }

                string Message = "<div class=\"p-3 mb-2 bg-success text-white\">File Uploaded Successfully!!</div>";
                return Message;
            }

            catch (Exception ex)
            {
                string Message = "<div class=\"p-3 mb-2 bg-danger text-white\">File upload failed!!</div>";
                return Message;
                throw ex;
            }
        }

        public String ListQtdFilesUser()
        {
            String Message = null;
            String sHTML = String.Empty;
            string path_empresa = HttpContext.Current.Server.MapPath(path_core + id_empresa + "/");

            try
            {
                UsuarioDAO _usuarioDAO = new UsuarioDAO();
                List<UsuarioENT> listaUsuario = _usuarioDAO.ObterListaUsuario(id_empresa, 8);

                foreach (UsuarioENT itemUsuario in listaUsuario)
                {
                    List<FileManagerENT> arquivoLista = ObterArquivosDiretorio(path_empresa + "/" + itemUsuario.id_usuario);

                    if (arquivoLista.Count > 0) { 
                        sHTML += String.Format("<tr>");
                        sHTML += String.Format("        <td>{0}</td>", count);
                        sHTML += String.Format("        <td>{0}</td>", itemUsuario.nm_usuario);
                        sHTML += String.Format("        <td>{0}</td>", arquivoLista.Count);
                        sHTML += String.Format("        <td align=\"center\"><a href=\"/FileManager/ViewDownloadUser?id_usuario={0}\"><i class=\"fa fa-folder-open\" data-toggle=\"tooltip\" data-placement=\"top\" title=\"Abrir pasta\"></i></a></td>", itemUsuario.id_usuario);
                        sHTML += String.Format("</tr>");
                        count++;
                    }
                }
            }

            catch (Exception ex)
            {
                throw ex;
                Message = "<div class=\"p-3 mb-2 bg-danger text-white\">" + ex + "</div>";
                // return Message;
            }

            return sHTML;

        }
    }
}