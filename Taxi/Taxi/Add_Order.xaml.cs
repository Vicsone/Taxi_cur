using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

namespace Taxi
{
    /// <summary>
    /// Логика взаимодействия для Add_Order.xaml
    /// </summary>
    public partial class Add_Order : Page
    {
        SqlConnection connection = new SqlConnection();
        public Add_Order()
        {
            InitializeComponent();
        }

        private void Back_button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new UserMain());
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            string startposition = StartPositionTextBox.Text;
            string nextposition = NextPositionTextBox.Text;
            int client_id = 0;
            try
            {

                if (startposition != "" && nextposition != "")
                {
                    if(startposition.Length<30 && nextposition.Length < 30)
                    {
                        string connectionString = @"Data Source=DESKTOP-R1EIB3B\SQLEXPRESS;Initial Catalog=Taxi;Integrated Security=True";
                        string ins_ord = $"INSERT INTO Request(AddressFrom,AddressWhere,ClientId) VALUES (@startposition,@nextposition,@client_id)";
                        string sel_client = "SELECT Client.Id FROM Client,[User] WHERE Client.Id = [User].Id";

                        connection = new SqlConnection(connectionString);
                        connection.Open();

                        SqlCommand sqlCommand = new SqlCommand(sel_client, connection);
                        SqlDataReader reader = sqlCommand.ExecuteReader();
                        while (reader.Read())
                        {
                            client_id = reader.GetInt32(reader.GetOrdinal("Id"));
                        }
                        reader.Close();

                        SqlCommand sqlCommand1 = new SqlCommand(ins_ord, connection);
                        sqlCommand1.Parameters.AddWithValue("startposition", startposition);
                        sqlCommand1.Parameters.AddWithValue("nextposition", nextposition);
                        sqlCommand1.Parameters.AddWithValue("client_id", client_id);
                        sqlCommand1.ExecuteNonQuery();

                        connection.Close();
                    }
                    else
                    {
                        MessageBox.Show("Слишком много данных", "Error!", MessageBoxButton.OK, MessageBoxImage.Stop);
                    }
                }
                else
                {
                    MessageBox.Show("Есть незаполненные поля", "Error!", MessageBoxButton.OK, MessageBoxImage.Stop);
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
