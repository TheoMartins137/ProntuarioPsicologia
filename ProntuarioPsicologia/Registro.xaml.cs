using ModernWpf.Controls;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
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
    /// Lógica interna para Registro.xaml
    /// </summary>
    public partial class Registro : Window
    {
        public Registro()
        {
            InitializeComponent();
        }

        public MySqlConnection Conexao = new MySqlConnection();
        private string data_source = "datasource=localhost;username=root;password=Martinsfreitas8;database=db_prontuario";

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if(PacienteSelecionado.Selecionado.Count == 0)
            {
                btnInsert.IsEnabled = true;
                btnInsert.Visibility = Visibility.Visible;
                btnUpdate.Visibility = Visibility.Collapsed;
                btnUpdate.IsEnabled = false;
            }
            else
            {
                btnUpdate.IsEnabled = true;
                btnUpdate.Visibility = Visibility.Visible;
                btnInsert.Visibility = Visibility.Collapsed;
                btnInsert.IsEnabled = false;

                foreach (PacienteSelecionado paciente in PacienteSelecionado.Selecionado)
                {
                    Conexao = new MySqlConnection(data_source);
                    Conexao.Open();

                    string sql = "SELECT data_registro, registro FROM registros WHERE id_registro = @id";
                    MySqlCommand buscar = new MySqlCommand(sql, Conexao);
                    buscar.Parameters.AddWithValue("@id", paciente.idRegistro);

                    MySqlDataReader reader = buscar.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            DTARegistro.Text = reader.GetString(0);
                            txtProntuario.Text = reader.GetString(1);
                        }
                        reader.Close();
                    }
                    Conexao.Close();
                }
            }
        }

        private void btnInsert_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                foreach (ListaPacientes paciente in ListaPacientes.lista)
                {
                    Conexao = new MySqlConnection(data_source);
                    Conexao.Open();

                    MySqlCommand cmd = new MySqlCommand();
                    cmd.Connection = Conexao;

                    cmd.Parameters.Clear();
                    cmd.CommandText = "INSERT INTO registros (id_paciente, registro, data_registro) " +
                        "VALUES (@id_paciente, @registro, @data_paciente)";

                    cmd.Parameters.AddWithValue("@id_paciente", paciente.id);
                    cmd.Parameters.AddWithValue("@registro", txtProntuario.Text);
                    cmd.Parameters.AddWithValue("@data_paciente", DTARegistro.Text);
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Registro salvo!");
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

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                foreach (PacienteSelecionado pacientes in PacienteSelecionado.Selecionado)
                {
                    Conexao = new MySqlConnection(data_source);
                    Conexao.Open();

                    MySqlCommand cmd = new MySqlCommand();
                    cmd.Connection = Conexao;

                    cmd.Parameters.Clear();
                    cmd.CommandText = "UPDATE registros " +
                                      "SET registro = @registro, data_registro = @data_registro " +
                                      "WHERE id_registro = @id";

                    cmd.Parameters.AddWithValue("@registro", txtProntuario.Text);
                    cmd.Parameters.AddWithValue("@data_registro", DTARegistro.Text);
                    cmd.Parameters.AddWithValue("@id", pacientes.idRegistro);
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Registro atualizado com sucesso.");
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
    }
}
