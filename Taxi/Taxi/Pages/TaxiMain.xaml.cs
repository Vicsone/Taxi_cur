using System;
using System.Collections.Generic;
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
using Taxi.Models;
using Taxi.Pages;

namespace Taxi
{
    /// <summary>
    /// Логика взаимодействия для TaxiMain.xaml
    /// </summary>
    public partial class TaxiMain : Page
    {
        private TaxiDB _taxiDb = new TaxiDB();

        public TaxiMain(User driver)
        {
            InitializeComponent();
            _driver = driver;
            LeastToMost.IsChecked = true;
        }

        private User _driver;

        private void MostToLeast_OnChecked(object sender, RoutedEventArgs e)
        {
            UpdateGrid();
        }

        private void LeastToMost_OnChecked(object sender, RoutedEventArgs e)
        {
            UpdateGrid();
        }

        private void EditDriveButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (RequestDataGrid.SelectedItem != null)
            {
                NavigationService.Navigate(new EditDrive((Drive)RequestDataGrid.SelectedItem));
            }
            else
                MessageBox.Show("Сначала выберите строку в таблице!");
        }

        private void SearchTextBox_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateGrid();
        }

        private void UpdateGrid()
        {
            List<Drive> drives = _taxiDb.Drives.Where(c => c.Driver.Id == _driver.Id).ToList();
            if (LeastToMost == null) return;

            if (drives.Count != 0)
            {
                if (LeastToMost.IsChecked == true)
                {
                    RequestDataGrid.ItemsSource = drives.Where(c =>
                            c.Request.AddressFrom.ToLower().Contains(SearchTextBox.Text.ToLower()) ||
                            c.Request.AddressWhere.ToLower().Contains(SearchTextBox.Text.ToLower()))
                        .OrderBy(c => c.Request.Date).ToList();
                }

                else
                {
                    RequestDataGrid.ItemsSource =
                        drives.Where(c =>
                                c.Request.AddressFrom.ToLower().Contains(SearchTextBox.Text.ToLower()) ||
                                c.Request.AddressWhere.ToLower().Contains(SearchTextBox.Text.ToLower()))
                            .OrderByDescending(c => c.Request.Date).ToList();
                }
            }
        }

        private void TaxiMain_OnLoaded(object sender, RoutedEventArgs e)
        {
            _taxiDb = new TaxiDB();
        }
    }
}