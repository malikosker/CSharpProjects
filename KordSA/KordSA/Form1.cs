using System;
using System.IO;
using System.Collections.Generic;
using Microsoft.VisualBasic.FileIO;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using EasyModbus;

namespace KordSA
{
    public partial class Form1 : Form
    {
        System.Timers.Timer _independent = new System.Timers.Timer();
        Plc _plc = new Plc("192.168.0.1", 502);
        TaksPlusDal _taksplus = new TaksPlusDal();
        AutoBarcode _autoBardcode;
        MobileBarcode _mobileBarcode;
        Indicator _indicator;
        StickerMachine _stickerMachine;

        string _barcode1;
        string _barcode2;
        public bool tryAgain = false;
        public bool programmaticalPress = false;
        public bool timer1Enabled = false;
        public bool timer2Enabled = false;
        Urun[] uruns = new Urun[4];
        Point _point1 = new Point(721, 475);
        Point _point2 = new Point(726, 220);
        Point _point3 = new Point(403, 220);
        Point _point4 = new Point(126, 220);
        Point _mpoint1 = new Point(721, 325);
        Point _mpoint2 = new Point(726, 70);
        Point _mpoint3 = new Point(403, 70);
        Point _mpoint4 = new Point(126, 70);

        public Form1()
        {
            InitializeComponent();
            _plc.mainForm = this;

            this.pnlManualMode.Size = new Size(921, 600);
            this.btnLightColumn1Buzzer.BackgroundImage = ımageList1.Images[0];
            this.btnLightColumn2Buzzer.BackgroundImage = ımageList1.Images[0];
            this.btnLightColumn3Buzzer.BackgroundImage = ımageList1.Images[0];
            adjustLocation();
        }

