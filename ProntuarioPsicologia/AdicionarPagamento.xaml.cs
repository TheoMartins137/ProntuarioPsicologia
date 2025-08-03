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
using MySql.Data.MySqlClient;

namespace ProntuarioPsicologia
{
    /// <summary>
    /// Lógica interna para AdicionarPagamento.xaml
    /// </summary>
    public partial class AdicionarPagamento : Window
    {
        public AdicionarPagamento()
        {
            InitializeComponent();
        }

        public MySqlConnection Conexao = new MySqlConnection();
        private string data_source = "datasource=localhost;username=root;password=Martinsfreitas8;database=db_prontuario";

        private void btnVoltar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void VerificaçãoData()
        {
            if (DataPagamento.SelectedDate == null)
            {
                MessageBox.Show("Selecione uma data válida");
                return;
            }

        }

        private void btnAdicionar_Click(object sender, RoutedEventArgs e)
        {
         VerificaçãoData();
            try
            {
                foreach (ListaPacientes pacientes in ListaPacientes.lista)
                {

                    Conexao = new MySqlConnection(data_source);
                    Conexao.Open();

                    MySqlCommand cmd = new MySqlCommand();
                    cmd.Connection = Conexao;

                    cmd.Parameters.Clear();
                    cmd.CommandText = "INSERT INTO pagamentos (id_pacientes, valor, data_pagamento)" +
                                     "VALUES (@id_paciente, @valor, @data_pagamento)";

                    cmd.Parameters.AddWithValue("@valor", Convert.ToDecimal(txtValor.Text));
                    cmd.Parameters.AddWithValue("@data_pagamento", Convert.ToDateTime(DataPagamento.Text));
                    cmd.Parameters.AddWithValue("@id_paciente", pacientes.id);



                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Pagamento adicionado com sucesso", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
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

        private void AdicionarPagamento1_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (ListaPacientes pacientes in ListaPacientes.lista)
            {
                using (var conexao = new MySqlConnection(data_source))
                {
                    conexao.Open();

                    string sql = "SELECT valor FROM pacientes WHERE id_pacientes = @id";
                    using (MySqlCommand comando = new MySqlCommand(sql, conexao))
                    {
                        comando.Parameters.AddWithValue("@id", pacientes.id);

                        using (MySqlDataReader reader = comando.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                txtValor.Text = reader.GetString(0);
                            }
                            ;
                        }
                    }
                }
            }
        }
    }
}
