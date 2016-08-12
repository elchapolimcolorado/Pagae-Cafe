using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entidades
{
    [Table("Turma")]
    public class Turma
    {
        [Key]
        public int IdTurma { get; set; }
        public int IdUsuarioCriador { get; set; }
        public int QuantidadeTurma { get; set; }
        public DateTime DataCriacao { get; set; }
    }
}
