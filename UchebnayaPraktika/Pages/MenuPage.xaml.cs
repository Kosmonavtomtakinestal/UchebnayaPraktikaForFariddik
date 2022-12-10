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
    /// Логика взаимодействия для MenuPage.xaml
    /// </summary>
    public partial class MenuPage : Page
    {
        public MenuPage()
        {
            InitializeComponent();
            if (Navigation.role == 1)
            {
                GetProdBTN.Visibility = Visibility.Collapsed;
            }
            else if (Navigation.role == 3)
            {
                GetProdBTN.Visibility = Visibility.Collapsed;
            }
            else if (Navigation.role == 4)
            {
                OrdersBTN.Visibility = Visibility.Collapsed;
            }
        }

        private void GetProdBTN_Click(object sender, RoutedEventArgs e)
        {
            Navigation.NextPage(new Nav("Поступление продуктов", new ProductReceiptPage()));
        }

        private void OrdersBTN_Click_1(object sender, RoutedEventArgs e)
        {
            Navigation.NextPage(new Nav("Наши заказы", new OrdersPage()));
        }

        private void ProductsBTN_Click(object sender, RoutedEventArgs e)
        {
            Navigation.NextPage(new Nav("Наши продукты", new ProductListPage()));
        }
    }
}
