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
    /// Логика взаимодействия для RegistrPage.xaml
    /// </summary>
    public partial class RegistrPage : Page
    {
        public RegistrPage()
        {
            InitializeComponent();
        }

        private void CloseBTN_Click(object sender, RoutedEventArgs e)
        {
            PasswordInfoSP.Visibility = Visibility.Collapsed;
        }

        private void PasswordTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            PasswordInfoSP.Visibility = Visibility.Visible;
            if (PasswordTB.Text.Length >= 6)
                Min6TB.Foreground = TestTB.Foreground;
            else
                Min6TB.Foreground = TestTB2.Foreground;
            if (PasswordTB.Text.Any(char.IsUpper))
                Min1aTB.Foreground = TestTB.Foreground;
            else
                Min1aTB.Foreground = TestTB2.Foreground;
            if (PasswordTB.Text.Any(char.IsNumber))
                Min1bTB.Foreground = TestTB.Foreground;
            else
                Min1bTB.Foreground = TestTB2.Foreground;
            if (PasswordTB.Text.Contains('!') || PasswordTB.Text.Contains('@') || PasswordTB.Text.Contains('#') ||
                PasswordTB.Text.Contains('$') || PasswordTB.Text.Contains('%') || PasswordTB.Text.Contains('^'))
                Min1cTB.Foreground = TestTB.Foreground;
            else
                Min1cTB.Foreground = TestTB2.Foreground;
        }

        private void AddUserBTN_Click(object sender, RoutedEventArgs e)
        {
            Navigation.ARUser = DbConnection.db.User.ToList().Find(x => x.Login == LoginTB.Text.Trim());
            if (LoginTB.Text.Trim() == "" || PasswordTB.Text.Trim() == "" || PhoneTB.Text.Trim() == "" || EmailTB.Text.Trim() == "" ||
                SurnameTB.Text.Trim() == "" || NameTB.Text.Trim() == "" || PatronymicTB.Text.Trim() == "") 
            {
                MessageBox.Show("Не заполнены поля", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else if (Navigation.ARUser != null)
            {
                MessageBox.Show("Такой Логин уже существует\nПридумайте другой", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else if (Min6TB.Foreground == TestTB2.Foreground || Min1aTB.Foreground == TestTB2.Foreground || Min1bTB.Foreground == TestTB2.Foreground || Min1cTB.Foreground == TestTB2.Foreground)
            {
                MessageBox.Show("Пароль не соответтсвует требованиям", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                User user = new User()
                {
                    Login = LoginTB.Text.Trim(),
                    Password = PasswordTB.Text.Trim(),
                    Phone = PhoneTB.Text.Trim(),
                    Email = EmailTB.Text.Trim(),
                    Surname = SurnameTB.Text.Trim(),
                    Name = NameTB.Text.Trim(),
                    Patronymic = PatronymicTB.Text.Trim(),
                    RoleId = 1
                };
                DbConnection.db.User.Add(user);
                DbConnection.db.SaveChanges();
                MessageBox.Show("Успешно", "Уведомдение", MessageBoxButton.OK, MessageBoxImage.Information);
                Navigation.NextPage(new Nav("Авторизация", new AuthPage()));
            }
        }
    }
}
