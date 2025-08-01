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
    /// Interação lógica para UC_Pesquisar.xam
    /// </summary>
    public partial class UC_Pesquisar : UserControl
    {
        public MySqlConnection Conexao = new MySqlConnection();
        private string data_source = "datasource=localhost;username=root;password=Martinsfreitas8;database=db_prontuario";

        public UC_Pesquisar()
        {
            InitializeComponent();

            Conexao = new MySqlConnection(data_source);
            Conexao.Open();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = Conexao;

            string sql = "SELECT id_pacientes, nome, cpf_pacientes, telefone, valor, valor_nota FROM pacientes ORDER BY id_pacientes ASC";
            MySqlCommand comand = new MySqlCommand(sql, Conexao);

            MySqlDataReader reader = comand.ExecuteReader();
            LstPacientes.Items.Clear();

            while (reader.Read())
            {
                var pacientes = new ListaPacientes
                {
                    id = reader.GetInt32(0),
                    nome = reader.GetString(1),
                    cpf = reader.GetString(2),
                    telefone = reader.GetString(3),
                    valor = reader.GetString(4),
                    nota = reader.GetInt32(5) == 1 ? "Com Nota" : "Sem Nota",
                };

                LstPacientes.Items.Add(pacientes);
            }

        }


        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (LstPacientes.SelectedItem is ListaPacientes pacientes)
            {
                pacientes.id = pacientes.id;
                pacientes.cpf = pacientes.cpf;

                ListaPacientes.lista.Clear();
                ListaPacientes.lista.Add(pacientes);
                TelaPaciente tela = new TelaPaciente();
                tela.ShowDialog();
                Atualizar();
            }
        }

        private void LstPacientes_Selected(object sender, RoutedEventArgs e)
        {

        }

        private void cbxPsi_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LstPacientes.Items.Clear();
            Atualizar();
        }

        private void cbxStatus_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LstPacientes.Items.Clear();
            Atualizar();
        }

        private void Atualizar()
        {
            int? pago;

            try
            {
                Conexao = new MySqlConnection(data_source);
                Conexao.Open();

                string sql = "SELECT id_pacientes, nome, cpf_pacientes, telefone, valor, valor_nota FROM pacientes WHERE 1=1";

                if (cbxPsi.SelectedIndex != -1)
                    sql += " AND id_psicologo = @id";

                sql += " ORDER BY id_pacientes ASC";

                MySqlCommand comand = new MySqlCommand(sql, Conexao);

                if (cbxPsi.SelectedIndex == 0)
                    pago = 1;
                else
                    pago = 2;

                comand.Parameters.AddWithValue("@id", pago);

                MySqlDataReader reader = comand.ExecuteReader();
                LstPacientes.Items.Clear();

                while (reader.Read())
                {
                    var pacientes = new ListaPacientes
                    {
                        id = reader.GetInt32(0),
                        nome = reader.GetString(1),
                        cpf = reader.GetString(2),
                        telefone = reader.GetString(3),
                        valor = reader.GetString(4),
                        nota = reader.GetInt32(5) == 1 ? "Com Nota" : "Sem Nota",
                    };

                    LstPacientes.Items.Add(pacientes);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Conexao.Close();
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            LstPacientes.Items.Refresh();
        }

        private void btnAtualizar_Click(object sender, RoutedEventArgs e)
        {
            LstPacientes.Items.Clear();
            Atualizar();
        }
    }
}
