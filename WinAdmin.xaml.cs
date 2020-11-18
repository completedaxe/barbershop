using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Data.Entity;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Data.SqlClient;
using System.Data;

namespace barbershop
{
    /// <summary>
    /// Логика взаимодействия для WinAdmin.xaml
    /// </summary>
    public partial class WinAdmin : Window
    {
        
        client model = new client();
        service modelS = new service();
        record modelR = new record();
        master modelM = new master();
        admin modelA = new admin();

        public WinAdmin()
        {
            InitializeComponent();
            Upd();
        }

        public List<admin> Update()
        {
            hair_salonEntities entities = new hair_salonEntities();
            var a = entities.admin.ToList();
            return a;
        }

        public List<client> UpdateClient()
        {
            hair_salonEntities entities = new hair_salonEntities();
            var a = entities.client.ToList();
            return a;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Upd()
        {
            hair_salonEntities db = new hair_salonEntities();

            var data = from a in db.client select a;
            dgClient.ItemsSource = data.ToList();

            var dataS = from a in db.service select a;
            dgService.ItemsSource = dataS.ToList();

            var dataCmb = from a in db.service select new { a.name_service };

            cmbService.ItemsSource = dataCmb.ToList();

            //cmbService.Items.Add(dataCmb.ToString());

            //this.cmbService.Items.Add(db.[0].ToString());
        }

        private void btnAddClient_Click(object sender, RoutedEventArgs e)
        {
            model.surname = txtSurname.Text.Trim();
            using (hair_salonEntities db = new hair_salonEntities())
            {
                db.client.Add(model);
                db.SaveChanges();
            }
            MessageBox.Show("Клиент добавлен.");
            txtSurname.Text = null;
            Upd();
        }

        private void dgClient_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (dgClient.SelectedItem != null)
                {
                    long ID = Convert.ToInt64((dgClient.SelectedCells[0].Column.GetCellContent(dgClient.SelectedItem) as TextBlock).Text);
                    model.ID_client = Convert.ToInt32(dgClient.SelectedIndex + 1);
                    using (hair_salonEntities db = new hair_salonEntities())
                    {
                        var queryInfo = from a in db.record
                                        join b in db.service on a.ID_service equals b.ID_service
                                        join c in db.client on a.ID_client equals c.ID_client
                                        join d in db.admin on a.ID_admin equals d.ID_admin
                                        join f in db.master on a.ID_master equals f.ID_master
                                        where c.ID_client == ID
                                        select new { b.name_service, a.datetime };
                        dgInfo.ItemsSource = queryInfo.ToList();
                        model = db.client.Where(x => x.ID_client == model.ID_client).FirstOrDefault();
                        
                        txtSurname.Text = model.surname;
                    }
                }
            }
            catch
            {
                MessageBox.Show("Не удалось выбрать клиента.");
            }
        }

        private void btnUpdateClient_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                model.surname = txtSurname.Text.Trim();
                using (hair_salonEntities db = new hair_salonEntities())
                {
                    db.Entry(model).State = EntityState.Modified;
                    db.SaveChanges();
                }
                MessageBox.Show("Клиент изменен.");
                Upd();
            }
            catch
            {
                MessageBox.Show("Не удалось изменить клиента.");
            }
        }

        private void txtSurname_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtSurname.Text == "")
            {
                btnUpdateClient.IsEnabled = false;
                btnAddClient.IsEnabled = false;
            }
            else
            {
                btnUpdateClient.IsEnabled = true;
                btnAddClient.IsEnabled = true;
            }
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            Upd();
        }

        private void txtSearchOrder_TextChanged(object sender, TextChangedEventArgs e)
        {
            string str = txtSearchOrder.Text;
            if (str == "") Upd();
            else
            {
                using (hair_salonEntities db = new hair_salonEntities())
                {
                    var querySearch = from c in db.client
                                      where c.surname.Contains(str)
                                      select c;

                    dgClient.ItemsSource = querySearch.ToList();
                }
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            MainWindow win = new MainWindow();
            win.Show();
        }

        private void dgService_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (dgService.SelectedItem != null)
                {
                    modelS.ID_service = Convert.ToInt32(dgService.SelectedIndex + 1);
                    using (hair_salonEntities db = new hair_salonEntities())
                    {
                        modelS = db.service.Where(x => x.ID_service == modelS.ID_service).FirstOrDefault();

                        txtNameService.Text = modelS.name_service;
                        txtPriceService.Text = Convert.ToString(modelS.price);
                    }
                }
            }
            catch
            {
                MessageBox.Show("Не удалось выбрать услугу.");
            }
        }

        private void txtNameService_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtNameService.Text == "")
            {
                btnAddService.IsEnabled = false;
                btnUpdateService.IsEnabled = false;
            }
            else
            {
                btnAddService.IsEnabled = true;
                btnUpdateService.IsEnabled = true;
            }
        }

        private void txtPriceService_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtNameService.Text == "")
            {
                btnAddService.IsEnabled = false;
                btnUpdateService.IsEnabled = false;
            }
            else
            {
                btnAddService.IsEnabled = true;
                btnUpdateService.IsEnabled = true;
            }
        }

        private void btnAddService_Click(object sender, RoutedEventArgs e)
        {
            modelS.name_service = txtNameService.Text.Trim();
            modelS.price = Convert.ToDecimal(txtPriceService.Text.Trim());

            using (hair_salonEntities db = new hair_salonEntities())
            {
                db.service.Add(modelS);
                db.SaveChanges();
            }
            MessageBox.Show("Услуга добавлена.");
            txtNameService.Text = null;
            txtPriceService.Text = null;
            Upd();
        }

        private void btnUpdateService_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                modelS.name_service = txtNameService.Text.Trim();
                modelS.price = Convert.ToDecimal(txtPriceService.Text.Trim());
                using (hair_salonEntities db = new hair_salonEntities())
                {
                    db.Entry(modelS).State = EntityState.Modified;
                    db.SaveChanges();
                }
                MessageBox.Show("Услуга изменена.");
                Upd();
            }
            catch
            {
                MessageBox.Show("Не удалось изменить услугу.");
            }
        }

        private void txtSearchService_TextChanged(object sender, TextChangedEventArgs e)
        {
            string str = txtSearchService.Text;
            if (str == "") Upd();
            else
            {
                using (hair_salonEntities db = new hair_salonEntities())
                {
                    var querySearch = from c in db.service
                                      where c.name_service.Contains(str)

                                      select c;

                    dgService.ItemsSource = querySearch.ToList();
                }
            }
        }

        private void cmbService_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dpRecord.Text == "")
            {
                btnAddRecord.IsEnabled = false;
            }
            else
            {
                btnAddRecord.IsEnabled = true;
            }
        }

        private void dpRecord_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dpRecord.Text == "")
            {
                btnAddRecord.IsEnabled = false;
            }
            else
            {
                btnAddRecord.IsEnabled = true;
            }
        }

        private void btnAddRecord_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                modelR.ID_client = Convert.ToInt32(dgClient.SelectedIndex + 1);
                //modelR.ID_master = modelM.ID_master;
                //modelS.ID_service = modelS.ID_service;
                //modelA.ID_admin = modelR.ID_admin;
                modelR.datetime = Convert.ToDateTime(dpRecord.Text);

                using (hair_salonEntities db = new hair_salonEntities())
                {
                    db.record.Add(modelR);
                    db.SaveChanges();
                }
                MessageBox.Show("Посещение добавлено.");
                dpRecord.Text = null;
                cmbService.Text = null;
                Upd();
            }
            catch
            {
                MessageBox.Show("Не удалось добавить посещение.");
            }
        }
    }
}
