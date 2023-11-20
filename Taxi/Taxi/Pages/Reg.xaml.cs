using System;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;

namespace Taxi.Pages;

public partial class Reg : Page
{
    public Reg()
    {
        InitializeComponent();
    }

    private DB _db = new DB();

    private void RegButton_OnClick(object sender, RoutedEventArgs e)
    {
        if (FirstNameTextBox.Text != String.Empty && MiddleNameTextBox.Text != String.Empty && LastNameTextBox.Text != String.Empty &&
            PhoneTextBox.Text != String.Empty && LoginTextBox.Text != String.Empty && PasswordTextBox.Text != String.Empty)
        {
            User user = _db.Users.Find(c => c.Login == LoginTextBox.Text);
            if (user == null)
            {
                using (SqlConnection connection = new SqlConnection(_db.connectionString))
                {
                    connection.Open();
                    string query = $"insert into [User] values (@Login,@Password,@FirstName,@LastName,@MiddleName,@Phone)";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Login", LoginTextBox.Text);
                        command.Parameters.AddWithValue("@Password", PasswordTextBox.Text);
                        command.Parameters.AddWithValue("@Phone", PhoneTextBox.Text);
                        command.Parameters.AddWithValue("@FirstName", FirstNameTextBox.Text);
                        command.Parameters.AddWithValue("@LastName", LastNameTextBox.Text);
                        command.Parameters.AddWithValue("@MiddleName", MiddleNameTextBox.Text);

                        command.ExecuteNonQuery();
                    }

                    connection.Close();
                }

                _db = new DB();

                user = _db.Users.Find(c => c.Login == LoginTextBox.Text);
                using (SqlConnection connection = new SqlConnection(_db.connectionString))
                {
                    connection.Open();
                    string query = $"insert into [Client] values (@Id)";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Id", user.Id);

                        command.ExecuteNonQuery();
                    }

                    connection.Close();
                }

                MessageBox.Show("Вы зарегистрировались!");
                NavigationService.GoBack();
            }
            else
                MessageBox.Show("Логин уже занят!");
        }
        else
        {
            MessageBox.Show("Поля не могут быть пустыми!", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private void Reg_OnLoaded(object sender, RoutedEventArgs e)
    {
        _db = new DB();
    }
}