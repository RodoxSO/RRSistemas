using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RRSistemas.Entity
{
    public class UploadDownloadENT
    {
        public int id_usuario { get; set; }
        public int id_file { get; set; }
        public string nm_file { get; set; }
        public string ds_file { get; set; }
        public Nullable<DateTime> dt_file { get; set; }
        public int nr_fileSize { get; set; }
    }
}