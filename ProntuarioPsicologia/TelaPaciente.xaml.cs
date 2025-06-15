using Microsoft.Win32;
using MySql.Data.MySqlClient;
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

        public MySqlConnection Conexao = new MySqlConnection();
        private string data_source = "datasource=localhost;username=root;password=Martinsfreitas8;database=db_prontuario";

        private void btnAdicionar_Click(object sender, RoutedEventArgs e)
        {
            Registro registro = new Registro();
            this.Hide();
            registro.Show();
            return;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            int nota;
            int pago;
            int psi;

            foreach (ListaPacientes pacientes in ListaPacientes.lista)
            {
                Conexao = new MySqlConnection(data_source);
                Conexao.Open();

                string sql = "SELECT cpf_pacientes, nome, data_nascimento, telefone, valor, valor_nota, valor_pago, nome_responsavel, telefone_responsavel, telefone_confianca, id_psicologo FROM pacientes WHERE id_pacientes = @id";
                MySqlCommand buscar = new MySqlCommand(sql, Conexao);
                buscar.Parameters.AddWithValue("@id", pacientes.id);

                MySqlDataReader reader = buscar.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        txtCPF.Text = reader.GetString(0);
                        txtNome.Text = reader.GetString(1);
                        DTANasc.Text = reader.GetString(2);
                        txtTelefone.Text = reader.GetString(3);
                        txtValor.Text = reader.GetString(4);
                        nota = reader.GetInt32(5);
                        if (nota == 0)
                            ckbNota.IsChecked = false;
                        else
                            ckbNota.IsChecked = true;
                        pago = reader.GetInt32(6);
                        if (pago == 0)
                            ckbPago.IsChecked = false;
                        else
                            ckbPago.IsChecked = true;
                        txtNomeResponsavel.Text = reader.GetString(7);
                        txtTelefoneResponsavel.Text= reader.GetString(8);
                        txtTelefoneConfianca.Text = reader.GetString(9);
                        psi = reader.GetInt32(10);
                        if (psi == 1)
                            txtPsicologo.Text = "Alice Martins";
                        else
                            txtPsicologo.Text = "Igor Ferreira";
                    }
                    reader.Close();
                }
            }
        }
    }
}
