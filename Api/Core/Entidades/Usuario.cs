using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entidades
{
    [Table("Usuario")]
    public class Usuario
    {
        [Key]
        public int idusuario { get; set; }
        public string nmusuario { get; set; }
        public string nmlogin { get; set; }
        public string nmemailusuario { get; set; }
        public string celular { get; set; }
        public string telefone { get; set; }
        public string cpf { get; set; }
        public string nmsenha { get; set; }
        public bool flativo { get; set; }
        public bool fladm { get; set; }
        public DateTime datainclusao { get; set; }
        public DateTime dataalteracao { get; set; }

    }
}
