using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RRSistemas.Entity
{
    public class UsuarioENT
    {
        public int nr_linha { get; set; }
        public int id_usuario { get; set; }
        public int id_empresa { get; set; }
        public int id_perfil { get; set; }
        public int id_cargo { get; set; }
        public int id_departamento { get; set; }
        public int id_perfil_jornada { get; set; }
        public int id_sexo { get; set; }
        public int id_estado { get; set; }
        public string nm_usuario { get; set; }
        public Nullable<DateTime> dt_nascimento { get; set; }
        public string nm_endereco { get; set; }
        public string nr_endereco { get; set; }
        public string nm_complemento { get; set; }
        public string nm_bairro { get; set; }
        public string nm_cidade { get; set; }
        public string nr_cep { get; set; }
        public string nr_rg { get; set; }
        public string nm_rg_orgao_emissor { get; set; }
        public Nullable<DateTime> dt_rg_expedicao { get; set; }
        public string nr_cpf { get; set; }
        public string nr_telefone { get; set; }
        public string nr_celular { get; set; }
        public string nm_email { get; set; }
        public string nm_login { get; set; }
        public string pw_senha { get; set; }
        public string pw_senha_ura { get; set; }
        public string nm_diretorio { get; set; }
        public string nm_arquivo_foto { get; set; }
        public bool dv_primeiro_acesso { get; set; }
        public bool dv_reset_senha { get; set; }
        public bool dv_ponto_mobile { get; set; }
        public string cd_usuario { get; set; }
        public Nullable<DateTime> dt_admissao { get; set; }
        public Nullable<DateTime> dt_cadastro { get; set; }
        public int id_usuario_cadastro { get; set; }
        public bool dv_ativo { get; set; }


        public int id_usuario_sistema { get; set; }
    }
}
