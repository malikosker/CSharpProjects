using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PekpanBoyahane
{
    public partial class Form1 : Form
    {
        PekpanDal _pekpanDal = new PekpanDal();
        string _kullaniciAdi = string.Empty;
        int _id = 0;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _pekpanDal.ConnectToDB("server IP", "userName", "pass", "dataBase");
            dgvRefresh();
        }

        private void btnGiris_Click(object sender, EventArgs e)
        {
            Login login = new Login(_pekpanDal);
            if (login.ShowDialog() == DialogResult.OK)
            {
                _kullaniciAdi = login._kullaniciAdi;
                lblKullaniciAdi.Text = "Kullanıcı Adı : " + _pekpanDal.ExecuteScalarStr("SELECT DBO.TRK(ISIM) FROM TBLMRPISCI WHERE SICILNO = '" + login._kullaniciAdi + "'");
            }
        }

        private void btnCikis_Click(object sender, EventArgs e)
        {
            _kullaniciAdi = string.Empty;
            lblKullaniciAdi.Text = "Kullanıcı Adı : ";
        }

        public void dgvRefresh()
        {
            gvIsEmri.Rows.Clear();
            using (SqlDataReader rdr = _pekpanDal.GetDataReader("SELECT U.ID ID,RIGHT(R.PLAN_ID,4) PLANNO,U.ISEMRINO,R.SIPARIS_NO SIPNO,R.EXPORTREFNO PROFNO,DBO.TRK(R.CARI_ISIM) CARIISMI,R.STOK_KODU,R.STOK_ADI,R.ISEMRI_MIKTARI MIKTAR,U.MIKTAR GIDEN FROM TARA_UretimBoya U LEFT JOIN VW_DZNISEMRILIST R (NOLOCK) ON (R.ISEMRI_NO=U.ISEMRINO) WHERE U.STATU <> 2 order by U.TARIH DESC"))
            {
                if (rdr != null)
                {
                    while (rdr.Read())
                    {
                        DataGridViewRow row = new DataGridViewRow();
                        row.CreateCells(gvIsEmri);
                        row.Cells[0].Value = rdr["PLANNO"].ToString();
                        row.Cells[1].Value = rdr["ISEMRINO"].ToString();
                        row.Cells[2].Value = rdr["SIPNO"].ToString();
                        row.Cells[3].Value = rdr["PROFNO"].ToString();
                        row.Cells[4].Value = rdr["CARIISMI"].ToString();
                        row.Cells[5].Value = rdr["STOK_KODU"].ToString();
                        row.Cells[6].Value = rdr["STOK_ADI"].ToString();
                        row.Cells[7].Value = rdr["MIKTAR"].ToString();
                        row.Cells[8].Value = rdr["GIDEN"].ToString();
                        row.Cells[11].Value = rdr["ID"].ToString();
                        gvIsEmri.Rows.Add(row);
                    }
                }
            }
        }

        private void btnGonder_Click(object sender, EventArgs e)
        {
            if (_kullaniciAdi != string.Empty)
            {
                if (txbIsEmriNo.Text.Length == 15)
                {
                    if (nudMiktar.Value != 0)
                    {
                        if ("1" == _pekpanDal.ExecuteScalarStr("select count(*) from tblIsEmri where isEmriNo = '" + txbIsEmriNo.Text + "' and right (stok_kodu,2) = 'KB'"))
                        {
                            _pekpanDal.ExecuteNonQuerySilent("INSERT INTO TARA_uretimBoya (TARIH, VARDIYA, OPTKODU, ISEMRINO, MIKTAR,OKUNANURUN,STATU) VALUES (GETDATE(),(SELECT CASE WHEN CONVERT(VARCHAR(5),GETDATE(),108) BETWEEN '08:00:00' AND '16:00:00' THEN '08-16' WHEN CONVERT(VARCHAR(5),GETDATE(),108) BETWEEN '00:00:00' AND '08:00:00' THEN '24-08' WHEN CONVERT(VARCHAR(5),GETDATE(),108) BETWEEN '16:00:00' AND '00:00:00' THEN '16-24' ELSE '0' END), '" + _kullaniciAdi + "', '" + txbIsEmriNo.Text + "', '" + nudMiktar.Value.ToString() + "',0,1)");
                            dgvRefresh();
                        }
                        else
                            MessageBox.Show("Girilen iş emri hatalıdır !!!", "İş Emri Hatalı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        MessageBox.Show("Miktarı sıfır giremezsiniz !!!", "Miktar Hatalı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Yanlış iş emri numarası girdiniz !!!", "İş Emri Hatalı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Kullanıcı girişi yapmadınız !!!", "Kullanıcı Girişi Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void gvIsEmri_DoubleClick(object sender, EventArgs e)
        {
            txbIsEmriNoG.Text = gvIsEmri.CurrentRow.Cells[1].Value.ToString();
            _id = Convert.ToInt32(gvIsEmri.CurrentRow.Cells[11].Value);
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            if (_kullaniciAdi != string.Empty)
            {
                if (txbIsEmriNoG.Text.Length == 15)
                {
                    if (nudMiktarG.Value != 0)
                    {
                        if (_id != 0)
                        {
                            DialogResult dialog = MessageBox.Show("Güncelleme yapılacak emin misiniz ?", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (dialog == DialogResult.Yes)
                            {
                                _pekpanDal.ExecuteNonQuerySilent("UPDATE U SET Miktar = " + nudMiktarG.Value + " FROM TARA_UretimBoya U WHERE ID = " + _id + "");
                                dgvRefresh();
                                _id = 0;
                            }
                        }
                        else
                            MessageBox.Show("Lütfen işlem yapmak istediğiniz iş emrinin üstüne gelip çift tıklayın !!!", "Kullanım Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        MessageBox.Show("Miktarı sıfır giremezsiniz !!!", "Miktar Hatalı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Yanlış iş emri numarası girdiniz !!!", "İş Emri Hatalı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Kullanıcı girişi yapmadınız !!!", "Kullanıcı Girişi Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            if (_kullaniciAdi != string.Empty)
            {
                if (txbIsEmriNoG.Text.Length == 15)
                {
                    if (_id != 0)
                    {
                        if ("0" != _pekpanDal.ExecuteScalarStr("SELECT COUNT(*) FROM TARA_UretimBoya U LEFT JOIN TARA_HucreDurum D (NOLOCK) ON (SELECT REFISEMRINO FROM TBLISEMRI WHERE ISEMRINO =U.IsemriNo) = D.isEmri WHERE IsemriNo =  '" + txbIsEmriNoG.Text + "' AND ID = " + _id + " AND D.designID IS NULL AND OkunanUrun = 0 AND U.Statu = 1"))
                        {
                            DialogResult dialog = MessageBox.Show("Kayıt silinecektir! Emin misniz?", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (dialog==DialogResult.Yes)
                            {
                                _pekpanDal.ExecuteNonQuerySilent("DELETE FROM TARA_UretimBoya WHERE ID  = " + _id + "");
                                dgvRefresh();
                            }
                        }
                        else
                            MessageBox.Show("Palet açılmış yada Paletleme işlemi yapılmış kayıt silinemez !!!\n Robot Operatörüne danışınız.", "Silinemez Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        _id = 0;
                    }
                    else
                        MessageBox.Show("Lütfen işlem yapmak istediğiniz iş emrinin üstüne gelip çift tıklayın !!!", "Kullanım Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show("Yanlış iş emri numarası girdiniz !!!", "İş Emri Hatalı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Kullanıcı girişi yapmadınız !!!", "Kullanıcı Girişi Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}