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
using UchebnayaPraktika.Pages;
using UchebnayaPraktika.Components;

namespace UchebnayaPraktika.Pages
{
    /// <summary>
    /// Логика взаимодействия для OrdersPage.xaml
    /// </summary>
    public partial class OrdersPage : Page
    {
        public OrdersPage()
        {
            InitializeComponent();
            if (Navigation.role == 1)
            {
                OrdersList.ItemsSource = DbConnection.db.Order.ToList().Where(x => x.IsDelete == false && x.User1.Id == Navigation.ARUser.Id);
            }
            else
            {
                OrdersList.ItemsSource = DbConnection.db.Order.ToList().Where(x => x.IsDelete == false);
            }
        }

        private void EditOrder_Click(object sender, RoutedEventArgs e)
        {
            var selOrder = (sender as Button).DataContext as Order;
            if (selOrder.Status.Name != "Новый" && Navigation.role == 1)
            {
                MessageBox.Show("Заказ уже на стадии обработки", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                if (Navigation.role == 3)
                {
                    selOrder.User = Navigation.ARUser;
                    Navigation.NextPage(new Nav("Редактирование заказа", new AddEditOrderPage(selOrder)));
                }
                else if (Navigation.role == 1)
                {
                    Navigation.NextPage(new Nav("Редактирование заказа", new AddEditOrderPage(selOrder)));
                }
            }

        }

        private void DeleteOrder_Click(object sender, RoutedEventArgs e)
        {
            var selOrder = (sender as Button).DataContext as Order;
            if (selOrder.Status.Name != "Новый" && Navigation.role == 1)
            {
                MessageBox.Show("Заказ уже на стадии обработки", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                selOrder.IsDelete = true;
                DbConnection.db.SaveChanges();
                OrdersList.ItemsSource = DbConnection.db.Order.ToList().Where(x => x.IsDelete == false && x.User1.Id == Navigation.ARUser.Id);
            }
        }

        private void AddOrderBTN_Click(object sender, RoutedEventArgs e)
        {
            if (Navigation.role == 1)
            {
                Navigation.NextPage(new Nav("Оформить заказа", new AddEditOrderPage(new Order()
                {
                    OrderDate = DateTime.Now,
                    StatusId = 1,
                    User1 = Navigation.ARUser
                }))); ;
            }
        }
    }
}
