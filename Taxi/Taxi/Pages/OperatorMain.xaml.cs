using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
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
    /// Логика взаимодействия для OperatorMain.xaml
    /// </summary>
    public partial class OperatorMain : Page
    {
        public OperatorMain(User operatorr)
        {
            InitializeComponent();
            _operator = operatorr;
            
            RequestFilterComboBox.SelectedIndex = 0;
            TaxiFilterComboBox.SelectedIndex = 0;

            RequestLeastToMost.IsChecked = true;
            TaxiLeastToMost.IsChecked = true;
            
            UpdateRequestGrid();
            UpdateTaxiGrid();
        }

        private User _operator;


        private void UpdateRequestGrid()
        {
            List<Request> requests = DB.entities.Requests;
            // if (RequestFilterComboBox == null || RequestLeastToMost == null) return;

            if (RequestFilterComboBox.SelectedIndex == 0)
            {
                if (RequestLeastToMost.IsChecked == true)
                {
                    RequestDataGrid.ItemsSource = requests.Where(c =>
                            c.AddressFrom.ToLower().Contains(RequestSearchTextBox.Text.ToLower()) ||
                            c.AddressWhere.ToLower().Contains(RequestSearchTextBox.Text.ToLower()))
                        .OrderBy(c => c.Date).ToList();
                }
                else
                {
                    RequestDataGrid.ItemsSource =
                        requests.Where(c =>
                                c.AddressFrom.ToLower().Contains(RequestSearchTextBox.Text.ToLower()) ||
                                c.AddressWhere.ToLower().Contains(RequestSearchTextBox.Text.ToLower()))
                            .OrderByDescending(c => c.Date).ToList();
                }
            }

            else
            {
                if (RequestLeastToMost.IsChecked == true)
                {
                    RequestDataGrid.ItemsSource = requests.Where(c =>
                            (c.AddressFrom.ToLower().Contains(RequestSearchTextBox.Text.ToLower()) ||
                             c.AddressWhere.ToLower().Contains(RequestSearchTextBox.Text.ToLower())) &&
                            c.Operator == _operator)
                        .OrderBy(c => c.Date).ToList();
                }
                else
                {
                    RequestDataGrid.ItemsSource =
                        requests.Where(c =>
                            (c.AddressFrom.ToLower().Contains(RequestSearchTextBox.Text.ToLower()) ||
                             c.AddressWhere.ToLower().Contains(RequestSearchTextBox.Text.ToLower())) &&
                            c.Operator == _operator).OrderByDescending(c => c.Date).ToList();
                }
            }
        }

        private void UpdateTaxiGrid()
        {
            List<Drive> drives = DB.entities.Drives;
            // if (TaxiFilterComboBox == null || TaxiLeastToMost == null) return;

            if (RequestFilterComboBox.SelectedIndex == 0)
            {
                if (TaxiLeastToMost.IsChecked == true)
                {
                    RequestDataGrid.ItemsSource = drives.Where(c =>
                            c.Request.AddressFrom.ToLower().Contains(TaxiSearchTextBox.Text.ToLower()) ||
                            c.Request.AddressWhere.ToLower().Contains(TaxiSearchTextBox.Text.ToLower()))
                        .OrderBy(c => c.Request.Date).ToList();
                }
                else
                {
                    RequestDataGrid.ItemsSource =
                        drives.Where(c =>
                                c.Request.AddressFrom.ToLower().Contains(TaxiSearchTextBox.Text.ToLower()) ||
                                c.Request.AddressWhere.ToLower().Contains(TaxiSearchTextBox.Text.ToLower()))
                            .OrderByDescending(c => c.Request.Date).ToList();
                }
            }

            else
            {
                if (TaxiLeastToMost.IsChecked == true)
                {
                    RequestDataGrid.ItemsSource = drives.Where(c =>
                            (c.Request.AddressFrom.ToLower().Contains(TaxiSearchTextBox.Text.ToLower()) ||
                             c.Request.AddressWhere.ToLower().Contains(TaxiSearchTextBox.Text.ToLower())) &&
                            c.Request.Operator == _operator)
                        .OrderBy(c => c.Request.Date).ToList();
                }
                else
                {
                    RequestDataGrid.ItemsSource =
                        drives.Where(c =>
                            (c.Request.AddressFrom.ToLower().Contains(TaxiSearchTextBox.Text.ToLower()) ||
                             c.Request.AddressWhere.ToLower().Contains(TaxiSearchTextBox.Text.ToLower())) &&
                            c.Request.Operator == _operator).OrderByDescending(c => c.Request.Date).ToList();
                }
            }
        }

        private void DeleteButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (RequestDataGrid.SelectedItem != null)
            {
                var result = MessageBox.Show("Вы точно хотите удалить заказ?", "Сообщение",
                    MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    using (SqlConnection connection = new SqlConnection(DB.entities.connectionString))
                    {
                        connection.Open();
                        string query =
                            $"delete from [Request] where Id = {((Request)RequestDataGrid.SelectedItem).Id}";
                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.ExecuteNonQuery();
                        }

                        connection.Close();
                    }

                    MessageBox.Show("Заявка удалена!");
                    DB.entities.UpdateAll();
                    UpdateRequestGrid();
                }
            }
            else
                MessageBox.Show("Сначала выберите строку в таблице!");
        }

        private void EditButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (RequestDataGrid.SelectedItem != null)
            {
                NavigationService.Navigate(new EditRequest((Request)RequestDataGrid.SelectedItem, _operator));
                DB.entities.UpdateAll();
                UpdateRequestGrid();
            }
            else
                MessageBox.Show("Сначала выберите строку в таблице!");
        }

        private void TaxiSearchTextBox_OnTextChanged(object sender, TextChangedEventArgs e) => UpdateTaxiGrid();

        private void TaxiLeastToMost_OnChecked(object sender, RoutedEventArgs e) => UpdateTaxiGrid();

        private void TaxiMostToLeast_OnChecked(object sender, RoutedEventArgs e) => UpdateTaxiGrid();

        private void TaxiFilterComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e) =>
            UpdateTaxiGrid();

        private void RequestMostToLeast_OnChecked(object sender, RoutedEventArgs e) => UpdateRequestGrid();

        private void RequestSearchTextBox_OnTextChanged(object sender, TextChangedEventArgs e) =>
            UpdateRequestGrid();

        private void RequestFilterComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e) =>
            UpdateRequestGrid();

        private void RequestLeastToMost_OnChecked(object sender, RoutedEventArgs e) => UpdateRequestGrid();
    }
}