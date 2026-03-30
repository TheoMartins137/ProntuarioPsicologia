using System;
using System.Collections.Generic;
using System.Configuration;
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
    /// Lógica interna para Pagamentos.xaml
    /// </summary>
    public partial class Pagamentos : Window
    {
        public Pagamentos()
        {
            InitializeComponent();
            DataPagamento.SelectedDate = DateTime.Now;
        }

        public MySqlConnection Conexao = new MySqlConnection();
        private string data_source = "datasource=localhost;username=root;password=Martinsfreitas8;database=db_prontuario";

        DateTime DataSelecionada;

        private void Pagamentos1_Loaded(object sender, RoutedEventArgs e)
        {
            LstPagamentos.Items.Clear();
            PesquisaData();
        }

        private void DataPagamento_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            LstPagamentos.Items.Clear();
            PesquisaData();
        }

        private void PesquisaData()
        {
            DataSelecionada = Convert.ToDateTime(DataPagamento.Text);
            LstPagamentos.Items.Clear();

            foreach (ListaPacientes pacientes in ListaPacientes.lista)
            {
                using (var conexao = new MySqlConnection(data_source))
                {
                    conexao.Open();

                    string sql = "SELECT id_pagamento, valor, data_pagamento FROM pagamentos WHERE id_pacientes = @id AND MONTH(data_pagamento) = @mes AND YEAR(data_pagamento) = @ano ORDER BY id_pagamento ASC";
                    using (MySqlCommand comand = new MySqlCommand(sql, conexao))
                    {
                        comand.Parameters.AddWithValue("@id", pacientes.id);
                        comand.Parameters.AddWithValue("@mes", DataSelecionada.Month);
                        comand.Parameters.AddWithValue("@ano", DataSelecionada.Year);

                        using (MySqlDataReader reader = comand.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var pagamento = new HistoricoPago
                                {
                                    id_pagamento = reader.GetInt32(0),
                                    valor = reader.GetFloat(1),
                                    data_pagamento = reader.GetDateTime(2),
                                };
                                LstPagamentos.Items.Add(pagamento);
                            }
                        }
                    }

                    txtTotal.Text = "";

                    string sql2 = "SELECT SUM(valor) FROM pagamentos WHERE id_pacientes = @id AND MONTH(data_pagamento) = @mes AND YEAR(data_pagamento) = @ano";
                    using (MySqlCommand comand2 = new MySqlCommand(sql2, conexao))
                    {
                        comand2.Parameters.AddWithValue("@id", pacientes.id);
                        comand2.Parameters.AddWithValue("@mes", DataSelecionada.Month);
                        comand2.Parameters.AddWithValue("@ano", DataSelecionada.Year);

                        using (MySqlDataReader reader2 = comand2.ExecuteReader())
                        {
                            while (reader2.Read())
                            {
                                if (reader2.IsDBNull(0))
                                    txtTotal.Text = "0";
                                else
                                    txtTotal.Text = Convert.ToString(reader2.GetDecimal(0));
                            }
                        }
                    }

                }
            }
        }


        private void btnVoltar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnAdicionar_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            AdicionarPagamento adicionarPagamento = new AdicionarPagamento();
            adicionarPagamento.ShowDialog();
            this.Show();
            PesquisaData();
        }

        private void DataPagamento_CalendarOpened(object sender, RoutedEventArgs e)
        {
            if (sender is DatePicker dp)
            {
                if (dp.Template.FindName("PART_Popup", dp) is System.Windows.Controls.Primitives.Popup popup)
                {
                    if (popup.Child is Calendar cal)
                    {
                        // Mostra a seleção por mês
                        cal.DisplayMode = CalendarMode.Year;

                        cal.DisplayModeChanged += (s, ev) =>
                        {
                            if (cal.DisplayMode == CalendarMode.Month)
                            {
                                if (cal.DisplayMode == CalendarMode.Month)
                                {
                                    dp.SelectedDate = new DateTime(cal.DisplayDate.Year, cal.DisplayDate.Month, 1);
                                }
                            }
                        };
                    }
                }
            }
        }
  
        private void LstPagamentos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }
    }
}
