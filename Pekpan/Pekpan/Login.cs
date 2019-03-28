using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Pekpan
{
    public partial class Login : Form
    {
        PekpanDal _pekpanDal = new PekpanDal();
        public string _kullaniciAdi = string.Empty;

        public Login(PekpanDal pekpanDal)
        {
            InitializeComponent();
            _pekpanDal = pekpanDal;
        }

        private void Login_Load(object sender, EventArgs e)
        {
            listLogins();
            cbxUsers.SelectedIndex = 0;
            _kullaniciAdi = cbxUsers.Text;
        }

        private void listLogins()
        {
            List<string> logins = _pekpanDal.GetDataReaderList("Select KullaniciAdi From TARA_Kullanicilar Order By KullaniciAdi");
            cbxUsers.Items.Clear();
            foreach (string s in logins)
                cbxUsers.Items.Add(s);
        }

        private void btnDegistir_Click(object sender, EventArgs e)
        {
            txbEskiKullanici.Text = cbxUsers.SelectedItem.ToString();
            pnlEski.Visible = true;
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            pnlYeni.Visible = true;
        }

        private void btnYeniTamam_Click(object sender, EventArgs e)
        {
            if (txbYeniKullaniciAdi.Text != "" && txbYeniSifre.Text != "" && txbYeniSifreTekrar.Text != "" && txbYeniSifre.Text == txbYeniSifreTekrar.Text)
            {
                _pekpanDal.ExecuteNonQuerySilent("insert into TARA_Kullanicilar (KullaniciAdi,Sifre,Mode) values('" + txbYeniKullaniciAdi.Text + "','" + txbYeniSifre.Text + "',2)");
                listLogins();
                pnlYeni.Visible = false;
            }
            else
            {
                MessageBox.Show("Lütfen kullanıcı adınızı ve şifrenizi doğru yazdığınızdan emin olun");
            }
        }

        private void btnYeniVazgec_Click(object sender, EventArgs e)
        {
            pnlYeni.Visible = false;
        }

        private void btnEskiTamam_Click(object sender, EventArgs e)
        {
            if (_pekpanDal.ExecuteScalarStr("select Sifre from TARA_Kullanicilar where KullaniciAdi = '" + txbEskiKullanici.Text + "'") == txbEskiEskiSifre.Text && txbEskiYeniSifre.Text == txbEskiYeniSifreTekrar.Text)
            {
                _pekpanDal.ExecuteNonQuerySilent("update TARA_Kullanicilar set Sifre=" + txbEskiYeniSifre.Text + " where KullaniciAdi='" + txbEskiKullanici.Text + "'");
            }
            pnlEski.Visible = false;
        }

        private void btnEskiVazgec_Click(object sender, EventArgs e)
        {
            pnlEski.Visible = false;
        }

        private void btnGiris_Click(object sender, EventArgs e)
        {
            if (_pekpanDal.ExecuteScalarStr("select Sifre from TARA_Kullanicilar where KullaniciAdi='" + cbxUsers.Text + "'") == txbSifre.Text)
            {
                this.DialogResult = DialogResult.OK;
            }
            else
                MessageBox.Show("Şifre hatalı");
        }

        private void btnVazgec_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cbxUsers_SelectedIndexChanged(object sender, EventArgs e)
        {
            _kullaniciAdi = cbxUsers.Text;
        }
    }
}
