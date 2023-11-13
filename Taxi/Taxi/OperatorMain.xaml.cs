using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
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
    /// Логика взаимодействия для OperatorMain.xaml
    /// </summary>
    public partial class OperatorMain : Page
    {
        SqlConnection connection = new SqlConnection();
        List<Drive> drives;
        DataTable dataTable;
        public OperatorMain()
        {
            InitializeComponent();
            drives = new List<Drive>();
            dataTable = new DataTable();
            dataTable.Columns.Add("Drive_id", typeof(int));
            dataTable.Columns.Add("StatusId", typeof(int));
            dataTable.Columns.Add("DriverId", typeof(int));
            dataTable.Columns.Add("RequestId", typeof(int));
            string connectionString = @"Data Source=DESKTOP-R1EIB3B\SQLEXPRESS;Initial Catalog=Taxi;Integrated Security=True";
            string sqlExpression = "SELECT * FROM Drive";
            connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand sqlCommand = new SqlCommand(sqlExpression, connection);
            SqlDataReader reader = sqlCommand.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Drive drive = new Drive();
                    drive.DriveId = reader.GetInt32(reader.GetOrdinal("Id"));
                    drive.StasusId = reader.GetInt32(reader.GetOrdinal("StatusId"));
                    drive.DriverId = reader.GetInt32(reader.GetOrdinal("driverid"));
                    drive.RequestId = reader.GetInt32(reader.GetOrdinal("requestid"));
                    drives.Add(drive);
                    dataTable.Rows.Add(drive.DriverId,drive.StasusId, drive.DriverId, drive.RequestId);
                }
            }
            OrdersDataGrid.ItemsSource = dataTable.DefaultView;
            OrdersDataGrid.ItemsSource = drives;
            connection.Close();
        }

        private void Back_button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void OrdersDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Add_Driver_OrderButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Add_Driver_Foe_Order());
        }
    }
}
