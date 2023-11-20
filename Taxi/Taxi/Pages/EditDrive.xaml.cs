using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using Taxi.Models;

namespace Taxi.Pages;

public partial class EditDrive : Page
{
    public EditDrive(Drive drive)
    {
        InitializeComponent();

        DataContext = drive;
        StatusComboBox.ItemsSource = _taxiDb.StatusList;
    }

    private TaxiDB _taxiDb = new TaxiDB();

    private void SaveButton_OnClick(object sender, RoutedEventArgs e)
    {
        using (SqlConnection connection = new SqlConnection(_taxiDb.connectionString))
        {
            connection.Open();
            string query =
                $"update [Drive] set StatusId = {((Status)StatusComboBox.SelectedItem).Id} where Id = {((Drive)DataContext).Id}";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.ExecuteNonQuery();
            }

            connection.Close();
        }

        MessageBox.Show("Сохранения изменены!");
        NavigationService.GoBack();
    }
}