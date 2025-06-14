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
using System.Windows.Shapes;

namespace ProntuarioPsicologia
{
    /// <summary>
    /// Lógica interna para TelaPaciente.xaml
    /// </summary>
    public partial class TelaPaciente : Window
    {
        public TelaPaciente()
        {
            InitializeComponent();
        }

        private void btnAdicionar_Click(object sender, RoutedEventArgs e)
        {
            Registro registro = new Registro();
            this.Hide();
            registro.Show();
            return;
        }
    }
}
