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

namespace barbershop
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            txtLogin.Focus();
        }

        public List<admin> Enter()
        {
            hair_salonEntities entities = new hair_salonEntities();
            var a = entities.admin.ToList();
            return a;
        }

        private void btnEnter_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var str = Enter();
                foreach (var st in str)
                {
                        if (st.login == txtLogin.Text & st.password == "000")
                        {
                            MessageBox.Show("Авторизация успешна");
                            MessageBox.Show("Смените пароль");
                            WinAdminPassword winpass = new WinAdminPassword();
                            winpass.ShowDialog();
                            WinAdmin win = new WinAdmin();
                            win.Show();
                            this.Hide();
                            break;
                        }
                        else if (st.login == txtLogin.Text & st.password == passwordBox.Password)
                        {
                            MessageBox.Show("Авторизация успешна");
                            WinAdmin win1 = new WinAdmin();
                            win1.Show();
                            this.Hide();
                            break;
                        }
                        else if (st.login != txtLogin.Text || st.password != passwordBox.Password)
                        {
                            MessageBox.Show("Неправильный логин или пароль");
                            break;
                        }

                }
            }
            catch
            {
                MessageBox.Show("Нет подключения к БД");
            }
        }

        private void txtLogin_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtLogin.Text == "" || passwordBox.Password == "")
            {
                btnEnter.IsEnabled = false;
            }
            else
            {
                btnEnter.IsEnabled = true;
            }
        }

        private void passwordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (txtLogin.Text == "" || passwordBox.Password == "")
            {
                btnEnter.IsEnabled = false;
            }
            else
            {
                btnEnter.IsEnabled = true;
            }
        }
    }
}
