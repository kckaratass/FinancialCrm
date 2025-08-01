using FinancialCrm.Models;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace FinancialCrm
{
    public partial class FrmBanks : Form
    {
        public FrmBanks()
        {
            InitializeComponent();
        }
        FinancialCrmDbEntities1 db = new FinancialCrmDbEntities1();
        private void FrmBanks_Load(object sender, EventArgs e)
        {
            // Banka bakiyeleri
            var ziraatBankBalance = db.Banks.Where(x => x.BankTitle == "Ziraat Bankası").Select(y => y.BankBalance).FirstOrDefault();
            var vakifBankBalance = db.Banks.Where(x => x.BankTitle == "Vakıfbank").Select(y => y.BankBalance).FirstOrDefault();
            var isBankBalance = db.Banks.Where(x => x.BankTitle == "İş Bankası").Select(y => y.BankBalance).FirstOrDefault();

            lblİsBankBalance.Text = isBankBalance.ToString() + " TL";
            lblZiraatBankBalance.Text = ziraatBankBalance.ToString() + " TL";
            lblVakifBankBalance.Text = vakifBankBalance.ToString() + " TL";

            // Banka işlemleri (son 5 işlem)
            var bankProcesses = db.BankProcesses
                                  .OrderByDescending(x => x.BankProcessId)
                                  .Take(5)
                                  .ToList();

            if (bankProcesses.Count > 0)
                lblBankProcess1.Text = FormatBankProcess(bankProcesses[0]);

            if (bankProcesses.Count > 1)
                lblBankProcess2.Text = FormatBankProcess(bankProcesses[1]);

            if (bankProcesses.Count > 2)
                lblBankProcess3.Text = FormatBankProcess(bankProcesses[2]);

            if (bankProcesses.Count > 3)
                lblBankProcess4.Text = FormatBankProcess(bankProcesses[3]);

            if (bankProcesses.Count > 4)
                lblBankProcess5.Text = FormatBankProcess(bankProcesses[4]);
        }

        // Yardımcı metod – işlemi biçimlendir
        private string FormatBankProcess(BankProcesses process)
        {
            string description = process.Description?.Trim() ?? "Açıklama yok";
            string amount = process.Amount.HasValue ? process.Amount.Value.ToString("N2") : "0,00";
            string date = process.ProcessDate.HasValue ? process.ProcessDate.Value.ToString("dd.MM.yyyy") : "Tarih yok";

            return $"{description} - {amount} TL - {date}";
        }

        private void btnBillForm_Click(object sender, EventArgs e)
        {
            FrmBilling frm = new FrmBilling();
            frm.Show();
            this.Hide();
        }
        
        private void btnDashboard_Click(object sender, EventArgs e)
        {
            FrmDashboard frm = new FrmDashboard();
            frm.Show();
            this.Hide();
        }
    }
}