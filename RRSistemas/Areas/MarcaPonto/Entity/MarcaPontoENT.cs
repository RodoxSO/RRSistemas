using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RRSistemas.Areas.MarcaPonto.Entity
{
    public class MarcaPontoENT
    {
        public string lbl_hora_atual {get; set;}
        public int id_ponto_eletronico { get; set; }
        public string nr_matricula { get; set;}
        public string nm_usuario {get; set;}
        public string nm_dia_semana { get; set;}
        public string dt_marcacao { get; set; }
        public string hr_marcacao { get; set;}
        public string nr_endereco_ip_local { get; set;}
        public string nm_tipo_acesso { get; set;}
        public string nm_tipo_marcacao { get; set; }
        public string nm_endereco_reg { get; set;}
    }
}