using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
    /// Логика взаимодействия для Add_Driver_Foe_Order.xaml
    /// </summary>
    public partial class Add_Driver_Foe_Order : Page
    {
        SqlConnection connection = new SqlConnection();

        public Add_Driver_Foe_Order()
        {
            InitializeComponent();
            string connectionString = @"Data Source=DESKTOP-R1EIB3B\SQLEXPRESS;Initial Catalog=Taxi;Integrated Security=True";
            string sel_name = "SELECT FirstName,LastName FROM [User],Driver WHERE Driver.Id=[User].Id";
            string sel_count = "SELECT COUNT(FirstName) AS Counts FROM[User],Driver WHERE Driver.Id=[User].Id";

            connection = new SqlConnection(connectionString);
            connection.Open();

            int count = 0;
            int z = 0;
            SqlCommand sqlCommand1 = new SqlCommand(sel_count, connection);
            SqlDataReader reader1 = sqlCommand1.ExecuteReader();
            while (reader1.Read())
            {
                count = reader1.GetInt32(reader1.GetOrdinal("Counts"));
            }

            reader1.Close();

            string[] name = new string[count];
            string[] last_name = new string[count];

            SqlCommand sqlCommand = new SqlCommand(sel_name, connection);
            SqlDataReader reader = sqlCommand.ExecuteReader();
            while (reader.Read())
            {
                name[z] += reader.GetString(reader.GetOrdinal("firstname"));
                last_name[z] += reader.GetString(reader.GetOrdinal("lastname"));
                z++;
            }

            reader.Close();
            for (int i = 0; i < name.Length; i++)
            {
                NameDriver_ComboBox.Items.Add(name[i] + " " + last_name[i]);
            }

            connection.Close();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (NameDriver_ComboBox.Text != "")
            {
                int num_status = 0;
                string connectionString = @"Data Source=DESKTOP-R1EIB3B\SQLEXPRESS;Initial Catalog=Taxi;Integrated Security=True";
                string ins_drive = $"INSERT INTO Drive(StasusId,DriverId,RequestId) VALUES (@num_status,)";
            }
            else
            {
                MessageBox.Show("", "", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void Back_button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new OperatorMain());
        }
    }
}