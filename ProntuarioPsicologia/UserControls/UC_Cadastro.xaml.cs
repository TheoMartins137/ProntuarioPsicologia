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
using MySql.Data.MySqlClient;

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
            EsconderRet();
            EsconderTudo();
        }

        public MySqlConnection Conexao = new MySqlConnection();
        private string data_source = "datasource=localhost;username=root;password=Martinsfreitas8;database=db_prontuario";

        public void EsconderRet()
        {
            RetOpcional.Visibility = Visibility.Collapsed;
        }

        public void MostrarRet()
        {
            RetOpcional.Visibility = Visibility.Visible;
        }

        public void EsconderTudo()
        {
            var controles = new Control[]
            {
                txtNome, txtNomeResponsavel, txtTelefone, txtTelefoneConfianca, txtValor,
                lblNasc, lblNome, lblNomeResponsavel, lblTelefone, lblTelefone, lblTelefoneConfianca, lblValor,
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
                txtNome, txtNomeResponsavel, txtTelefone, txtTelefoneConfianca, txtValor,
                lblNasc, lblNome, lblNomeResponsavel, lblTelefone, lblTelefone, lblTelefoneConfianca, lblValor,
                DTANasc, ckbNota, btnCadastrar
            };
            foreach (var controle in controles)
            {
                controle.Visibility = Visibility.Visible;
                controle.IsEnabled = true;

            }
        }

        public void LimparTudo()
        {
            txtNome.Text = "";
            txtNomeResponsavel.Text = "";
            txtTelefone.Text = "";
            txtTelefoneConfianca.Text = "";
            txtValor.Text = "";
            txtCPF.Text = "";

            cbxPsi.SelectedIndex = -1;
            DTANasc.SelectedDate = null;
            ckbNota.IsChecked = false;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Conexao = new MySqlConnection(data_source);
                Conexao.Open();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = Conexao;

                string sql = "SELECT id_pacientes FROM pacientes WHERE cpf_pacientes = @cpf";
                MySqlCommand comando = new MySqlCommand(sql, Conexao);

                if (string.IsNullOrEmpty(txtCPF.Text))
                {
                    MessageBox.Show("Digite um CPF", "ERRO", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if (cbxPsi.SelectedIndex == -1)
                {
                    MessageBox.Show("Selecione um psicólogo", "ERRO", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                comando.Parameters.AddWithValue("@cpf", txtCPF.Text);
                Object cpf = comando.ExecuteScalar();
                if (cpf != null)
                {
                    MessageBox.Show("Paciente já cadastrado", "EXISTENTE", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                else
                {
                    MostrarRet();
                    MostrarTudo();
                }

            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error " + "has occured: " + ex.Message,
                   "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Has occured: " + ex.Message,
                    "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                Conexao.Close();
            }
        }

        private void cbxPsi_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            EsconderRet();
            EsconderTudo();
        }

        int nota;
        int psi;

        private void btnCadastrar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Conexao = new MySqlConnection(data_source);
                Conexao.Open();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = Conexao;

                cmd.Parameters.Clear();
                cmd.CommandText = "INSERT INTO pacientes (cpf_pacientes, nome, data_nascimento, telefone, valor, valor_nota, valor_pago, nome_responsavel, telefone_responsavel, id_psicologo)" +
                             "VALUES(@cpf, @nome, @data_nasc , @telefone, @valor, @nota, @pago, @nome_responsavel, @telefone_confianca, @id_psicologo);";

                cmd.Parameters.AddWithValue("@cpf", txtCPF.Text);
                cmd.Parameters.AddWithValue("@nome", txtNome.Text);
                cmd.Parameters.AddWithValue("@data_nasc", DTANasc.Text);
                cmd.Parameters.AddWithValue("@telefone", txtTelefone.Text);
                cmd.Parameters.AddWithValue("@valor", txtValor.Text);

                if (ckbNota.IsChecked == true)
                {
                    nota = 1;
                    cmd.Parameters.AddWithValue("@nota", nota);
                }
                else
                {
                    nota = 0;
                    cmd.Parameters.AddWithValue("@nota", nota);
                }
                cmd.Parameters.AddWithValue("@pago", 0);
                cmd.Parameters.AddWithValue("@nome_responsavel", txtNomeResponsavel.Text);
                cmd.Parameters.AddWithValue("@telefone_confianca", txtTelefoneConfianca.Text);

                if (cbxPsi.Text == "Alice Martins")
                {
                    psi = 1;
                    cmd.Parameters.AddWithValue("id_psicologo", psi);
                }
                else if (cbxPsi.Text == "Igor Ferreira")
                {
                    psi = 2;
                    cmd.Parameters.AddWithValue("id_psicologo", psi);
                }
                else
                {
                    MessageBox.Show("Selecione um psicólogo", "ERRO", MessageBoxButton.OK, MessageBoxImage.Warning);
                }

                if(ValidarCampos())
                { 
                MessageBoxResult result = MessageBox.Show("Verifique os campos antes de cadastrar \n \r Deseja Cadastrar?", "ATENÇÂO", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (result == MessageBoxResult.Yes)
                    {
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Cadastro realizado com sucesso", "SUCESSO", MessageBoxButton.OK, MessageBoxImage.Information);
                        EsconderTudo();
                        EsconderRet();
                        LimparTudo();
                    }
                    else
                    {
                        return;
                    }
                }

            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error " + "has occured: " + ex.Message,
                   "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Has occured: " + ex.Message,
                    "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                Conexao.Close();
            }
        }
        public bool ValidarCampos()
        {
            if (string.IsNullOrWhiteSpace(txtNome.Text))
            {
                MessageBox.Show("Preencha o nome", "ERRO", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtTelefone.Text))
            {
                MessageBox.Show("Preencha o telefone", "ERRO", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtValor.Text))
            {
                MessageBox.Show("Preencha o valor", "ERRO", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (DTANasc.SelectedDate == null)
            {
                MessageBox.Show("Selecione uma data de nascimento", "ERRO", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if(string.IsNullOrEmpty(txtCPF.Text))
            {
                MessageBox.Show("Preencha o CPF", "ERRO", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }
    }
}
