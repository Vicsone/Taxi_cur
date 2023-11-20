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
    public partial class Auth : Page
    {
        public Auth()
        {
            InitializeComponent();
        }

        private readonly DB _db = new DB();
        
        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            if (LoginTextBox.Text != "" && PasswordTextBox.Text != "")
            {
                if (LoginTextBox.Text.Length <= 15 && PasswordTextBox.Text.Length <= 15)
                {
                    Client client = _db.Clients.FirstOrDefault(c =>
                        c.User.Login == LoginTextBox.Text && c.User.Password == PasswordTextBox.Text);
                    if (client != null)
                    {
                        NavigationService.Navigate(new UserMain(client.User));
                    }
                    else
                    {
                        MessageBox.Show("Клиент с такими данными не найден!", "Error!", MessageBoxButton.OK,
                            MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Поля содержат слишком много данных");
                }
            }
            else
            {
                MessageBox.Show("Логин или пароль не может быть пустым", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void LoginTaxi_Button_Click(object sender, RoutedEventArgs e)
        {
            if (LoginTextBox.Text != "" && PasswordTextBox.Text != "")
            {
                Driver driver = _db.Drivers.FirstOrDefault(c => c.User.Login == LoginTextBox.Text && c.User.Password == PasswordTextBox.Text);
                if (driver != null)
                {
                    NavigationService.Navigate(new TaxiMain(driver.User));
                }
                else
                {
                    MessageBox.Show("Водитель с такими данными не найден!", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Логин или пароль не может быть пустым", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void LoginOperator_Button_Click(object sender, RoutedEventArgs e)
        {
            if (LoginTextBox.Text != "" && PasswordTextBox.Text != "")
            {
                Operator dbOperator = _db.Operators.FirstOrDefault(c => c.User.Login == LoginTextBox.Text && c.User.Password == PasswordTextBox.Text);
                if (dbOperator != null)
                {
                    NavigationService.Navigate(new OperatorMain(dbOperator.User));
                }
                else
                {
                    MessageBox.Show("Оператор с такими данными не найден!", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Логин или пароль не может быть пустым", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
