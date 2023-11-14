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
    /// Логика взаимодействия для Change_Order.xaml
    /// </summary>
    public partial class Change_Order : Page
    {
        SqlConnection connection = new SqlConnection();
        public Change_Order()
        {
            InitializeComponent();
        }

        private void Back_button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new UserMain());
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            string typeMoney = Type_moneyComboBox.Text;
            string startposition = StartPositionTextBox.Text;
            string nextposition = NextPositionTextBox.Text;
            try
            {
                if (typeMoney != "" && startposition != "" && nextposition != "")
                {

                }
                else
                {
                    MessageBox.Show("Есть незаполненные поля", "Error!", MessageBoxButton.OK, MessageBoxImage.Stop);
                }
            }
            catch (Exception ex)
            {

            }
            }
    }
}
