using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RRSistemas.Areas.MarcaPresencaEsporte.Entity
{
    public class AlunoENT
    {
    //CADASTRO DO ALUNO
        public int nr_linha { get; set; }
        public int id_usuario_responsavel { get; set; }
        public String nr_matricula { get; set; }
        public String cd_responsavel { get; set; }
        public String nm_responsavel { get; set; }
        public String nm_usuario { get; set; }
        public int id_aluno { get; set; }
        public String nm_aluno { get; set; }
        //public DateTime? dt_nascimento { get; set; }
        public String dt_nascimento { get; set; }
        public int id_sexo { get; set; }
        public string nm_sexo { get; set; }
        public String nm_foto_aluno { get; set; }
        public String nr_telefone { get; set; }
        public String nr_celular { get; set; }
        public String nm_email { get; set; }
        public String nr_rg { get; set; }
        public String nm_rg_orgao_emissor { get; set; }
        //public DateTime? dt_rg_expedicao { get; set; }
        public String dt_rg_expedicao { get; set; }
        public String nr_cpf { get; set; }

   //QUESTIONARIO DE SAUDE
        public int nr_altura { get; set; }
        public int nr_peso { get; set; }
        public String nr_pressao { get; set; }
        public int id_tipo_sanguineo { get; set; }
        public int dv_diabetes { get; set; }
        public int dv_pressao_alta { get; set; }
        public int dv_atividade_fisica { get; set; }
        public String ds_atividade_fisica { get; set; }
        public int dv_cirurgia { get; set; }
        public String ds_cirurgia { get; set; }
        public int dv_doenca_cronica { get; set; }
        public String ds_doenca_cronica { get; set; }
        public int dv_alergia { get; set; }
        public String ds_alergia { get; set; }
        public int dv_alergia_medicamento { get; set; }
        public String ds_alergia_medicamento { get; set; }
        public int dv_medicamento { get; set; }
        public String ds_medicamento { get; set; }
        public String hr_medicamento { get; set; }
        public DateTime? dt_cadastro { get; set; }
        public int id_usuario_cadastro { get; set; }
        public bool dv_ativo { get; set; }

        //ASSOCIASSAO ALUNO -> TURMA
        public int id_turma_aluno { get; set; }
        public int id_turma { get; set; }
        public String nm_turma { get; set; }
        public String nm_professor { get; set; }
        public String nm_dia_semana { get; set; }
        public String hr_inicio_aula { get; set; }
        public String hr_final_aula { get; set; }
    }
}