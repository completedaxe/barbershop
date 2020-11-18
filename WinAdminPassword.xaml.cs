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
using System.Windows.Shapes;
using System.Data.Entity;

namespace barbershop
{
    /// <summary>
    /// Логика взаимодействия для WinAdminPassword.xaml
    /// </summary>
    public partial class WinAdminPassword : Window
    {
        admin modelA = new admin();
        public WinAdminPassword()
        {
            InitializeComponent();
            txtPasswordUpdate.Focus();
        }

        public List<admin> Update()
        {
            hair_salonEntities entities = new hair_salonEntities();
            var a = entities.admin.ToList();
            return a;
        }

        private void btnEnter_Click(object sender, RoutedEventArgs e)
        {
            foreach (var s in Update())
            {
                if (s.password == "000")
                {
                    using (hair_salonEntities db = new hair_salonEntities())
                    {
                        admin a = db.admin.FirstOrDefault();
                        a.password = txtPasswordUpdate.Password;
                        db.SaveChanges();
                    }
                    MessageBox.Show("Пароль успешно изменен");
                    break;
                }

                else
                {
                    MessageBox.Show("Пароль уже был изменен");
                    break;
                }

            }
            this.Close();
        }

        private void txtPasswordUpdate_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (txtPasswordUpdate.Password == "")
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
