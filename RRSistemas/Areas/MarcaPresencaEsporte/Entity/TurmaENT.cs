using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RRSistemas.Areas.MarcaPresencaEsporte.Entity
{
    public class TurmaENT
    {
        public int nr_linha { get; set; }
        public int id_turma { get; set; }
        public int id_aula { get; set; }
        public int id_empresa { get; set; }
        public int id_professor { get; set; }
        public String nm_turma { get; set; }
        public string ds_turma { get; set; }
        public String hr_inicio_aula { get; set; }
        public String hr_final_aula { get; set; }
        public bool dv_domingo { get; set; }
        public bool dv_segunda { get; set; }
        public bool dv_terca { get; set; }
        public bool dv_quarta { get; set; }
        public bool dv_quinta { get; set; }
        public bool dv_sexta { get; set; }
        public bool dv_sabado { get; set; }
        public int nr_vagas_total { get; set; }
        public bool dv_ativo { get; set; }
        public int id_usuario_sistema { get; set; }
        public String nm_usuario_professor { get; set; }
        public String nm_dia_semana { get; set; }
        public int nr_vagas_disponiveis { get; set; }
        public int nr_qtd_aluno { get; set; }
        public int dv_aula_liberada { get; set; }
    }
}