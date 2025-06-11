using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProntuarioPsicologia
{
    public class ListaPacientes
    {
        public ListaPacientes(int? id, string? nome, string? telefone, string? nomeResponsavel, string? telefoneResponsavel)
        {
            this.id = id;
            this.nome = nome;
            this.telefone = telefone;
            this.nomeResponsavel = nomeResponsavel;
            this.telefoneResponsavel = telefoneResponsavel;
        }

        public ListaPacientes() { }

        public int? id { get; set; }
        public string? nome { get; set; }
        public string? telefone { get; set; }
        public string? nomeResponsavel { get; set; }
        public string? telefoneResponsavel { get; set; }

        public static List<ListaPacientes> lista = new List<ListaPacientes>(); 
    }
}
