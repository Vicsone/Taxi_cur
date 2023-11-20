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
using Taxi.Models;

namespace Taxi
{
    /// <summary>
    /// Логика взаимодействия для UserMain.xaml
    /// </summary>
    public partial class UserMain : Page
    {
        public UserMain(User client)
        {
            InitializeComponent();
            _client = client;
            RequestUpdateGrid();
            TaxiUpdateGrid();
            RequestLeastToMost.IsChecked = true;
            TaxiLeastToMost.IsChecked = true;
        }

        private User _client;

        private void AddRequestButton_OnClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Add_Order(_client));
        }

        private void RequestUpdateGrid()
        {
            List<Request> requests = DB.entities.Requests.Where(c => c.ClientId == _client.Id).ToList();

            if (requests.Count != 0)
            {
                if (RequestMostToLeast.IsChecked == true)
                {
                    RequestDataGrid.ItemsSource =
                        requests.Where(c =>
                                c.AddressFrom.ToLower().Contains(RequestSearchTextBox.Text.ToLower()) ||
                                c.AddressWhere.ToLower().Contains(RequestSearchTextBox.Text.ToLower()))
                            .OrderByDescending(c => c.Date).ToList();
                }
                else
                {
                    RequestDataGrid.ItemsSource = requests.Where(c =>
                            c.AddressFrom.ToLower().Contains(RequestSearchTextBox.Text.ToLower()) ||
                            c.AddressWhere.ToLower().Contains(RequestSearchTextBox.Text.ToLower()))
                        .OrderBy(c => c.Date).ToList();
                }
            }
        }

        private void TaxiUpdateGrid()
        {
            List<Drive> drives = DB.entities.Drives.Where(c => c.Request.ClientId == _client.Id).ToList();

            if (drives.Count != 0)
            {
                if (TaxiMostToLeast.IsChecked == true)
                {
                    TaxiDataGrid.ItemsSource =
                        drives.Where(c =>
                                c.Request.AddressFrom.ToLower().Contains(TaxiSearchTextBox.Text.ToLower()) ||
                                c.Request.AddressWhere.ToLower().Contains(TaxiSearchTextBox.Text.ToLower()))
                            .OrderByDescending(c => c.Request.Date).ToList();
                }
                else
                {
                    TaxiDataGrid.ItemsSource = drives.Where(c =>
                            c.Request.AddressFrom.ToLower().Contains(TaxiSearchTextBox.Text.ToLower()) ||
                            c.Request.AddressWhere.ToLower().Contains(TaxiSearchTextBox.Text.ToLower()))
                        .OrderBy(c => c.Request.Date).ToList();
                }
            }
        }

        private void TaxiLeastToMost_OnChecked(object sender, RoutedEventArgs e) => TaxiUpdateGrid();

        private void TaxiMostToLeast_OnChecked(object sender, RoutedEventArgs e) => TaxiUpdateGrid();

        private void RequestMostToLeast_OnChecked(object sender, RoutedEventArgs e) => RequestUpdateGrid();

        private void RequestLeastToMost_OnChecked(object sender, RoutedEventArgs e) => RequestUpdateGrid();

        private void RequestSearchTextBox_OnTextChanged(object sender, TextChangedEventArgs e) => RequestUpdateGrid();

        private void TaxiSearchTextBox_OnTextChanged(object sender, TextChangedEventArgs e) => TaxiUpdateGrid();
    }
}