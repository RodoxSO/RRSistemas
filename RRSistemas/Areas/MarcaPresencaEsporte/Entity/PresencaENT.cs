using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RRSistemas.Areas.MarcaPresencaEsporte.Entity
{
    public class PresencaENT
    {
        public int id_turma_aluno { get; set; }
        public int id_turma { get; set; }
        public int id_aula { get; set; }
        public int id_aluno { get; set; }
        public Nullable<int> nr_aluno { get; set; }
        public string nm_aluno { get; set; }
        public string nm_foto_aluno { get; set; }
        public bool dv_presenca { get; set; }
        public string nm_turma { get; set; }
        public string nm_professor { get; set; }
        public string dt_aula { get; set; }
        public string hr_aula { get; set; }
        public string dt_aula_semana { get; set; }
    }
}