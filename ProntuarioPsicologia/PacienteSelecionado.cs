using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProntuarioPsicologia
{
    public class PacienteSelecionado
    {
        public PacienteSelecionado(int? id, string? cpf)
        {
            this.id = id;
            this.cpf = cpf;
        }

        public PacienteSelecionado() { }

        public int? id { get; set; }
        public string? cpf { get; set; }

        public static List<PacienteSelecionado> Selecionado = new List<PacienteSelecionado>();
    }
}
