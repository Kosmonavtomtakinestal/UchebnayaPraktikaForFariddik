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
using UchebnayaPraktika;
using System.Windows.Threading;

namespace UchebnayaPraktika.Pages
{
    /// <summary>
    /// Логика взаимодействия для AuthPage.xaml
    /// </summary>
    public partial class AuthPage : Page
    {
        int countAuth = Properties.Settings.Default.CountAuth;
        DispatcherTimer timer = new DispatcherTimer();
        public AuthPage()
        {
            InitializeComponent();
            LoginTB.Text = Properties.Settings.Default.Login;
            PasswordTB.Password = Properties.Settings.Default.Password;
            countAuth = Properties.Settings.Default.CountAuth;
        }

        private void LogBTN_Click(object sender, RoutedEventArgs e)
        {
            if (countAuth < 2)
            {
                if (LoginTB.Text != "" || PasswordTB.Password != "")
                {
                    var inUser = DbConnection.db.User.ToList().Find(x => x.Login == LoginTB.Text.Trim() && x.Password == PasswordTB.Password.Trim());
                    if (inUser != null)
                    {
                        if (SaveDataCB.IsChecked == true)
                        {
                            Properties.Settings.Default.Login = LoginTB.Text.Trim();
                            Properties.Settings.Default.Password = PasswordTB.Password.Trim();
                            Properties.Settings.Default.Save();
                        }
                        else
                        {
                            Properties.Settings.Default.Login = "";
                            Properties.Settings.Default.Password = "";
                            Properties.Settings.Default.Save();
                        }
                        Navigation.ARUser = inUser;
                        Navigation.role = (int)inUser.RoleId;
                        Navigation.isAuth = true;
                        Properties.Settings.Default.CountAuth = 0;
                        Navigation.NextPage(new Nav("Меню", new MenuPage()));
                    }
                    else
                    {
                        countAuth += 1;
                        Properties.Settings.Default.CountAuth = countAuth;
                        MessageBox.Show("Такого пользователя нет", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Не заполнены поля", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else
            {
                MessageBox.Show("Вы ввели 3 раза неправильный пароль\nВход заблокировани на 1 минуту", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                countAuth = 0;
                Properties.Settings.Default.CountAuth = 0;
                LogBTN.IsEnabled = false;
                RegBTN.IsEnabled = false;
                timer.Interval = new TimeSpan(0, 1, 0);
                timer.Tick += new EventHandler(isVisibleBTN);
                timer.Start();
            }
        }

        private void isVisibleBTN(object sender, EventArgs e)
        {
            LogBTN.IsEnabled = true;
            RegBTN.IsEnabled = true;
            timer.Stop();
        }

        private void RegBTN_Click(object sender, RoutedEventArgs e)
        {
            Navigation.NextPage(new Nav("Регистрация", new RegistrPage()));
        }
    }
}
