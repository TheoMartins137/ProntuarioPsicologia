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
    /// Interação lógica para UC_Cadastro.xam
    /// </summary>
    public partial class UC_Cadastro : UserControl
    {
        public UC_Cadastro()
        {
            InitializeComponent();
            EsconderTudo();
            
        }

        public void EsconderTudo()
        {
            var controles = new Control[]
            {
                txtNome, txtNomeResponsavel, txtTelefone, txtTelefoneConfianca, txtTelefoneResponsavel, txtValor,
                lblNasc, lblNome, lblNomeResponsavel, lblTelefone, lblTelefone, lblTelefoneConfianca, lblTelefoneResponsavel, lblValor,
                DTANasc, ckbNota, btnCadastrar
            };
            foreach (var controle in controles)
            {
                controle.Visibility = Visibility.Collapsed;
                controle.IsEnabled = false;

            }
        }

        public void MostrarTudo()
        {
            var controles = new Control[]
            {
                txtNome, txtNomeResponsavel, txtTelefone, txtTelefoneConfianca, txtTelefoneResponsavel, txtValor,
                lblNasc, lblNome, lblNomeResponsavel, lblTelefone, lblTelefone, lblTelefoneConfianca, lblTelefoneResponsavel, lblValor,
                DTANasc, ckbNota, btnCadastrar
            };
            foreach (var controle in controles)
            {
                controle.Visibility = Visibility.Visible;
                controle.IsEnabled = true;

            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MostrarTudo();
        }
    }
}
