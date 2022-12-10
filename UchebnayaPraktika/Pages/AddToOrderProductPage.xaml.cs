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
    /// Логика взаимодействия для AddToOrderProductPage.xaml
    /// </summary>
    public partial class AddToOrderProductPage : Page
    {
        int IDOrder;
        public AddToOrderProductPage(int _IDOrder)
        {
            InitializeComponent();
            IDOrder = _IDOrder;
            ProductList.ItemsSource = DbConnection.db.Product.ToList().Where(x => x.IsDelete == false);
            GeneralCount.Text = DbConnection.db.Product.Where(x => x.IsDelete == false).Count().ToString();
        }
        private void Refresh()
        {

            IEnumerable<Product> filter = DbConnection.db.Product;

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
        private void txt_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0))
            {
                e.Handled = true;
            }

        }

        private void AddProductBTN_Click(object sender, RoutedEventArgs e)
        {
            if (IDOrder == 0)
            {
                MessageBox.Show("Сначала необходимо создать заказ без товаров", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                var selProduct = ProductList.SelectedItem as Product;
                if ((selProduct.Count - int.Parse(CountTB.Text)) >= 0)
                {
                    selProduct.Count -= int.Parse(CountTB.Text);
                    ProductOrder productOrder = new ProductOrder()
                    {
                        OrderId = IDOrder,
                        ProductId = selProduct.Id,
                        Count = int.Parse(CountTB.Text)
                    };
                    DbConnection.db.ProductOrder.Add(productOrder);
                
                    DbConnection.db.SaveChanges();
                    int cnt = Navigation.navs.Count;
                    Navigation.navs[cnt - 2].Page.UpdateLayout();
                    Navigation.navs[cnt - 1].Page.UpdateLayout();
                    Navigation.BackPage();
                }
                else
                {
                    MessageBox.Show("Не имеется данное кол-во продуктов");
                }
                AddEditOrderPage.Update();
            }
        }
    }
}
