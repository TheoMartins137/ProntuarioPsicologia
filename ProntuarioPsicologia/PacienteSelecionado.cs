using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProntuarioPsicologia
{
    public class PacienteSelecionado
    {

        public PacienteSelecionado() { }

        public int? idRegistro { get; set; }
        public string? dataPaciente { get; set; }

        public static List<PacienteSelecionado> Selecionado = new List<PacienteSelecionado>();
    }
}
