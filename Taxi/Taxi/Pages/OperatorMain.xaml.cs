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
            List<string> filter = new List<string>() { "Все", "Мои" };
            FilterComboBox.ItemsSource = filter;
            FilterComboBox.SelectedIndex = 0;
        }

        private User _operator;
        private DB _db = new DB();

        private void LeastToMost_OnChecked(object sender, RoutedEventArgs e)
        {
            UpdateGrid();
        }

        private void MostToLeast_OnChecked(object sender, RoutedEventArgs e)
        {
            UpdateGrid();
        }

        private void SearchTextBox_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateGrid();
        }

        private void UpdateGrid()
        {
            List<Request> requests = _db.Requests;
            if (MostToLeast == null) return;

            if (FilterComboBox.SelectedIndex == 0 && LeastToMost.IsChecked == true)
            {
                RequestDataGrid.ItemsSource = requests.Where(c =>
                        c.AddressFrom.ToLower().Contains(SearchTextBox.Text.ToLower()) || c.AddressWhere.ToLower().Contains(SearchTextBox.Text.ToLower()))
                    .OrderBy(c => c.Date).ToList();
            }

            if (FilterComboBox.SelectedIndex == 0 && MostToLeast.IsChecked == true)
            {
                RequestDataGrid.ItemsSource =
                    requests.Where(c =>
                            c.AddressFrom.ToLower().Contains(SearchTextBox.Text.ToLower()) ||
                            c.AddressWhere.ToLower().Contains(SearchTextBox.Text.ToLower()))
                        .OrderByDescending(c => c.Date).ToList();
            }

            if (FilterComboBox.SelectedIndex != 0 && LeastToMost.IsChecked == true)
            {
                RequestDataGrid.ItemsSource = requests.Where(c =>
                        (c.AddressFrom.ToLower().Contains(SearchTextBox.Text.ToLower()) || c.AddressWhere.ToLower().Contains(SearchTextBox.Text.ToLower())) &&
                        c.Operator == _operator)
                    .OrderBy(c => c.Date).ToList();
            }

            if (FilterComboBox.SelectedIndex != 0 && MostToLeast.IsChecked == true)
            {
                RequestDataGrid.ItemsSource =
                    requests.Where(c =>
                        (c.AddressFrom.ToLower().Contains(SearchTextBox.Text.ToLower()) || c.AddressWhere.ToLower().Contains(SearchTextBox.Text.ToLower())) &&
                        c.Operator == _operator).OrderByDescending(c => c.Date).ToList();
            }
        }

        private void DeleteButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (RequestDataGrid.SelectedItem != null)
            {
                var result = MessageBox.Show("Вы точно хотите удалить заказ?", "Сообщение", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    using (SqlConnection connection = new SqlConnection(_db.connectionString))
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
                    _db = new DB();
                    UpdateGrid();
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
            }
            else
                MessageBox.Show("Сначала выберите строку в таблице!");
        }

        private void OperatorMain_OnLoaded(object sender, RoutedEventArgs e)
        {
            _db = new DB();
        }

        private void FilterComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateGrid();
        }
    }
}