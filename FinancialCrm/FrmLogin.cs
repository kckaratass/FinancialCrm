using FinancialCrm.Models;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace FinancialCrm
{
    public partial class FrmLogin : Form
    {
        FinancialCrmDbEntities1 db = new FinancialCrmDbEntities1();

        public FrmLogin()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string userName = txtUserName.Text.Trim().ToLower();
            string password = txtPassword.Text.Trim();

            // Kullanıcı kontrolü
            var user = db.Users.FirstOrDefault(x => x.UserName.ToLower() == userName && x.Password == password);

            if (user != null)
            {
                MessageBox.Show("Giriş Başarılı!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // FrmDashboard ekranına geçiş
                FrmBanks bank = new FrmBanks();
                bank.Show();
                this.Hide(); // Login formu gizlenir
            }
            else
            {
                MessageBox.Show("Kullanıcı adı veya şifre hatalı!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
