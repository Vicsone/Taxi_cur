using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using Taxi.Models;

namespace Taxi.Pages;

public partial class EditRequest : Page
{
    public EditRequest(Request request, User operatorr)
    {
        InitializeComponent();
        DataContext = request;
        _operator = operatorr;
        DriverComboBox.ItemsSource = _taxiDb.Drivers;
    }

    private TaxiDB _taxiDb = new TaxiDB();
    User _operator;
    
    private void SaveButton_OnClick(object sender, RoutedEventArgs e)
    {
        if (DriverComboBox.SelectedItem != null)
        {
            using (SqlConnection connection = new SqlConnection(_taxiDb.connectionString))
            {
                connection.Open();
                string query =
                    $"update [Request] set OperatorId = {_operator.Id} where Id = {((Request)DataContext).Id}";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.ExecuteNonQuery();
                }

                connection.Close();
            }
            
            using (SqlConnection connection = new SqlConnection(_taxiDb.connectionString))
            {
                connection.Open();
                string query = $"insert into [Drive] values (@StatusId,@DriverId,@RequestId)";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@StatusId", 1);
                    command.Parameters.AddWithValue("@DriverId", ((Driver)DriverComboBox.SelectedItem).Id);
                    command.Parameters.AddWithValue("@RequestId", ((Request)DataContext).Id);

                    command.ExecuteNonQuery();
                }

                connection.Close();
            }
            
            MessageBox.Show("Сохранения изменены!");
            NavigationService.GoBack();
        }
        else
            MessageBox.Show("Выберите водителя!");
    }
}