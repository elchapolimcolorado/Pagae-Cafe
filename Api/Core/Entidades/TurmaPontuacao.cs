using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entidades
{
    [Table("TurmaPontuacao")]
    public class TurmaPontuacao
    {
        [Key]
        public int IdTurmaPontuacao { get; set; }
        public int IdTurma { get; set; }
        public int IdUsuario { get; set; }
        public int OrdemUsuario { get; set; }
        public int Pontuacao { get; set; }
        public int QuantidadeRodadas { get; set; }
        public DateTime DataUltimaAlteracao { get; set; }
    }
}