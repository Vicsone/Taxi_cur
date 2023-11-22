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
        StatusComboBox.ItemsSource = DB.entities.StatusList;
    }

    private void SaveButton_OnClick(object sender, RoutedEventArgs e)
    {
        using (SqlConnection connection = new SqlConnection(DB.entities.connectionString))
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