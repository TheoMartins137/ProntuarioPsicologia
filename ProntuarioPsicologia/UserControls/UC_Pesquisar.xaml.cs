using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ProntuarioPsicologia.UserControls
{
    /// <summary>
    /// Interação lógica para UC_Pesquisar.xam
    /// </summary>
    public partial class UC_Pesquisar : UserControl
    {
        public UC_Pesquisar()
        {
            InitializeComponent();

            ListaPacientes pacientes = new ListaPacientes();
            pacientes.id = 1;
            pacientes.nome = "Teste";
            pacientes.telefone = "12345";
            pacientes.nomeResponsavel = "Carlos";
            pacientes.telefoneResponsavel = "123456";

            ListaPacientes.lista.Add(pacientes);

            ListaPacientes pacientes1 = new ListaPacientes();
            pacientes1.id = 2;
            pacientes1.nome = "Teste";
            pacientes1.telefone = "12345";
            pacientes1.nomeResponsavel = "Carlos";
            pacientes1.telefoneResponsavel = "123456";

            ListaPacientes.lista.Add(pacientes1);

            foreach (ListaPacientes paci in ListaPacientes.lista)
            {
                LstPacientes.Items.Add(paci);
            }



        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
