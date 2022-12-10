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
    /// Логика взаимодействия для AddEditProductReceiptPage.xaml
    /// </summary>
    public partial class AddEditProductReceiptPage : Page
    {
        static ReceiptOfProduct receiptOfProduct;
        public static AddEditProductReceiptPage addEditProductReceiptPage;
        public AddEditProductReceiptPage(ReceiptOfProduct _receiptOfProduct)
        {
            InitializeComponent();
            receiptOfProduct = _receiptOfProduct;
            DataContext = receiptOfProduct;
            StatusCb.ItemsSource = DbConnection.db.StatusReceipt.ToList();
            StatusCb.DisplayMemberPath = "Name";
            UserCb.ItemsSource = DbConnection.db.Supplier.ToList();
            UserCb.DisplayMemberPath = "Name";
            if (receiptOfProduct.SupplierId != null)
            {
                UserCb.IsEnabled = false;

            }
            addEditProductReceiptPage = this;
            NameTB.IsEnabled = false;
            DateAddTB.IsEnabled = false;
            TotalCosrProdTB.IsEnabled = false;
        }
        public static void Update()
        {
            addEditProductReceiptPage.ProductsList.ItemsSource = DbConnection.db.ProductSupplier.ToList().Where(x => x.ReceiptOfProductId == receiptOfProduct.Id);
        }
        private void AddProductBTN_Click(object sender, RoutedEventArgs e)
        {
            Navigation.NextPage(new Nav("Продукты", new ProductSupplierPage(receiptOfProduct.Id)));
        }

        private void SeaveBtn_Click(object sender, RoutedEventArgs e)
        {
            if (NameTB.Text.Trim() != "" && DateAddTB.Text != "" && StatusCb.SelectedItem != null)
            {
                if (receiptOfProduct.Id == 0)
                {
                    receiptOfProduct.IsDelete = false;
                    DbConnection.db.ReceiptOfProduct.Add(receiptOfProduct);
                }
                DbConnection.db.SaveChanges();
                MessageBox.Show("Все успешно");
                Navigation.NextPage(new Nav("Поступление продуктов", new ProductReceiptPage()));
            }
            else
            {
                MessageBox.Show("Заполните все поля", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void DelProductBTN_Click(object sender, RoutedEventArgs e)
        {
            var selProduct = ProductsList.SelectedItem as ProductSupplier;
            if (selProduct != null)
            {
                var prod = DbConnection.db.Product.ToList().Find(x => x.Id == selProduct.ProductId);
                prod.Count += selProduct.Count;
                DbConnection.db.ProductSupplier.Remove(selProduct);
                DbConnection.db.SaveChanges();
                ProductsList.ItemsSource = DbConnection.db.ProductSupplier.ToList().Where(x => x.ReceiptOfProductId == receiptOfProduct.Id);
            }
            else
            {
                MessageBox.Show("Выберите продукт", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
