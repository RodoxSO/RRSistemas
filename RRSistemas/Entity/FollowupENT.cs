using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RRSistemas.Entity
{
    public class FollowupENT
    {
        public int nr_linha { get; set; }
        public int id_followup { get; set; }
        public int id_followup_tipo { get; set; }
        public int id_empresa { get; set; }
        public int id_usuario { get; set; }
        public Nullable<DateTime> dt_followup { get; set; }
        public String ds_followup { get; set; }
        public int id_usuario_sistema { get; set; }
        public Boolean dv_ativo { get; set; }

        public String nm_followup_tipo { get; set; }
        public string cat_followup { get; set; }
        public int id_parametro { get; set; }
        public int id_followup_categoria { get; set; }
}
}