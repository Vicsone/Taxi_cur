using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
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
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Taxi
{
    /// <summary>
    /// Логика взаимодействия для UserMain.xaml
    /// </summary>
    public partial class UserMain : Page
    {
        private DB _db = new DB();

        public UserMain(User client)
        {
            InitializeComponent();
            _client = client;
            LeastToMost.IsChecked = true;
        }

        private User _client;

        private void LeastToMost_OnChecked(object sender, RoutedEventArgs e)
        {
            UpdateGrid();
        }

        private void MostToLeast_OnChecked(object sender, RoutedEventArgs e)
        {
            UpdateGrid();
        }

        private void AddRequestButton_OnClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Add_Order(_client));
        }

        private void SearchTextBox_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateGrid();
        }

        private void UpdateGrid()
        {
            List<Request> requests = _db.Requests.Where(c => c.ClientId == _client.Id).ToList();
            if (LeastToMost == null) return;
            
            if (requests.Count != 0)
            {
                if (LeastToMost.IsChecked == true)
                {
                    RequestDataGrid.ItemsSource = requests.Where(c =>
                            c.AddressFrom.ToLower().Contains(SearchTextBox.Text.ToLower()) || c.AddressWhere.ToLower().Contains(SearchTextBox.Text.ToLower()))
                        .OrderBy(c => c.Date).ToList();
                }
                else
                {
                    RequestDataGrid.ItemsSource =
                        requests.Where(c =>
                                c.AddressFrom.ToLower().Contains(SearchTextBox.Text.ToLower()) ||
                                c.AddressWhere.ToLower().Contains(SearchTextBox.Text.ToLower()))
                            .OrderByDescending(c => c.Date).ToList();
                }
            }
        }

        private void UserMain_OnLoaded(object sender, RoutedEventArgs e)
        {
            _db = new DB();
        }
    }
}