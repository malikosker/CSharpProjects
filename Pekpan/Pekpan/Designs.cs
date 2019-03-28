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

namespace Pekpan
{
    public partial class Designs : Form
    {
        public string _kullaniciAdi = string.Empty;
        PekpanDal _pekpanDal = new PekpanDal();

        public Designs(PekpanDal pekpanDal, string kullaniciAdi)
        {
            InitializeComponent();
            _pekpanDal = pekpanDal;
            _kullaniciAdi = kullaniciAdi;
        }

        private void Designs_Load(object sender, EventArgs e)
        {
            listDesigns();
        }
        private void listDesigns()
        {
            grDesigns.Rows.Clear();
            string sql = "SELECT *, (SELECT COUNT(*) FROM TARA_PalMarka WHERE DesignID = TARA_PalDesign.ID) MarkaCounter FROM TARA_PalDesign";
            using (SqlDataReader rdr = _pekpanDal.GetDataReader(sql))
            {
                if (rdr != null)
                {
                    while (rdr.Read())
                    {
                        DataGridViewRow row = new DataGridViewRow();
                        row.CreateCells(grDesigns);
                        row.Cells[0].Value = rdr["ID"].ToString();
                        row.Cells[1].Value = rdr["Aciklama"].ToString();
                        row.Cells[2].Value = rdr["XBaslangic"].ToString();
                        row.Cells[3].Value = rdr["YBaslangic"].ToString();
                        row.Cells[4].Value = rdr["XOffset"].ToString();
                        row.Cells[5].Value = rdr["YOffset"].ToString();
                        row.Cells[6].Value = rdr["PaletZ"].ToString();
                        row.Cells[7].Value = rdr["GenislikMin"].ToString();
                        row.Cells[8].Value = rdr["GenislikMax"].ToString();
                        row.Cells[9].Value = rdr["BoyMin"].ToString();
                        row.Cells[10].Value = rdr["BoyMax"].ToString();
                        row.Cells[11].Value = rdr["Tork"].ToString();
                        row.Cells[12].Value = rdr["MarkaCounter"]; // 12. Cell aşağıda setlenecek
                        row.Cells[13].Value = rdr["Tasarlayan"].ToString();
                        row.Cells[14].Value = rdr["RecTime"].ToString();
                        row.Cells[15].Value = rdr["Degistiren"].ToString();
                        row.Cells[16].Value = rdr["DegistirmeTarihi"].ToString();
                        grDesigns.Rows.Add(row);
                    }
                }
            }
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            PaletDesigner pd = new PaletDesigner(_pekpanDal, _kullaniciAdi);
            if (pd.ShowDialog() == DialogResult.OK)
                listDesigns();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            DialogResult dialog = MessageBox.Show("Seçili dizayn silinecektir!\n Emin misniz?", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog==DialogResult.Yes)
            {
                int designKimlik = Convert.ToInt32(_pekpanDal.ExecuteScalarStr("select ID from TARA_PalDesign where Aciklama= '" + grDesigns.CurrentRow.Cells[1].Value.ToString() + "'"));
                _pekpanDal.ExecuteNonQuerySilent("DELETE TARA_PalCells where DesignID =" + designKimlik);
                _pekpanDal.ExecuteNonQuerySilent("DELETE TARA_PalDesign where Aciklama='" + grDesigns.CurrentRow.Cells[1].Value.ToString() + "'");
                _pekpanDal.ExecuteNonQuerySilent("DELETE TARA_PalMarka where DesignID =" + designKimlik);
                listDesigns();
            }
        }

        private void btnDegistir_Click(object sender, EventArgs e)
        {
            PaletDesigner pd = new PaletDesigner(_pekpanDal, _kullaniciAdi, Convert.ToInt32(_pekpanDal.ExecuteScalarStr("select ID from TARA_PalDesign where Aciklama= '" + grDesigns.CurrentRow.Cells[1].Value.ToString() + "'")), _pekpanDal.GetDataReaderListColumn("select Aciklama, XBaslangic, YBaslangic, XOffset, YOffset, PaletZ, Tork, GenislikMin, GenislikMax, BoyMin, BoyMax from TARA_PalDesign where ID=" + Convert.ToInt32(_pekpanDal.ExecuteScalarStr("select ID from TARA_PalDesign where Aciklama= '" + grDesigns.CurrentRow.Cells[1].Value.ToString() + "'")) + "", 11));
            if (pd.ShowDialog() == DialogResult.OK)
                listDesigns();
        }
    }
}
