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
using Taxi.Models;

namespace Taxi
{
    /// <summary>
    /// Логика взаимодействия для Add_Order.xaml
    /// </summary>
    public partial class Add_Order : Page
    {
        SqlConnection connection = new SqlConnection();
        public Add_Order(User user)
        {
            InitializeComponent();
            _user = user;
        }

        private TaxiDB _taxiDb = new TaxiDB();
        private User _user;
        
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (StartPositionTextBox.Text != String.Empty && NextPositionTextBox.Text != String.Empty)
            {
                using (SqlConnection connection = new SqlConnection(_taxiDb.connectionString))
                {
                    connection.Open();
                    string query = $"insert into [Request] values (@AddressFrom,@AddressWhere,@ClientId,@OperatorId,@Date)";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@AddressFrom", StartPositionTextBox.Text);
                        command.Parameters.AddWithValue("@AddressWhere", NextPositionTextBox.Text);
                        command.Parameters.AddWithValue("@ClientId", _user.Id);
                        command.Parameters.AddWithValue("@OperatorId", DBNull.Value);
                        command.Parameters.AddWithValue("@Date", DateTime.Now);

                        command.ExecuteNonQuery();
                    }

                    connection.Close();
                }

                MessageBox.Show("Ваш заказ отправлен!");
                NavigationService.GoBack();
            }
        }
    }
}
