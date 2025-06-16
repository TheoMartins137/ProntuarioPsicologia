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
            PacienteSelecionado.Selecionado.Clear();
            this.Hide();
            Registro registro = new Registro();
            registro.ShowDialog();
            this.Show();
            CarregarRegistros();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }


        private void lstProntuario_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void CarregarRegistros()
        {
            lstProntuario.Items.Clear();

            foreach (ListaPacientes pacientes in ListaPacientes.lista)
            {
                using (var conexao = new MySqlConnection(data_source))
                {
                    conexao.Open();

                    string sql = "SELECT id_registro, data_registro FROM registros WHERE id_paciente = @id ORDER BY id_registro ASC";
                    using (MySqlCommand comand = new MySqlCommand(sql, conexao))
                    {
                        comand.Parameters.AddWithValue("@id", pacientes.id);

                        using (MySqlDataReader reader = comand.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var registro = new PacienteSelecionado
                                {
                                    idRegistro = reader.GetInt32(0),
                                    dataPaciente = reader.GetString(1)
                                };

                                lstProntuario.Items.Add(registro);
                            }
                        }
                    }
                }
            }
        }

        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            CarregarRegistros();

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
                        txtTelefoneResponsavel.Text = reader.GetString(8);
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

        private void lstProntuario_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            if (lstProntuario.SelectedItem is PacienteSelecionado pacienteSelecionado)
            {
                pacienteSelecionado.idRegistro = pacienteSelecionado.idRegistro;
                pacienteSelecionado.dataPaciente = pacienteSelecionado.dataPaciente;

                PacienteSelecionado.Selecionado.Clear();
                PacienteSelecionado.Selecionado.Add(pacienteSelecionado);

                this.Hide();
                Registro registro = new Registro();
                registro.ShowDialog();
                CarregarRegistros();
                this.Show();

            }

        }

        private void btnAtualizar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                foreach (ListaPacientes pacientes in ListaPacientes.lista)
                {
                    Conexao = new MySqlConnection(data_source);
                    Conexao.Open();

                    MySqlCommand cmd = new MySqlCommand();
                    cmd.Connection = Conexao;

                    cmd.Parameters.Clear();
                    cmd.CommandText = "UPDATE pacientes " +
                                      "SET nome = @nome, telefone = @telefone, valor = @valor, nome_responsavel = @responsavel, telefone_responsavel = @telresponsavel, telefone_confianca = @telconfianca, valor_nota = @nota, valor_pago = @pago  " +
                                      "WHERE id_pacientes = @id";

                    cmd.Parameters.AddWithValue("@nome", txtNome.Text);
                    cmd.Parameters.AddWithValue("@telefone", txtTelefone.Text);
                    cmd.Parameters.AddWithValue("@valor", txtValor.Text);
                    cmd.Parameters.AddWithValue("@responsavel", txtNomeResponsavel.Text);
                    cmd.Parameters.AddWithValue("@telresponsavel", txtTelefoneResponsavel.Text);
                    cmd.Parameters.AddWithValue("@telconfianca", txtTelefoneConfianca.Text);

                    if (ckbNota.IsChecked == true)
                        cmd.Parameters.AddWithValue("@nota", 1);
                    else
                        cmd.Parameters.AddWithValue("@nota", 0);
                    if (ckbPago.IsChecked == true)
                        cmd.Parameters.AddWithValue("@pago", 1);
                    else
                        cmd.Parameters.AddWithValue("@pago", 0);

                    cmd.Parameters.AddWithValue("@id", pacientes.id);
                    cmd.ExecuteNonQuery();

                    MessageBoxResult result = MessageBox.Show("Verifique os campos antes de atualizar. \n \r Deseja Atualizar?", "ATENÇÂO", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (result == MessageBoxResult.Yes)
                    {
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Cadastro atualizado com sucesso.", "SUCESSO", MessageBoxButton.OK, MessageBoxImage.Information);
                        this.Close();
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

        private void btnExcluir_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show(" Deseja excluir o cadastro do paciente? \n \r Os prontuários serão excluídos em conjunto. \n \r Não é possível reverter a ação.", "EXCLUIR", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    foreach (ListaPacientes pacientes in ListaPacientes.lista)
                    {
                        Conexao = new MySqlConnection(data_source);
                        Conexao.Open();

                        MySqlCommand cmd = new MySqlCommand();
                        cmd.Connection = Conexao;

                        cmd.Parameters.Clear();
                        cmd.CommandText = "DELETE FROM registros WHERE id_paciente = @id";
                        cmd.Parameters.AddWithValue("id", pacientes.id);
                        cmd.ExecuteNonQuery();

                        cmd.CommandText = "DELETE FROM pacientes WHERE id_pacientes = @id2";
                        cmd.Parameters.AddWithValue("@id2", pacientes.id);
                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Cadastro excluido com sucesso. \n \r Atualize a lista na tela principal!", "SUCESSO", MessageBoxButton.OK, MessageBoxImage.Information);

                        this.Close();
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
            else
            {
                return;
            }
        }
    }
}
