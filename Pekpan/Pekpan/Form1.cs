using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pekpan
{
    public partial class Form1 : Form
    {
        PekpanDal _pekpanDal = new PekpanDal();
        PekpanDal _pekpanDalBarcode = new PekpanDal();
        PekpanDal _pekpanDalPlcListener = new PekpanDal();
        Plc _plc = new Plc("192.168.0.1", 502);
        System.Timers.Timer _plcListener = new System.Timers.Timer();
        System.Timers.Timer _independent = new System.Timers.Timer();

        bool usingPlc = false;
        bool dataReceivedEnabled = false;
        bool writePlcWithButtons = false;
        string _kullaniciAdi = string.Empty;
        int _density1 = 0;
        int _density2 = 0;
        Hucre[] _hucre = new Hucre[9];

        public struct Hucre
        {
            public string isEmri { get; set; }
            public string designAciklama { get; set; }
            public string urunAciklama { get; set; }
            public string stokKodu { get; set; }
            public int designID { get; set; }
            public int baslangicX { get; set; }
            public int baslangicY { get; set; }
            public int paletZ { get; set; }
            public int offsetX { get; set; }
            public int offsetY { get; set; }
            public int tork { get; set; }//9
            public int katUrunAdedi { get; set; }
            public int urunYukseklik { get; set; }//1
            public int urunGenislik { get; set; }//2
            public int urunBoy { get; set; }//3
            public int rot { get; set; }//8

            public int statu { get; set; }
            public int kat { get; set; }
            public int prodNo { get; set; }
            public int rowNo { get; set; }
            public int colNo { get; set; }
            public int acilisOrder { get; set; }
            public int acilisOrderKM1 { get; set; }
            public int acilisOrderKM2 { get; set; }
            public int hucreNo { get; set; }//7
            public int kapasite { get; set; }//10
            public int urunAdet { get; set; }//11
            public int palUrunAdet { get; set; }
            public int locationX { get; set; }//4
            public int locationY { get; set; }//5
            public int locationZ { get; set; }//6
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _independent.Elapsed += _independent_Elapsed;
            _independent.Enabled = true;
            _plcListener.Elapsed += _plcListener_Elapsed;
            _plc.ConnectingEvent += _plc_ConnectingEvent;
            _plc.HucreDurumEvent += _plc_HucreDurumEvent;
            _plc.WatchProductsEvent += _plc_WatchProductsEvent;
            _plc.SensorInformationsEvent += _plc_SensorInformationsEvent;
            _plc.ProductDroppedEvent1 += _plc_ProductDroppedEvent1;
            _plc.ProductDroppedEvent2 += _plc_ProductDroppedEvent2;
            _plc.DensityEvent2 += _plc_DensityEvent2;
            _plcListener.Enabled = true;
            btnPlcReader.ForeColor = (_plcListener.Enabled == true) ? Color.Lime : Color.Black;
            serialPortBarcodeReader.Open();
            _pekpanDal.ConnectToDB("server IP", "userName", "pass", "dataBase");
            _pekpanDalBarcode.ConnectToDB("server IP", "userName", "pass", "dataBase");
            _pekpanDalPlcListener.ConnectToDB("server IP", "userName", "pass", "dataBase");

            Login login = new Login(_pekpanDal);
            if (login.ShowDialog() == DialogResult.OK)
            {
                _kullaniciAdi = login._kullaniciAdi;
                lbKullaniciAdi.Text = "Operatör : " + _pekpanDal.ExecuteScalarStr("SELECT DBO.TRK(ISIM) FROM TBLMRPISCI WHERE SICILNO = '" + login._kullaniciAdi + "'");
            }

            takeCsvDatas();
            for (int i = 0; i < 9; i++)
            {
                initializeHucres(i);
            }
            if ("0" != _pekpanDal.ExecuteScalarStr("select count(*) from TARA_HucreDurum"))
            {
                DialogResult dialog = MessageBox.Show("Uygulamanın kapatılmadan önceki hali ile devam etmek ister misiniz ?", "Uygulamanın devam eden kayıtları bulundu !", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dialog == DialogResult.Yes)
                {
                    adjustHucres();
                    calculateInitialDensities();
                }
                else
                    TARA_HucreDurumDeleteAll();
            }
            bCom.BackColor = (serialPortBarcodeReader.IsOpen) ? Color.Lime : Color.Red;
            bDB.BackColor = (_pekpanDal.Connected == true && _pekpanDalBarcode.Connected == true && _pekpanDalPlcListener.Connected == true) ? Color.Lime : Color.Red;

            usingPlc = true;
            Thread.Sleep(1000);//-------------------timer "Stopwatch stopwatch = Stopwatch.StartNew();" ile dinlenecek sonra aralık güncellenecek
        again:
            _plc.writeMultiplePlc(14, new int[1] { 2 });
            if (_plc.tryAgain == true)
            {
                _plc.tryAgain = false;
                goto again;
            }
            usingPlc = false;
            _plcListener.Enabled = true;
            _independent.Enabled = true;
        }

        private void initializeHucres(int index)
        {
            _hucre[index].isEmri = "";
            _hucre[index].designAciklama = "";
            _hucre[index].urunAciklama = "";
            _hucre[index].stokKodu = "";
            _hucre[index].designID = 0;
            _hucre[index].baslangicX = 0;
            _hucre[index].baslangicY = 0;
            _hucre[index].paletZ = 0;
            _hucre[index].offsetX = 0;
            _hucre[index].offsetY = 0;
            _hucre[index].tork = 0;
            _hucre[index].katUrunAdedi = 0;
            _hucre[index].urunYukseklik = 0;
            _hucre[index].urunGenislik = 0;
            _hucre[index].urunBoy = 0;
            _hucre[index].rot = 0;

            _hucre[index].statu = 0;
            _hucre[index].kat = 0;
            _hucre[index].prodNo = 1;
            _hucre[index].rowNo = 0;
            _hucre[index].colNo = 0;
            _hucre[index].acilisOrder = 0;
            _hucre[index].acilisOrderKM1 = 0;
            _hucre[index].acilisOrderKM2 = 0;
            _hucre[index].hucreNo = 0;
            _hucre[index].kapasite = 0;
            _hucre[index].urunAdet = 0;
            _hucre[index].palUrunAdet = 0;
            _hucre[index].locationX = 0;
            _hucre[index].locationY = 0;
            _hucre[index].locationZ = 0;
        }

        public void adjustHucres()
        {
            List<Hucre> hucreDatas = _pekpanDal.GetDataReaderListHucre("select * from TARA_HucreDurum order by hucreNo");
            for (int i = 0; i < hucreDatas.Count; i++)
            {
                _hucre[hucreDatas[i].hucreNo] = hucreDatas[i];
            }
            for (int i = 1; i < 9; i++)
            {
                if (_hucre[i].isEmri != "" && _hucre[i].isEmri != "CLOSED")
                {
                    turnLA(i.ToString()).Text = _hucre[i].isEmri;
                    turnLB(i.ToString()).Text = _hucre[i].designAciklama;
                    turnXB(i.ToString()).Maximum = _hucre[i].kapasite;
                    turnXB(i.ToString()).Value = _hucre[i].palUrunAdet;
                    turnPB(i.ToString()).Maximum = _hucre[i].kapasite;
                    turnPB(i.ToString()).Value = _hucre[i].urunAdet;
                    turnLM(i.ToString()).Text = _hucre[i].urunAdet + " / " + _hucre[i].kapasite;
                    turnLC(i.ToString()).Text = _hucre[i].urunAciklama;
                    turnPN(i.ToString()).Visible = true;
                }
            }
        }

        private void serialPortBarcodeReader_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            this.Invoke(new Action(() => dataReceivedEnabled = true));
            Thread.Sleep(15);
            string isEmri = serialPortBarcodeReader.ReadLine();
            serialPortBarcodeReader.DiscardInBuffer();
            if (_pekpanDalBarcode.ExecuteScalarStr("select count(stokKodu) from TARA_HucreDurum where statu=1 and stokKodu = (select stok_kodu from TARA_IsEmri where isEmriNo = '" + isEmri + "') and (hucreNo=1 OR hucreNo=2 OR hucreNo=3 OR hucreNo=4)") != "0" && _pekpanDalBarcode.ExecuteScalarStr("select count(stokKodu) from TARA_HucreDurum where statu=1 and stokKodu = (select stok_kodu from TARA_IsEmri where isEmriNo = '" + isEmri + "') and (hucreNo=5 OR hucreNo=6 OR hucreNo=7 OR hucreNo=8)") != "0")
                karisikMode(isEmri);
            else
                normalMode(isEmri);
            this.Invoke(new Action(() => dataReceivedEnabled = false));
        }

        public void normalMode(string isEmri)
        {
            if (isEmri != null && isEmri != "?")
            {
                string designID = _pekpanDalBarcode.ExecuteScalarStr("SELECT Y.ID FROM TARA_IsEmri I LEFT JOIN (SELECT D.ID, D.GenislikMin, D.GenislikMax, D.BoyMin, D.BoyMax, M.MarkaAciklama, MA.MarkaKod FROM TARA_PalDesign D LEFT JOIN TARA_PalMarka M (NOLOCK) ON M.DesignID = D.ID LEFT JOIN TARA_Markalar MA (NOLOCK) ON MA.MarkaAciklama=M.MarkaAciklama) Y ON (I.BOY BETWEEN Y.BoyMin AND Y.BoyMax) AND (I.GENISLIK BETWEEN Y.GenislikMin AND Y.GenislikMax) AND RIGHT(I.STOK_KODU,7) = Y.MarkaKod WHERE I.ISEMRINO = '" + isEmri + "'");
                if (designID != "")
                {
                    bool rework = true;
                    string designAciklama = _pekpanDalBarcode.ExecuteScalarStr("select aciklama from tara_paldesign where ID=" + designID + "");
                    for (int i = 1; i < 9; i++)
                    {
                        if (turnLB(i.ToString()).Text == designAciklama && _hucre[i].acilisOrder == 0 && _hucre[i].statu == 1 && _hucre[i].stokKodu == _pekpanDalBarcode.ExecuteScalarStr("select stok_kodu from TARA_isEmri where isEmriNo='" + isEmri + "'"))
                        {
                            List<int> datas = _pekpanDalBarcode.GetDataReaderListColumnInt("SELECT Y.XBaslangic, Y.YBaslangic, Y.XOffset, Y.YOffset, Y.Tork, Y.PaletZ, (SELECT COUNT(*) FROM TARA_PalCells WHERE DesignID = Y.ID) KatUrunAdedi, CONVERT(INT, (CASE WHEN I.TIP = '22' OR I.TIP = '20' THEN '115' WHEN I.TIP = '10' OR I.TIP = '11' THEN '75' WHEN I.TIP = '21' THEN '80' WHEN I.TIP = '30' OR I.TIP = '33' THEN '175' END)) TIP, CONVERT(INT, I.GENISLIK) GENISLIK, CONVERT(INT, I.BOY) BOY, Y.RowNo, Y.ColNo, Y.Rot FROM TARA_IsEmri I LEFT JOIN (SELECT M.MarkaAciklama, MA.MarkaKod, D.*, C.RowNo, C.ColNo, C.Rot, C.ProdNo FROM TARA_PalDesign D LEFT JOIN TARA_PalMarka M (NOLOCK) ON M.DesignID = D.ID LEFT JOIN TARA_Markalar MA (NOLOCK) ON MA.MarkaAciklama=M.MarkaAciklama LEFT JOIN TARA_PalCells C (NOLOCK) ON C.DesignID = D.ID) Y ON (I.BOY BETWEEN Y.BoyMin AND Y.BoyMax) AND (I.GENISLIK BETWEEN Y.GenislikMin AND Y.GenislikMax) AND RIGHT(I.STOK_KODU,7) = Y.MarkaKod WHERE I.ISEMRINO = '" + isEmri + "' AND Y.ProdNo = " + _hucre[i].prodNo + "", 13);
                            _hucre[i].baslangicX = datas[0];
                            _hucre[i].baslangicY = datas[1];
                            _hucre[i].offsetX = datas[2];
                            _hucre[i].offsetY = datas[3];
                            _hucre[i].tork = datas[4];
                            _hucre[i].paletZ = datas[5];
                            _hucre[i].katUrunAdedi = datas[6];
                            _hucre[i].urunYukseklik = datas[7];
                            _hucre[i].urunGenislik = datas[8];
                            _hucre[i].urunBoy = datas[9];
                            _hucre[i].rowNo = datas[10];
                            _hucre[i].colNo = datas[11];
                            _hucre[i].rot = datas[12];

                            _hucre[i].prodNo++;
                            _hucre[i].urunAdet++;
                            paletlemeAlgoritmasi(_hucre[i], i);
                            sendProductDatasToPlc(_hucre[i]);
                            _pekpanDalBarcode.ExecuteNonQuerySilent("INSERT INTO TARA_UretimPaket (Tarih, Vardiya, OptKodu, IsemriNo, Miktar) VALUES (GETDATE(),(SELECT CASE WHEN CONVERT(VARCHAR(5),GETDATE(),108) BETWEEN '08:00:00' AND '16:00:00' THEN '08-16' WHEN CONVERT(VARCHAR(5),GETDATE(),108) BETWEEN '00:00:00' AND '08:00:00' THEN '24-08' WHEN CONVERT(VARCHAR(5),GETDATE(),108) BETWEEN '16:00:00' AND '00:00:00' THEN '16-24' ELSE '0' END),'" + _kullaniciAdi + "', '" + isEmri + "', 1)");
                            _pekpanDalBarcode.ExecuteNonQuerySilent("insert into TARA_Analiz values ('" + isEmri + "'," + i + "," + _hucre[i].urunAdet + "," + _hucre[i].locationX + "," + _hucre[i].locationY + "," + _hucre[i].locationZ + ",getdate())");
                            this.Invoke(new Action(() => turnPB(i.ToString()).Value = _hucre[i].urunAdet));
                            this.Invoke(new Action(() => turnLM(i.ToString()).Text = _hucre[i].urunAdet + " / " + _hucre[i].kapasite));
                            if (_hucre[i].prodNo == _hucre[i].katUrunAdedi + 1)
                            {
                                _hucre[i].prodNo = 1;
                                _hucre[i].kat++;
                            }
                            if (_hucre[i].kapasite == _hucre[i].urunAdet)
                            {
                                _hucre[i].statu = 2;
                                this.Invoke(new Action(() => turnLB(i.ToString()).Text = "BİTTİ !!!"));
                                TARA_HucreDurumUpdateBarcode(_hucre[i]);
                                List<int> adjustAcilisOrder = _pekpanDalBarcode.GetDataReaderListInt("select hucreNo from TARA_HucreDurum where designID = " + designID + " and statu = 1 and stokKodu='" + _hucre[i].stokKodu + "' order by acilisOrder");
                                for (int k = 0; k < adjustAcilisOrder.Count; k++)
                                {
                                    _hucre[adjustAcilisOrder[k]].acilisOrder = k;
                                    TARA_HucreDurumUpdateBarcode(_hucre[adjustAcilisOrder[k]]);
                                }
                                if (i < 5)
                                {
                                    adjustAcilisOrder.Clear();
                                    adjustAcilisOrder = _pekpanDalBarcode.GetDataReaderListInt("select hucreNo from TARA_HucreDurum where designID = " + designID + " and statu = 1 and stokKodu='" + _hucre[i].stokKodu + "' and (hucreNo=1 OR hucreNo=2 OR hucreNo=3 OR hucreNo=4) order by acilisOrderKM1");
                                    for (int k = 0; k < adjustAcilisOrder.Count; k++)
                                    {
                                        _hucre[adjustAcilisOrder[k]].acilisOrderKM1 = k;
                                        TARA_HucreDurumUpdateBarcode(_hucre[adjustAcilisOrder[k]]);
                                    }
                                }
                                if (i > 4)
                                {
                                    adjustAcilisOrder.Clear();
                                    adjustAcilisOrder = _pekpanDalBarcode.GetDataReaderListInt("select hucreNo from TARA_HucreDurum where designID = " + designID + " and statu = 1 and stokKodu='" + _hucre[i].stokKodu + "' and (hucreNo=5 OR hucreNo=6 OR hucreNo=7 OR hucreNo=8) order by acilisOrderKM2");
                                    for (int k = 0; k < adjustAcilisOrder.Count; k++)
                                    {
                                        _hucre[adjustAcilisOrder[k]].acilisOrderKM2 = k;
                                        TARA_HucreDurumUpdateBarcode(_hucre[adjustAcilisOrder[k]]);
                                    }
                                }
                            }
                            else
                                TARA_HucreDurumUpdateBarcode(_hucre[i]);
                            isEmriKapat(isEmri);
                            if (i < 5)
                                _density1++;
                            else
                                _density2++;
                            rework = false;
                            break;
                        }
                    }
                    if (rework)
                    {
                        sendProductDatasToPlc(_hucre[0], 1);
                        _pekpanDal.ExecuteNonQuerySilent("INSERT INTO TARA_UretimPaket (Tarih, Vardiya, OptKodu, IsemriNo, Miktar,ReworkMu) VALUES (GETDATE(),(SELECT CASE WHEN CONVERT(VARCHAR(5),GETDATE(),108) BETWEEN '08:00:00' AND '16:00:00' THEN '08-16' WHEN CONVERT(VARCHAR(5),GETDATE(),108) BETWEEN '00:00:00' AND '08:00:00' THEN '24-08' WHEN CONVERT(VARCHAR(5),GETDATE(),108) BETWEEN '16:00:00' AND '00:00:00' THEN '16-24' ELSE '0' END),'" + _kullaniciAdi + "', '" + isEmri + "', 1,'E')");
                    }
                }
                else
                {
                    sendProductDatasToPlc(_hucre[0], 1);
                    _pekpanDal.ExecuteNonQuerySilent("INSERT INTO TARA_UretimPaket (Tarih, Vardiya, OptKodu, IsemriNo, Miktar,ReworkMu) VALUES (GETDATE(),(SELECT CASE WHEN CONVERT(VARCHAR(5),GETDATE(),108) BETWEEN '08:00:00' AND '16:00:00' THEN '08-16' WHEN CONVERT(VARCHAR(5),GETDATE(),108) BETWEEN '00:00:00' AND '08:00:00' THEN '24-08' WHEN CONVERT(VARCHAR(5),GETDATE(),108) BETWEEN '16:00:00' AND '00:00:00' THEN '16-24' ELSE '0' END),'" + _kullaniciAdi + "', '" + isEmri + "', 1,'E')");
                }
            }
            else
                sendProductDatasToPlc(_hucre[0], 1);
        }

        public void karisikMode(string isEmri)
        {
            if (isEmri != null && isEmri != "?")
            {
                string designID = _pekpanDalBarcode.ExecuteScalarStr("SELECT Y.ID FROM TARA_IsEmri I LEFT JOIN (SELECT D.ID, D.GenislikMin, D.GenislikMax, D.BoyMin, D.BoyMax, M.MarkaAciklama, MA.MarkaKod FROM TARA_PalDesign D LEFT JOIN TARA_PalMarka M (NOLOCK) ON M.DesignID = D.ID LEFT JOIN TARA_Markalar MA (NOLOCK) ON MA.MarkaAciklama=M.MarkaAciklama) Y ON (I.BOY BETWEEN Y.BoyMin AND Y.BoyMax) AND (I.GENISLIK BETWEEN Y.GenislikMin AND Y.GenislikMax) AND RIGHT(I.STOK_KODU,7) = Y.MarkaKod WHERE I.ISEMRINO = '" + isEmri + "'");
                if (designID != "")
                {
                    string designAciklama = _pekpanDalBarcode.ExecuteScalarStr("select aciklama from tara_paldesign where ID=" + designID + "");
                    int operatorDensity = 0;
                    this.Invoke(new Action(() => operatorDensity = tbDensity.Value));
                    if (_plc.density2 < operatorDensity)
                    {
                        for (int i = 5; i < 9; i++)
                        {
                            if (turnLB(i.ToString()).Text == designAciklama && _hucre[i].acilisOrderKM2 == 0 && _hucre[i].statu == 1 && _hucre[i].stokKodu == _pekpanDalBarcode.ExecuteScalarStr("select stok_kodu from TARA_isEmri where isEmriNo='" + isEmri + "'"))
                            {
                                List<int> datas = _pekpanDalBarcode.GetDataReaderListColumnInt("SELECT Y.XBaslangic, Y.YBaslangic, Y.XOffset, Y.YOffset, Y.Tork, Y.PaletZ, (SELECT COUNT(*) FROM TARA_PalCells WHERE DesignID = Y.ID) KatUrunAdedi, CONVERT(INT, (CASE WHEN I.TIP = '22' OR I.TIP = '20' THEN '115' WHEN I.TIP = '10' OR I.TIP = '11' THEN '75' WHEN I.TIP = '21' THEN '80' WHEN I.TIP = '30' OR I.TIP = '33' THEN '175' END)) TIP, CONVERT(INT, I.GENISLIK) GENISLIK, CONVERT(INT, I.BOY) BOY, Y.RowNo, Y.ColNo, Y.Rot FROM TARA_IsEmri I LEFT JOIN (SELECT M.MarkaAciklama, MA.MarkaKod, D.*, C.RowNo, C.ColNo, C.Rot, C.ProdNo FROM TARA_PalDesign D LEFT JOIN TARA_PalMarka M (NOLOCK) ON M.DesignID = D.ID LEFT JOIN TARA_Markalar MA (NOLOCK) ON MA.MarkaAciklama=M.MarkaAciklama LEFT JOIN TARA_PalCells C (NOLOCK) ON C.DesignID = D.ID) Y ON (I.BOY BETWEEN Y.BoyMin AND Y.BoyMax) AND (I.GENISLIK BETWEEN Y.GenislikMin AND Y.GenislikMax) AND RIGHT(I.STOK_KODU,7) = Y.MarkaKod WHERE I.ISEMRINO = '" + isEmri + "' AND Y.ProdNo = " + _hucre[i].prodNo + "", 13);
                                _hucre[i].baslangicX = datas[0];
                                _hucre[i].baslangicY = datas[1];
                                _hucre[i].offsetX = datas[2];
                                _hucre[i].offsetY = datas[3];
                                _hucre[i].tork = datas[4];
                                _hucre[i].paletZ = datas[5];
                                _hucre[i].katUrunAdedi = datas[6];
                                _hucre[i].urunYukseklik = datas[7];
                                _hucre[i].urunGenislik = datas[8];
                                _hucre[i].urunBoy = datas[9];
                                _hucre[i].rowNo = datas[10];
                                _hucre[i].colNo = datas[11];
                                _hucre[i].rot = datas[12];

                                _hucre[i].prodNo++;
                                _hucre[i].urunAdet++;
                                paletlemeAlgoritmasi(_hucre[i], i);
                                sendProductDatasToPlc(_hucre[i]);
                                _pekpanDalBarcode.ExecuteNonQuerySilent("INSERT INTO TARA_UretimPaket (Tarih, Vardiya, OptKodu, IsemriNo, Miktar) VALUES (GETDATE(),(SELECT CASE WHEN CONVERT(VARCHAR(5),GETDATE(),108) BETWEEN '08:00:00' AND '16:00:00' THEN '08-16' WHEN CONVERT(VARCHAR(5),GETDATE(),108) BETWEEN '00:00:00' AND '08:00:00' THEN '24-08' WHEN CONVERT(VARCHAR(5),GETDATE(),108) BETWEEN '16:00:00' AND '00:00:00' THEN '16-24' ELSE '0' END),'" + _kullaniciAdi + "', '" + isEmri + "', 1)");
                                _pekpanDalBarcode.ExecuteNonQuerySilent("insert into TARA_Analiz values ('" + isEmri + "'," + i + "," + _hucre[i].urunAdet + "," + _hucre[i].locationX + "," + _hucre[i].locationY + "," + _hucre[i].locationZ + ",getdate())");
                                this.Invoke(new Action(() => turnPB(i.ToString()).Value = _hucre[i].urunAdet));
                                this.Invoke(new Action(() => turnLM(i.ToString()).Text = _hucre[i].urunAdet + " / " + _hucre[i].kapasite));
                                if (_hucre[i].prodNo == _hucre[i].katUrunAdedi + 1)
                                {
                                    _hucre[i].prodNo = 1;
                                    _hucre[i].kat++;
                                }
                                if (_hucre[i].kapasite == _hucre[i].urunAdet)
                                {
                                    _hucre[i].statu = 2;
                                    this.Invoke(new Action(() => turnLB(i.ToString()).Text = "BİTTİ !!!"));
                                    TARA_HucreDurumUpdateBarcode(_hucre[i]);
                                    List<int> adjustAcilisOrder = _pekpanDalBarcode.GetDataReaderListInt("select hucreNo from TARA_HucreDurum where designID = " + designID + " and statu = 1 and stokKodu='" + _hucre[i].stokKodu + "' order by acilisOrder");
                                    for (int k = 0; k < adjustAcilisOrder.Count; k++)
                                    {
                                        _hucre[adjustAcilisOrder[k]].acilisOrder = k;
                                        TARA_HucreDurumUpdateBarcode(_hucre[adjustAcilisOrder[k]]);
                                    }
                                    adjustAcilisOrder.Clear();
                                    adjustAcilisOrder = _pekpanDalBarcode.GetDataReaderListInt("select hucreNo from TARA_HucreDurum where designID = " + designID + " and statu = 1 and stokKodu='" + _hucre[i].stokKodu + "' and (hucreNo=5 OR hucreNo=6 OR hucreNo=7 OR hucreNo=8) order by acilisOrderKM2");
                                    for (int k = 0; k < adjustAcilisOrder.Count; k++)
                                    {
                                        _hucre[adjustAcilisOrder[k]].acilisOrderKM2 = k;
                                        TARA_HucreDurumUpdateBarcode(_hucre[adjustAcilisOrder[k]]);
                                    }
                                }
                                else
                                    TARA_HucreDurumUpdateBarcode(_hucre[i]);
                                isEmriKapat(isEmri);
                                _density2++;
                                break;
                            }
                        }
                    }
                    else if (!(_plc.density2 < operatorDensity))
                    {
                        for (int i = 1; i < 5; i++)
                        {
                            if (turnLB(i.ToString()).Text == designAciklama && _hucre[i].acilisOrderKM1 == 0 && _hucre[i].statu == 1 && _hucre[i].stokKodu == _pekpanDalBarcode.ExecuteScalarStr("select stok_kodu from TARA_isEmri where isEmriNo='" + isEmri + "'"))
                            {
                                List<int> datas = _pekpanDalBarcode.GetDataReaderListColumnInt("SELECT Y.XBaslangic, Y.YBaslangic, Y.XOffset, Y.YOffset, Y.Tork, Y.PaletZ, (SELECT COUNT(*) FROM TARA_PalCells WHERE DesignID = Y.ID) KatUrunAdedi, CONVERT(INT, (CASE WHEN I.TIP = '22' OR I.TIP = '20' THEN '115' WHEN I.TIP = '10' OR I.TIP = '11' THEN '75' WHEN I.TIP = '21' THEN '80' WHEN I.TIP = '30' OR I.TIP = '33' THEN '175' END)) TIP, CONVERT(INT, I.GENISLIK) GENISLIK, CONVERT(INT, I.BOY) BOY, Y.RowNo, Y.ColNo, Y.Rot FROM TARA_IsEmri I LEFT JOIN (SELECT M.MarkaAciklama, MA.MarkaKod, D.*, C.RowNo, C.ColNo, C.Rot, C.ProdNo FROM TARA_PalDesign D LEFT JOIN TARA_PalMarka M (NOLOCK) ON M.DesignID = D.ID LEFT JOIN TARA_Markalar MA (NOLOCK) ON MA.MarkaAciklama=M.MarkaAciklama LEFT JOIN TARA_PalCells C (NOLOCK) ON C.DesignID = D.ID) Y ON (I.BOY BETWEEN Y.BoyMin AND Y.BoyMax) AND (I.GENISLIK BETWEEN Y.GenislikMin AND Y.GenislikMax) AND RIGHT(I.STOK_KODU,7) = Y.MarkaKod WHERE I.ISEMRINO = '" + isEmri + "' AND Y.ProdNo = " + _hucre[i].prodNo + "", 13);
                                _hucre[i].baslangicX = datas[0];
                                _hucre[i].baslangicY = datas[1];
                                _hucre[i].offsetX = datas[2];
                                _hucre[i].offsetY = datas[3];
                                _hucre[i].tork = datas[4];
                                _hucre[i].paletZ = datas[5];
                                _hucre[i].katUrunAdedi = datas[6];
                                _hucre[i].urunYukseklik = datas[7];
                                _hucre[i].urunGenislik = datas[8];
                                _hucre[i].urunBoy = datas[9];
                                _hucre[i].rowNo = datas[10];
                                _hucre[i].colNo = datas[11];
                                _hucre[i].rot = datas[12];

                                _hucre[i].prodNo++;
                                _hucre[i].urunAdet++;
                                paletlemeAlgoritmasi(_hucre[i], i);
                                sendProductDatasToPlc(_hucre[i]);
                                _pekpanDalBarcode.ExecuteNonQuerySilent("INSERT INTO TARA_UretimPaket (Tarih, Vardiya, OptKodu, IsemriNo, Miktar) VALUES (GETDATE(),(SELECT CASE WHEN CONVERT(VARCHAR(5),GETDATE(),108) BETWEEN '08:00:00' AND '16:00:00' THEN '08-16' WHEN CONVERT(VARCHAR(5),GETDATE(),108) BETWEEN '00:00:00' AND '08:00:00' THEN '24-08' WHEN CONVERT(VARCHAR(5),GETDATE(),108) BETWEEN '16:00:00' AND '00:00:00' THEN '16-24' ELSE '0' END),'" + _kullaniciAdi + "', '" + isEmri + "', 1)");
                                _pekpanDalBarcode.ExecuteNonQuerySilent("insert into TARA_Analiz values ('" + isEmri + "'," + i + "," + _hucre[i].urunAdet + "," + _hucre[i].locationX + "," + _hucre[i].locationY + "," + _hucre[i].locationZ + ",getdate())");
                                this.Invoke(new Action(() => turnPB(i.ToString()).Value = _hucre[i].urunAdet));
                                this.Invoke(new Action(() => turnLM(i.ToString()).Text = _hucre[i].urunAdet + " / " + _hucre[i].kapasite));
                                if (_hucre[i].prodNo == _hucre[i].katUrunAdedi + 1)
                                {
                                    _hucre[i].prodNo = 1;
                                    _hucre[i].kat++;
                                }
                                if (_hucre[i].kapasite == _hucre[i].urunAdet)
                                {
                                    _hucre[i].statu = 2;
                                    this.Invoke(new Action(() => turnLB(i.ToString()).Text = "BİTTİ !!!"));
                                    TARA_HucreDurumUpdateBarcode(_hucre[i]);
                                    List<int> adjustAcilisOrder = _pekpanDalBarcode.GetDataReaderListInt("select hucreNo from TARA_HucreDurum where designID = " + designID + " and statu = 1 and stokKodu='" + _hucre[i].stokKodu + "' order by acilisOrder");
                                    for (int k = 0; k < adjustAcilisOrder.Count; k++)
                                    {
                                        _hucre[adjustAcilisOrder[k]].acilisOrder = k;
                                        TARA_HucreDurumUpdateBarcode(_hucre[adjustAcilisOrder[k]]);
                                    }
                                    adjustAcilisOrder.Clear();
                                    adjustAcilisOrder = _pekpanDalBarcode.GetDataReaderListInt("select hucreNo from TARA_HucreDurum where designID = " + designID + " and statu = 1 and stokKodu='" + _hucre[i].stokKodu + "' and (hucreNo=1 OR hucreNo=2 OR hucreNo=3 OR hucreNo=4) order by acilisOrderKM1");
                                    for (int k = 0; k < adjustAcilisOrder.Count; k++)
                                    {
                                        _hucre[adjustAcilisOrder[k]].acilisOrderKM1 = k;
                                        TARA_HucreDurumUpdateBarcode(_hucre[adjustAcilisOrder[k]]);
                                    }
                                }
                                else
                                    TARA_HucreDurumUpdateBarcode(_hucre[i]);
                                isEmriKapat(isEmri);
                                _density1++;
                                break;
                            }
                        }
                    }
                }
                else
                {
                    sendProductDatasToPlc(_hucre[0], 1);
                    _pekpanDal.ExecuteNonQuerySilent("INSERT INTO TARA_UretimPaket (Tarih, Vardiya, OptKodu, IsemriNo, Miktar,ReworkMu) VALUES (GETDATE(),(SELECT CASE WHEN CONVERT(VARCHAR(5),GETDATE(),108) BETWEEN '08:00:00' AND '16:00:00' THEN '08-16' WHEN CONVERT(VARCHAR(5),GETDATE(),108) BETWEEN '00:00:00' AND '08:00:00' THEN '24-08' WHEN CONVERT(VARCHAR(5),GETDATE(),108) BETWEEN '16:00:00' AND '00:00:00' THEN '16-24' ELSE '0' END),'" + _kullaniciAdi + "', '" + isEmri + "', 1,'E')");
                }
            }
            else
                sendProductDatasToPlc(_hucre[0], 1);
        }

        public void paletlemeAlgoritmasi(Hucre hucre, int index)
        {
            switch (hucre.rowNo)
            {
                case 1:
                    if (hucre.colNo == 1)
                    {
                        _hucre[index].locationX = hucre.baslangicX + hucre.urunBoy / 2;
                        _hucre[index].locationY = hucre.baslangicY + hucre.urunGenislik / 2;
                        _hucre[index].locationZ = hucre.paletZ + hucre.kat * hucre.urunYukseklik;
                    }
                    else if (hucre.colNo == 2)
                    {
                        _hucre[index].locationX = hucre.baslangicX + 3 * hucre.urunBoy / 2 + hucre.offsetX;
                        _hucre[index].locationY = hucre.baslangicY + hucre.urunGenislik / 2;
                        _hucre[index].locationZ = hucre.paletZ + hucre.kat * hucre.urunYukseklik;
                    }
                    else if (hucre.colNo == 3)
                    {
                        _hucre[index].locationX = hucre.baslangicX + 5 * hucre.urunBoy / 2 + 2 * hucre.offsetX;
                        _hucre[index].locationY = hucre.baslangicY + hucre.urunGenislik / 2;
                        _hucre[index].locationZ = hucre.paletZ + hucre.kat * hucre.urunYukseklik;
                    }
                    break;
                case 2:
                    if (hucre.colNo == 1)
                    {
                        _hucre[index].locationX = hucre.baslangicX + hucre.urunBoy / 2;
                        _hucre[index].locationY = hucre.baslangicY + 3 * hucre.urunGenislik / 2 + hucre.offsetY;
                        _hucre[index].locationZ = hucre.paletZ + hucre.kat * hucre.urunYukseklik;
                    }
                    else if (hucre.colNo == 2)
                    {
                        _hucre[index].locationX = hucre.baslangicX + 3 * hucre.urunBoy / 2 + hucre.offsetX;
                        _hucre[index].locationY = hucre.baslangicY + 3 * hucre.urunGenislik / 2 + hucre.offsetY;
                        _hucre[index].locationZ = hucre.paletZ + hucre.kat * hucre.urunYukseklik;
                    }
                    else if (hucre.colNo == 3)
                    {
                        _hucre[index].locationX = hucre.baslangicX + 5 * hucre.urunBoy / 2 + 2 * hucre.offsetX;
                        _hucre[index].locationY = hucre.baslangicY + 3 * hucre.urunGenislik / 2 + hucre.offsetY;
                        _hucre[index].locationZ = hucre.paletZ + hucre.kat * hucre.urunYukseklik;
                    }
                    break;
                case 3:
                    if (hucre.colNo == 1)
                    {
                        _hucre[index].locationX = hucre.baslangicX + hucre.urunBoy / 2;
                        _hucre[index].locationY = hucre.baslangicY + 5 * hucre.urunGenislik / 2 + 2 * hucre.offsetY;
                        _hucre[index].locationZ = hucre.paletZ + hucre.kat * hucre.urunYukseklik;
                    }
                    else if (hucre.colNo == 2)
                    {
                        _hucre[index].locationX = hucre.baslangicX + 3 * hucre.urunBoy / 2 + hucre.offsetX;
                        _hucre[index].locationY = hucre.baslangicY + 5 * hucre.urunGenislik / 2 + 2 * hucre.offsetY;
                        _hucre[index].locationZ = hucre.paletZ + hucre.kat * hucre.urunYukseklik;
                    }
                    else if (hucre.colNo == 3)
                    {
                        _hucre[index].locationX = hucre.baslangicX + 5 * hucre.urunBoy / 2 + 2 * hucre.offsetX;
                        _hucre[index].locationY = hucre.baslangicY + 5 * hucre.urunGenislik / 2 + 2 * hucre.offsetY;
                        _hucre[index].locationZ = hucre.paletZ + hucre.kat * hucre.urunYukseklik;
                    }
                    break;
                case 4:
                    if (hucre.colNo == 1)
                    {
                        _hucre[index].locationX = hucre.baslangicX + hucre.urunBoy / 2;
                        _hucre[index].locationY = hucre.baslangicY + 7 * hucre.urunGenislik / 2 + 3 * hucre.offsetY;
                        _hucre[index].locationZ = hucre.paletZ + hucre.kat * hucre.urunYukseklik;
                    }
                    else if (hucre.colNo == 2)
                    {
                        _hucre[index].locationX = hucre.baslangicX + 3 * hucre.urunBoy / 2 + hucre.offsetX;
                        _hucre[index].locationY = hucre.baslangicY + 7 * hucre.urunGenislik / 2 + 3 * hucre.offsetY;
                        _hucre[index].locationZ = hucre.paletZ + hucre.kat * hucre.urunYukseklik;
                    }
                    else if (hucre.colNo == 3)
                    {
                        _hucre[index].locationX = hucre.baslangicX + 5 * hucre.urunBoy / 2 + 2 * hucre.offsetX;
                        _hucre[index].locationY = hucre.baslangicY + 7 * hucre.urunGenislik / 2 + 3 * hucre.offsetY;
                        _hucre[index].locationZ = hucre.paletZ + hucre.kat * hucre.urunYukseklik;
                    }
                    break;
                default:
                    break;
            }
        }

        private void sendProductDatasToPlc(Hucre hucre, int rework = 0)
        {
            int[] datas = new int[14];
            datas[0] = hucre.urunBoy;
            datas[1] = hucre.urunGenislik;
            datas[2] = hucre.urunYukseklik;
            datas[3] = hucre.locationX;
            datas[4] = hucre.locationY;
            datas[5] = hucre.locationZ;
            datas[6] = 0;//ağırlık ölçüleri yollanmayacak
            datas[7] = hucre.hucreNo;
            datas[8] = hucre.rot;
            datas[9] = hucre.tork;
            datas[10] = hucre.kapasite;
            datas[11] = hucre.urunAdet;
            datas[12] = rework;//ürün rework bilgisi
            datas[13] = 1;//ürün gönderildi bilgisi

            usingPlc = true;
            Thread.Sleep(1000);//-------------------timer "Stopwatch stopwatch = Stopwatch.StartNew();" ile dinlenecek sonra aralık güncellenecek
        again:
            if (writePlcWithButtons)
                Thread.Sleep(250);
            _plc.writeMultiplePlc(0, datas);
            if (_plc.tryAgain == true)
            {
                _pekpanDalBarcode.ExecuteNonQuerySilent("insert into TARA_Deneme values (" + hucre.isEmri + "," + hucre.hucreNo + ",5,5,5,5,getdate())");
                _plc.tryAgain = false;
                goto again;
            }
            usingPlc = false;
            _plcListener.Enabled = true;
            _independent.Enabled = true;
        }

        public void TARA_HucreDurumInsert(Hucre hucre)
        {
            _pekpanDal.ExecuteNonQuerySilent("insert into TARA_HucreDurum values('" + hucre.isEmri + "','" + hucre.designAciklama + "','" + hucre.urunAciklama + "','" + hucre.stokKodu + "'," + hucre.designID + "," + hucre.baslangicX + "," + hucre.baslangicY + "," + hucre.paletZ + "," + hucre.offsetX + "," + hucre.offsetY + "," + hucre.tork + "," + hucre.katUrunAdedi + "," + hucre.urunYukseklik + "," + hucre.urunGenislik + "," + hucre.urunBoy + "," + hucre.rot + "," + hucre.statu + "," + hucre.kat + "," + hucre.prodNo + "," + hucre.rowNo + "," + hucre.colNo + "," + hucre.acilisOrder + "," + hucre.acilisOrderKM1 + "," + hucre.acilisOrderKM2 + "," + hucre.hucreNo + "," + hucre.kapasite + "," + hucre.urunAdet + "," + hucre.palUrunAdet + "," + hucre.locationX + "," + hucre.locationY + "," + hucre.locationZ + ")");
        }
        public void TARA_HucreDurumDelete(int index)
        {
            _pekpanDal.ExecuteNonQuerySilent("delete TARA_HucreDurum where hucreNo=" + index + "");
        }
        public void TARA_HucreDurumUpdate(Hucre hucre)
        {
            _pekpanDal.ExecuteNonQuerySilent("update TARA_HucreDurum set isEmri='" + hucre.isEmri + "',designAciklama='" + hucre.designAciklama + "',urunAciklama='" + hucre.urunAciklama + "',stokKodu='" + hucre.stokKodu + "',designID=" + hucre.designID + ",baslangicX=" + hucre.baslangicX + ",baslangicY=" + hucre.baslangicY + ",paletZ=" + hucre.paletZ + ",offsetX=" + hucre.offsetX + ",offsetY=" + hucre.offsetY + ",tork=" + hucre.tork + ",katUrunAdedi=" + hucre.katUrunAdedi + ",urunYukseklik=" + hucre.urunYukseklik + ",urunGenislik=" + hucre.urunGenislik + ",urunBoy=" + hucre.urunBoy + ",rot=" + hucre.rot + ",statu=" + hucre.statu + ",kat=" + hucre.kat + ",prodNo=" + hucre.prodNo + ",rowNo=" + hucre.rowNo + ",colNo=" + hucre.colNo + ",acilisOrder=" + hucre.acilisOrder + ",acilisOrderKM1=" + hucre.acilisOrderKM1 + ",acilisOrderKM2=" + hucre.acilisOrderKM2 + ",kapasite=" + hucre.kapasite + ",urunAdet=" + hucre.urunAdet + ",palUrunAdet=" + hucre.palUrunAdet + ",locationX=" + hucre.locationX + ",locationY=" + hucre.locationY + ",locationZ=" + hucre.locationZ + "where hucreNo=" + hucre.hucreNo + "");
        }
        public void TARA_HucreDurumDeleteAll()
        {
            _pekpanDal.ExecuteNonQuerySilent("delete from TARA_HucreDurum");
        }
        public void TARA_HucreDurumUpdateBarcode(Hucre hucre)
        {
            _pekpanDalBarcode.ExecuteNonQuerySilent("update TARA_HucreDurum set isEmri='" + hucre.isEmri + "',designAciklama='" + hucre.designAciklama + "',urunAciklama='" + hucre.urunAciklama + "',stokKodu='" + hucre.stokKodu + "',designID=" + hucre.designID + ",baslangicX=" + hucre.baslangicX + ",baslangicY=" + hucre.baslangicY + ",paletZ=" + hucre.paletZ + ",offsetX=" + hucre.offsetX + ",offsetY=" + hucre.offsetY + ",tork=" + hucre.tork + ",katUrunAdedi=" + hucre.katUrunAdedi + ",urunYukseklik=" + hucre.urunYukseklik + ",urunGenislik=" + hucre.urunGenislik + ",urunBoy=" + hucre.urunBoy + ",rot=" + hucre.rot + ",statu=" + hucre.statu + ",kat=" + hucre.kat + ",prodNo=" + hucre.prodNo + ",rowNo=" + hucre.rowNo + ",colNo=" + hucre.colNo + ",acilisOrder=" + hucre.acilisOrder + ",acilisOrderKM1=" + hucre.acilisOrderKM1 + ",acilisOrderKM2=" + hucre.acilisOrderKM2 + ",kapasite=" + hucre.kapasite + ",urunAdet=" + hucre.urunAdet + ",palUrunAdet=" + hucre.palUrunAdet + ",locationX=" + hucre.locationX + ",locationY=" + hucre.locationY + ",locationZ=" + hucre.locationZ + "where hucreNo=" + hucre.hucreNo + "");
        }
        public void TARA_HucreDurumUpdatePlcListener(Hucre hucre)
        {
            _pekpanDalPlcListener.ExecuteNonQuerySilent("update TARA_HucreDurum set isEmri='" + hucre.isEmri + "',designAciklama='" + hucre.designAciklama + "',urunAciklama='" + hucre.urunAciklama + "',stokKodu='" + hucre.stokKodu + "',designID=" + hucre.designID + ",baslangicX=" + hucre.baslangicX + ",baslangicY=" + hucre.baslangicY + ",paletZ=" + hucre.paletZ + ",offsetX=" + hucre.offsetX + ",offsetY=" + hucre.offsetY + ",tork=" + hucre.tork + ",katUrunAdedi=" + hucre.katUrunAdedi + ",urunYukseklik=" + hucre.urunYukseklik + ",urunGenislik=" + hucre.urunGenislik + ",urunBoy=" + hucre.urunBoy + ",rot=" + hucre.rot + ",statu=" + hucre.statu + ",kat=" + hucre.kat + ",prodNo=" + hucre.prodNo + ",rowNo=" + hucre.rowNo + ",colNo=" + hucre.colNo + ",acilisOrder=" + hucre.acilisOrder + ",acilisOrderKM1=" + hucre.acilisOrderKM1 + ",acilisOrderKM2=" + hucre.acilisOrderKM2 + ",kapasite=" + hucre.kapasite + ",urunAdet=" + hucre.urunAdet + ",palUrunAdet=" + hucre.palUrunAdet + ",locationX=" + hucre.locationX + ",locationY=" + hucre.locationY + ",locationZ=" + hucre.locationZ + "where hucreNo=" + hucre.hucreNo + "");
        }
        public void isEmriKapat(string isEmriNo)
        {
            isEmriNo = _pekpanDalBarcode.ExecuteScalarStr("select top 1 isEmriNo from tblIsEmri where refIsEmriNo = '" + isEmriNo + "' and stok_kodu like '%KB'");
            if ("0" != _pekpanDalBarcode.ExecuteScalarStr("select count(*) from TARA_uretimBoya where statu = 1 and isEmriNo = '" + isEmriNo + "'"))
            {
                int okunanUrun = Convert.ToInt32(_pekpanDalBarcode.ExecuteScalarStr("select top 1 okunanUrun from TARA_uretimBoya where statu = 1 and isEmriNo = '" + isEmriNo + "' order by tarih"));
                okunanUrun++;
                _pekpanDalBarcode.ExecuteNonQuerySilent("update b set okunanUrun = " + okunanUrun + " from TARA_uretimBoya b where ID = (select top 1 ID from TARA_uretimBoya where statu = 1 and isEmriNo = '" + isEmriNo + "' order by tarih)");
                if ("1" == _pekpanDalBarcode.ExecuteScalarStr("select case when miktar=okunanUrun then 1 else 0 end from TARA_uretimBoya where ID = (select top 1 ID from TARA_uretimBoya where statu = 1 and isEmriNo = '" + isEmriNo + "' order by tarih)"))
                    _pekpanDalBarcode.ExecuteNonQuerySilent("update b set statu = 2 from TARA_uretimBoya b where ID = (select top 1 ID from TARA_uretimBoya where statu = 1 and isEmriNo = '" + isEmriNo + "' order by tarih)");
            }
            else
            {
                int okunanUrun = Convert.ToInt32(_pekpanDalBarcode.ExecuteScalarStr("select top 1 okunanUrun from TARA_uretimBoya where isEmriNo='" + isEmriNo + "' order by ID desc"));
                okunanUrun++;
                _pekpanDalBarcode.ExecuteNonQuerySilent("update b set okunanUrun = " + okunanUrun + " from TARA_uretimBoya b where ID = (select top 1 ID from TARA_uretimBoya where isEmriNo = '" + isEmriNo + "' order by ID desc)");

            }
        }

        private void _plc_ConnectingEvent()
        {
            this.Invoke(new Action(() => btnPlcReader_Click(btnPlcReader, EventArgs.Empty)));
        }

        private void _plc_HucreDurumEvent()
        {
            string binary = Convert.ToString((ushort)_plc.hucreDurum + 65536, 2);
            binary = binary.Remove(0, 1);

            if (binary[7] == '0' && binary[15] == '0')
                phd1.BackgroundImage = pHucreDurum.Images[2];
            else if (binary[7] == '1')
                phd1.BackgroundImage = pHucreDurum.Images[0];
            else if (binary[15] == '1')
                phd1.BackgroundImage = pHucreDurum.Images[1];

            if (binary[6] == '0' && binary[14] == '0')
                phd2.BackgroundImage = pHucreDurum.Images[2];
            else if (binary[6] == '1')
                phd2.BackgroundImage = pHucreDurum.Images[0];
            else if (binary[14] == '1')
                phd2.BackgroundImage = pHucreDurum.Images[1];

            if (binary[5] == '0' && binary[13] == '0')
                phd3.BackgroundImage = pHucreDurum.Images[2];
            else if (binary[5] == '1')
                phd3.BackgroundImage = pHucreDurum.Images[0];
            else if (binary[13] == '1')
                phd3.BackgroundImage = pHucreDurum.Images[1];

            if (binary[4] == '0' && binary[12] == '0')
                phd4.BackgroundImage = pHucreDurum.Images[2];
            else if (binary[4] == '1')
                phd4.BackgroundImage = pHucreDurum.Images[0];
            else if (binary[12] == '1')
                phd4.BackgroundImage = pHucreDurum.Images[1];

            if (binary[3] == '0' && binary[11] == '0')
                phd5.BackgroundImage = pHucreDurum.Images[2];
            else if (binary[3] == '1')
                phd5.BackgroundImage = pHucreDurum.Images[0];
            else if (binary[11] == '1')
                phd5.BackgroundImage = pHucreDurum.Images[1];

            if (binary[2] == '0' && binary[10] == '0')
                phd6.BackgroundImage = pHucreDurum.Images[2];
            else if (binary[2] == '1')
                phd6.BackgroundImage = pHucreDurum.Images[0];
            else if (binary[10] == '1')
                phd6.BackgroundImage = pHucreDurum.Images[1];

            if (binary[1] == '0' && binary[9] == '0')
                phd7.BackgroundImage = pHucreDurum.Images[2];
            else if (binary[1] == '1')
                phd7.BackgroundImage = pHucreDurum.Images[0];
            else if (binary[9] == '1')
                phd7.BackgroundImage = pHucreDurum.Images[1];

            if (binary[0] == '0' && binary[8] == '0')
                phd8.BackgroundImage = pHucreDurum.Images[2];
            else if (binary[0] == '1')
                phd8.BackgroundImage = pHucreDurum.Images[0];
            else if (binary[8] == '1')
                phd8.BackgroundImage = pHucreDurum.Images[1];
        }

        private void _plc_WatchProductsEvent()
        {

        }

        private void _plc_SensorInformationsEvent()
        {
            string binary = Convert.ToString((ushort)_plc.sensorInformations + 65536, 2);
            binary = binary.Remove(0, 1);
            pbx1.BackgroundImage = (binary[15] == '0') ? sensors.Images[0] : sensors.Images[1];
            pbx2.BackgroundImage = (binary[14] == '0') ? sensors.Images[0] : sensors.Images[1];
            pbx3.BackgroundImage = (binary[13] == '0') ? sensors.Images[0] : sensors.Images[1];
            pbx4.BackgroundImage = (binary[12] == '0') ? sensors.Images[0] : sensors.Images[1];
            pbx5.BackgroundImage = (binary[11] == '0') ? sensors.Images[0] : sensors.Images[1];
            pbx6.BackgroundImage = (binary[10] == '0') ? sensors.Images[0] : sensors.Images[1];
            pbx7.BackgroundImage = (binary[9] == '0') ? sensors.Images[0] : sensors.Images[1];
            pbx8.BackgroundImage = (binary[8] == '0') ? sensors.Images[0] : sensors.Images[1];
            pbx9.BackgroundImage = (binary[7] == '0') ? sensors.Images[0] : sensors.Images[1];
            pbx10.BackgroundImage = (binary[6] == '0') ? sensors.Images[0] : sensors.Images[1];
            pbx11.BackgroundImage = (binary[5] == '0') ? sensors.Images[0] : sensors.Images[1];
            pbx12.BackgroundImage = (binary[4] == '0') ? sensors.Images[0] : sensors.Images[1];
            pbx13.BackgroundImage = (binary[3] == '0') ? sensors.Images[0] : sensors.Images[1];
            pbx14.BackgroundImage = (binary[2] == '0') ? sensors.Images[0] : sensors.Images[1];
            pbx15.BackgroundImage = (binary[1] == '0') ? sensors.Images[0] : sensors.Images[1];
            pbx16.BackgroundImage = (binary[0] == '0') ? sensors.Images[0] : sensors.Images[1];
        }

        private void _plc_ProductDroppedEvent1()
        {
            if (_plc.productDropped1[3] == '1' && _plc.productDropped1[3] != _plc.__productDropped1[3])
            {
                this.Invoke(new Action(() => xb1.Value++));
                _hucre[1].palUrunAdet++;
                _density1--;
                TARA_HucreDurumUpdate(_hucre[1]);
            }
            if (_plc.productDropped1[2] == '1' && _plc.productDropped1[2] != _plc.__productDropped1[2])
            {
                this.Invoke(new Action(() => xb2.Value++));
                _hucre[2].palUrunAdet++;
                _density1--;
                TARA_HucreDurumUpdate(_hucre[2]);
            }
            if (_plc.productDropped1[1] == '1' && _plc.productDropped1[1] != _plc.__productDropped1[1])
            {
                this.Invoke(new Action(() => xb3.Value++));
                _hucre[3].palUrunAdet++;
                _density1--;
                TARA_HucreDurumUpdate(_hucre[3]);
            }
            if (_plc.productDropped1[0] == '1' && _plc.productDropped1[0] != _plc.__productDropped1[0])
            {
                this.Invoke(new Action(() => xb4.Value++));
                _hucre[4].palUrunAdet++;
                _density1--;
                TARA_HucreDurumUpdate(_hucre[4]);
            }
            _plc.__productDropped1 = _plc.productDropped1;
        }

        private void _plc_ProductDroppedEvent2()
        {

            if (_plc.productDropped2[3] == '1' && _plc.productDropped2[3] != _plc.__productDropped2[3])
            {
                this.Invoke(new Action(() => xb5.Value++));
                _hucre[5].palUrunAdet++;
                _density2--;
                TARA_HucreDurumUpdate(_hucre[5]);
            }
            if (_plc.productDropped2[2] == '1' && _plc.productDropped2[2] != _plc.__productDropped2[2])
            {
                this.Invoke(new Action(() => xb6.Value++));
                _hucre[6].palUrunAdet++;
                _density2--;
                TARA_HucreDurumUpdate(_hucre[6]);
            }
            if (_plc.productDropped2[1] == '1' && _plc.productDropped2[1] != _plc.__productDropped2[1])
            {
                this.Invoke(new Action(() => xb7.Value++));
                _hucre[7].palUrunAdet++;
                _density2--;
                TARA_HucreDurumUpdate(_hucre[7]);
            }
            if (_plc.productDropped2[0] == '1' && _plc.productDropped2[0] != _plc.__productDropped2[0])
            {
                this.Invoke(new Action(() => xb8.Value++));
                _hucre[8].palUrunAdet++;
                _density2--;
                TARA_HucreDurumUpdate(_hucre[8]);
            }
            _plc.__productDropped2 = _plc.productDropped2;
        }

        private void _plc_DensityEvent2()
        {
            this.Invoke(new Action(() => pbDensity2.Value = _plc.density2));
            this.Invoke(new Action(() => lbDensity2.Text = "Yoğunluk2 : " + _plc.density2 + ""));
        }

        private void btnPalDesign_Click(object sender, EventArgs e)
        {
            showPalDesigns();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            //    usingPlc = true;
            //    Thread.Sleep(1000);//-------------------timer "Stopwatch stopwatch = Stopwatch.StartNew();" ile dinlenecek sonra aralık güncellenecek
            //again:
            //    _plc.writeMultiplePlc(18, new int[1] { 1 });
            //    if (_plc.tryAgain == true)
            //    {
            //        _plc.tryAgain = false;
            //        goto again;
            //    }
            //    usingPlc = false;
            //    _plcListener.Enabled = true;
        }

        private void btnStop_Click(object sender, EventArgs e)
        {

        }

        private void btnDisplay_Click(object sender, EventArgs e)
        {
            if (tabControl1.Visible == false)
            {
                this.btnDisplay.BackgroundImage = global::Pekpan.Properties.Resources.açık;
                tabControl1.Visible = true;
                tabControl1.BringToFront();
                takeCsvDatas();
            }
            else
            {
                this.btnDisplay.BackgroundImage = global::Pekpan.Properties.Resources.kapalı;
                tabControl1.Visible = false;
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            DialogResult dialog = MessageBox.Show("Uygulama kapatılacaktır !\n Emin misiniz ?", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog == DialogResult.Yes)
            {
                _independent.Enabled = false;
                usingPlc = true;
                Thread.Sleep(1000);
            again:
                if (dataReceivedEnabled)
                {
                    Thread.Sleep(750);
                    goto again;
                }
                _plc.writeMultiplePlc(14, new int[1] { 0 });
                Close();
            }
        }

        private void home_Click(object sender, EventArgs e)
        {
            if (dataReceivedEnabled != true)
            {
                writePlcWithButtons = true;
                usingPlc = true;
                Thread.Sleep(1000);//-------------------timer "Stopwatch stopwatch = Stopwatch.StartNew();" ile dinlenecek sonra aralık güncellenecek
            again:
                if (sender == home1)
                    _plc.writeMultiplePlc(15, new int[1] { 1 });
                else if (sender == home2)
                    _plc.writeMultiplePlc(15, new int[1] { 2 });
                else if (sender == btnSistemReset)
                    _plc.writeMultiplePlc(15, new int[1] { 3 });

                if (_plc.tryAgain == true)
                {
                    _plc.tryAgain = false;
                    goto again;
                }
                usingPlc = false;
                _plcListener.Enabled = true;
                _independent.Enabled = true;
                writePlcWithButtons = false;
            }
        }

        private void bd_Click(object sender, EventArgs e)
        {
            Button button_active = sender as Button;
            string index = button_active.Name.Remove(0, 2);
            if (_kullaniciAdi == "")
            {
                Login login = new Login(_pekpanDal);
                if (login.ShowDialog() == DialogResult.OK)
                {
                    _kullaniciAdi = login._kullaniciAdi;
                    lbKullaniciAdi.Text = "Operatör : " + _pekpanDal.ExecuteScalarStr("SELECT DBO.TRK(ISIM) FROM TBLMRPISCI WHERE SICILNO = '" + login._kullaniciAdi + "'");
                    PaletDesigner pd = new PaletDesigner(_pekpanDal, _kullaniciAdi, Convert.ToInt32(_pekpanDal.ExecuteScalarStr("select ID from TARA_PalDesign where Aciklama= '" + _hucre[Convert.ToInt32(index)].designAciklama + "'")), _pekpanDal.GetDataReaderListColumn("select Aciklama, XBaslangic, YBaslangic, XOffset, YOffset, PaletZ, Tork, GenislikMin, GenislikMax, BoyMin, BoyMax from TARA_PalDesign where ID=" + Convert.ToInt32(_pekpanDal.ExecuteScalarStr("select ID from TARA_PalDesign where Aciklama= '" + _hucre[Convert.ToInt32(index)].designAciklama + "'")) + "", 11));
                    pd.ShowDialog();
                }
            }
            else
            {
                PaletDesigner pd = new PaletDesigner(_pekpanDal, _kullaniciAdi, Convert.ToInt32(_pekpanDal.ExecuteScalarStr("select ID from TARA_PalDesign where Aciklama= '" + _hucre[Convert.ToInt32(index)].designAciklama + "'")), _pekpanDal.GetDataReaderListColumn("select Aciklama, XBaslangic, YBaslangic, XOffset, YOffset, PaletZ, Tork, GenislikMin, GenislikMax, BoyMin, BoyMax from TARA_PalDesign where ID=" + Convert.ToInt32(_pekpanDal.ExecuteScalarStr("select ID from TARA_PalDesign where Aciklama= '" + _hucre[Convert.ToInt32(index)].designAciklama + "'")) + "", 11));
                pd.ShowDialog();
            }
        }

        private void bg_Click(object sender, EventArgs e)
        {
            Button button_active = sender as Button;
            string index = button_active.Name.Remove(0, 2);
            adjustAcilisOrders(index);
            if (InvokeRequired)
            {
                this.Invoke(new Action(() => turnPN(index).Visible = false));
                this.Invoke(new Action(() => turnLA(index).Text = "CLOSED"));
                this.Invoke(new Action(() => turnLB(index).Text = "CLOSED"));
                this.Invoke(new Action(() => turnLC(index).Text = "CLOSED"));
                this.Invoke(new Action(() => turnLM(index).Text = "CLOSED"));
            }
            else
            {
                turnPN(index).Visible = false;
                turnLA(index).Text = "CLOSED";
                turnLB(index).Text = "CLOSED";
                turnLC(index).Text = "CLOSED";
                turnLM(index).Text = "CLOSED";
            }
            initializeHucres(Convert.ToInt32(index));
            TARA_HucreDurumDelete(Convert.ToInt32(index));
            takeCsvDatas();
        }

        private void btnPlcReader_Click(object sender, EventArgs e)
        {
            if (e == EventArgs.Empty)
            {
                if (_plc.Connecting)
                    this.Invoke(new Action(() => bPLC.BackColor = Color.Lime));
                else
                    this.Invoke(new Action(() => bPLC.BackColor = Color.Red));
                this.Invoke(new Action(() => usingPlc = (bPLC.BackColor == Color.Lime) ? false : true));
                this.Invoke(new Action(() => _plcListener.Enabled = (bPLC.BackColor == Color.Lime) ? true : false));
                this.Invoke(new Action(() => btnPlcReader.ForeColor = (_plcListener.Enabled == true) ? Color.Lime : Color.Black));
            }
            else
            {
                _plc.Connecting = (btnPlcReader.ForeColor == Color.Lime) ? false : true;
            }
        }

        private void createPallet_Click(object sender, EventArgs e)
        {
            if (turnPN(nmCellNumber.Value.ToString()).Visible == false)
            {
                List<string> data = _pekpanDal.GetDataReaderListColumn3("SELECT Y.ID,I.TIP,I.GENISLIK,I.BOY,I.STOK_KODU FROM TARA_IsEmri I LEFT JOIN (SELECT D.ID, D.GenislikMin, D.GenislikMax, D.BoyMin, D.BoyMax, M.MarkaAciklama, MA.MarkaKod FROM TARA_PalDesign D LEFT JOIN TARA_PalMarka M (NOLOCK) ON M.DesignID = D.ID LEFT JOIN TARA_Markalar MA (NOLOCK) ON MA.MarkaAciklama=M.MarkaAciklama) Y ON (I.BOY BETWEEN Y.BoyMin AND Y.BoyMax) AND (I.GENISLIK BETWEEN Y.GenislikMin AND Y.GenislikMax) AND RIGHT(I.STOK_KODU,7) = Y.MarkaKod WHERE I.ISEMRINO = '" + txbIsEmriNoForPallet.Text + "'", 5);
                data[0] = _pekpanDal.ExecuteScalarStr("SELECT Y.ID FROM TARA_IsEmri I LEFT JOIN (SELECT D.ID, D.GenislikMin, D.GenislikMax, D.BoyMin, D.BoyMax, M.MarkaAciklama, MA.MarkaKod FROM TARA_PalDesign D LEFT JOIN TARA_PalMarka M (NOLOCK) ON M.DesignID = D.ID LEFT JOIN TARA_Markalar MA (NOLOCK) ON MA.MarkaAciklama=M.MarkaAciklama) Y ON (I.BOY BETWEEN Y.BoyMin AND Y.BoyMax) AND (I.GENISLIK BETWEEN Y.GenislikMin AND Y.GenislikMax) AND RIGHT(I.STOK_KODU,7) = Y.MarkaKod WHERE I.ISEMRINO = '" + txbIsEmriNoForPallet.Text + "'");

                string designID = data[0];
                if (designID != "")
                {
                    int hucreCounter = 0;
                    int hucreCounterKM1 = 0;
                    int hucreCounterKM2 = 0;
                    string designAciklama = _pekpanDal.ExecuteScalarStr("select aciklama from TARA_paldesign where ID=" + designID);
                    hucreCounter = Convert.ToInt32(_pekpanDal.ExecuteScalarStr("select count(*) from TARA_HucreDurum where statu = 1 and stokKodu='" + data[4] + "'"));
                    if (nmCellNumber.Value < 5)
                        hucreCounterKM1 = Convert.ToInt32(_pekpanDal.ExecuteScalarStr("select count(*) from TARA_HucreDurum where statu = 1 and stokKodu='" + data[4] + "' and (hucreNo=1 OR hucreNo=2 OR hucreNo=3 OR hucreNo=4)"));
                    if (nmCellNumber.Value > 4)
                        hucreCounterKM2 = Convert.ToInt32(_pekpanDal.ExecuteScalarStr("select count(*) from TARA_HucreDurum where statu = 1 and stokKodu='" + data[4] + "' and (hucreNo=5 OR hucreNo=6 OR hucreNo=7 OR hucreNo=8)"));

                    _hucre[(int)nmCellNumber.Value].statu = 1;
                    _hucre[(int)nmCellNumber.Value].isEmri = txbIsEmriNoForPallet.Text;
                    _hucre[(int)nmCellNumber.Value].acilisOrder = hucreCounter;
                    _hucre[(int)nmCellNumber.Value].acilisOrderKM1 = hucreCounterKM1;
                    _hucre[(int)nmCellNumber.Value].acilisOrderKM2 = hucreCounterKM2;

                    _hucre[(int)nmCellNumber.Value].hucreNo = (int)nmCellNumber.Value;
                    _hucre[(int)nmCellNumber.Value].designAciklama = designAciklama;
                    _hucre[(int)nmCellNumber.Value].urunAciklama = data[1] + "-" + data[2] + "-" + data[3];
                    _hucre[(int)nmCellNumber.Value].stokKodu = data[4];
                    _hucre[(int)nmCellNumber.Value].designID = Convert.ToInt32(designID);
                    _hucre[(int)nmCellNumber.Value].kapasite = (int)nmAmount.Value;
                    setPallet(nmCellNumber.Value.ToString(), _hucre[(int)nmCellNumber.Value]);
                    TARA_HucreDurumInsert(_hucre[(int)nmCellNumber.Value]);
                    takeCsvDatas();
                }
                else
                {
                    MessageBox.Show("İş emrine uygun dizayn bulunamadı !!!\nLütfen uygun bir dizayn oluşturun", "Eşlenme Hatası", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void btnElleKapatAc_Click(object sender, EventArgs e)
        {
            if (sender == btnElleKapat)
                _pekpanDal.ExecuteNonQuerySilent("update b set b.statu = 2 from TARA_uretimBoya b where b.isEmriNo =(SELECT ISEMRINO FROM TBLISEMRI WHERE RIGHT(STOK_KODU,2) = 'KB' AND REFISEMRINO = '" + txbIsEmriNoForElleKapat.Text + "')");
            else if (sender == button1)
                _pekpanDal.ExecuteNonQuerySilent("UPDATE B SET B.Statu = 2 FROM TARA_UretimBoya B WHERE B.ID = (SELECT TOP 1 ID FROM TARA_UretimBoya WHERE IsemriNo = (SELECT ISEMRINO FROM TBLISEMRI WHERE RIGHT(STOK_KODU,2) = 'KB' AND REFISEMRINO = '" + txbIsEmriNoForElleKapat.Text + "') ORDER BY ID DESC)");
            takeCsvDatas();
        }

        private void gvIsEmri_DoubleClick(object sender, EventArgs e)
        {
            List<string> receteDatas = _pekpanDal.GetDataReaderListColumnString("SELECT ISNULL(DBO.TRK(KAPAK), '---') KAPAK,ISNULL(DBO.TRK(KARTON), '---') KARTON,ISNULL(DBO.TRK(KOSE), '---') KOSE,ISNULL(DBO.TRK(YKONSOL), '---') YKONSOL,ISNULL(DBO.TRK(INSERTT), '---') INSERTT,ISNULL(DBO.TRK(KILAVUZ1), '---') KILAVUZ1,ISNULL(DBO.TRK(KILAVUZ2), '---') KILAVUZ2,ISNULL(DBO.TRK(ETIKET), '---') ETIKET,ISNULL(DBO.TRK(DPANEL), '---') DPANEL,ISNULL(DBO.TRK(ISEMRI_ACIKLAMA), '---') ACIKLAMA FROM VW_DZNISEMRILISTRECETE WHERE ISEMRI_NO = '" + gvIsEmri.CurrentRow.Cells[1].Value + "'", 10);
            Recete recete = new Recete(receteDatas);
            if (recete.ShowDialog() == DialogResult.OK)
            {
            }
        }

        private Panel turnPN(string index)
        {
            if (index == "1") return pn1;
            else if (index == "2") return pn2;
            else if (index == "3") return pn3;
            else if (index == "4") return pn4;
            else if (index == "5") return pn5;
            else if (index == "6") return pn6;
            else if (index == "7") return pn7;
            else if (index == "8") return pn8;
            else return null;
        }
        private Label turnLA(string index)
        {
            if (index == "1") return la1;
            else if (index == "2") return la2;
            else if (index == "3") return la3;
            else if (index == "4") return la4;
            else if (index == "5") return la5;
            else if (index == "6") return la6;
            else if (index == "7") return la7;
            else if (index == "8") return la8;
            else return null;
        }
        private Label turnLB(string index)
        {
            if (index == "1") return lb1;
            else if (index == "2") return lb2;
            else if (index == "3") return lb3;
            else if (index == "4") return lb4;
            else if (index == "5") return lb5;
            else if (index == "6") return lb6;
            else if (index == "7") return lb7;
            else if (index == "8") return lb8;
            else return null;
        }
        private Label turnLM(string index)
        {
            if (index == "1") return lm1;
            else if (index == "2") return lm2;
            else if (index == "3") return lm3;
            else if (index == "4") return lm4;
            else if (index == "5") return lm5;
            else if (index == "6") return lm6;
            else if (index == "7") return lm7;
            else if (index == "8") return lm8;
            else return null;
        }
        private Label turnLC(string index)
        {
            if (index == "1") return lc1;
            else if (index == "2") return lc2;
            else if (index == "3") return lc3;
            else if (index == "4") return lc4;
            else if (index == "5") return lc5;
            else if (index == "6") return lc6;
            else if (index == "7") return lc7;
            else if (index == "8") return lc8;
            else return null;
        }
        private Button turnBG(string index)
        {
            if (index == "1") return bg1;
            else if (index == "2") return bg2;
            else if (index == "3") return bg3;
            else if (index == "4") return bg4;
            else if (index == "5") return bg5;
            else if (index == "6") return bg6;
            else if (index == "7") return bg7;
            else if (index == "8") return bg8;
            else return null;
        }
        private ProgressBar turnXB(string index)
        {
            if (index == "1") return xb1;
            else if (index == "2") return xb2;
            else if (index == "3") return xb3;
            else if (index == "4") return xb4;
            else if (index == "5") return xb5;
            else if (index == "6") return xb6;
            else if (index == "7") return xb7;
            else if (index == "8") return xb8;
            else return null;
        }
        private ProgressBar turnPB(string index)
        {
            if (index == "1") return pb1;
            else if (index == "2") return pb2;
            else if (index == "3") return pb3;
            else if (index == "4") return pb4;
            else if (index == "5") return pb5;
            else if (index == "6") return pb6;
            else if (index == "7") return pb7;
            else if (index == "8") return pb8;
            else return null;
        }

        private void _plcListener_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            _plcListener.Enabled = false;
        again:
            _plc.readSensorDataInPlc(16);
            if (_plc.tryAgain == true)
            {
                _plc.tryAgain = false;
                goto again;
            }
            if (usingPlc != true)
                _plcListener.Enabled = true;
        }

        private void _independent_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                _independent.Enabled = false;
                this.Invoke(new Action(() => lbSaat.Text = System.DateTime.Now.ToString().Remove(0, 11)));
                this.Invoke(new Action(() => lbRegisters.Text = "PLC Registers : " + _plc.hucreDurum + " ; urun izleme icin ; " + _plc.sensorInformations + " ; " + _plc.productDropped2 + " ; " + _plc.productDropped1 + " ; " + _plc.density2 + ""));
                //this.Invoke(new Action(() => pbDensity1.Value = _density1));
                //this.Invoke(new Action(() => lbDensity1.Text = "Yoğunluk1 : " + _density1 + ""));
                //this.Invoke(new Action(() => pbDensity2.Value = _density2));
                //this.Invoke(new Action(() => lbDensity2.Text = "Yoğunluk2 : " + _density2 + ""));
                _independent.Enabled = true;
            }
            catch (System.ObjectDisposedException)
            {
            }
        }

        private void showPalDesigns()
        {
            if (_kullaniciAdi == "")
            {
                Login login = new Login(_pekpanDal);
                if (login.ShowDialog() == DialogResult.OK)
                {
                    _kullaniciAdi = login._kullaniciAdi;
                    lbKullaniciAdi.Text = "Operatör : " + _pekpanDal.ExecuteScalarStr("SELECT DBO.TRK(ISIM) FROM TBLMRPISCI WHERE SICILNO = '" + login._kullaniciAdi + "'");
                    Designs designs = new Designs(_pekpanDal, login._kullaniciAdi);
                    designs.ShowDialog();
                }
            }
            else
            {
                Designs designs = new Designs(_pekpanDal, _kullaniciAdi);
                designs.ShowDialog();
            }
        }

        public void takeCsvDatas()
        {
            gvIsEmri.Rows.Clear();
            using (SqlDataReader rdr = _pekpanDal.GetDataReader("select *,(select sum(miktar) from TARA_uretimPaket where isEmriNo = I.IsEmriNo) uretim from TARA_IsEmri I where statu <>2 order by TARIH"/*"SELECT * FROM TARA_IsEmri where statu <>2 order by TARIH"*/))
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
                        row.Cells[8].Value = rdr["GELEN"].ToString();
                        row.Cells[9].Value = rdr["URETIM"].ToString();
                        gvIsEmri.Rows.Add(row);
                    }
                }
            }
            for (int i = 0; i < gvIsEmri.Rows.Count; i++)
            {
                if ("0" != _pekpanDal.ExecuteScalarStr("SELECT count(*) FROM TARA_HucreDurum where stokKodu = (select STOK_KODU from tblisemri where isEmrino = '" + gvIsEmri.Rows[i].Cells[1].Value + "') and statu = 1"))
                    gvIsEmri.Rows[i].DefaultCellStyle.BackColor = Color.Lime;
            }
            for (int i = 0; i < gvIsEmri.Rows.Count; i++)
            {
                if ("1" == _pekpanDal.ExecuteScalarStr("SELECT COUNT(Y.ID) FROM TARA_IsEmri I LEFT JOIN (SELECT D.ID, D.GenislikMin, D.GenislikMax, D.BoyMin, D.BoyMax, M.MarkaAciklama, MA.MarkaKod FROM TARA_PalDesign D LEFT JOIN TARA_PalMarka M (NOLOCK) ON M.DesignID = D.ID LEFT JOIN TARA_Markalar MA (NOLOCK) ON MA.MarkaAciklama=M.MarkaAciklama ) Y ON (I.BOY BETWEEN Y.BoyMin AND Y.BoyMax) AND (I.GENISLIK BETWEEN Y.GenislikMin AND Y.GenislikMax) AND RIGHT(I.STOK_KODU,7) = Y.MarkaKod WHERE I.ISEMRINO = '" + gvIsEmri.Rows[i].Cells[1].Value + "' AND I.ISEMRINO NOT IN (SELECT ISEMRI FROM TARA_HucreDurum)"))
                    gvIsEmri.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;
            }
            for (int i = 0; i < gvIsEmri.Rows.Count; i++)
            {
                if ("0" == _pekpanDal.ExecuteScalarStr("SELECT COUNT(Y.ID) FROM TARA_IsEmri I LEFT JOIN (SELECT D.ID, D.GenislikMin, D.GenislikMax, D.BoyMin, D.BoyMax, M.MarkaAciklama, MA.MarkaKod FROM TARA_PalDesign D LEFT JOIN TARA_PalMarka M (NOLOCK) ON M.DesignID = D.ID LEFT JOIN TARA_Markalar MA (NOLOCK) ON MA.MarkaAciklama=M.MarkaAciklama) Y ON (I.BOY BETWEEN Y.BoyMin AND Y.BoyMax) AND (I.GENISLIK BETWEEN Y.GenislikMin AND Y.GenislikMax) AND RIGHT(I.STOK_KODU,7) = Y.MarkaKod WHERE I.ISEMRINO = '" + gvIsEmri.Rows[i].Cells[1].Value + "'"))
                    gvIsEmri.Rows[i].DefaultCellStyle.BackColor = Color.Red;
            }
        }

        public void adjustAcilisOrders(string index)
        {
            _hucre[Convert.ToInt32(index)].statu = 2;
            string designID = _pekpanDal.ExecuteScalarStr("SELECT Y.ID FROM TARA_IsEmri I LEFT JOIN (SELECT D.ID, D.GenislikMin, D.GenislikMax, D.BoyMin, D.BoyMax, M.MarkaAciklama, MA.MarkaKod FROM TARA_PalDesign D LEFT JOIN TARA_PalMarka M (NOLOCK) ON M.DesignID = D.ID LEFT JOIN TARA_Markalar MA (NOLOCK) ON MA.MarkaAciklama=M.MarkaAciklama) Y ON (I.BOY BETWEEN Y.BoyMin AND Y.BoyMax) AND (I.GENISLIK BETWEEN Y.GenislikMin AND Y.GenislikMax) AND RIGHT(I.STOK_KODU,7) = Y.MarkaKod WHERE I.ISEMRINO = '" + turnLA(index).Text + "'");
            TARA_HucreDurumUpdate(_hucre[Convert.ToInt32(index)]);
            List<int> adjustAcilisOrder = _pekpanDal.GetDataReaderListInt("select hucreNo from TARA_HucreDurum where designID = " + designID + " and statu = 1 and stokKodu='" + _hucre[Convert.ToInt32(index)].stokKodu + "' order by acilisOrder");
            for (int k = 0; k < adjustAcilisOrder.Count; k++)
            {
                _hucre[adjustAcilisOrder[k]].acilisOrder = k;
                TARA_HucreDurumUpdate(_hucre[adjustAcilisOrder[k]]);
            }
            if (Convert.ToInt32(index) < 5)
            {
                adjustAcilisOrder.Clear();
                adjustAcilisOrder = _pekpanDal.GetDataReaderListInt("select hucreNo from TARA_HucreDurum where designID = " + designID + " and statu = 1 and stokKodu='" + _hucre[Convert.ToInt32(index)].stokKodu + "' and (hucreNo=1 OR hucreNo=2 OR hucreNo=3 OR hucreNo=4) order by acilisOrderKM1");
                for (int k = 0; k < adjustAcilisOrder.Count; k++)
                {
                    _hucre[adjustAcilisOrder[k]].acilisOrderKM1 = k;
                    TARA_HucreDurumUpdate(_hucre[adjustAcilisOrder[k]]);
                }
            }
            if (Convert.ToInt32(index) > 4)
            {
                adjustAcilisOrder.Clear();
                adjustAcilisOrder = _pekpanDal.GetDataReaderListInt("select hucreNo from TARA_HucreDurum where designID = " + designID + " and statu = 1 and stokKodu='" + _hucre[Convert.ToInt32(index)].stokKodu + "' and (hucreNo=5 OR hucreNo=6 OR hucreNo=7 OR hucreNo=8) order by acilisOrderKM2");
                for (int k = 0; k < adjustAcilisOrder.Count; k++)
                {
                    _hucre[adjustAcilisOrder[k]].acilisOrderKM2 = k;
                    TARA_HucreDurumUpdate(_hucre[adjustAcilisOrder[k]]);
                }
            }
        }

        private void setPallet(string index, Hucre hucre)
        {
            turnLA(index).Text = hucre.isEmri;
            turnLB(index).Text = hucre.designAciklama;
            turnLC(index).Text = hucre.urunAciklama;
            turnLM(index).Text = 0 + " / " + hucre.kapasite;
            turnXB(index).Maximum = hucre.kapasite;
            turnPB(index).Maximum = hucre.kapasite;
            turnXB(index).Value = 0;
            turnPB(index).Value = 0;
            turnPN(index).Visible = true;
        }

        public void calculateInitialDensities()
        {
            for (int i = 1; i < 9; i++)
            {
                _density1 = (i < 5) ? _density1 + _hucre[i].urunAdet - _hucre[i].palUrunAdet : _density1;
                _density2 = (i > 4) ? _density2 + _hucre[i].urunAdet - _hucre[i].palUrunAdet : _density2;
            }
        }
    }
}
