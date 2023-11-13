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
    /// Логика взаимодействия для MainWin.xaml
    /// </summary>
    public partial class MainWin : Page
    {
        SqlConnection connection = new SqlConnection();
        List<User> users; 
        public MainWin()
        {
            InitializeComponent();
        }

        private void Login_Button_Click(object sender, RoutedEventArgs e)
        {
            if (LoginTextBox.Text != "" && PasswordTextBox.Text != "")
            {
                users = new List<User>();
                string connectionString = @"Data Source=DESKTOP-R1EIB3B\SQLEXPRESS;Initial Catalog=Taxi;Integrated Security=True";
                string sqlExpression = "SELECT [Login],[Password] FROM [User],Client WHERE Client.Id=[User].Id";

                connection = new SqlConnection(connectionString);
                connection.Open();

                SqlCommand sqlCommand = new SqlCommand(sqlExpression, connection);
                SqlDataReader reader = sqlCommand.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        User user = new User();
                        user.Login = reader.GetString(reader.GetOrdinal("login"));
                        user.Password = reader.GetString(reader.GetOrdinal("password"));
                        users.Add(user);
                    }
                }
                reader.Close();
                foreach(var item in users)
                {
                    if (LoginTextBox.Text == item.Login && PasswordTextBox.Text == item.Password)
                    {
                        NavigationService.Navigate(new UserMain());
                    }
                }
            }
            else
            {
                MessageBox.Show("Логин или пароль не может быть пустым", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Login_Taxi_Button_Click(object sender, RoutedEventArgs e)
        {
            if (LoginTextBox.Text != "" && PasswordTextBox.Text != "")
            {
                users = new List<User>();
                string connectionString = @"Data Source=DESKTOP-R1EIB3B\SQLEXPRESS;Initial Catalog=Taxi;Integrated Security=True";
                string sqlExpression = "SELECT [Login],[Password] FROM [User],Driver WHERE Driver.Id=[User].Id";

                connection = new SqlConnection(connectionString);
                connection.Open();
                SqlCommand sqlCommand = new SqlCommand(sqlExpression, connection);
                sqlCommand.Parameters.AddWithValue("login", LoginTextBox.Text);
                SqlDataReader reader = sqlCommand.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        User user = new User();
                        user.Login = reader.GetString(reader.GetOrdinal("login"));
                        user.Password = reader.GetString(reader.GetOrdinal("password"));
                        users.Add(user);
                    }
                }
                reader.Close();
                foreach (var item in users)
                {
                    if (LoginTextBox.Text == item.Login && PasswordTextBox.Text == item.Password)
                    {
                        NavigationService.Navigate(new TaxiMain());
                    }
                }
            }
            else
            {
                MessageBox.Show("Логин или пароль не может быть пустым", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Login_Operator_Button_Click(object sender, RoutedEventArgs e)
        {
            if (LoginTextBox.Text != "" && PasswordTextBox.Text != "")
            {
                users = new List<User>();
                string connectionString = @"Data Source=DESKTOP-R1EIB3B\SQLEXPRESS;Initial Catalog=Taxi;Integrated Security=True";
                string sqlExpression = "SELECT [Login],[Password] FROM [User],Operator WHERE Operator.Id=[User].Id";

                connection = new SqlConnection(connectionString);
                connection.Open();
                SqlCommand sqlCommand = new SqlCommand(sqlExpression, connection);
                sqlCommand.Parameters.AddWithValue("login", LoginTextBox.Text);
                SqlDataReader reader = sqlCommand.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        User user = new User();
                        user.Login = reader.GetString(reader.GetOrdinal("login"));
                        user.Password = reader.GetString(reader.GetOrdinal("password"));
                        users.Add(user);
                    }
                }
                reader.Close();
                foreach (var item in users)
                {
                    if (LoginTextBox.Text == item.Login && PasswordTextBox.Text == item.Password)
                    {
                        NavigationService.Navigate(new OperatorMain());
                    }
                }
            }
            else
            {
                MessageBox.Show("Логин или пароль не может быть пустым", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
