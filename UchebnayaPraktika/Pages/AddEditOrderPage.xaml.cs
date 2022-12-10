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
    /// Логика взаимодействия для AddEditOrderPage.xaml
    /// </summary>
    public partial class AddEditOrderPage : Page
    {
        static Order order;
        public static AddEditOrderPage orderPage;
        public AddEditOrderPage(Order _order)
        {
            InitializeComponent();
            order = _order;
            DataContext = order;
            StatusCb.ItemsSource = DbConnection.db.Status.ToList();
            StatusCb.DisplayMemberPath = "Name";
            TotalCosrProdTB.IsEnabled = false;
            orderPage = this;
            if (Navigation.role == 1)
            {
                StatusCb.IsEnabled = false;
            }
            NameTB.IsEnabled = false;
            DateAddTB.IsEnabled = false;
            Update();
        }
        public static void Update()
        {
            
            orderPage.ProductsList.ItemsSource = DbConnection.db.ProductOrder.ToList().Where(x => x.OrderId == order.Id);
        }

        private void AddProductBTN_Click(object sender, RoutedEventArgs e)
        {
            Navigation.NextPage(new Nav("Продукты", new AddToOrderProductPage(order.Id)));
            ProductsList.ItemsSource = DbConnection.db.ProductOrder.ToList().Where(x => x.OrderId == order.Id);
            TotalCosrProdTB.Text = order.GeneralCost.ToString();

        }

        private void SeaveBtn_Click(object sender, RoutedEventArgs e)
        {
            if (NameTB.Text.Trim() != "" && DateAddTB.Text != "")
            {
                if (order.Id == 0)
                {
                    order.IsDelete = false;
                    DbConnection.db.Order.Add(order);
                }
                DbConnection.db.SaveChanges();
                MessageBox.Show("Все успешно");
                Navigation.NextPage(new Nav("Наши заказы", new OrdersPage()));
            }
            else
            {
                MessageBox.Show("Заполните все поля", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void DelProductBTN_Click(object sender, RoutedEventArgs e)
        {
            var selProduct = ProductsList.SelectedItem as ProductOrder;
            if (selProduct != null)
            {
                var prod = DbConnection.db.Product.ToList().Find(x => x.Id == selProduct.ProductId);
                prod.Count += selProduct.Count;
                DbConnection.db.ProductOrder.Remove(selProduct);
                DbConnection.db.SaveChanges();
                ProductsList.ItemsSource = DbConnection.db.ProductOrder.ToList().Where(x => x.OrderId == order.Id);
            }
            else
            {
                MessageBox.Show("Выберите продукт", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
            }

        }


        private void UpdateBTN_Click(object sender, RoutedEventArgs e)
        {
            ProductsList.ItemsSource = DbConnection.db.ProductOrder.ToList().Where(x => x.OrderId == order.Id);
        }
    }
}
