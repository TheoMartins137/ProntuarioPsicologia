using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProntuarioPsicologia
{
    public class ListaPacientes
    {


        public ListaPacientes(int? id, string? nome, string? telefone, string? cpf, string? valor, string? status)
        {
            this.id = id;
            this.nome = nome;
            this.telefone = telefone;
            this.cpf = cpf;
            this.valor = valor;
            this.status = status;
        }
        public ListaPacientes() { }

        public int? id { get; set; }
        public string? nome { get; set; }
        public string? telefone { get; set; }
        public string? cpf {  get; set; }
        public string? valor { get; set; }
        public string? status { get; set; }

        public static List<ListaPacientes> lista = new List<ListaPacientes>(); 
    }
}
