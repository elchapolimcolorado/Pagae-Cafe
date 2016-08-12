using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiCafe.Areas.Turma.Models
{
    public class TurmaPontuacaoModel
    {
        public int IdTurmaPontuacao { get; set; }
        public int IdTurma { get; set; }
        public int IdUsuario { get; set; }
        public int OrdemUsuario { get; set; }
        public int Pontuacao { get; set; }
        public int QuantidadeRodadas { get; set; }
        public DateTime DataUltimaAlteracao { get; set; }

    }
}