        private void _independent_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                string binary = Convert.ToString((ushort)_plc.sensor + 8, 2).Remove(0, 1);
                this.Invoke(new Action(() => pbSensor1.Visible = (binary[2] == '1') ? true : false));
                this.Invoke(new Action(() => pbSensor2.Visible = (binary[1] == '1') ? true : false));
                this.Invoke(new Action(() => pbSensor3.Visible = (binary[0] == '1') ? true : false));
            }
            catch (System.ObjectDisposedException)
            {
            }
        }

        private void adjustLocation(int x = 0, int y = 0)
        {
            this.btnStart.Location = new System.Drawing.Point(70, 5);
            this.btnAutoMode.Location = new System.Drawing.Point(150, 5);
            this.btnManualMode.Location = new System.Drawing.Point(288, 5);
            this.btnReset.Location = new System.Drawing.Point(426, 5);
            this.btnExit.Location = new System.Drawing.Point(880, 5);
            this.lblMessage.Location = new System.Drawing.Point(70, 55);
            this.sevenSegmentArray1.Location = new System.Drawing.Point(950, 12);
            this.pnlManualMode.Location = new System.Drawing.Point(0, 152);
            this.btnConveyor1.Location = new System.Drawing.Point(773, 348);
            this.btnConveyor2.Location = new System.Drawing.Point(720, 123);
            this.btnConveyor3.Location = new System.Drawing.Point(620, 123);
            this.btnConveyor4.Location = new System.Drawing.Point(310, 123);
            this.btnConveyorTurn.Location = new System.Drawing.Point(830, 123);
            this.btnLightColumn1Red.Location = new System.Drawing.Point(89, 298);
            this.btnLightColumn1Yellow.Location = new System.Drawing.Point(89, 360);
            this.btnLightColumn1Green.Location = new System.Drawing.Point(89, 422);
            this.btnLightColumn1Buzzer.Location = new System.Drawing.Point(89, 484);
            this.btnLightColumn2Red.Location = new System.Drawing.Point(189, 360);
            this.btnLightColumn2Green.Location = new System.Drawing.Point(189, 422);
            this.btnLightColumn2Buzzer.Location = new System.Drawing.Point(189, 484);
            this.btnLightColumn3Red.Location = new System.Drawing.Point(289, 360);
            this.btnLightColumn3Green.Location = new System.Drawing.Point(289, 422);
            this.btnLightColumn3Buzzer.Location = new System.Drawing.Point(289, 484);
            this.lblBarkod.Location = new System.Drawing.Point(75, 538);
            this.lblWeighing.Location = new System.Drawing.Point(175, 538);
            this.lblSystemReady.Location = new System.Drawing.Point(265, 538);
            this.pbxMarka.Location = new System.Drawing.Point(1160, 610);
            this.pbSensor1.Location = new System.Drawing.Point(740, 273);
            this.pbSensor2.Location = new System.Drawing.Point(660, 95);
            this.pbSensor3.Location = new System.Drawing.Point(740, 25);
            this.lbSil.Location = new System.Drawing.Point(440, 319);
            this.nmConveyor.Location = new System.Drawing.Point(530, 317);
            this.btnSil.Location = new System.Drawing.Point(439, 352);

        }

        private void adjustLocation2(int x = 0, int y = 0)
        {
            this.btnLightColumn1Red.Location = new System.Drawing.Point(89 + x, 298 + y);
            this.btnLightColumn1Yellow.Location = new System.Drawing.Point(89 + x, 360 + y);
            this.btnLightColumn1Green.Location = new System.Drawing.Point(89 + x, 422 + y);
            this.btnLightColumn1Buzzer.Location = new System.Drawing.Point(89 + x, 484 + y);
            this.btnLightColumn2Red.Location = new System.Drawing.Point(189 + x, 360 + y);
            this.btnLightColumn2Green.Location = new System.Drawing.Point(189 + x, 422 + y);
            this.btnLightColumn2Buzzer.Location = new System.Drawing.Point(189 + x, 484 + y);
            this.btnLightColumn3Red.Location = new System.Drawing.Point(289 + x, 360 + y);
            this.btnLightColumn3Green.Location = new System.Drawing.Point(289 + x, 422 + y);
            this.btnLightColumn3Buzzer.Location = new System.Drawing.Point(289 + x, 484 + y);
            this.lblBarkod.Location = new System.Drawing.Point(75 + x, 538 + y);
            this.lblWeighing.Location = new System.Drawing.Point(175 + x, 538 + y);
            this.lblSystemReady.Location = new System.Drawing.Point(265 + x, 538 + y);

            if (btnAutoMode.BackColor == Color.Lime)
            {
                pnlManualMode.Controls.Remove(btnLightColumn1Red);
                pnlManualMode.Controls.Remove(btnLightColumn1Yellow);
                pnlManualMode.Controls.Remove(btnLightColumn1Green);
                pnlManualMode.Controls.Remove(btnLightColumn1Buzzer);
                pnlManualMode.Controls.Remove(btnLightColumn2Red);
                pnlManualMode.Controls.Remove(btnLightColumn2Green);
                pnlManualMode.Controls.Remove(btnLightColumn2Buzzer);
                pnlManualMode.Controls.Remove(btnLightColumn3Red);
                pnlManualMode.Controls.Remove(btnLightColumn3Green);
                pnlManualMode.Controls.Remove(btnLightColumn3Buzzer);
                pnlManualMode.Controls.Remove(lblBarkod);
                pnlManualMode.Controls.Remove(lblWeighing);
                pnlManualMode.Controls.Remove(lblSystemReady);

                this.Controls.Add(btnLightColumn1Red);
                this.Controls.Add(btnLightColumn1Yellow);
                this.Controls.Add(btnLightColumn1Green);
                this.Controls.Add(btnLightColumn1Buzzer);
                this.Controls.Add(btnLightColumn2Red);
                this.Controls.Add(btnLightColumn2Green);
                this.Controls.Add(btnLightColumn2Buzzer);
                this.Controls.Add(btnLightColumn3Red);
                this.Controls.Add(btnLightColumn3Green);
                this.Controls.Add(btnLightColumn3Buzzer);
                this.Controls.Add(lblBarkod);
                this.Controls.Add(lblWeighing);
                this.Controls.Add(lblSystemReady);
            }
            else if (btnManualMode.BackColor == Color.Lime)
            {
                this.Controls.Remove(btnLightColumn1Red);
                this.Controls.Remove(btnLightColumn1Yellow);
                this.Controls.Remove(btnLightColumn1Green);
                this.Controls.Remove(btnLightColumn1Buzzer);
                this.Controls.Remove(btnLightColumn2Red);
                this.Controls.Remove(btnLightColumn2Green);
                this.Controls.Remove(btnLightColumn2Buzzer);
                this.Controls.Remove(btnLightColumn3Red);
                this.Controls.Remove(btnLightColumn3Green);
                this.Controls.Remove(btnLightColumn3Buzzer);
                this.Controls.Remove(lblBarkod);
                this.Controls.Remove(lblWeighing);
                this.Controls.Remove(lblSystemReady);

                pnlManualMode.Controls.Add(btnLightColumn1Red);
                pnlManualMode.Controls.Add(btnLightColumn1Yellow);
                pnlManualMode.Controls.Add(btnLightColumn1Green);
                pnlManualMode.Controls.Add(btnLightColumn1Buzzer);
                pnlManualMode.Controls.Add(btnLightColumn2Red);
                pnlManualMode.Controls.Add(btnLightColumn2Green);
                pnlManualMode.Controls.Add(btnLightColumn2Buzzer);
                pnlManualMode.Controls.Add(btnLightColumn3Red);
                pnlManualMode.Controls.Add(btnLightColumn3Green);
                pnlManualMode.Controls.Add(btnLightColumn3Buzzer);
                pnlManualMode.Controls.Add(lblBarkod);
                pnlManualMode.Controls.Add(lblWeighing);
                pnlManualMode.Controls.Add(lblSystemReady);
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _independent.Elapsed += _independent_Elapsed;
            _independent.Enabled = true;

            sevenSegmentArray1.Value = "0.0";
        again:
            _plc.writePlc((int)datas.barkodeAndWeight, 0);
            if (_plc.IOException == true)
            {
                _plc.IOException = false;
                goto again;
            }

            _autoBardcode = new AutoBarcode();
            _mobileBarcode = new MobileBarcode(serialPortMobileBarcod);
            _indicator = new Indicator(serialPortIndicator, sevenSegmentArray1);
            _stickerMachine = new StickerMachine(serialPortStickerMachine);

            _taksplus.ConnectToDB("Server IP", "userName", "pass", "dataBase");
            _mobileBarcode.open();
            _indicator.open();
            _stickerMachine.open();


            //EVENTS SUBSCRIPTIONS
            _mobileBarcode.DataEvent += _mobileBarcode_DataEvent;
            _plc.BtnStartEvent += _plc_BtnStartEvent;
            _plc.LightColumnBarcodeYellowEvent += _plc_LightColumnBarcodeYellowEvent;
            _plc.LightColumnBarcodeGreenEvent += _plc_LightColumnBarcodeGreenEvent;
            _plc.LightColumnBarcodeRedEvent += _plc_LightColumnBarcodeRedEvent;
            _plc.LightColumnBarcodeBuzzerEvent += _plc_LightColumnBarcodeBuzzerEvent;
            _plc.LightColumnWeighingGreenEvent += _plc_LightColumnWeighingGreenEvent;
            _plc.LightColumnWeighinhRedEvent += _plc_LightColumnWeighinhRedEvent;
            _plc.LightColumnWeighingBuzzerEvent += _plc_LightColumnWeighingBuzzerEvent;
            _plc.LightColumnReadyGreenEvent += _plc_LightColumnReadyGreenEvent;
            _plc.LightColumnReadyRedEvent += _plc_LightColumnReadyRedEvent;
            _plc.LightColumnReadyBuzzerEvent += _plc_LightColumnReadyBuzzerEvent;

            _plc.StreclemeSonucEvent += _plc_StreclemeSonucEvent;
            _plc.StreclemeHataEvent += _plc_StreclemeHataEvent;
            _plc.EtiketSonucEvent += _plc_EtiketSonucEvent;
            _plc.EtiketHataEvent += _plc_EtiketHataEvent;
            _plc.MovingEvent += _plc_MovingEvent;
            _plc.SecurityEvent += _plc_SecurityEvent;
            _plc.AutomaticModeActiveEvent += _plc_AutomaticModeActiveEvent;
            readDataBase();

            _stickerMachine.sendData("#!A1#!XM1007");
            Thread.Sleep(20);
            string readedData = _stickerMachine.readData();
            if (readedData.Length > 0 && readedData == "0\n")
            {
                _stickerMachine.sendData(readDataBaseForSticker());
            }
        }

        enum inputs
        {
            lightBarrier1,
            lightBarrier2,
            btnStart,
            btnReset,
            btnEmergency1,
            btnEmergency2,
            rfSensor1,
            rfSensor2,
            rfSensor3,
            rfSensor4,
            door1,
            door2
        }
        enum outputs
        {
            conveyor1 = 12,
            conveyor2,
            conveyor3,
            conveyor4,
            turntable,
            lightColumnBarcodeYellow,
            lightColumnBarcodeGreen,
            lightColumnBarcodeRed,
            lightColumnBarcodeBuzzer,
            lightColumnWeighingGreen,
            lightColumnWeighingRed,
            lightColumnWeighingBuzzer,
            lightColumnReadyGreen,
            lightColumnReadyRed,
            lightColumnReadyBuzzer
        }
        enum datas
        {
            barkodeAndWeight = 27,
            streclemeSonuc,
            streclemeHata,
            etiketSonuc,
            etiketHata,
            moving,
            mode,
            security,
            automaticModeActive
        }

        private void btnStart_MouseDown(object sender, MouseEventArgs e)
        {
            if (btnAutoMode.BackColor != Color.Lime)
                btnStart.BackColor = Color.Lime;
        }

        private void btnStart_MouseUp(object sender, MouseEventArgs e)
        {
            _stickerMachine.sendData("#!A1#!XM1007");
            Thread.Sleep(15);
            if (_stickerMachine.readData().Length > 0)
            {
                if (((uruns[0] != null && (uruns[0].Location == _point1 || uruns[0].Location == _mpoint1)) || (uruns[1] != null && (uruns[1].Location == _point1 || uruns[1].Location == _mpoint1)) || (uruns[2] != null && (uruns[2].Location == _point1 || uruns[2].Location == _mpoint1)) || (uruns[3] != null && (uruns[3].Location == _point1 || uruns[3].Location == _mpoint1))))
                {
                    double tarti = Convert.ToDouble(sevenSegmentArray1.Value);
                    if (tarti > 5)
                    {
                        if (btnManualMode.BackColor != Color.Lime)
                        {
                            plcListener.Enabled = false;
                            if (tryAgain == false)
                            {
                                if ((_barcode1 != null && _barcode2 != null) && (_barcode1 != "000000000" && _barcode2 != "000000000000"))
                                {
                                    if (_taksplus.ExecuteNonQuerySilent(_taksplus.ilkInsert(_barcode1, sevenSegmentArray1.Value.ToString())))//
                                    {
                                        lblMessage.Text = "TAKS'tan onay bekleniyor...";
                                        dataBaseListener.Enabled = true;
                                    }
                                    Thread.Sleep(200);
                                    btnStart.BackColor = Color.DarkGreen;
                                    plcListener.Enabled = true;
                                    return;
                                }
                                _autoBardcode.runSabitBarcod();
                                _barcode1 = _autoBardcode._barcode1;
                                _barcode2 = _autoBardcode._barcode2;
                                if (_barcode1 != null && _barcode2 != null && _barcode1.Length == 9 && _barcode2.Length == 12)
                                {
                                    if (uruns[0] != null && uruns[0].Location == _point1)
                                    {
                                        uruns[0].refBarcode.Text = _barcode1;
                                        uruns[0].bobinBarcode.Text = _barcode2;
                                    }
                                    else if (uruns[1] != null && uruns[1].Location == _point1)
                                    {
                                        uruns[1].refBarcode.Text = _barcode1;
                                        uruns[1].bobinBarcode.Text = _barcode2;
                                    }
                                    else if (uruns[2] != null && uruns[2].Location == _point1)
                                    {
                                        uruns[2].refBarcode.Text = _barcode1;
                                        uruns[2].bobinBarcode.Text = _barcode2;
                                    }
                                    else if (uruns[3] != null && uruns[3].Location == _point1)
                                    {
                                        uruns[3].refBarcode.Text = _barcode1;
                                        uruns[3].bobinBarcode.Text = _barcode2;
                                    }
                                    if (_taksplus.ExecuteScalarStr(_taksplus.ilkSorgu(_barcode1, _barcode2)) == "1")//
                                    {
                                        if (uruns[0] != null && uruns[0].Location == _point1)
                                        {
                                            uruns[0].refBarcode.BackColor = Color.Lime;
                                            uruns[0].bobinBarcode.BackColor = Color.Lime;
                                        }
                                        else if (uruns[1] != null && uruns[1].Location == _point1)
                                        {
                                            uruns[1].refBarcode.BackColor = Color.Lime;
                                            uruns[1].bobinBarcode.BackColor = Color.Lime;
                                        }
                                        else if (uruns[2] != null && uruns[2].Location == _point1)
                                        {
                                            uruns[2].refBarcode.BackColor = Color.Lime;
                                            uruns[2].bobinBarcode.BackColor = Color.Lime;
                                        }
                                        else if (uruns[3] != null && uruns[3].Location == _point1)
                                        {
                                            uruns[3].refBarcode.BackColor = Color.Lime;
                                            uruns[3].bobinBarcode.BackColor = Color.Lime;
                                        }
                                        if (_taksplus.ExecuteNonQuerySilent(_taksplus.ilkInsert(_barcode1, sevenSegmentArray1.Value.ToString())))//
                                        {
                                            lblMessage.Text = "TAKS'tan onay bekleniyor...";
                                            dataBaseListener.Enabled = true;
                                        }
                                        //_barcode1 = null;
                                        //_barcode2 = null;
                                    }
                                    else
                                    {
                                        if (uruns[0] != null && uruns[0].Location == _point1)
                                        {
                                            uruns[0].refBarcode.BackColor = Color.Red;
                                            uruns[0].bobinBarcode.BackColor = Color.Red;
                                        }
                                        else if (uruns[1] != null && uruns[1].Location == _point1)
                                        {
                                            uruns[1].refBarcode.BackColor = Color.Red;
                                            uruns[1].bobinBarcode.BackColor = Color.Red;
                                        }
                                        else if (uruns[2] != null && uruns[2].Location == _point1)
                                        {
                                            uruns[2].refBarcode.BackColor = Color.Red;
                                            uruns[2].bobinBarcode.BackColor = Color.Red;
                                        }
                                        else if (uruns[3] != null && uruns[3].Location == _point1)
                                        {
                                            uruns[3].refBarcode.BackColor = Color.Red;
                                            uruns[3].bobinBarcode.BackColor = Color.Red;
                                        }
                                    again:
                                        _plc.writePlc((int)datas.barkodeAndWeight, 3);
                                        if (_plc.IOException == true)
                                        {
                                            _plc.IOException = false;
                                            goto again;
                                        }
                                        _barcode1 = null;
                                        _barcode2 = null;
                                    }
                                }
                                else
                                {
                                    if (uruns[0] != null && uruns[0].Location == _point1)
                                    {
                                        uruns[0].refBarcode.BackColor = Color.Yellow;
                                        uruns[0].bobinBarcode.BackColor = Color.Yellow;
                                    }
                                    else if (uruns[1] != null && uruns[1].Location == _point1)
                                    {
                                        uruns[1].refBarcode.BackColor = Color.Yellow;
                                        uruns[1].bobinBarcode.BackColor = Color.Yellow;
                                    }
                                    else if (uruns[2] != null && uruns[2].Location == _point1)
                                    {
                                        uruns[2].refBarcode.BackColor = Color.Yellow;
                                        uruns[2].bobinBarcode.BackColor = Color.Yellow;
                                    }
                                    else if (uruns[3] != null && uruns[3].Location == _point1)
                                    {
                                        uruns[3].refBarcode.BackColor = Color.Yellow;
                                        uruns[3].bobinBarcode.BackColor = Color.Yellow;
                                    }
                                    _barcode1 = null;
                                    _barcode2 = null;
                                again:
                                    _plc.writePlc((int)datas.barkodeAndWeight, 2);
                                    if (_plc.IOException == true)
                                    {
                                        _plc.IOException = false;
                                        goto again;
                                    }
                                    lblMessage.Text = "Sabit barkod okuyucu barkodları okuyamadı!!!\nLütfen mobil barkod okuyucuyu kullanın...";
                                }
                            }
                            else
                                tryAgain = false;
                            Thread.Sleep(200);
                            btnStart.BackColor = Color.DarkGreen;
                            this.Invoke(new Action(() => plcListener.Enabled = true));
                            writeToDataBase();
                        }
                        else if (btnManualMode.BackColor == Color.Lime)
                        {
                            MessageBox.Show("Manual mod aktif iken sistem çalıştırılamaz !", "Uygulama Hatası", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    else
                    {
                        DialogResult dialog = MessageBox.Show("Lütfen tamama bastıktan sonra ürünü tekrar tartın.", "Tartı Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
            }
            else
            {
                DialogResult dialog = MessageBox.Show("Etiket makinası açık değil lütfen kontrol ediniz.", "Etiket Makinası Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAutoMode_Click(object sender, EventArgs e)
        {
            if (btnAutoMode.BackColor != Color.Lime)
            {
                if (_plc.automaticModeActive != 1)
                {
                again:
                    programmaticalPress = true;
                    if (_plc.IOException == true)
                    {
                        _plc.IOException = false;
                        goto again;
                    }
                    _plc.writePlc((int)datas.mode, 1);
                    Thread.Sleep(5000);
                again2:
                    _plc.writePlc((int)datas.mode, 0);
                    if (_plc.IOException == true)
                    {
                        _plc.IOException = false;
                        goto again2;
                    }
                }
                if (tryAgain == false)
                {
                    btnAutoMode.BackColor = Color.Lime;
                    btnManualMode.BackColor = Color.Red;
                    pnlManualMode.Visible = false;
                    //adjustLocation2(0,600);//burası ayarlanacak
                    lblMessage.Text = "Otomatik mod aktif\nYeni ürün bekleniyor...";

                    if (uruns[0] != null)
                    {
                        pnlManualMode.Controls.Remove(uruns[0]);
                        if (uruns[0].Location == _mpoint1)
                            uruns[0].Location = _point1;
                        else if (uruns[0].Location == _mpoint2)
                            uruns[0].Location = _point2;
                        else if (uruns[0].Location == _mpoint3)
                            uruns[0].Location = _point3;
                        else if (uruns[0].Location == _mpoint4)
                            uruns[0].Location = _point4;
                        this.Controls.Add(uruns[0]);
                    }
                    if (uruns[1] != null)
                    {
                        pnlManualMode.Controls.Remove(uruns[1]);
                        if (uruns[1].Location == _mpoint1)
                            uruns[1].Location = _point1;
                        else if (uruns[1].Location == _mpoint2)
                            uruns[1].Location = _point2;
                        else if (uruns[1].Location == _mpoint3)
                            uruns[1].Location = _point3;
                        else if (uruns[1].Location == _mpoint4)
                            uruns[1].Location = _point4;
                        this.Controls.Add(uruns[1]);
                    }
                    if (uruns[2] != null)
                    {
                        pnlManualMode.Controls.Remove(uruns[2]);
                        if (uruns[2].Location == _mpoint1)
                            uruns[2].Location = _point1;
                        else if (uruns[2].Location == _mpoint2)
                            uruns[2].Location = _point2;
                        else if (uruns[2].Location == _mpoint3)
                            uruns[2].Location = _point3;
                        else if (uruns[2].Location == _mpoint4)
                            uruns[2].Location = _point4;
                        this.Controls.Add(uruns[2]);
                    }
                    if (uruns[3] != null)
                    {
                        pnlManualMode.Controls.Remove(uruns[3]);
                        if (uruns[3].Location == _mpoint1)
                            uruns[3].Location = _point1;
                        else if (uruns[3].Location == _mpoint2)
                            uruns[3].Location = _point2;
                        else if (uruns[3].Location == _mpoint3)
                            uruns[3].Location = _point3;
                        else if (uruns[3].Location == _mpoint4)
                            uruns[3].Location = _point4;
                        this.Controls.Add(uruns[3]);
                    }
                }
                else
                    tryAgain = false;
            }
        }

        private void btnManualMode_Click(object sender, EventArgs e)
        {
            if (_plc.automaticModeActive != 0)
            {
                programmaticalPress = true;
            again:
                _plc.writePlc((int)datas.mode, 2);
                if (_plc.IOException == true)
                {
                    _plc.IOException = false;
                    goto again;
                }
            }
            if (tryAgain == false)
            {
                btnManualMode.BackColor = Color.Lime;
                btnAutoMode.BackColor = Color.Red;
                pnlManualMode.Visible = true;
                pnlManualMode.BackColor = Color.Transparent;
                //adjustLocation2();//burası ayarlanacak
                lblMessage.Text = "Manual mod aktif";

                if (uruns[0] != null)
                {
                    this.Controls.Remove(uruns[0]);
                    if (uruns[0].Location == _point1)
                        uruns[0].Location = _mpoint1;
                    else if (uruns[0].Location == _point2)
                        uruns[0].Location = _mpoint2;
                    else if (uruns[0].Location == _point3)
                        uruns[0].Location = _mpoint3;
                    else if (uruns[0].Location == _point4)
                        uruns[0].Location = _mpoint4;
                    pnlManualMode.Controls.Add(uruns[0]);
                }
                if (uruns[1] != null)
                {
                    this.Controls.Remove(uruns[1]);
                    if (uruns[1].Location == _point1)
                        uruns[1].Location = _mpoint1;
                    else if (uruns[1].Location == _point2)
                        uruns[1].Location = _mpoint2;
                    else if (uruns[1].Location == _point3)
                        uruns[1].Location = _mpoint3;
                    else if (uruns[1].Location == _point4)
                        uruns[1].Location = _mpoint4;
                    pnlManualMode.Controls.Add(uruns[1]);
                }
                if (uruns[2] != null)
                {
                    this.Controls.Remove(uruns[2]);
                    if (uruns[2].Location == _point1)
                        uruns[2].Location = _mpoint1;
                    else if (uruns[2].Location == _point2)
                        uruns[2].Location = _mpoint2;
                    else if (uruns[2].Location == _point3)
                        uruns[2].Location = _mpoint3;
                    else if (uruns[2].Location == _point4)
                        uruns[2].Location = _mpoint4;
                    pnlManualMode.Controls.Add(uruns[2]);
                }
                if (uruns[3] != null)
                {
                    this.Controls.Remove(uruns[3]);
                    if (uruns[3].Location == _point1)
                        uruns[3].Location = _mpoint1;
                    else if (uruns[3].Location == _point2)
                        uruns[3].Location = _mpoint2;
                    else if (uruns[3].Location == _point3)
                        uruns[3].Location = _mpoint3;
                    else if (uruns[3].Location == _point4)
                        uruns[3].Location = _mpoint4;
                    pnlManualMode.Controls.Add(uruns[3]);
                }
            }
            else
                tryAgain = false;
        }

        private void btnReset_MouseDown(object sender, MouseEventArgs e)
        {
            if (btnAutoMode.BackColor == Color.Lime)
                plcListener.Enabled = false;
            again:
            _plc.writePlc((int)inputs.btnReset, 1);
            if (_plc.IOException == true)
            {
                _plc.IOException = false;
                goto again;
            }
            if (tryAgain == false)
            {
                btnReset.BackColor = Color.Yellow;
                lblMessage.Text = "Sistem güvenli...";
            }
            else
                tryAgain = false;
        }

        private void btnReset_MouseUp(object sender, MouseEventArgs e)//tryAgain kontrolü yapılmamış
        {
            Thread.Sleep(200);
            btnReset.BackColor = Color.DarkGoldenrod;
        again:
            _plc.writePlc((int)inputs.btnReset, 0);
            if (_plc.IOException == true)
            {
                _plc.IOException = false;
                goto again;
            }
            if (btnAutoMode.BackColor == Color.Lime)
                plcListener.Enabled = true;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            writeToDataBase();
            _independent.Enabled = false;
            plcListener.Enabled = false;
            Thread.Sleep(750);
        again:
            _plc.writePlc((int)datas.barkodeAndWeight, 5);
            if (_plc.IOException == true)
            {
                _plc.IOException = false;
                goto again;
            }
            Close();
        }

        private void plcListener_Tick(object sender, EventArgs e)
        {
            plcListener.Enabled = false;
            if (btnAutoMode.BackColor == Color.Lime)
            {
            again:
                _plc.readAllInputs();
                if (_plc.IOException == true)
                {
                    _plc.IOException = false;
                    goto again;
                }
            }
            else if (btnManualMode.BackColor == Color.Lime)
            {
            again1:
                _plc.readPlcManualMode();
                if (_plc.IOException == true)
                {
                    _plc.IOException = false;
                    goto again1;
                }
            }
            plcListener.Enabled = true;
        }
        int dataBaseCounter = 0;
        private void dataBaseListener_Tick(object sender, EventArgs e)
        {
            dataBaseListener.Enabled = false;
            if (dataBaseCounter < 5)
            {
                if (_taksplus.ExecuteScalarStr(_taksplus.ikinciSorgu("count(*)", "1", _barcode1)) == "1")//
                {
                    dataBaseCounter = 0;
                    if (uruns[0] != null && uruns[0].Location == _point1)
                    {
                        uruns[0].weight.Text = sevenSegmentArray1.Value.ToString() + " kg";
                        uruns[0].weight.BackColor = Color.Lime;
                        writeToDataBase();
                    }
                    else if (uruns[1] != null && uruns[1].Location == _point1)
                    {
                        uruns[1].weight.Text = sevenSegmentArray1.Value.ToString() + " kg";
                        uruns[1].weight.BackColor = Color.Lime;
                        writeToDataBase();
                    }
                    else if (uruns[2] != null && uruns[2].Location == _point1)
                    {
                        uruns[2].weight.Text = sevenSegmentArray1.Value.ToString() + " kg";
                        uruns[2].weight.BackColor = Color.Lime;
                        writeToDataBase();
                    }
                    else if (uruns[3] != null && uruns[3].Location == _point1)
                    {
                        uruns[3].weight.Text = sevenSegmentArray1.Value.ToString() + " kg";
                        uruns[3].weight.BackColor = Color.Lime;
                        writeToDataBase();
                    }

                again:
                    _plc.writePlc((int)datas.barkodeAndWeight, 1);
                    if (_plc.IOException == true)
                    {
                        _plc.IOException = false;
                        goto again;
                    }
                    List<string> DBdatas = _taksplus.GetDataReaderList(_taksplus.ikinciSorgu("TIP,REF,PAKET,GERCEKMERGE,DENYE,DTEX,DENIER,FILAMENT,PACKDATE,UNIT,REFERANS,TICAGKG,TICAGLB,TOPLAMAGKG,TOPLAMAGLB,SAPKOD", "1", _barcode1));
                    _stickerMachine.adjustProperties(DBdatas);
                    _stickerMachine.sendData("#!A1#!CA");
                    _stickerMachine.sendData(_stickerMachine.orderText(_stickerMachine._sticker));
                    _taksplus.ExecuteNonQuerySilent(_taksplus.update(_barcode1, 2.ToString()));
                    _barcode1 = null;
                    _barcode2 = null;
                    this.Invoke(new Action(() => lblMessage.Text = "Ürün onaylandı."));

                    string directory = Application.StartupPath + @"\Data Base";//database klasörü oluştur
                    string fileName = "stickerInformations" + ".dat";
                    string memoryPath = Path.Combine(directory, fileName);
                    using (StreamWriter sw = File.CreateText(memoryPath))
                    {
                        sw.WriteLine(_stickerMachine.orderText(_stickerMachine._sticker));
                    }
                    return;
                }
                dataBaseCounter++;
            }
            else
            {
                dataBaseCounter = 0;
                lblMessage.Text = "TAKS onay vermedi !\nÜrünü alınız";
                if (Convert.ToInt32(_taksplus.ExecuteScalarStr(_taksplus.ikinciSorgu("count(*)", "98", _barcode1))) > 0)
                {
                    DialogResult dialog = MessageBox.Show(_taksplus.ExecuteScalarStr(_taksplus.ikinciSorgu("hata", "98", _barcode1)));//top1 gerekebilir
                }
            again:
                _plc.writePlc((int)datas.barkodeAndWeight, 4);
                if (_plc.IOException == true)
                {
                    _plc.IOException = false;
                    goto again;
                }

                if (uruns[0] != null && (uruns[0].Location == _point1 || uruns[0].Location == _mpoint1))
                {
                    uruns[0].refBarcode.BackColor = Color.Red;
                    uruns[0].bobinBarcode.BackColor = Color.Red;
                    uruns[0].weight.BackColor = Color.Red;
                    uruns[0].etiket.BackColor = Color.Red;
                    uruns[0].strec.BackColor = Color.Red;
                    uruns[0].weight.Text = sevenSegmentArray1.Value.ToString() + " kg";
                    writeToDataBase();
                }
                else if (uruns[1] != null && (uruns[1].Location == _point1 || uruns[1].Location == _mpoint1))
                {
                    uruns[1].refBarcode.BackColor = Color.Red;
                    uruns[1].bobinBarcode.BackColor = Color.Red;
                    uruns[1].weight.BackColor = Color.Red;
                    uruns[1].etiket.BackColor = Color.Red;
                    uruns[1].strec.BackColor = Color.Red;
                    uruns[1].weight.Text = sevenSegmentArray1.Value.ToString() + " kg";
                    writeToDataBase();
                }
                else if (uruns[2] != null && (uruns[2].Location == _point1 || uruns[2].Location == _mpoint1))
                {
                    uruns[2].refBarcode.BackColor = Color.Red;
                    uruns[2].bobinBarcode.BackColor = Color.Red;
                    uruns[2].weight.BackColor = Color.Red;
                    uruns[2].etiket.BackColor = Color.Red;
                    uruns[2].strec.BackColor = Color.Red;
                    uruns[2].weight.Text = sevenSegmentArray1.Value.ToString() + " kg";
                    writeToDataBase();
                }
                else if (uruns[3] != null && (uruns[3].Location == _point1 || uruns[3].Location == _mpoint1))
                {
                    uruns[3].refBarcode.BackColor = Color.Red;
                    uruns[3].bobinBarcode.BackColor = Color.Red;
                    uruns[3].weight.BackColor = Color.Red;
                    uruns[3].etiket.BackColor = Color.Red;
                    uruns[3].strec.BackColor = Color.Red;
                    uruns[3].weight.Text = sevenSegmentArray1.Value.ToString() + " kg";
                    writeToDataBase();
                }
                _barcode1 = null;
                _barcode2 = null;
                return;
            }
            dataBaseListener.Enabled = true;
        }

        private void btnConveyor1_MouseDown(object sender, MouseEventArgs e)
        {
        again:
            _plc.writePlc((int)outputs.conveyor1, 1);
            if (_plc.IOException == true)
            {
                _plc.IOException = false;
                goto again;
            }
            if (!tryAgain)
            {
                btnConveyor1.Text = "ON";
                btnConveyor1.BackColor = Color.Lime;
            }
            else
                tryAgain = false;
        }

        private void btnConveyor1_MouseUp(object sender, MouseEventArgs e)
        {
        again:
            _plc.writePlc((int)outputs.conveyor1, 0);
            if (_plc.IOException == true)
            {
                _plc.IOException = false;
                goto again;
            }
            if (!tryAgain)
            {
                btnConveyor1.Text = "OFF";
                btnConveyor1.BackColor = Color.Red;
            }
            else
                tryAgain = false;
        }

        private void btnConveyor2_MouseDown(object sender, MouseEventArgs e)
        {
        again:
            _plc.writePlc((int)outputs.conveyor2, 1);
            if (_plc.IOException == true)
            {
                _plc.IOException = false;
                goto again;
            }
            if (!tryAgain)
            {
                btnConveyor2.Text = "ON";
                btnConveyor2.BackColor = Color.Lime;
            }
            else
                tryAgain = false;
        }

        private void btnConveyor2_MouseUp(object sender, MouseEventArgs e)
        {
        again:
            _plc.writePlc((int)outputs.conveyor2, 0);
            if (_plc.IOException == true)
            {
                _plc.IOException = false;
                goto again;
            }
            if (!tryAgain)
            {
                btnConveyor2.Text = "OFF";
                btnConveyor2.BackColor = Color.Red;
            }
            else
                tryAgain = false;
        }

        private void btnConveyor3_MouseDown(object sender, MouseEventArgs e)
        {
        again2:
            _plc.writePlc((int)outputs.conveyor3, 1);
            if (_plc.IOException == true)
            {
                _plc.IOException = false;
                goto again2;
            }
            if (!tryAgain)
            {
                btnConveyor3.Text = "ON";
                btnConveyor3.BackColor = Color.Lime;
            }
            else
                tryAgain = false;
        }

        private void btnConveyor3_MouseUp(object sender, MouseEventArgs e)
        {
        again:
            _plc.writePlc((int)outputs.conveyor3, 0);
            if (_plc.IOException == true)
            {
                _plc.IOException = false;
                goto again;
            }
            if (!tryAgain)
            {
                btnConveyor3.Text = "OFF";
                btnConveyor3.BackColor = Color.Red;
            }
            else
                tryAgain = false;
        }

        private void btnConveyor4_MouseDown(object sender, MouseEventArgs e)
        {
        again:
            _plc.writePlc((int)outputs.conveyor4, 1);
            if (_plc.IOException == true)
            {
                _plc.IOException = false;
                goto again;
            }
            if (!tryAgain)
            {
                btnConveyor4.Text = "ON";
                btnConveyor4.BackColor = Color.Lime;
            }
            else
                tryAgain = false;
        }

        private void btnConveyor4_MouseUp(object sender, MouseEventArgs e)
        {
        again:
            _plc.writePlc((int)outputs.conveyor4, 0);
            if (_plc.IOException == true)
            {
                _plc.IOException = false;
                goto again;
            }
            if (!tryAgain)
            {
                btnConveyor4.Text = "OFF";
                btnConveyor4.BackColor = Color.Red;
            }
            else
                tryAgain = false;
        }

        private void btnConveyorTurn_MouseDown(object sender, MouseEventArgs e)
        {
        again:
            _plc.writePlc((int)outputs.turntable, 1);
            if (_plc.IOException == true)
            {
                _plc.IOException = false;
                goto again;
            }
            if (!tryAgain)
            {
                btnConveyorTurn.Text = "TURN ON";
                btnConveyorTurn.BackColor = Color.Lime;
            }
            else
                tryAgain = false;
        }

        private void btnConveyorTurn_MouseUp(object sender, MouseEventArgs e)
        {
        again:
            _plc.writePlc((int)outputs.turntable, 0);
            if (_plc.IOException == true)
            {
                _plc.IOException = false;
                goto again;
            }
            if (!tryAgain)
            {
                btnConveyorTurn.Text = "TURN OFF";
                btnConveyorTurn.BackColor = Color.Red;
            }
            else
                tryAgain = false;
        }

        private void writeToDataBase()
        {
            string directory = Application.StartupPath + @"\Data Base";//database klasörü oluştur
            string fileName = "urunsInformations" + ".dat";
            string memoryPath = Path.Combine(directory, fileName);
            using (StreamWriter sw = File.CreateText(memoryPath))
            {
                if (uruns[0] == null)
                    sw.WriteLine("null;0;000000000;000000000000;0,0 kg;White;White;White;White;White");
                else if (uruns[0] != null && (uruns[0].Location == _point1 || uruns[0].Location == _mpoint1))
                    sw.WriteLine("notNull;1;" + uruns[0].refBarcode.Text + ";" + uruns[0].bobinBarcode.Text + ";" + uruns[0].weight.Text + ";" + uruns[0].refBarcode.BackColor.Name + ";" + uruns[0].bobinBarcode.BackColor.Name + ";" + uruns[0].weight.BackColor.Name + ";" + uruns[0].etiket.BackColor.Name + ";" + uruns[0].strec.BackColor.Name);
                else if (uruns[0] != null && (uruns[0].Location == _point2 || uruns[0].Location == _mpoint2))
                    sw.WriteLine("notNull;2;" + uruns[0].refBarcode.Text + ";" + uruns[0].bobinBarcode.Text + ";" + uruns[0].weight.Text + ";" + uruns[0].refBarcode.BackColor.Name + ";" + uruns[0].bobinBarcode.BackColor.Name + ";" + uruns[0].weight.BackColor.Name + ";" + uruns[0].etiket.BackColor.Name + ";" + uruns[0].strec.BackColor.Name);
                else if (uruns[0] != null && (uruns[0].Location == _point3 || uruns[0].Location == _mpoint3))
                    sw.WriteLine("notNull;3;" + uruns[0].refBarcode.Text + ";" + uruns[0].bobinBarcode.Text + ";" + uruns[0].weight.Text + ";" + uruns[0].refBarcode.BackColor.Name + ";" + uruns[0].bobinBarcode.BackColor.Name + ";" + uruns[0].weight.BackColor.Name + ";" + uruns[0].etiket.BackColor.Name + ";" + uruns[0].strec.BackColor.Name);
                else if (uruns[0] != null && (uruns[0].Location == _point4 || uruns[0].Location == _mpoint4))
                    sw.WriteLine("notNull;4;" + uruns[0].refBarcode.Text + ";" + uruns[0].bobinBarcode.Text + ";" + uruns[0].weight.Text + ";" + uruns[0].refBarcode.BackColor.Name + ";" + uruns[0].bobinBarcode.BackColor.Name + ";" + uruns[0].weight.BackColor.Name + ";" + uruns[0].etiket.BackColor.Name + ";" + uruns[0].strec.BackColor.Name);
                if (uruns[1] == null)
                    sw.WriteLine("null;0;000000000;000000000000;0,0 kg;White;White;White;White;White");
                else if (uruns[1] != null && (uruns[1].Location == _point1 || uruns[1].Location == _mpoint1))
                    sw.WriteLine("notNull;1;" + uruns[1].refBarcode.Text + ";" + uruns[1].bobinBarcode.Text + ";" + uruns[1].weight.Text + ";" + uruns[1].refBarcode.BackColor.Name + ";" + uruns[1].bobinBarcode.BackColor.Name + ";" + uruns[1].weight.BackColor.Name + ";" + uruns[1].etiket.BackColor.Name + ";" + uruns[1].strec.BackColor.Name);
                else if (uruns[1] != null && (uruns[1].Location == _point2 || uruns[1].Location == _mpoint2))
                    sw.WriteLine("notNull;2;" + uruns[1].refBarcode.Text + ";" + uruns[1].bobinBarcode.Text + ";" + uruns[1].weight.Text + ";" + uruns[1].refBarcode.BackColor.Name + ";" + uruns[1].bobinBarcode.BackColor.Name + ";" + uruns[1].weight.BackColor.Name + ";" + uruns[1].etiket.BackColor.Name + ";" + uruns[1].strec.BackColor.Name);
                else if (uruns[1] != null && (uruns[1].Location == _point3 || uruns[1].Location == _mpoint3))
                    sw.WriteLine("notNull;3;" + uruns[1].refBarcode.Text + ";" + uruns[1].bobinBarcode.Text + ";" + uruns[1].weight.Text + ";" + uruns[1].refBarcode.BackColor.Name + ";" + uruns[1].bobinBarcode.BackColor.Name + ";" + uruns[1].weight.BackColor.Name + ";" + uruns[1].etiket.BackColor.Name + ";" + uruns[1].strec.BackColor.Name);
                else if (uruns[1] != null && (uruns[1].Location == _point4 || uruns[1].Location == _mpoint4))
                    sw.WriteLine("notNull;4;" + uruns[1].refBarcode.Text + ";" + uruns[1].bobinBarcode.Text + ";" + uruns[1].weight.Text + ";" + uruns[1].refBarcode.BackColor.Name + ";" + uruns[1].bobinBarcode.BackColor.Name + ";" + uruns[1].weight.BackColor.Name + ";" + uruns[1].etiket.BackColor.Name + ";" + uruns[1].strec.BackColor.Name);
                if (uruns[2] == null)
                    sw.WriteLine("null;0;000000000;000000000000;0,0 kg;White;White;White;White;White");
                else if (uruns[2] != null && (uruns[2].Location == _point1 || uruns[2].Location == _mpoint1))
                    sw.WriteLine("notNull;1;" + uruns[2].refBarcode.Text + ";" + uruns[2].bobinBarcode.Text + ";" + uruns[2].weight.Text + ";" + uruns[2].refBarcode.BackColor.Name + ";" + uruns[2].bobinBarcode.BackColor.Name + ";" + uruns[2].weight.BackColor.Name + ";" + uruns[2].etiket.BackColor.Name + ";" + uruns[2].strec.BackColor.Name);
                else if (uruns[2] != null && (uruns[2].Location == _point2 || uruns[2].Location == _mpoint2))
                    sw.WriteLine("notNull;2;" + uruns[2].refBarcode.Text + ";" + uruns[2].bobinBarcode.Text + ";" + uruns[2].weight.Text + ";" + uruns[2].refBarcode.BackColor.Name + ";" + uruns[2].bobinBarcode.BackColor.Name + ";" + uruns[2].weight.BackColor.Name + ";" + uruns[2].etiket.BackColor.Name + ";" + uruns[2].strec.BackColor.Name);
                else if (uruns[2] != null && (uruns[2].Location == _point3 || uruns[2].Location == _mpoint3))
                    sw.WriteLine("notNull;3;" + uruns[2].refBarcode.Text + ";" + uruns[2].bobinBarcode.Text + ";" + uruns[2].weight.Text + ";" + uruns[2].refBarcode.BackColor.Name + ";" + uruns[2].bobinBarcode.BackColor.Name + ";" + uruns[2].weight.BackColor.Name + ";" + uruns[2].etiket.BackColor.Name + ";" + uruns[2].strec.BackColor.Name);
                else if (uruns[2] != null && (uruns[2].Location == _point4 || uruns[2].Location == _mpoint4))
                    sw.WriteLine("notNull;4;" + uruns[2].refBarcode.Text + ";" + uruns[2].bobinBarcode.Text + ";" + uruns[2].weight.Text + ";" + uruns[2].refBarcode.BackColor.Name + ";" + uruns[2].bobinBarcode.BackColor.Name + ";" + uruns[2].weight.BackColor.Name + ";" + uruns[2].etiket.BackColor.Name + ";" + uruns[2].strec.BackColor.Name);
                if (uruns[3] == null)
                    sw.WriteLine("null;0;000000000;000000000000;0,0 kg;White;White;White;White;White");
                else if (uruns[3] != null && (uruns[3].Location == _point1 || uruns[3].Location == _mpoint1))
                    sw.WriteLine("notNull;1;" + uruns[3].refBarcode.Text + ";" + uruns[3].bobinBarcode.Text + ";" + uruns[3].weight.Text + ";" + uruns[3].refBarcode.BackColor.Name + ";" + uruns[3].bobinBarcode.BackColor.Name + ";" + uruns[3].weight.BackColor.Name + ";" + uruns[3].etiket.BackColor.Name + ";" + uruns[3].strec.BackColor.Name);
                else if (uruns[3] != null && (uruns[3].Location == _point2 || uruns[3].Location == _mpoint2))
                    sw.WriteLine("notNull;2;" + uruns[3].refBarcode.Text + ";" + uruns[3].bobinBarcode.Text + ";" + uruns[3].weight.Text + ";" + uruns[3].refBarcode.BackColor.Name + ";" + uruns[3].bobinBarcode.BackColor.Name + ";" + uruns[3].weight.BackColor.Name + ";" + uruns[3].etiket.BackColor.Name + ";" + uruns[3].strec.BackColor.Name);
                else if (uruns[3] != null && (uruns[3].Location == _point3 || uruns[3].Location == _mpoint3))
                    sw.WriteLine("notNull;3;" + uruns[3].refBarcode.Text + ";" + uruns[3].bobinBarcode.Text + ";" + uruns[3].weight.Text + ";" + uruns[3].refBarcode.BackColor.Name + ";" + uruns[3].bobinBarcode.BackColor.Name + ";" + uruns[3].weight.BackColor.Name + ";" + uruns[3].etiket.BackColor.Name + ";" + uruns[3].strec.BackColor.Name);
                else if (uruns[3] != null && (uruns[3].Location == _point4 || uruns[3].Location == _mpoint4))
                    sw.WriteLine("notNull;4;" + uruns[3].refBarcode.Text + ";" + uruns[3].bobinBarcode.Text + ";" + uruns[3].weight.Text + ";" + uruns[3].refBarcode.BackColor.Name + ";" + uruns[3].bobinBarcode.BackColor.Name + ";" + uruns[3].weight.BackColor.Name + ";" + uruns[3].etiket.BackColor.Name + ";" + uruns[3].strec.BackColor.Name);
            }
        }

        private string readDataBaseForSticker()
        {
            TextFieldParser datReader = new TextFieldParser(Application.StartupPath + @"\Data Base\stickerInformations.dat");
            datReader.SetDelimiters(new string[] { ";" });
            datReader.HasFieldsEnclosedInQuotes = true;
            string colFields = datReader.ReadToEnd();
            return colFields;
        }

        private void readDataBase()
        {
            TextFieldParser datReader = new TextFieldParser(Application.StartupPath + @"\Data Base\urunsInformations.dat");
            datReader.SetDelimiters(new string[] { ";" });
            datReader.HasFieldsEnclosedInQuotes = true;
            string colFields = datReader.ReadToEnd();
            List<string> lineOrder = new List<string>();

            using (StringReader sr = new StringReader(colFields))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    lineOrder.Add(line);
                }
            }
            for (int i = 0; i < 4; i++)
            {
                string[] lineParser = lineOrder[i].Split(';');
                if (lineParser[0] == "notNull")
                {
                    uruns[i] = new Urun();
                    uruns[i].Location = returnPoint(lineParser[1]);
                    uruns[i].refBarcode.Text = lineParser[2];
                    uruns[i].bobinBarcode.Text = lineParser[3];
                    uruns[i].weight.Text = lineParser[4];
                    uruns[i].refBarcode.BackColor = Color.FromName(lineParser[5]);
                    uruns[i].bobinBarcode.BackColor = Color.FromName(lineParser[6]);
                    uruns[i].weight.BackColor = Color.FromName(lineParser[7]);
                    uruns[i].etiket.BackColor = Color.FromName(lineParser[8]);
                    uruns[i].strec.BackColor = Color.FromName(lineParser[9]);
                    if (btnAutoMode.BackColor == Color.Lime)
                    {
                        this.Controls.Add(uruns[i]);
                    }
                    else
                    {
                        pnlManualMode.Controls.Add(uruns[i]);
                    }
                }
            }
        }

        private Point returnPoint(string number)
        {
            if (btnAutoMode.BackColor == Color.Lime)
            {
                if (number == "1")
                    return _point1;
                else if (number == "2")
                    return _point2;
                else if (number == "3")
                    return _point3;
                else if (number == "4")
                    return _point4;
                else
                    return new Point(0, 0);
            }
            else
            {
                if (number == "1")
                    return _mpoint1;
                else if (number == "2")
                    return _mpoint2;
                else if (number == "3")
                    return _mpoint3;
                else if (number == "4")
                    return _mpoint4;
                else
                    return new Point(0, 0);
            }
        }

        private void _mobileBarcode_DataEvent()
        {
            if (_mobileBarcode.data != "?")
            {
                if ((_barcode1 != null && _barcode2 != null) && ((_barcode1.Length == 9 && _mobileBarcode.data.Length == 9) || (_barcode2.Length == 12 && _mobileBarcode.data.Length == 12)))
                {
                    _barcode1 = null;
                    _barcode2 = null;
                    string ErrorMessage = "Barkodlar düzgün okutulmadı.";
                    ErrorMessage += Environment.NewLine;
                    ErrorMessage += Environment.NewLine;
                    ErrorMessage += "Lütfen tekrar okutunuz.";
                    MessageBox.Show(ErrorMessage, "Okuma Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (_mobileBarcode.data.Length == 9)
                {
                    _barcode1 = _mobileBarcode.data;
                }
                else if (_mobileBarcode.data.Length == 12)
                {
                    _barcode2 = _mobileBarcode.data;
                }
                else
                {
                    if (_barcode1 != null && _barcode2 != null)
                    {
                        if (uruns[0] != null && uruns[0].Location == _point1)
                        {
                            this.Invoke(new Action(() => uruns[0].refBarcode.BackColor = Color.Yellow));
                            this.Invoke(new Action(() => uruns[0].bobinBarcode.BackColor = Color.Yellow));
                        }
                        else if (uruns[1] != null && uruns[1].Location == _point1)
                        {
                            this.Invoke(new Action(() => uruns[1].refBarcode.BackColor = Color.Yellow));
                            this.Invoke(new Action(() => uruns[1].bobinBarcode.BackColor = Color.Yellow));
                        }
                        else if (uruns[2] != null && uruns[2].Location == _point1)
                        {
                            this.Invoke(new Action(() => uruns[2].refBarcode.BackColor = Color.Yellow));
                            this.Invoke(new Action(() => uruns[2].bobinBarcode.BackColor = Color.Yellow));
                        }
                        else if (uruns[3] != null && uruns[3].Location == _point1)
                        {
                            this.Invoke(new Action(() => uruns[3].refBarcode.BackColor = Color.Yellow));
                            this.Invoke(new Action(() => uruns[3].bobinBarcode.BackColor = Color.Yellow));
                        }
                    }
                    else if (_barcode1 != null)
                    {
                        if (uruns[0] != null && uruns[0].Location == _point1)
                        {
                            this.Invoke(new Action(() => uruns[0].bobinBarcode.BackColor = Color.Yellow));
                        }
                        else if (uruns[1] != null && uruns[1].Location == _point1)
                        {
                            this.Invoke(new Action(() => uruns[1].bobinBarcode.BackColor = Color.Yellow));
                        }
                        else if (uruns[2] != null && uruns[2].Location == _point1)
                        {
                            this.Invoke(new Action(() => uruns[2].bobinBarcode.BackColor = Color.Yellow));
                        }
                        else if (uruns[3] != null && uruns[3].Location == _point1)
                        {
                            this.Invoke(new Action(() => uruns[3].bobinBarcode.BackColor = Color.Yellow));
                        }
                    }
                    else if (_barcode2 != null)
                    {
                        if (uruns[0] != null && uruns[0].Location == _point1)
                        {
                            this.Invoke(new Action(() => uruns[0].refBarcode.BackColor = Color.Yellow));
                        }
                        else if (uruns[1] != null && uruns[1].Location == _point1)
                        {
                            this.Invoke(new Action(() => uruns[1].refBarcode.BackColor = Color.Yellow));
                        }
                        else if (uruns[2] != null && uruns[2].Location == _point1)
                        {
                            this.Invoke(new Action(() => uruns[2].refBarcode.BackColor = Color.Yellow));
                        }
                        else if (uruns[3] != null && uruns[3].Location == _point1)
                        {
                            this.Invoke(new Action(() => uruns[3].refBarcode.BackColor = Color.Yellow));
                        }
                    }
                    _plc.writePlc((int)datas.barkodeAndWeight, 2);
                    _barcode1 = null;
                    _barcode2 = null;
                    string ErrorMessage = "Barkodlar düzgün okutulmadı.";
                    ErrorMessage += Environment.NewLine;
                    ErrorMessage += Environment.NewLine;
                    ErrorMessage += "Lütfen tekrar okutunuz.";
                    MessageBox.Show(ErrorMessage, "Okuma Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                if ((_barcode1 != null && _barcode2 != null) && (_barcode1.Length == 9 && _barcode2.Length == 12))
                {
                    this.Invoke(new Action(() => plcListener.Enabled = false));
                    if (uruns[0] != null && uruns[0].Location == _point1)
                    {
                        this.Invoke(new Action(() => uruns[0].refBarcode.Text = _barcode1));
                        this.Invoke(new Action(() => uruns[0].bobinBarcode.Text = _barcode2));
                    }
                    else if (uruns[1] != null && uruns[1].Location == _point1)
                    {
                        this.Invoke(new Action(() => uruns[1].refBarcode.Text = _barcode1));
                        this.Invoke(new Action(() => uruns[1].bobinBarcode.Text = _barcode2));
                    }
                    else if (uruns[2] != null && uruns[2].Location == _point1)
                    {
                        this.Invoke(new Action(() => uruns[2].refBarcode.Text = _barcode1));
                        this.Invoke(new Action(() => uruns[2].bobinBarcode.Text = _barcode2));
                    }
                    else if (uruns[3] != null && uruns[3].Location == _point1)
                    {
                        this.Invoke(new Action(() => uruns[3].refBarcode.Text = _barcode1));
                        this.Invoke(new Action(() => uruns[3].bobinBarcode.Text = _barcode2));
                    }
                    if (_taksplus.ExecuteScalarStr(_taksplus.ilkSorgu(_barcode1, _barcode2)) == "1")//_taksplus.ExecuteScalarStr(_taksplus.ilkSorgu(_barcode1, _barcode2)) == "1"
                    {
                        if (uruns[0] != null && uruns[0].Location == _point1)
                        {
                            this.Invoke(new Action(() => uruns[0].refBarcode.BackColor = Color.Lime));
                            this.Invoke(new Action(() => uruns[0].bobinBarcode.BackColor = Color.Lime));
                        }
                        else if (uruns[1] != null && uruns[1].Location == _point1)
                        {
                            this.Invoke(new Action(() => uruns[1].refBarcode.BackColor = Color.Lime));
                            this.Invoke(new Action(() => uruns[1].bobinBarcode.BackColor = Color.Lime));
                        }
                        else if (uruns[2] != null && uruns[2].Location == _point1)
                        {
                            this.Invoke(new Action(() => uruns[2].refBarcode.BackColor = Color.Lime));
                            this.Invoke(new Action(() => uruns[2].bobinBarcode.BackColor = Color.Lime));
                        }
                        else if (uruns[3] != null && uruns[3].Location == _point1)
                        {
                            this.Invoke(new Action(() => uruns[3].refBarcode.BackColor = Color.Lime));
                            this.Invoke(new Action(() => uruns[3].bobinBarcode.BackColor = Color.Lime));
                        }
                    }
                    else
                    {
                        if (uruns[0] != null && uruns[0].Location == _point1)
                        {
                            this.Invoke(new Action(() => uruns[0].refBarcode.BackColor = Color.Red));
                            this.Invoke(new Action(() => uruns[0].bobinBarcode.BackColor = Color.Red));
                        }
                        else if (uruns[1] != null && uruns[1].Location == _point1)
                        {
                            this.Invoke(new Action(() => uruns[1].refBarcode.BackColor = Color.Red));
                            this.Invoke(new Action(() => uruns[1].bobinBarcode.BackColor = Color.Red));
                        }
                        else if (uruns[2] != null && uruns[2].Location == _point1)
                        {
                            this.Invoke(new Action(() => uruns[2].refBarcode.BackColor = Color.Red));
                            this.Invoke(new Action(() => uruns[2].bobinBarcode.BackColor = Color.Red));
                        }
                        else if (uruns[3] != null && uruns[3].Location == _point1)
                        {
                            this.Invoke(new Action(() => uruns[3].refBarcode.BackColor = Color.Red));
                            this.Invoke(new Action(() => uruns[3].bobinBarcode.BackColor = Color.Red));
                        }
                    again:
                        _plc.writePlc((int)datas.barkodeAndWeight, 3);
                        if (_plc.IOException == true)
                        {
                            _plc.IOException = false;
                            goto again;
                        }
                        _barcode1 = null;
                        _barcode2 = null;
                    }
                    this.Invoke(new Action(() => plcListener.Enabled = true));
                    writeToDataBase();
                }
            }
        }

        private void _plc_BtnStartEvent()
        {
            _stickerMachine.sendData("#!A1#!XM1007");
            Thread.Sleep(15);
            if (_stickerMachine.readData().Length > 0)
            {
                if (((uruns[0] != null && (uruns[0].Location == _point1 || uruns[0].Location == _mpoint1)) || (uruns[1] != null && (uruns[1].Location == _point1 || uruns[1].Location == _mpoint1)) || (uruns[2] != null && (uruns[2].Location == _point1 || uruns[2].Location == _mpoint1)) || (uruns[3] != null && (uruns[3].Location == _point1 || uruns[3].Location == _mpoint1))))
                {
                    double tarti = Convert.ToDouble(sevenSegmentArray1.Value);
                    if (tarti > 5)
                    {
                        if (_plc.streclemeHata != 1 && _plc.etiketHata != 1 && _plc.security != 1 && _plc.automaticModeActive == 1)
                        {
                            if (_plc.btnStart == 1)
                            {
                                btnStart.BackColor = Color.Lime;
                                Thread.Sleep(200);
                                _autoBardcode.runSabitBarcod();
                                _barcode1 = _autoBardcode._barcode1;
                                _barcode2 = _autoBardcode._barcode2;
                                if (_barcode1 != null && _barcode2 != null && _barcode1.Length == 9 && _barcode2.Length == 12)
                                {
                                    if (uruns[0] != null && uruns[0].Location == _point1)
                                    {
                                        uruns[0].refBarcode.Text = _barcode1;
                                        uruns[0].bobinBarcode.Text = _barcode2;
                                    }
                                    else if (uruns[1] != null && uruns[1].Location == _point1)
                                    {
                                        uruns[1].refBarcode.Text = _barcode1;
                                        uruns[1].bobinBarcode.Text = _barcode2;
                                    }
                                    else if (uruns[2] != null && uruns[2].Location == _point1)
                                    {
                                        uruns[2].refBarcode.Text = _barcode1;
                                        uruns[2].bobinBarcode.Text = _barcode2;
                                    }
                                    else if (uruns[3] != null && uruns[3].Location == _point1)
                                    {
                                        uruns[3].refBarcode.Text = _barcode1;
                                        uruns[3].bobinBarcode.Text = _barcode2;
                                    }
                                    if (_taksplus.ExecuteScalarStr(_taksplus.ilkSorgu(_barcode1, _barcode2)) == "1")//_taksplus.ExecuteScalarStr(_taksplus.ilkSorgu(_barcode1, _barcode2)) == "1"
                                    {
                                        if (uruns[0] != null && uruns[0].Location == _point1)
                                        {
                                            uruns[0].refBarcode.BackColor = Color.Lime;
                                            uruns[0].bobinBarcode.BackColor = Color.Lime;
                                        }
                                        else if (uruns[1] != null && uruns[1].Location == _point1)
                                        {
                                            uruns[1].refBarcode.BackColor = Color.Lime;
                                            uruns[1].bobinBarcode.BackColor = Color.Lime;
                                        }
                                        else if (uruns[2] != null && uruns[2].Location == _point1)
                                        {
                                            uruns[2].refBarcode.BackColor = Color.Lime;
                                            uruns[2].bobinBarcode.BackColor = Color.Lime;
                                        }
                                        else if (uruns[3] != null && uruns[3].Location == _point1)
                                        {
                                            uruns[3].refBarcode.BackColor = Color.Lime;
                                            uruns[3].bobinBarcode.BackColor = Color.Lime;
                                        }
                                        if (_taksplus.ExecuteNonQuerySilent(_taksplus.ilkInsert(_barcode1, sevenSegmentArray1.Value.ToString())))//_taksplus.ExecuteNonQuerySilent("insert yap")
                                        {
                                            lblMessage.Text = "TAKS'tan onay bekleniyor...";
                                            dataBaseListener.Enabled = true;
                                        }
                                    }
                                    else
                                    {
                                        if (uruns[0] != null && uruns[0].Location == _point1)
                                        {
                                            uruns[0].refBarcode.BackColor = Color.Red;
                                            uruns[0].bobinBarcode.BackColor = Color.Red;
                                        }
                                        else if (uruns[1] != null && uruns[1].Location == _point1)
                                        {
                                            uruns[1].refBarcode.BackColor = Color.Red;
                                            uruns[1].bobinBarcode.BackColor = Color.Red;
                                        }
                                        else if (uruns[2] != null && uruns[2].Location == _point1)
                                        {
                                            uruns[2].refBarcode.BackColor = Color.Red;
                                            uruns[2].bobinBarcode.BackColor = Color.Red;
                                        }
                                        else if (uruns[3] != null && uruns[3].Location == _point1)
                                        {
                                            uruns[3].refBarcode.BackColor = Color.Red;
                                            uruns[3].bobinBarcode.BackColor = Color.Red;
                                        }
                                    again2:
                                        _plc.writePlc((int)datas.barkodeAndWeight, 3);
                                        if (_plc.IOException == true)
                                        {
                                            _plc.IOException = false;
                                            goto again2;
                                        }
                                        _barcode1 = null;
                                        _barcode2 = null;
                                    }
                                }
                                else
                                {
                                    if (uruns[0] != null && uruns[0].Location == _point1)
                                    {
                                        uruns[0].refBarcode.BackColor = Color.Yellow;
                                        uruns[0].bobinBarcode.BackColor = Color.Yellow;
                                    }
                                    else if (uruns[1] != null && uruns[1].Location == _point1)
                                    {
                                        uruns[1].refBarcode.BackColor = Color.Yellow;
                                        uruns[1].bobinBarcode.BackColor = Color.Yellow;
                                    }
                                    else if (uruns[2] != null && uruns[2].Location == _point1)
                                    {
                                        uruns[2].refBarcode.BackColor = Color.Yellow;
                                        uruns[2].bobinBarcode.BackColor = Color.Yellow;
                                    }
                                    else if (uruns[3] != null && uruns[3].Location == _point1)
                                    {
                                        uruns[3].refBarcode.BackColor = Color.Yellow;
                                        uruns[3].bobinBarcode.BackColor = Color.Yellow;
                                    }
                                    _barcode1 = null;
                                    _barcode2 = null;
                                again1:
                                    _plc.writePlc((int)datas.barkodeAndWeight, 2);
                                    if (_plc.IOException == true)
                                    {
                                        _plc.IOException = false;
                                        goto again1;
                                    }
                                    lblMessage.Text = "Sabit barkod okuyucu barkodları okuyamadı!!!\nLütfen mobil barkod okuyucuyu kullanın...";
                                }
                                btnStart.BackColor = Color.DarkGreen;
                            }
                            else if (_plc.btnStart == 2 && _barcode1 != null && _barcode2 != null)
                            {
                                if (_taksplus.ExecuteNonQuerySilent(_taksplus.ilkInsert(_barcode1, sevenSegmentArray1.Value.ToString())))//_taksplus.ExecuteNonQuerySilent("insert yap")
                                {
                                    btnStart.BackColor = Color.Lime;
                                    Thread.Sleep(200);
                                    lblMessage.Text = "TAKS'tan onay bekleniyor...";
                                    dataBaseListener.Enabled = true;
                                    btnStart.BackColor = Color.DarkGreen;
                                }
                                else
                                {
                                    //insert yapılamadı mesajını göster
                                }
                            }
                            else if ((_plc.btnStart == 2 && (_barcode1 == null || _barcode2 == null)) || (_plc.btnStart == 2 && (_barcode1.Length != 9 || _barcode2.Length != 12)))
                            {
                                lblMessage.Text = "Barkodlar mobil barkod ile okutulmamış yada düzgün okutulamamış\nLütfen tekrar okuttuktan sonra yeniden deneyiniz.";
                                btnStart.BackColor = Color.DarkGreen;
                            }
                            else
                                btnStart.BackColor = Color.DarkGreen;
                            writeToDataBase();
                        }
                        else
                        {
                            lblMessage.Text = "Start için sistem uygun değil.\nKontrol ediniz !";
                        }
                    }
                    else
                    {
                        DialogResult dialog = MessageBox.Show("Lütfen tamama bastıktan sonra ürünü tekrar tartın.", "Tartı Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                DialogResult dialog = MessageBox.Show("Etiket makinası açık değil lütfen kontrol ediniz.", "Etiket Makinası Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        again:
            _plc.writePlc((int)inputs.btnStart, 0);
            if (_plc.IOException == true)
            {
                _plc.IOException = false;
                goto again;
            }
        }

        private void _plc_LightColumnBarcodeYellowEvent()
        {
            if (_plc.lightColumnBarcodeYellow == 1)
                btnLightColumn1Yellow.BackColor = Color.Yellow;
            else
                btnLightColumn1Yellow.BackColor = Color.DarkGoldenrod;
        }

        private void _plc_LightColumnBarcodeGreenEvent()
        {
            if (_plc.lightColumnBarcodeGreen == 1)
                btnLightColumn1Green.BackColor = Color.Lime;
            else
                btnLightColumn1Green.BackColor = Color.DarkGreen;
        }

        private void _plc_LightColumnBarcodeRedEvent()
        {
            if (_plc.lightColumnBarcodeRed == 1)
                btnLightColumn1Red.BackColor = Color.Red;
            else
                btnLightColumn1Red.BackColor = Color.DarkRed;
        }

        private void _plc_LightColumnBarcodeBuzzerEvent()
        {
            if (_plc.lightColumnBarcodeBuzzer == 1)
                btnLightColumn1Buzzer.BackgroundImage = ımageList1.Images[1];
            else
                btnLightColumn1Buzzer.BackgroundImage = ımageList1.Images[0];
        }

        private void _plc_LightColumnWeighingGreenEvent()
        {
            if (_plc.lightColumnWeighingGreen == 1)
                btnLightColumn2Green.BackColor = Color.Lime;
            else
                btnLightColumn2Green.BackColor = Color.DarkGreen;
        }

        private void _plc_LightColumnWeighinhRedEvent()
        {
            if (_plc.lightColumnWeighingRed == 1)
                btnLightColumn2Red.BackColor = Color.Red;
            else
                btnLightColumn2Red.BackColor = Color.DarkRed;
        }

        private void _plc_LightColumnWeighingBuzzerEvent()
        {
            if (_plc.lightColumnWeighingBuzzer == 1)
                btnLightColumn2Buzzer.BackgroundImage = ımageList1.Images[1];
            else
                btnLightColumn2Buzzer.BackgroundImage = ımageList1.Images[0];
        }

        private void _plc_LightColumnReadyGreenEvent()
        {
            if (_plc.lightColumnReadyGreen == 1)
                btnLightColumn3Green.BackColor = Color.Lime;
            else
                btnLightColumn3Green.BackColor = Color.DarkGreen;
        }

        private void _plc_LightColumnReadyRedEvent()
        {
            if (_plc.lightColumnReadyRed == 1)
                btnLightColumn3Red.BackColor = Color.Red;
            else
                btnLightColumn3Red.BackColor = Color.DarkRed;
        }

        private void _plc_LightColumnReadyBuzzerEvent()
        {
            if (_plc.lightColumnReadyBuzzer == 1)
                btnLightColumn3Buzzer.BackgroundImage = ımageList1.Images[1];
            else
                btnLightColumn3Buzzer.BackgroundImage = ımageList1.Images[0];
        }



        private void _plc_StreclemeSonucEvent()
        {
            if (_plc.streclemeSonuc == 1)
            {
                if ((uruns[0] != null) && (uruns[0].Location == _point2 || uruns[0].Location == _mpoint2))
                    uruns[0].strec.BackColor = Color.Lime;
                else if ((uruns[1] != null) && (uruns[1].Location == _point2 || uruns[1].Location == _mpoint2))
                    uruns[1].strec.BackColor = Color.Lime;
                else if ((uruns[2] != null) && (uruns[2].Location == _point2 || uruns[2].Location == _mpoint2))
                    uruns[2].strec.BackColor = Color.Lime;
                else if ((uruns[3] != null) && (uruns[3].Location == _point2 || uruns[3].Location == _mpoint2))
                    uruns[3].strec.BackColor = Color.Lime;
            }
        again:
            _plc.writePlc((int)datas.streclemeSonuc, 0);
            if (_plc.IOException == true)
            {
                _plc.IOException = false;
                goto again;
            }
        }

        private void _plc_StreclemeHataEvent()
        {
            if (_plc.streclemeHata == 1)
            {
                lblMessage.Text = "Streç makinesei reset bekliyor.\nHMI Ekranını kontrol ediniz !";
                btnStart.Enabled = false;
                btnAutoMode.Enabled = false;
                btnManualMode.Enabled = false;
            }
            else if (_plc.streclemeHata == 0)
            {
                lblMessage.Text = "Streçleme makinesi hazır.(Hata yok)";
                btnStart.Enabled = true;
                btnAutoMode.Enabled = true;
                btnManualMode.Enabled = true;
            }
        }

        private void _plc_EtiketSonucEvent()
        {
            if (_plc.etiketSonuc == 1)
            {
                if ((uruns[0] != null) && (uruns[0].Location == _point2 || uruns[0].Location == _mpoint2))
                    uruns[0].etiket.BackColor = Color.Lime;
                else if ((uruns[1] != null) && (uruns[1].Location == _point2 || uruns[1].Location == _mpoint2))
                    uruns[1].etiket.BackColor = Color.Lime;
                else if ((uruns[2] != null) && (uruns[2].Location == _point2 || uruns[2].Location == _mpoint2))
                    uruns[2].etiket.BackColor = Color.Lime;
                else if ((uruns[3] != null) && (uruns[3].Location == _point2 || uruns[2].Location == _mpoint2))
                    uruns[3].etiket.BackColor = Color.Lime;
                again:
                _plc.writePlc((int)datas.etiketSonuc, 0);
                if (_plc.IOException == true)
                {
                    _plc.IOException = false;
                    goto again;
                }
            }
        }

        private void _plc_EtiketHataEvent()
        {
            if (_plc.etiketHata == 1)
            {
                MessageBox.Show("Etiket makinesi hataya düştü!\nLütfen etiket makinesini kontrol ediniz", "Etiket Makinesi Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (_plc.etiketHata == 2)
            {
                lblMessage.Text = "Etiket hazır";
            }
        }

        private void _plc_MovingEvent()
        {
            switch (_plc.moving)
            {
                case 12:
                    if (!((uruns[0] != null && (uruns[0].Location == _point1 || uruns[0].Location == _mpoint1)) || (uruns[1] != null && (uruns[1].Location == _point1 || uruns[1].Location == _mpoint1)) || (uruns[2] != null && (uruns[2].Location == _point1 || uruns[2].Location == _mpoint1)) || (uruns[3] != null && (uruns[3].Location == _point1 || uruns[3].Location == _mpoint1))))
                    {
                        if (uruns[0] == null)
                        {
                            uruns[0] = new Urun();
                            if (btnAutoMode.BackColor == Color.Lime)
                            {
                                uruns[0].Location = _point1;
                                this.Controls.Add(uruns[0]);
                                writeToDataBase();
                            }
                            else if (btnManualMode.BackColor == Color.Lime)
                            {
                                uruns[0].Location = _mpoint1;
                                pnlManualMode.Controls.Add(uruns[0]);
                                writeToDataBase();
                            }
                        }
                        else if (uruns[1] == null)
                        {
                            uruns[1] = new Urun();
                            if (btnAutoMode.BackColor == Color.Lime)
                            {
                                uruns[1].Location = _point1;
                                this.Controls.Add(uruns[1]);
                                writeToDataBase();
                            }
                            else if (btnManualMode.BackColor == Color.Lime)
                            {
                                uruns[1].Location = _mpoint1;
                                pnlManualMode.Controls.Add(uruns[1]);
                                writeToDataBase();
                            }
                        }
                        else if (uruns[2] == null)
                        {
                            uruns[2] = new Urun();
                            if (btnAutoMode.BackColor == Color.Lime)
                            {
                                uruns[2].Location = _point1;
                                this.Controls.Add(uruns[2]);
                                writeToDataBase();
                            }
                            else if (btnManualMode.BackColor == Color.Lime)
                            {
                                uruns[2].Location = _mpoint1;
                                pnlManualMode.Controls.Add(uruns[2]);
                                writeToDataBase();
                            }
                        }
                        else if (uruns[3] == null)
                        {
                            uruns[3] = new Urun();
                            if (btnAutoMode.BackColor == Color.Lime)
                            {
                                uruns[3].Location = _point1;
                                this.Controls.Add(uruns[3]);
                                writeToDataBase();
                            }
                            else if (btnManualMode.BackColor == Color.Lime)
                            {
                                uruns[3].Location = _mpoint1;
                                pnlManualMode.Controls.Add(uruns[3]);
                                writeToDataBase();
                            }
                        }
                    }
                    break;
                case 23:
                    if (!((uruns[0] != null && (uruns[0].Location == _point2 || uruns[0].Location == _mpoint2)) || (uruns[1] != null && (uruns[1].Location == _point2 || uruns[1].Location == _mpoint2)) || (uruns[2] != null && (uruns[2].Location == _point2 || uruns[2].Location == _mpoint2)) || (uruns[3] != null && (uruns[3].Location == _point2 || uruns[3].Location == _mpoint2))))
                    {
                        if (uruns[3] != null && (uruns[3].Location == _point1 || uruns[3].Location == _mpoint1))
                        {
                            if (btnAutoMode.BackColor == Color.Lime)
                                uruns[3].Location = _point2;
                            else if (btnManualMode.BackColor == Color.Lime)
                                uruns[3].Location = _mpoint2;
                            writeToDataBase();
                        }
                        else if (uruns[2] != null && (uruns[2].Location == _point1 || uruns[2].Location == _mpoint1))
                        {
                            if (btnAutoMode.BackColor == Color.Lime)
                                uruns[2].Location = _point2;
                            else if (btnManualMode.BackColor == Color.Lime)
                                uruns[2].Location = _mpoint2;
                            writeToDataBase();
                        }
                        else if (uruns[1] != null && (uruns[1].Location == _point1 || uruns[1].Location == _mpoint1))
                        {
                            if (btnAutoMode.BackColor == Color.Lime)
                                uruns[1].Location = _point2;
                            else if (btnManualMode.BackColor == Color.Lime)
                                uruns[1].Location = _mpoint2;
                            writeToDataBase();
                        }
                        else if (uruns[0] != null && (uruns[0].Location == _point1 || uruns[0].Location == _mpoint1))
                        {
                            if (btnAutoMode.BackColor == Color.Lime)
                                uruns[0].Location = _point2;
                            else if (btnManualMode.BackColor == Color.Lime)
                                uruns[0].Location = _mpoint2;
                            writeToDataBase();
                        }
                    }
                    break;
                case 34:
                    if (!((uruns[0] != null && (uruns[0].Location == _point3 || uruns[0].Location == _mpoint3)) || (uruns[1] != null && (uruns[1].Location == _point3 || uruns[1].Location == _mpoint3)) || (uruns[2] != null && (uruns[2].Location == _point3 || uruns[2].Location == _mpoint3)) || (uruns[3] != null && (uruns[3].Location == _point3 || uruns[3].Location == _mpoint3))))
                    {
                        if (uruns[3] != null && (uruns[3].Location == _point2 || uruns[3].Location == _mpoint2))
                        {
                            if (btnAutoMode.BackColor == Color.Lime)
                                uruns[3].Location = _point3;
                            else if (btnManualMode.BackColor == Color.Lime)
                                uruns[3].Location = _mpoint3;
                            writeToDataBase();
                        }
                        else if (uruns[2] != null && (uruns[2].Location == _point2 || uruns[2].Location == _mpoint2))
                        {
                            if (btnAutoMode.BackColor == Color.Lime)
                                uruns[2].Location = _point3;
                            else if (btnManualMode.BackColor == Color.Lime)
                                uruns[2].Location = _mpoint3;
                            writeToDataBase();
                        }
                        else if (uruns[1] != null && (uruns[1].Location == _point2 || uruns[1].Location == _mpoint2))
                        {
                            if (btnAutoMode.BackColor == Color.Lime)
                                uruns[1].Location = _point3;
                            else if (btnManualMode.BackColor == Color.Lime)
                                uruns[1].Location = _mpoint3;
                            writeToDataBase();
                        }
                        else if (uruns[0] != null && (uruns[0].Location == _point2 || uruns[0].Location == _mpoint2))
                        {
                            if (btnAutoMode.BackColor == Color.Lime)
                                uruns[0].Location = _point3;
                            else if (btnManualMode.BackColor == Color.Lime)
                                uruns[0].Location = _mpoint3;
                            writeToDataBase();
                        }
                    }
                    break;
                case 45:
                    if (!((uruns[0] != null && (uruns[0].Location == _point4 || uruns[0].Location == _mpoint4)) || (uruns[1] != null && (uruns[1].Location == _point4 || uruns[1].Location == _mpoint4)) || (uruns[2] != null && (uruns[2].Location == _point4 || uruns[2].Location == _mpoint4)) || (uruns[3] != null && (uruns[3].Location == _point4 || uruns[3].Location == _mpoint4))))
                    {
                        if (uruns[3] != null && (uruns[3].Location == _point3 || uruns[3].Location == _mpoint3))
                        {
                            if (btnAutoMode.BackColor == Color.Lime)
                                uruns[3].Location = _point4;
                            else if (btnManualMode.BackColor == Color.Lime)
                                uruns[3].Location = _mpoint4;
                            writeToDataBase();
                        }
                        else if (uruns[2] != null && (uruns[2].Location == _point3 || uruns[2].Location == _mpoint3))
                        {
                            if (btnAutoMode.BackColor == Color.Lime)
                                uruns[2].Location = _point4;
                            else if (btnManualMode.BackColor == Color.Lime)
                                uruns[2].Location = _mpoint4;
                            writeToDataBase();
                        }
                        else if (uruns[1] != null && (uruns[1].Location == _point3 || uruns[1].Location == _mpoint3))
                        {
                            if (btnAutoMode.BackColor == Color.Lime)
                                uruns[1].Location = _point4;
                            else if (btnManualMode.BackColor == Color.Lime)
                                uruns[1].Location = _mpoint4;
                            writeToDataBase();
                        }
                        else if (uruns[0] != null && (uruns[0].Location == _point3 || uruns[0].Location == _mpoint3))
                        {
                            if (btnAutoMode.BackColor == Color.Lime)
                                uruns[0].Location = _point4;
                            else if (btnManualMode.BackColor == Color.Lime)
                                uruns[0].Location = _mpoint4;
                            writeToDataBase();
                        }
                    }
                    break;
                case 56:
                    if ((uruns[0] != null && (uruns[0].Location == _point4 || uruns[0].Location == _mpoint4)) || (uruns[1] != null && (uruns[1].Location == _point4 || uruns[1].Location == _mpoint4)) || (uruns[2] != null && (uruns[2].Location == _point4 || uruns[2].Location == _mpoint4)) || (uruns[3] != null && (uruns[3].Location == _point4 || uruns[3].Location == _mpoint4)))
                    {
                        if (uruns[0] != null && (uruns[0].Location == _point4 || uruns[0].Location == _mpoint4))
                        {
                            uruns[0].Dispose();
                            uruns[0] = null;
                            writeToDataBase();
                        }
                        else if (uruns[1] != null && (uruns[1].Location == _point4 || uruns[1].Location == _mpoint4))
                        {
                            uruns[1].Dispose();
                            uruns[1] = null;
                            writeToDataBase();
                        }
                        else if (uruns[2] != null && (uruns[2].Location == _point4 || uruns[2].Location == _mpoint4))
                        {
                            uruns[2].Dispose();
                            uruns[2] = null;
                            writeToDataBase();
                        }
                        else if (uruns[3] != null && (uruns[3].Location == _point4 || uruns[3].Location == _mpoint4))
                        {
                            uruns[3].Dispose();
                            uruns[3] = null;
                            writeToDataBase();
                        }
                    }
                    break;
                case 21:
                    if (uruns[0] != null && (uruns[0].Location == _point1 || uruns[0].Location == _mpoint1))
                    {
                        uruns[0].Dispose();
                        uruns[0] = null;
                        writeToDataBase();
                    }
                    else if (uruns[1] != null && (uruns[1].Location == _point1 || uruns[1].Location == _mpoint1))
                    {
                        uruns[1].Dispose();
                        uruns[1] = null;
                        writeToDataBase();
                    }
                    else if (uruns[2] != null && (uruns[2].Location == _point1 || uruns[2].Location == _mpoint1))
                    {
                        uruns[2].Dispose();
                        uruns[2] = null;
                        writeToDataBase();
                    }
                    else if (uruns[3] != null && (uruns[3].Location == _point1 || uruns[3].Location == _mpoint1))
                    {
                        uruns[3].Dispose();
                        uruns[3] = null;
                        writeToDataBase();
                    }
                    break;
            }
        again:
            _plc.writePlc((int)datas.moving, 0);
            if (_plc.IOException == true)
            {
                _plc.IOException = false;
                goto again;
            }
        }

        private void _plc_SecurityEvent()
        {
            if (_plc.security == 1)
                lblMessage.Text = "Güvenlik ihlali\nReset bekleniyor...";
            else
                lblMessage.Text = "Sistem güvenli...";
        }

        private void _plc_AutomaticModeActiveEvent()
        {
            if (programmaticalPress == false && _plc.automaticModeActive == 1 && btnAutoMode.BackColor != Color.Lime)
            {
                btnAutoMode_Click(btnAutoMode, EventArgs.Empty);
            }
            else if (programmaticalPress == false && _plc.automaticModeActive == 0 && btnManualMode.BackColor == Color.Red)
            {
                btnManualMode_Click(btnManualMode, EventArgs.Empty);
            }
            programmaticalPress = false;
        }

        //private void serialPortMobileBarcod_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        //{
        //    Thread.Sleep(100);
        //    string data = serialPortMobileBarcod.ReadExisting();

        //    if ((_barcode1 != null && _barcode2 != null) && ((_barcode1.Length == 9 && data.Length == 9) || (_barcode2.Length == 12 && data.Length == 12)))
        //    {
        //        _barcode1 = null;
        //        _barcode2 = null;
        //        string ErrorMessage = "Barkodlar düzgün okutulmadı.";
        //        ErrorMessage += Environment.NewLine;
        //        ErrorMessage += Environment.NewLine;
        //        ErrorMessage += "Lütfen tekrar okutunuz.";
        //        MessageBox.Show(ErrorMessage, "Okuma Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //    else if (data.Length == 9)
        //    {
        //        _barcode1 = data;
        //    }
        //    else if (data.Length == 12)
        //    {
        //        _barcode2 =data;
        //    }
        //    else
        //    {
        //        timer1.Enabled = true;
        //    }
        //    if ((_barcode1 != null && _barcode2 != null) && (_barcode1.Length == 9 && _barcode2.Length == 12))
        //    {
        //        timer2.Enabled = true;
        //    }
        //}

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            if (_barcode1 != null && _barcode2 != null)
            {
                if (uruns[0] != null && uruns[0].Location == _point1)
                {
                    uruns[0].refBarcode.BackColor = Color.Yellow;
                    uruns[0].bobinBarcode.BackColor = Color.Yellow;
                }
                else if (uruns[1] != null && uruns[1].Location == _point1)
                {
                    uruns[1].refBarcode.BackColor = Color.Yellow;
                    uruns[1].bobinBarcode.BackColor = Color.Yellow;
                }
                else if (uruns[2] != null && uruns[2].Location == _point1)
                {
                    uruns[2].refBarcode.BackColor = Color.Yellow;
                    uruns[2].bobinBarcode.BackColor = Color.Yellow;
                }
                else if (uruns[3] != null && uruns[3].Location == _point1)
                {
                    uruns[3].refBarcode.BackColor = Color.Yellow;
                    uruns[3].bobinBarcode.BackColor = Color.Yellow;
                }
            }
            else if (_barcode1 != null)
            {
                if (uruns[0] != null && uruns[0].Location == _point1)
                {
                    uruns[0].bobinBarcode.BackColor = Color.Yellow;
                }
                else if (uruns[1] != null && uruns[1].Location == _point1)
                {
                    uruns[1].bobinBarcode.BackColor = Color.Yellow;
                }
                else if (uruns[2] != null && uruns[2].Location == _point1)
                {
                    uruns[2].bobinBarcode.BackColor = Color.Yellow;
                }
                else if (uruns[3] != null && uruns[3].Location == _point1)
                {
                    uruns[3].bobinBarcode.BackColor = Color.Yellow;
                }
            }
            else if (_barcode2 != null)
            {
                if (uruns[0] != null && uruns[0].Location == _point1)
                {
                    uruns[0].refBarcode.BackColor = Color.Yellow;
                }
                else if (uruns[1] != null && uruns[1].Location == _point1)
                {
                    uruns[1].refBarcode.BackColor = Color.Yellow;
                }
                else if (uruns[2] != null && uruns[2].Location == _point1)
                {
                    uruns[2].refBarcode.BackColor = Color.Yellow;
                }
                else if (uruns[3] != null && uruns[3].Location == _point1)
                {
                    uruns[3].refBarcode.BackColor = Color.Yellow;
                }
            }
        again:
            _plc.writePlc((int)datas.barkodeAndWeight, 2);
            if (_plc.IOException == true)
            {
                _plc.IOException = false;
                goto again;
            }
            _barcode1 = null;
            _barcode2 = null;
            writeToDataBase();
            string ErrorMessage = "Barkodlar düzgün okutulmadı.";
            ErrorMessage += Environment.NewLine;
            ErrorMessage += Environment.NewLine;
            ErrorMessage += "Lütfen tekrar okutunuz.";
            MessageBox.Show(ErrorMessage, "Okuma Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            timer2.Enabled = false;
            plcListener.Enabled = false;
            if (uruns[0] != null && uruns[0].Location == _point1)
            {
                uruns[0].refBarcode.Text = _barcode1;
                uruns[0].bobinBarcode.Text = _barcode2;
            }
            else if (uruns[1] != null && uruns[1].Location == _point1)
            {
                uruns[1].refBarcode.Text = _barcode1;
                uruns[1].bobinBarcode.Text = _barcode2;
            }
            else if (uruns[2] != null && uruns[2].Location == _point1)
            {
                uruns[2].refBarcode.Text = _barcode1;
                uruns[2].bobinBarcode.Text = _barcode2;
            }
            else if (uruns[3] != null && uruns[3].Location == _point1)
            {
                uruns[3].refBarcode.Text = _barcode1;
                uruns[3].bobinBarcode.Text = _barcode2;
            }
            if (_taksplus.ExecuteScalarStr(_taksplus.ilkSorgu(_barcode1, _barcode2)) == "1")//_taksplus.ExecuteScalarStr(_taksplus.ilkSorgu(_barcode1, _barcode2)) == "1"
            {
                if (uruns[0] != null && uruns[0].Location == _point1)
                {
                    uruns[0].refBarcode.BackColor = Color.Lime;
                    uruns[0].bobinBarcode.BackColor = Color.Lime;
                }
                else if (uruns[1] != null && uruns[1].Location == _point1)
                {
                    uruns[1].refBarcode.BackColor = Color.Lime;
                    uruns[1].bobinBarcode.BackColor = Color.Lime;
                }
                else if (uruns[2] != null && uruns[2].Location == _point1)
                {
                    uruns[2].refBarcode.BackColor = Color.Lime;
                    uruns[2].bobinBarcode.BackColor = Color.Lime;
                }
                else if (uruns[3] != null && uruns[3].Location == _point1)
                {
                    uruns[3].refBarcode.BackColor = Color.Lime;
                    uruns[3].bobinBarcode.BackColor = Color.Lime;
                }

            }
            else
            {
                if (uruns[0] != null && uruns[0].Location == _point1)
                {
                    uruns[0].refBarcode.BackColor = Color.Red;
                    uruns[0].bobinBarcode.BackColor = Color.Red;
                }
                else if (uruns[1] != null && uruns[1].Location == _point1)
                {
                    uruns[1].refBarcode.BackColor = Color.Red;
                    uruns[1].bobinBarcode.BackColor = Color.Red;
                }
                else if (uruns[2] != null && uruns[2].Location == _point1)
                {
                    uruns[2].refBarcode.BackColor = Color.Red;
                    uruns[2].bobinBarcode.BackColor = Color.Red;
                }
                else if (uruns[3] != null && uruns[3].Location == _point1)
                {
                    uruns[3].refBarcode.BackColor = Color.Red;
                    uruns[3].bobinBarcode.BackColor = Color.Red;
                }
            again:
                _plc.writePlc((int)datas.barkodeAndWeight, 3);
                if (_plc.IOException == true)
                {
                    _plc.IOException = false;
                    goto again;
                }
                _barcode1 = null;
                _barcode2 = null;
            }
            this.Invoke(new Action(() => plcListener.Enabled = true));
            writeToDataBase();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            switch (nmConveyor.Value)
            {
                case 1:
                    if ((uruns[0] != null && (uruns[0].Location == _point1 || uruns[0].Location == _mpoint1)))
                    {
                        uruns[0].Dispose();
                        uruns[0] = null;
                    }
                    else if ((uruns[1] != null && (uruns[1].Location == _point1 || uruns[1].Location == _mpoint1)))
                    {
                        uruns[1].Dispose();
                        uruns[1] = null;
                    }
                    else if ((uruns[2] != null && (uruns[2].Location == _point1 || uruns[2].Location == _mpoint1)))
                    {
                        uruns[2].Dispose();
                        uruns[2] = null;
                    }
                    else if ((uruns[3] != null && (uruns[3].Location == _point1 || uruns[3].Location == _mpoint1)))
                    {
                        uruns[3].Dispose();
                        uruns[3] = null;
                    }
                    break;
                case 2:
                    if ((uruns[0] != null && (uruns[0].Location == _point2 || uruns[0].Location == _mpoint2)))
                    {
                        uruns[0].Dispose();
                        uruns[0] = null;
                    }
                    else if ((uruns[1] != null && (uruns[1].Location == _point2 || uruns[1].Location == _mpoint2)))
                    {
                        uruns[1].Dispose();
                        uruns[1] = null;
                    }
                    else if ((uruns[2] != null && (uruns[2].Location == _point2 || uruns[2].Location == _mpoint2)))
                    {
                        uruns[2].Dispose();
                        uruns[2] = null;
                    }
                    else if ((uruns[3] != null && (uruns[3].Location == _point2 || uruns[3].Location == _mpoint2)))
                    {
                        uruns[3].Dispose();
                        uruns[3] = null;
                    }
                    break;
                case 3:
                    if ((uruns[0] != null && (uruns[0].Location == _point3 || uruns[0].Location == _mpoint3)))
                    {
                        uruns[0].Dispose();
                        uruns[0] = null;
                    }
                    else if ((uruns[1] != null && (uruns[1].Location == _point3 || uruns[1].Location == _mpoint3)))
                    {
                        uruns[1].Dispose();
                        uruns[1] = null;
                    }
                    else if ((uruns[2] != null && (uruns[2].Location == _point3 || uruns[2].Location == _mpoint3)))
                    {
                        uruns[2].Dispose();
                        uruns[2] = null;
                    }
                    else if ((uruns[3] != null && (uruns[3].Location == _point3 || uruns[3].Location == _mpoint3)))
                    {
                        uruns[3].Dispose();
                        uruns[3] = null;
                    }
                    break;
                case 4:
                    if ((uruns[0] != null && (uruns[0].Location == _point4 || uruns[0].Location == _mpoint4)))
                    {
                        uruns[0].Dispose();
                        uruns[0] = null;
                    }
                    else if ((uruns[1] != null && (uruns[1].Location == _point4 || uruns[1].Location == _mpoint4)))
                    {
                        uruns[1].Dispose();
                        uruns[1] = null;
                    }
                    else if ((uruns[2] != null && (uruns[2].Location == _point4 || uruns[2].Location == _mpoint4)))
                    {
                        uruns[2].Dispose();
                        uruns[2] = null;
                    }
                    else if ((uruns[3] != null && (uruns[3].Location == _point4 || uruns[3].Location == _mpoint4)))
                    {
                        uruns[3].Dispose();
                        uruns[3] = null;
                    }
                    break;
                default:
                    break;
            }
            writeToDataBase();
        }

        private void button1_Click(object sender, EventArgs e)
        {
        again1:
            _plc.writePlc((int)datas.mode, 2);
            if (_plc.IOException == true)
            {
                _plc.IOException = false;
                goto again1;
            }
        }
    }
}
