using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
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
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Taxi
{
    /// <summary>
    /// Логика взаимодействия для UserMain.xaml
    /// </summary>
    public partial class UserMain : Page
    {
        SqlConnection connection = new SqlConnection();
        List<Request> requests;
        DataTable dataTable;

        public UserMain()
        {
            InitializeComponent();
            requests = new List<Request>();
            dataTable = new DataTable();
            dataTable.Columns.Add("AddressFrom", typeof(string));
            dataTable.Columns.Add("AddressWhere", typeof(string));
            dataTable.Columns.Add("ClientId", typeof(int));
            string connectionString = @"Data Source=DESKTOP-R1EIB3B\SQLEXPRESS;Initial Catalog=Taxi;Integrated Security=True";
            string sqlExpression = "SELECT * FROM Request";
            connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand sqlCommand = new SqlCommand(sqlExpression, connection);
            SqlDataReader reader = sqlCommand.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Request request = new Request();
                    request.startpos = reader.GetString(reader.GetOrdinal("addressfrom"));
                    request.nextpos = reader.GetString(reader.GetOrdinal("addresswhere"));
                    request.client_id = reader.GetInt32(reader.GetOrdinal("clientid"));
                    requests.Add(request);
                    dataTable.Rows.Add(request.startpos, request.nextpos, request.client_id);
                }
            }
            OrdersDataGrid.ItemsSource = dataTable.DefaultView;
            OrdersDataGrid.ItemsSource = requests;
            connection.Close();
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Back_button_Click_1(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Auth());
        }

        private void AddOrderButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Add_Order());
        }

        private void OrdersDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
