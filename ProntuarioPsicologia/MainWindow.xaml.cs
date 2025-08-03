using Microsoft.Win32;
using MySql.Data.MySqlClient;
using ProntuarioPsicologia.UserControls;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MySql.Data;

namespace ProntuarioPsicologia
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Item1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MainContent.Content = null;
            MainContent.Content = new UC_Pesquisar();
        }

        private void Item2_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MainContent.Content = null;
            MainContent.Content = new UC_Cadastro();
        }

        private void Item3_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show(" Sistema feito para controle, cadastro, atualização e excluir pacientes para psicólogos. \n" +
                            " No primeiro se tem o botão para pesquisa de pacientes, por onde se é possível listar e filtrar os cadastros. \n" +
                            " No segundo se tem a parte de cadastro, na qual se utiliza dos dados dos pacientes para cadastro e vinculo com seu psicólogo. \n" +
                            " Ao clicar em um registro na lista se abre uma tela onde tem as informações do paciente. \n" +
                            " Ainda nessa tela, é possível atualizar os dados do paciente ou excluir seu registro do banco de dados. \n" +
                            " Também é possível fazer prontuários para esse paciente, que estarão disponíveis para consulta em uma lista no canto inferior direito.", "Informações sobre o sistema!", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void Item4_MouseDown(object sender, MouseButtonEventArgs e)
        {
            SaveFileDialog salvarArquivo = new SaveFileDialog();
            salvarArquivo.Title = "Salvar Backup do Banco de Dados";
            salvarArquivo.Filter = "Arquivo SQL (*.sql)|*.sql";
            salvarArquivo.FileName = "backup_banco.sql";

            if (salvarArquivo.ShowDialog() == true)
            {
                string caminhoBackup = salvarArquivo.FileName;
                MessageBox.Show("Local escolhido: " + caminhoBackup);

                GerarBackup(caminhoBackup);
            }
        }

        private void GerarBackup(string caminho)
        {
            try
            {
                string conexao = "datasource=localhost;username=root;password=Martinsfreitas8;database=db_prontuario";
                using (MySqlConnection conn = new MySqlConnection(conexao))
                {
                    using (MySqlCommand cmd = new MySqlCommand())
                    {
                        using (MySqlBackup mb = new MySqlBackup(cmd))
                        {
                            conn.Open();
                            cmd.Connection = conn;
                            mb.ExportToFile(caminho);
                            conn.Close();
                        }
                    }
                }
                MessageBox.Show("Backup realizado com sucesso!", "Backup", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao gerar backup: " + ex.Message, "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}