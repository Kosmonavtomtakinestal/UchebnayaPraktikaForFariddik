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
    /// Логика взаимодействия для ProductReceiptPage.xaml
    /// </summary>
    public partial class ProductReceiptPage : Page
    {
        public ProductReceiptPage()
        {
            InitializeComponent();
            ReceiptList.ItemsSource = DbConnection.db.ReceiptOfProduct.ToList().Where(x => x.IsDelete == false);
        }

        private void EditOrder_Click(object sender, RoutedEventArgs e)
        {
            var selReceipt = (sender as Button).DataContext as ReceiptOfProduct;
            if (selReceipt.StatusId == 1)
            {
                MessageBox.Show("Документ уже принят", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                Navigation.NextPage(new Nav("Редактирование документа", new AddEditProductReceiptPage(selReceipt)));
            }
        }

        private void DeleteOrder_Click(object sender, RoutedEventArgs e)
        {
            var selReceipt = (sender as Button).DataContext as ReceiptOfProduct;
            if (selReceipt.StatusId == 1)
            {
                MessageBox.Show("Документ уже принят", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                selReceipt.IsDelete = true;
                DbConnection.db.SaveChanges();
            }
        }

        private void AddOrderBTN_Click(object sender, RoutedEventArgs e)
        {
            Navigation.NextPage(new Nav("Редактирование документа", new AddEditProductReceiptPage(new ReceiptOfProduct() { 
                Date = DateTime.Now
            })));
        }
    }
}
