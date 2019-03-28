using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pekpan
{
    public partial class PaletDesigner : Form
    {
        PekpanDal _pekpanDal = new PekpanDal();
        string _kullaniciAdi = string.Empty;
        int _designID = 0;

        public PaletDesigner()
        {
            InitializeComponent();
        }

        public PaletDesigner(PekpanDal pekpanDal, string kullaniciAdi, int designID = 0)
        {
            InitializeComponent();
            _pekpanDal = pekpanDal;
            _kullaniciAdi = kullaniciAdi;
            _designID = designID;
            using (SqlDataReader rdr = _pekpanDal.GetDataReader("select MarkaAciklama from TARA_Markalar"))
            {
                if (rdr != null)
                {
                    while (rdr.Read())
                    {
                        DataGridViewRow row = new DataGridViewRow();
                        row.CreateCells(gv);
                        row.Cells[0].Value = rdr["MarkaAciklama"].ToString();
                        gv.Rows.Add(row);
                    }
                }
            }
        }

        public PaletDesigner(PekpanDal pekpanDal, string kullaniciAdi, int designID, List<string> designList)
        {
            InitializeComponent();
            _pekpanDal = pekpanDal;
            _kullaniciAdi = kullaniciAdi;
            _designID = designID;
            txAd.Text = designList[0];
            udbx.Value = Convert.ToInt32(designList[1]);
            udby.Value = Convert.ToInt32(designList[2]);
            udox.Value = Convert.ToInt32(designList[3]);
            udoy.Value = Convert.ToInt32(designList[4]);
            udz.Value = Convert.ToInt32(designList[5]);
            udTork.Value = Convert.ToInt32(designList[6]);
            udgen1.Value = Convert.ToInt32(designList[7]);
            udgen2.Value = Convert.ToInt32(designList[8]);
            udboy1.Value = Convert.ToInt32(designList[9]);
            udboy2.Value = Convert.ToInt32(designList[10]);

            using (SqlDataReader rdr = _pekpanDal.GetDataReader("select MarkaAciklama from TARA_Markalar"))
            {
                if (rdr != null)
                {
                    while (rdr.Read())
                    {
                        DataGridViewRow row = new DataGridViewRow();
                        row.CreateCells(gv);
                        row.Cells[0].Value = rdr["MarkaAciklama"].ToString();
                        gv.Rows.Add(row);
                    }
                }
            }

            for (int i = 1; i <= 12; i++)
            {
                List<string> cells = _pekpanDal.GetDataReaderListColumn2("select PanelNo,ProdNo,Rot from TARA_PalCells where DesignID=" + _designID + " and PanelNo=" + i + "", 3);
                if (cells.Count != 0)
                {
                    CheckBox cb = ProdCheckBox(i);
                    cb.CheckState = CheckState.Checked;
                    NumericUpDown ud = ProdUpDown(i);
                    ud.Value = Convert.ToInt32(cells[1]);
                    Button bRot = ProdButton(i);
                    if (cells[2] == "90")
                        btnPalet_Click(bRot, EventArgs.Empty);
                    else if (cells[2] == "180")
                    {
                        for (int j = 0; j < 2; j++)
                        {
                            btnPalet_Click(bRot, EventArgs.Empty);
                        }
                    }
                    else if (cells[2] == "270")
                    {
                        for (int j = 0; j < 3; j++)
                        {
                            btnPalet_Click(bRot, EventArgs.Empty);
                        }
                    }
                }
            }
            List<string> markaAciklamas = _pekpanDal.GetDataReaderList("SELECT MarkaAciklama FROM TARA_PalMarka WHERE DesignID = '" + _designID + "'");
            for (int i = 0; i < markaAciklamas.Count; i++)
            {
                gvEkleForUpdate(Convert.ToInt32(_pekpanDal.ExecuteScalarStr("SELECT Y.Satir FROM ( SELECT ROW_NUMBER() OVER(ORDER BY MarkaKod) AS Satir,* FROM TARA_Markalar ) Y WHERE Y.MarkaAciklama= '" + markaAciklamas[i] + "'")));
            }
        }

        private void cbxPalet_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkBox_active = sender as CheckBox;
            string s = checkBox_active.Name.Remove(0, 8);
            int itg = Convert.ToInt32(s);

            Panel pnl = ProdPanel(itg);
            if (pnl != null) pnl.BackColor = checkBox_active.Checked ? Color.White : SystemColors.ButtonFace;

            NumericUpDown nudPalet_active = ProdUpDown(itg);
            if (nudPalet_active != null)
            {
                if (checkBox_active.Checked)
                {
                    if (ActiveCount() == 1) nudPalet_active.Value = 1;
                    else nudPalet_active.Value = MaxValue() + 1;
                }
                nudPalet_active.Enabled = checkBox_active.Checked;
            }

            Button bt = ProdButton(itg);
            if (bt != null) bt.Enabled = checkBox_active.Checked;

            Panel barPanel = BarPanel(itg);
            if (barPanel != null)
                barPanel.Visible = checkBox_active.Checked;
        }

        private Panel ProdPanel(int i)
        {
            if (i == 1) return pnlPalet1;
            else if (i == 2) return pnlPalet2;
            else if (i == 3) return pnlPalet3;
            else if (i == 4) return pnlPalet4;
            else if (i == 5) return pnlPalet5;
            else if (i == 6) return pnlPalet6;
            else if (i == 7) return pnlPalet7;
            else if (i == 8) return pnlPalet8;
            else if (i == 9) return pnlPalet9;
            else if (i == 10) return pnlPalet10;
            else if (i == 11) return pnlPalet11;
            else if (i == 12) return pnlPalet12;
            else return null;
        }

        private NumericUpDown ProdUpDown(int i)
        {
            if (i == 1) return nudPalet1;
            else if (i == 2) return nudPalet2;
            else if (i == 3) return nudPalet3;
            else if (i == 4) return nudPalet4;
            else if (i == 5) return nudPalet5;
            else if (i == 6) return nudPalet6;
            else if (i == 7) return nudPalet7;
            else if (i == 8) return nudPalet8;
            else if (i == 9) return nudPalet9;
            else if (i == 10) return nudPalet10;
            else if (i == 11) return nudPalet11;
            else if (i == 12) return nudPalet12;
            else return null;
        }

        private Button ProdButton(int i)
        {
            if (i == 1) return btnPalet1;
            else if (i == 2) return btnPalet2;
            else if (i == 3) return btnPalet3;
            else if (i == 4) return btnPalet4;
            else if (i == 5) return btnPalet5;
            else if (i == 6) return btnPalet6;
            else if (i == 7) return btnPalet7;
            else if (i == 8) return btnPalet8;
            else if (i == 9) return btnPalet9;
            else if (i == 10) return btnPalet10;
            else if (i == 11) return btnPalet11;
            else if (i == 12) return btnPalet12;
            else return null;
        }

        private CheckBox ProdCheckBox(int i)
        {
            if (i == 1) return cbxPalet1;
            else if (i == 2) return cbxPalet2;
            else if (i == 3) return cbxPalet3;
            else if (i == 4) return cbxPalet4;
            else if (i == 5) return cbxPalet5;
            else if (i == 6) return cbxPalet6;
            else if (i == 7) return cbxPalet7;
            else if (i == 8) return cbxPalet8;
            else if (i == 9) return cbxPalet9;
            else if (i == 10) return cbxPalet10;
            else if (i == 11) return cbxPalet11;
            else if (i == 12) return cbxPalet12;
            else return null;
        }

        private Panel BarPanel(int rNo)
        {
            if (rNo == 1) return pnlBarkod1;
            else if (rNo == 2) return pnlBarkod2;
            else if (rNo == 3) return pnlBarkod3;
            else if (rNo == 4) return pnlBarkod4;
            else if (rNo == 5) return pnlBarkod5;
            else if (rNo == 6) return pnlBarkod6;
            else if (rNo == 7) return pnlBarkod7;
            else if (rNo == 8) return pnlBarkod8;
            else if (rNo == 9) return pnlBarkod9;
            else if (rNo == 10) return pnlBarkod10;
            else if (rNo == 11) return pnlBarkod11;
            else if (rNo == 12) return pnlBarkod12;
            else return null;
        }

        private int katProdCount()
        {
            int adt = 0;
            for (int i = 1; i <= 12; i++)
            {
                CheckBox cb = ProdCheckBox(i);
                if (cb != null)
                {
                    if (cb.Checked) adt++;
                }
            }
            return adt;
        }

        private int ActiveCount()
        {
            int cnt = 0;

            if (cbxPalet1.Checked) cnt++;
            if (cbxPalet2.Checked) cnt++;
            if (cbxPalet3.Checked) cnt++;
            if (cbxPalet4.Checked) cnt++;
            if (cbxPalet5.Checked) cnt++;
            if (cbxPalet6.Checked) cnt++;
            if (cbxPalet7.Checked) cnt++;
            if (cbxPalet8.Checked) cnt++;
            if (cbxPalet9.Checked) cnt++;
            if (cbxPalet10.Checked) cnt++;
            if (cbxPalet11.Checked) cnt++;
            if (cbxPalet12.Checked) cnt++;
            return cnt;
        }
        private int MaxValue()
        {
            int maxv = 0;
            if (nudPalet1.Enabled && (nudPalet1.Value > maxv)) maxv = (int)nudPalet1.Value;
            if (nudPalet2.Enabled && (nudPalet2.Value > maxv)) maxv = (int)nudPalet2.Value;
            if (nudPalet3.Enabled && (nudPalet3.Value > maxv)) maxv = (int)nudPalet3.Value;
            if (nudPalet4.Enabled && (nudPalet4.Value > maxv)) maxv = (int)nudPalet4.Value;
            if (nudPalet5.Enabled && (nudPalet5.Value > maxv)) maxv = (int)nudPalet5.Value;
            if (nudPalet6.Enabled && (nudPalet6.Value > maxv)) maxv = (int)nudPalet6.Value;
            if (nudPalet7.Enabled && (nudPalet7.Value > maxv)) maxv = (int)nudPalet7.Value;
            if (nudPalet8.Enabled && (nudPalet8.Value > maxv)) maxv = (int)nudPalet8.Value;
            if (nudPalet9.Enabled && (nudPalet9.Value > maxv)) maxv = (int)nudPalet9.Value;
            if (nudPalet10.Enabled && (nudPalet10.Value > maxv)) maxv = (int)nudPalet10.Value;
            if (nudPalet11.Enabled && (nudPalet11.Value > maxv)) maxv = (int)nudPalet11.Value;
            if (nudPalet12.Enabled && (nudPalet12.Value > maxv)) maxv = (int)nudPalet12.Value;

            return maxv;
        }

        private void btnPalet_Click(object sender, EventArgs e)
        {
            Button rb = (Button)sender;
            Image img = null;

            int rNo = Convert.ToInt32(rb.Name.Remove(0, 8));

            if (rb.Text.Equals("0"))
            {
                rb.Text = "90";
                img = xb90.Image;
            }
            else if (rb.Text.Equals("90"))
            {
                rb.Text = "180";
                img = xb180.Image;
            }
            else if (rb.Text.Equals("180"))
            {
                rb.Text = "270";
                img = xb270.Image;
            }
            else
            {
                rb.Text = "0";
                img = xb0.Image;
            }

            Panel barPanel = BarPanel(rNo);
            if (barPanel != null)
                barPanel.BackgroundImage = img;
        }

        private void saveDesign()
        {
            _pekpanDal.ExecuteNonQuerySilent("Insert Into TARA_PalDesign(RecTime, Aciklama, XBaslangic, YBaslangic, XOffset, YOffset, PaletZ, BoyMin, BoyMax, GenislikMin, GenislikMax, Tork, Tasarlayan) Values (GETDATE(),'" + txAd.Text + "', " + udbx.Value + ", " + udby.Value + ", " + udox.Value + ", " + udoy.Value + ", " + udz.Value + ", " + udboy1.Value + ", " + udboy2.Value + ", " + udgen1.Value + ", " + udgen2.Value + ", " + udTork.Value + ", '" + _kullaniciAdi + "')"); //" + katProdCount() + ",
            int designID = Convert.ToInt32(_pekpanDal.ExecuteScalarStr("select ID from TARA_PalDesign where Aciklama ='" + txAd.Text + "'"));
            for (int i = 0; i < Convert.ToInt32(_pekpanDal.ExecuteScalarStr("select count(*) from TARA_Markalar")); i++)
            {
                if (gv.Rows[i].Cells[1].Value != null)
                    _pekpanDal.ExecuteNonQuerySilent("Insert Into TARA_PalMarka (DesignID,MarkaAciklama) values (" + designID + ",'" + gv.Rows[i].Cells[1].Value + "')");
            }
            for (int i = 1; i <= 12; i++)
            {
                CheckBox cb = ProdCheckBox(i);
                if (cb != null)
                {
                    if (cb.Checked)
                    {
                        NumericUpDown ud = ProdUpDown(i);
                        int pno = (int)ud.Value;

                        Button bRot = ProdButton(i);
                        int rot = Convert.ToInt32(bRot.Text);

                        int row = i % 4;
                        int col = 1;
                        if ((i >= 5) && (i < 9)) col = 2;
                        else if ((i >= 9) && (i < 13)) col = 3;
                        else if (i >= 13) col = 4;

                        _pekpanDal.ExecuteNonQuerySilent("Insert Into TARA_PalCells(DesignID, PanelNo, RowNo, ColNo, ProdNo, Rot) Values (" + designID + ", " + i + ", " + row + ", " + col + ", " + pno + ", " + rot + ")");
                    }
                }
                DialogResult = DialogResult.OK;
            }
        }

        private void updateDesign()
        {
            _pekpanDal.ExecuteNonQuerySilent("UPDATE TARA_PalDesign SET Aciklama = '" + txAd.Text + "', XBaslangic = '" + udbx.Value + "',YBaslangic = '" + udby.Value + "',XOffset = '" + udox.Value + "',YOffset = '" + udoy.Value + "',PaletZ = '" + udz.Value + "',GenislikMin = '" + udgen1.Value + "',GenislikMax = '" + udgen2.Value + "',BoyMin = '" + udboy1.Value + "', BoyMax = '" + udboy2.Value + "',Tork = '" + udTork.Value + "', Degistiren = '" + _kullaniciAdi + "',DegistirmeTarihi = GETDATE() WHERE ID = '" + _designID + "'");
            if (_designID != 0)
            {
                _pekpanDal.ExecuteNonQuerySilent("DELETE TARA_PalCells WHERE DesignID = '" + _designID + "'");
                _pekpanDal.ExecuteNonQuerySilent("DELETE TARA_PalMarka WHERE DesignID = " + _designID + "");
            }
            for (int i = 0; i < Convert.ToInt32(_pekpanDal.ExecuteScalarStr("select count(*) from TARA_Markalar")); i++)
            {
                if (gv.Rows[i].Cells[1].Value != null)
                    _pekpanDal.ExecuteNonQuerySilent("Insert Into TARA_PalMarka (DesignID,MarkaAciklama) values (" + _designID + ",'" + gv.Rows[i].Cells[1].Value + "')");
            }
            for (int i = 1; i <= 12; i++)
            {
                CheckBox cb = ProdCheckBox(i);
                if (cb != null)
                {
                    if (cb.Checked)
                    {
                        NumericUpDown ud = ProdUpDown(i);
                        int pno = (int)ud.Value;

                        Button bRot = ProdButton(i);
                        int rot = Convert.ToInt32(bRot.Text);

                        int row = i % 4;
                        int col = 1;
                        if ((i >= 5) && (i < 9)) col = 2;
                        else if ((i >= 9) && (i < 13)) col = 3;
                        else if (i >= 13) col = 4;

                        _pekpanDal.ExecuteNonQuerySilent("Insert Into TARA_PalCells(DesignID, PanelNo, RowNo, ColNo, ProdNo, Rot) Values (" + _designID + ", " + i + ", " + row + ", " + col + ", " + pno + ", " + rot + ")");
                    }
                }

                DialogResult = DialogResult.OK;
            }
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            List<string> dizaynAdi = _pekpanDal.GetDataReaderList("select Aciklama from TARA_PalDesign order by Aciklama");
            if (_designID == 0)
            {
                foreach (string s in dizaynAdi)
                {
                    if (txAd.Text == s)
                    {
                        MessageBox.Show("Lütfen başka bir isim veriniz", "Bu isimde bir palet tasarımı zaten var", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                saveDesign();
            }
            else
            {
                updateDesign();
            }
        }

        private void btnKapat_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void gvEkle_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = new DataGridViewRow();
            row.CreateCells(gv);
            row.Cells[1].Value = gv.CurrentRow.Cells[0].Value;
            gv.Rows.Insert(gv.CurrentRow.Index, row);
            gv.Rows.Remove(gv.CurrentRow);
        }

        private void gvEkleForUpdate(int index)
        {
            DataGridViewRow row = new DataGridViewRow();
            row.CreateCells(gv);
            row.Cells[1].Value = gv.Rows[index - 1].Cells[0].Value;
            gv.Rows.Insert(index - 1, row);
            gv.Rows.Remove(gv.Rows[index]);
        }

        private void gvCikar_Click(object sender, EventArgs e)
        {
            if (gv.CurrentRow.Cells[1].Value != null)
            {
                DataGridViewRow row = new DataGridViewRow();
                row.CreateCells(gv);
                row.Cells[0].Value = gv.CurrentRow.Cells[1].Value;
                gv.Rows.Insert(gv.CurrentRow.Index, row);
                gv.Rows.Remove(gv.CurrentRow);
            }
        }
    }
}
