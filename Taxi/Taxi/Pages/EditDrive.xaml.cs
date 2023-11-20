using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;

namespace Taxi.Pages;

public partial class EditDrive : Page
{
    public EditDrive(Drive drive)
    {
        InitializeComponent();

        DataContext = drive;
        StatusComboBox.ItemsSource = _db.StatusList;
    }

    private DB _db = new DB();

    private void SaveButton_OnClick(object sender, RoutedEventArgs e)
    {
        if (StatusComboBox.SelectedItem != null)
        {
            using (SqlConnection connection = new SqlConnection(_db.connectionString))
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
        else
        {
            MessageBox.Show("Поле статус не может быть пустым");
        }
    }
}