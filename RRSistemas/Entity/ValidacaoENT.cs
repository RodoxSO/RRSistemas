using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RRSistemas.Entity
{
    public class ValidacaoENT
    {
        //VALIDACAO DE CADASTRO DE USUARIO - BD
        public int dvUsuario { get; set; }
        public int dvDocumento { get; set; }
        public int dvEndereco { get; set; }
        public int dvContato { get; set; }
        public int dvAluno { get; set; }
        public int dvFolowup { get; set; }

        //VALIDACAO DE CADASTRO DE USUARIO - BREADCRUMB
        public string breadcrumbUsuario { get; set; }
        public string breadcrumbDocumento { get; set; }
        public string breadcrumbEndereco { get; set; }
        public string breadcrumbContato { get; set; }
        public string breadcrumbAluno { get; set; }

        //VALIDACAO DE CADASTRO DE USUARIO - TABS
        public string tabUsuario { get; set; }
        public string tabDocumento { get; set; }
        public string tabEndereco { get; set; }
        public string tabContato { get; set; }
        public string tabAluno { get; set; }
        public string tabFolowup { get; set; }
    }
}