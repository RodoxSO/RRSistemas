using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RRSistemas.Entity
{
    public class FileManagerENT
    {
        public int FileId { get; set; }
        public string FileName { get; set; }
        public int FileSize { get; set; }
        public string FileExtension { get; set; }
        public string DirectoryName { get; set; }
        public string FileDateCreation { get; set; }
    }
}