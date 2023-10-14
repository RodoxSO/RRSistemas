using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RRSistemas.Entity
{
    public class MenuENT
    {
        public int id_menu { get; set; }
        public int id_menu_pai { get; set; }
        public string nm_menu { get; set; }
        public string ds_menu { get; set; }
        public string link_menu { get; set; }
        public string nm_AreaName { get; set; }
        public string nm_ActionName { get; set; }
        public string nm_ControllerName { get; set; }
        public string parametro_icone_menu { get; set; }
        public string parametro_sub_menu { get; set; }
        public bool dv_ativo { get; set; }
    }
}
