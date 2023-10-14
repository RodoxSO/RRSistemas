using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RRSistemas.Areas.MarcaPresencaEsporte.Entity
{
    public class AulaENT
    {
        public int nr_linha { get; set; }
        public int id_aula { get; set; }
        public int id_turma { get; set; }
        public string nm_turma { get; set; }
        public int id_professor { get; set; }
        public string nm_professor { get; set; }
        public bool dv_aula_dada { get; set; }
        public String dt_aula { get; set; }
        public string hr_inicio_aula { get; set; }
        public string hr_final_aula { get; set; }
        public String nm_dia_semana { get; set; }
        public String hr_aula { get; set; }
        public int nr_vagas_total { get; set; }
        public int nr_qtd_alunos { get; set; }
//        public bool dv_aula_dada { get; set; }
        public bool dv_ativo { get; set; }

    }
}