using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RRSistemas.Areas.MarcaPresencaEsporte.Entity
{
    public class AulaListaENT
    {
        public int nr_linha { get; set; }
        public int id_aula { get; set; }
        public String nm_turma { get; set; }
        public String nm_professor { get; set; }
        public String dt_aula { get; set; }
        public String nm_dia_semana { get; set; }
        public String hr_aula { get; set; }
        public int nr_vagas_total { get; set; }
        public int nr_vagas_disponiveis { get; set; }
        public bool dv_aula_dada { get; set; }
        public bool dv_ativo { get; set; }
    }
}