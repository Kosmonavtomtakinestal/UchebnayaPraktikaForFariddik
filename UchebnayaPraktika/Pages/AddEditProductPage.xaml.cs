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
using System.IO;
using Microsoft.Win32;

namespace UchebnayaPraktika.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddEditProductPage.xaml
    /// </summary>
    public partial class AddEditProductPage : Page
    {
        Product product;
        public AddEditProductPage(Product _product)
        {
            InitializeComponent();
            product = _product;
            DataContext = product;
            UnitCb.ItemsSource = DbConnection.db.Unit.ToList();
            UnitCb.DisplayMemberPath = "Name";
            CountryCB.ItemsSource = DbConnection.db.Country.ToList();
            CountryCB.DisplayMemberPath = "Name";
        }

        private void txt_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0))
            {
                e.Handled = true;
            }

        }

        private void SeaveBtn_Click(object sender, RoutedEventArgs e)
        {
            if (NameTB.Text.Trim() != "" && DiscriptionTB.Text.Trim() != "" && DateAddTB.Text != "" && UnitCb.SelectedItem != null && CostTB.Text.Trim() != "" && PhotoProductIMG.Source != null)
            {
                if (product.Id == 0)
                {
                    product.IsDelete = false;
                    DbConnection.db.Product.Add(product);
                }
                DbConnection.db.SaveChanges();
                MessageBox.Show("Все успешно");
                Navigation.NextPage(new Nav("Наши продукты", new ProductListPage()));
            }
            else
            {
                MessageBox.Show("Заполните все поля", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void AddImageBtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif"
            };
            if (openFileDialog.ShowDialog().GetValueOrDefault())
            {
                product.Photo = File.ReadAllBytes(openFileDialog.FileName);
                PhotoProductIMG.Source = new BitmapImage(new Uri(openFileDialog.FileName));
            }
        }

        private void DelCountryBTN_Click(object sender, RoutedEventArgs e)
        {
            var selCountry = CountryList.SelectedItem as ProductCountry;
            if (selCountry == null)
            {
                MessageBox.Show("Не выбрана страна", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                var countryProductItem = DbConnection.db.ProductCountry.ToList().Find(x => x.CountryId == selCountry.CountryId && x.ProductId == selCountry.ProductId);
                if (countryProductItem != null)
                {
                    DbConnection.db.ProductCountry.Remove(countryProductItem);
                    DbConnection.db.SaveChanges();
                    CountryList.ItemsSource = DbConnection.db.ProductCountry.ToList().Where(x => x.ProductId == product.Id);
                }
                else
                {
                    MessageBox.Show("Ошибка", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }

        private void AddCountryBTN_Click(object sender, RoutedEventArgs e)
        {
            if (product.Id != 0)
            {
                var selCountry = CountryCB.SelectedItem as Country;
                if (selCountry != null)
                {
                    var selCountry1 = DbConnection.db.ProductCountry.ToList().Find(x => x.CountryId == selCountry.Id && x.ProductId == product.Id);
                    if(selCountry1 != null && selCountry.Id == selCountry1.CountryId)
                    {
                        MessageBox.Show("Данная страна уже есть", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                    {
                        ProductCountry productCountry = new ProductCountry()
                        {
                            ProductId = product.Id,
                            CountryId = selCountry.Id
                        };
                        product.ProductCountry.Add(productCountry);
                        DbConnection.db.SaveChanges();
                        CountryList.ItemsSource = DbConnection.db.ProductCountry.ToList().Where(x => x.ProductId == product.Id);
                    }
                }
                else
                {
                    MessageBox.Show("Не выбрана страна", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else
            {
                MessageBox.Show("Сначала создайте продукт, не прикрепляя стран", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
