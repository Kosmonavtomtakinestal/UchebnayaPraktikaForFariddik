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
using UchebnayaPraktika.Components;
using UchebnayaPraktika.Pages;

namespace UchebnayaPraktika.Pages
{
    /// <summary>
    /// Логика взаимодействия для ProductListPage.xaml
    /// </summary>
    public partial class ProductListPage : Page
    {
        IEnumerable<Product> filter = DbConnection.db.Product;
        public ProductListPage()
        {
            InitializeComponent();
            ProductList.ItemsSource = DbConnection.db.Product.ToList().Where(x => x.IsDelete == false);
            GeneralCount.Text = DbConnection.db.Product.ToList().Where(x => x.IsDelete == false).Count().ToString();
            FoundCount.Text = filter.Where(x => x.IsDelete == false).Count().ToString() + "из";
            if (Navigation.role == 1)
            {
                AddProductBTN.Visibility = Visibility.Hidden;
            }
        }
        private void Refresh()
        {
            filter = DbConnection.db.Product;

            if (InThisMounthCB.IsChecked == true)
            {
                filter = filter.Where(x => x.DateAdd.Value.Month == DateTime.Now.Month && x.DateAdd.Value.Year == DateTime.Now.Year);
            }

            if (Sort.SelectedIndex != 0)
            {
                if (Sort.SelectedIndex == 1)
                {
                    filter = filter.OrderBy(x => x.Name);
                }
                else if (Sort.SelectedIndex == 2)
                {
                    filter = filter.OrderBy(x => x.DateAdd);
                }
            }

            if (Search.Text.Length > 0)
            {
                filter = filter.Where(x => x.Name.ToLower().StartsWith(Search.Text.ToLower()) || x.Discription.ToLower().StartsWith(Search.Text.ToLower()));
            }

            if (Filter.SelectedIndex != 0)
            {
                if (Filter.SelectedIndex == 1)
                {
                    filter = filter.Where(x => x.Unit.Name == "кг");
                }
                if (Filter.SelectedIndex == 2)
                {
                    filter = filter.Where(x => x.Unit.Name == "л");
                }
                if (Filter.SelectedIndex == 3)
                {
                    filter = filter.Where(x => x.Unit.Name == "м");
                }
                if (Filter.SelectedIndex == 4)
                {
                    filter = filter.Where(x => x.Unit.Name == "шт");
                }
            }

            ProductList.ItemsSource = filter.ToList().Where(x => x.IsDelete == false);
            FoundCount.Text = filter.Where(x => x.IsDelete == false).Count().ToString() + "из";
        }

        private void Sort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Refresh();
        }

        private void Filter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Refresh();
        }

        private void Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            Refresh();
        }

        private void InThisMounthCB_Checked(object sender, RoutedEventArgs e)
        {
            Refresh();
        }

        private void EditProductBTN_Click(object sender, RoutedEventArgs e)
        {
            if (Navigation.role != 1)
            {
                var selProduct = (sender as Button).DataContext as Product;
                Navigation.NextPage(new Nav("Редактировать продукта", new AddEditProductPage(selProduct)));
            }
            else if (Navigation.role == 1)
            {
                MessageBox.Show("Отказано в доступе", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        private void AddProductBTN_Click(object sender, RoutedEventArgs e)
        {
            if (Navigation.role != 1)
            {
                Navigation.NextPage(new Nav("Добавление продукта", new AddEditProductPage(new Product()
                {
                    DateAdd = DateTime.Now
                }
                )));
            }
            else if (Navigation.role == 1)
            {
                MessageBox.Show("Отказано в доступе", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void DeleteProductBTN_Click(object sender, RoutedEventArgs e)
        {
            if (Navigation.role != 1)
            {
                var selProduct = (sender as Button).DataContext as Product;
                selProduct.IsDelete = true;
                ProductList.ItemsSource = DbConnection.db.Product.ToList().Where(x => x.IsDelete == false);
                GeneralCount.Text = DbConnection.db.Product.ToList().Where(x => x.IsDelete == false).Count().ToString();
            }
            else if (Navigation.role == 1)
            {
                MessageBox.Show("Отказано в доступе", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

    }
} 
