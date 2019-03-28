using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyModbus;
using System.Windows.Forms;

namespace KordSA
{
    //Input delegates
    public delegate void LightBarrier1Controller();
    public delegate void LightBarrier2Controller();
    public delegate void BtnStartController();
    public delegate void BtnResetController();
    public delegate void BtnEmergency1Controller();
    public delegate void BtnEmergency2Controller();
    public delegate void RfSensor1Controller();
    public delegate void RfSensor2Controller();
    public delegate void RfSensor3Controller();
    public delegate void RfSensor4Controller();
    public delegate void Door1Controller();
    public delegate void Door2Controller();
    //Output delegates
    public delegate void Conveyor1Controller();
    public delegate void Conveyor2Controller();
    public delegate void Conveyor3Controller();
    public delegate void Conveyor4Controller();
    public delegate void TurntableController();
    public delegate void LightColumnBarcodeYellowController();
    public delegate void LightColumnBarcodeGreenController();
    public delegate void LightColumnBarcodeRedController();
    public delegate void LightColumnBarcodeBuzzerController();
    public delegate void LightColumnWeighingGreenController();
    public delegate void LightColumnWeighingRedController();
    public delegate void LightColumnWeighingBuzzerController();
    public delegate void LightColumnReadyGreenController();
    public delegate void LightColumnReadyRedController();
    public delegate void LightColumnReadyBuzzerController();
    //Data delegates
    public delegate void BarkodeAndWeighingController();
    public delegate void StreclemeSonucController();
    public delegate void StreclemeHataController();
    public delegate void EtiketSonucController();
    public delegate void EtiketHataController();
    public delegate void MovingController();
    public delegate void ModeController();
    public delegate void SecurityController();
    public delegate void AutomaticModeActiveController();
    public delegate void SensorController();

    class Plc
    {
        //Input events
        public event LightBarrier1Controller LightBarrier1Event;
        public event LightBarrier2Controller LightBarrier2Event;
        public event BtnStartController BtnStartEvent;
        public event BtnResetController BtnResetEvent;
        public event BtnEmergency1Controller BtnEmergency1Event;
        public event BtnEmergency2Controller BtnEmergency2Event;
        public event RfSensor1Controller RfSensor1Event;
        public event RfSensor2Controller RfSensor2Event;
        public event RfSensor3Controller RfSensor3Event;
        public event RfSensor4Controller RfSensor4Event;
        public event Door1Controller Door1Event;
        public event Door2Controller Door2Event;

        //Output events
        public event Conveyor1Controller Conveyor1Event;
        public event Conveyor2Controller Conveyor2Event;
        public event Conveyor3Controller Conveyor3Event;
        public event Conveyor4Controller Conveyor4Event;
        public event TurntableController TurntableEvent;
        public event LightColumnBarcodeYellowController LightColumnBarcodeYellowEvent;
        public event LightColumnBarcodeGreenController LightColumnBarcodeGreenEvent;
        public event LightColumnBarcodeRedController LightColumnBarcodeRedEvent;
        public event LightColumnBarcodeBuzzerController LightColumnBarcodeBuzzerEvent;
        public event LightColumnWeighingGreenController LightColumnWeighingGreenEvent;
        public event LightColumnWeighingRedController LightColumnWeighinhRedEvent;
        public event LightColumnWeighingBuzzerController LightColumnWeighingBuzzerEvent;
        public event LightColumnReadyGreenController LightColumnReadyGreenEvent;
        public event LightColumnReadyRedController LightColumnReadyRedEvent;
        public event LightColumnReadyBuzzerController LightColumnReadyBuzzerEvent;

        //Data events
        public event BarkodeAndWeighingController BarcodeAndWeightEvent;
        public event StreclemeSonucController StreclemeSonucEvent;
        public event StreclemeHataController StreclemeHataEvent;
        public event EtiketSonucController EtiketSonucEvent;
        public event EtiketHataController EtiketHataEvent;
        public event MovingController MovingEvent;
        public event ModeController ModeEvent;
        public event SecurityController SecurityEvent;
        public event AutomaticModeActiveController AutomaticModeActiveEvent;
        public event SensorController SensorEvent;


        //Input properties
        public int lightBarrier1
        {
            get
            {
                return _lightBarrier1;
            }
            set
            {
                _lightBarrier1 = value;
                if (_lightBarrier1 != __lightBarrier1 && LightBarrier1Event != null)
                {
                    __lightBarrier1 = _lightBarrier1;
                    LightBarrier1Event();
                }
            }
        }
        public int lightBarrier2
        {
            get
            {
                return _lightBarrier2;
            }
            set
            {
                _lightBarrier2 = value;
                if (_lightBarrier2 != __lightBarrier2 && LightBarrier2Event != null)
                {
                    __lightBarrier2 = _lightBarrier2;
                    LightBarrier2Event();
                }
            }
        }
        public int btnStart
        {
            get
            {
                return _btnStart;
            }
            set
            {
                _btnStart = value;
                if (_btnStart != __btnStart && BtnStartEvent != null)
                {
                    __btnStart = _btnStart;
                    BtnStartEvent();
                }
            }
        }
        public int btnReset
        {
            get
            {
                return _btnReset;
            }
            set
            {
                _btnReset = value;
                if (_btnReset != __btnReset && BtnResetEvent != null)
                {
                    __btnReset = _btnReset;
                    BtnResetEvent();
                }
            }
        }
        public int btnEmergency1
        {
            get
            {
                return _btnEmergency1;
            }
            set
            {
                _btnEmergency1 = value;
                if (_btnEmergency1 != __btnEmergency1 && BtnEmergency1Event != null)
                {
                    __btnEmergency1 = _btnEmergency1;
                    BtnEmergency1Event();
                }
            }
        }
        public int btnEmergency2
        {
            get
            {
                return _btnEmergency2;
            }
            set
            {
                _btnEmergency2 = value;
                if (_btnEmergency2 != __btnEmergency2 && BtnEmergency2Event != null)
                {
                    __btnEmergency2 = _btnEmergency2;
                    BtnEmergency2Event();
                }
            }
        }
        public int rfSensor1
        {
            get
            {
                return _rfSensor1;
            }
            set
            {
                _rfSensor1 = value;
                if (_rfSensor1 != __rfSensor1 && RfSensor1Event != null)
                {
                    __rfSensor1 = _rfSensor1;
                    RfSensor1Event();
                }
            }
        }
        public int rfSensor2
        {
            get
            {
                return _rfSensor2;
            }
            set
            {
                _rfSensor2 = value;
                if (_rfSensor2 != __rfSensor2 && RfSensor2Event != null)
                {
                    __rfSensor2 = _rfSensor2;
                    RfSensor2Event();
                }
            }
        }
        public int rfSensor3
        {
            get
            {
                return _rfSensor3;
            }
            set
            {
                _rfSensor3 = value;
                if (_rfSensor3 != __rfSensor3 && RfSensor3Event != null)
                {
                    __rfSensor3 = _rfSensor3;
                    RfSensor3Event();
                }
            }
        }
        public int rfSensor4
        {
            get
            {
                return _rfSensor4;
            }
            set
            {
                _rfSensor4 = value;
                if (_rfSensor4 != __rfSensor4 && RfSensor4Event != null)
                {
                    __rfSensor4 = _rfSensor4;
                    RfSensor4Event();
                }
            }
        }
        public int door1
        {
            get
            {
                return _door1;
            }
            set
            {
                _door1 = value;
                if (_door1 != __door1 && Door1Event != null)
                {
                    __door1 = _door1;
                    Door1Event();
                }
            }
        }
        public int door2
        {
            get
            {
                return _door2;
            }
            set
            {
                _door2 = value;
                if (_door2 != __door2 && Door2Event != null)
                {
                    __door2 = _door2;
                    Door2Event();
                }
            }
        }

        //Output properties
        public int conveyor1
        {
            get
            {
                return _conveyor1;
            }
            set
            {
                _conveyor1 = value;
                if (_conveyor1 != __conveyor1 && Conveyor1Event != null)
                {
                    __conveyor1 = _conveyor1;
                    Conveyor1Event();
                }
            }
        }
        public int conveyor2
        {
            get
            {
                return _conveyor2;
            }
            set
            {
                _conveyor2 = value;
                if (_conveyor2 != __conveyor2 && Conveyor2Event != null)
                {
                    __conveyor2 = _conveyor2;
                    Conveyor2Event();
                }
            }
        }
        public int conveyor3
        {
            get
            {
                return _conveyor3;
            }
            set
            {
                _conveyor3 = value;
                if (_conveyor3 != __conveyor3 && Conveyor3Event != null)
                {
                    __conveyor3 = _conveyor3;
                    Conveyor3Event();
                }
            }
        }
        public int conveyor4
        {
            get
            {
                return _conveyor4;
            }
            set
            {
                _conveyor4 = value;
                if (_conveyor4 != __conveyor4 && Conveyor4Event != null)
                {
                    __conveyor4 = _conveyor4;
                    Conveyor4Event();
                }
            }
        }
        public int turntable
        {
            get
            {
                return _turntable;
            }
            set
            {
                _turntable = value;
                if (_turntable != __turntable && TurntableEvent != null)
                {
                    __turntable = _turntable;
                    TurntableEvent();
                }
            }
        }
        public int lightColumnBarcodeYellow
        {
            get
            {
                return _lightColumnBarcodeYellow;
            }
            set
            {
                _lightColumnBarcodeYellow = value;
                if (_lightColumnBarcodeYellow != __lightColumnBarcodeYellow && LightColumnBarcodeYellowEvent != null)
                {
                    __lightColumnBarcodeYellow = _lightColumnBarcodeYellow;
                    LightColumnBarcodeYellowEvent();
                }
            }
        }
        public int lightColumnBarcodeGreen
        {
            get
            {
                return _lightColumnBarcodeGreen;
            }
            set
            {
                _lightColumnBarcodeGreen = value;
                if (_lightColumnBarcodeGreen != __lightColumnBarcodeGreen && LightColumnBarcodeGreenEvent != null)
                {
                    __lightColumnBarcodeGreen = _lightColumnBarcodeGreen;
                    LightColumnBarcodeGreenEvent();
                }
            }
        }
        public int lightColumnBarcodeRed
        {
            get
            {
                return _lightColumnBarcodeRed;
            }
            set
            {
                _lightColumnBarcodeRed = value;
                if (_lightColumnBarcodeRed != __lightColumnBarcodeRed && LightColumnBarcodeRedEvent != null)
                {
                    __lightColumnBarcodeRed = _lightColumnBarcodeRed;
                    LightColumnBarcodeRedEvent();
                }
            }
        }
        public int lightColumnBarcodeBuzzer
        {
            get
            {
                return _lightColumnBarcodeBuzzer;
            }
            set
            {
                _lightColumnBarcodeBuzzer = value;
                if (_lightColumnBarcodeBuzzer != __lightColumnBarcodeBuzzer && LightColumnBarcodeBuzzerEvent != null)
                {
                    __lightColumnBarcodeBuzzer = _lightColumnBarcodeBuzzer;
                    LightColumnBarcodeBuzzerEvent();
                }
            }
        }
        public int lightColumnWeighingGreen
        {
            get
            {
                return _lightColumnWeighingGreen;
            }
            set
            {
                _lightColumnWeighingGreen = value;
                if (_lightColumnWeighingGreen != __lightColumnWeighingGreen && LightColumnWeighingGreenEvent != null)
                {
                    __lightColumnWeighingGreen = _lightColumnWeighingGreen;
                    LightColumnWeighingGreenEvent();
                }
            }
        }
        public int lightColumnWeighingRed
        {
            get
            {
                return _lightColumnWeighingRed;
            }
            set
            {
                _lightColumnWeighingRed = value;
                if (_lightColumnWeighingRed != __lightColumnWeighingRed && LightColumnWeighinhRedEvent != null)
                {
                    __lightColumnWeighingRed = _lightColumnWeighingRed;
                    LightColumnWeighinhRedEvent();
                }
            }
        }
        public int lightColumnWeighingBuzzer
        {
            get
            {
                return _lightColumnWeighingBuzzer;
            }
            set
            {
                _lightColumnWeighingBuzzer = value;
                if (_lightColumnWeighingBuzzer != __lightColumnWeighingBuzzer && LightColumnWeighingBuzzerEvent != null)
                {
                    __lightColumnWeighingBuzzer = _lightColumnWeighingBuzzer;
                    LightColumnWeighingBuzzerEvent();
                }
            }
        }
        public int lightColumnReadyGreen
        {
            get
            {
                return _lightColumnReadyGreen;
            }
            set
            {
                _lightColumnReadyGreen = value;
                if (_lightColumnReadyGreen != __lightColumnReadyGreen && LightColumnReadyGreenEvent != null)
                {
                    __lightColumnReadyGreen = _lightColumnReadyGreen;
                    LightColumnReadyGreenEvent();
                }
            }
        }
        public int lightColumnReadyRed
        {
            get
            {
                return _lightColumnReadyRed;
            }
            set
            {
                _lightColumnReadyRed = value;
                if (_lightColumnReadyRed != __lightColumnReadyRed && LightColumnReadyRedEvent != null)
                {
                    __lightColumnReadyRed = _lightColumnReadyRed;
                    LightColumnReadyRedEvent();
                }
            }
        }
        public int lightColumnReadyBuzzer
        {
            get
            {
                return _lightColumnReadyBuzzer;
            }
            set
            {
                _lightColumnReadyBuzzer = value;
                if (_lightColumnReadyBuzzer != __lightColumnReadyBuzzer && LightColumnReadyBuzzerEvent != null)
                {
                    __lightColumnReadyBuzzer = _lightColumnReadyBuzzer;
                    LightColumnReadyBuzzerEvent();
                }
            }
        }

        //Data properties
        public int barcodeAndWeight
        {
            get
            {
                return _barcodeAndWeight;
            }
            set
            {
                _barcodeAndWeight = value;
                if (_barcodeAndWeight != __barcodeAndWeight && BarcodeAndWeightEvent != null)
                {
                    __barcodeAndWeight = _barcodeAndWeight;
                    BarcodeAndWeightEvent();
                }
            }
        }
        public int streclemeSonuc
        {
            get
            {
                return _streclemeSonuc;
            }
            set
            {
                _streclemeSonuc = value;
                if (_streclemeSonuc != __streclemeSonuc && StreclemeSonucEvent != null)
                {
                    __streclemeSonuc = _streclemeSonuc;
                    if (_streclemeSonuc != 0)
                        StreclemeSonucEvent();
                }
            }
        }
        public int streclemeHata
        {
            get
            {
                return _streclemeHata;
            }
            set
            {
                _streclemeHata = value;
                if (_streclemeHata != __streclemeHata && StreclemeHataEvent != null)
                {
                    __streclemeHata = _streclemeHata;
                    StreclemeHataEvent();
                }
            }
        }
        public int etiketSonuc
        {
            get
            {
                return _etiketSonuc;
            }
            set
            {
                _etiketSonuc = value;
                if (_etiketSonuc != __etiketSonuc && EtiketSonucEvent != null)
                {
                    __etiketSonuc = _etiketSonuc;
                    if (_etiketSonuc != 0)
                        EtiketSonucEvent();
                }
            }
        }
        public int etiketHata
        {
            get
            {
                return _etiketHata;
            }
            set
            {
                _etiketHata = value;
                if (_etiketHata != __etiketHata && EtiketHataEvent != null)
                {
                    __etiketHata = _etiketHata;
                    EtiketHataEvent();
                }
            }
        }
        public int moving
        {
            get
            {
                return _moving;
            }
            set
            {
                _moving = value;
                if (_moving != __moving && MovingEvent != null)
                {
                    __moving = _moving;
                    MovingEvent();
                }
            }
        }
        public int mode
        {
            get
            {
                return _mode;
            }
            set
            {
                _mode = value;
                if (_mode != __mode && ModeEvent != null)
                {
                    __mode = _mode;
                    ModeEvent();
                }
            }
        }
        public int security
        {
            get
            {
                return _security;
            }
            set
            {
                _security = value;
                if (_security != __security && SecurityEvent != null)
                {
                    __security = _security;
                    SecurityEvent();
                }
            }
        }
        public int automaticModeActive
        {
            get
            {
                return _automaticModeActive;
            }

            set
            {
                _automaticModeActive = value;
                if (_automaticModeActive != __automaticModeActive && AutomaticModeActiveEvent != null)
                {
                    __automaticModeActive = _automaticModeActive;
                    AutomaticModeActiveEvent();
                }
            }
        }
        public int sensor
        {
            get
            {
                return _sensor;
            }
            set
            {
                _sensor = value;
            }
        }

        //Input buffers
        private int __lightBarrier1;
        private int __lightBarrier2;
        private int __btnStart;
        private int __btnReset;
        private int __btnEmergency1;
        private int __btnEmergency2;
        private int __rfSensor1;
        private int __rfSensor2;
        private int __rfSensor3;
        private int __rfSensor4;
        private int __door1;
        private int __door2;

        //Output buffers
        private int __conveyor1;
        private int __conveyor2;
        private int __conveyor3;
        private int __conveyor4;
        private int __turntable;
        private int __lightColumnBarcodeYellow;
        private int __lightColumnBarcodeGreen;
        private int __lightColumnBarcodeRed;
        private int __lightColumnBarcodeBuzzer;
        private int __lightColumnWeighingGreen;
        private int __lightColumnWeighingRed;
        private int __lightColumnWeighingBuzzer;
        private int __lightColumnReadyGreen;
        private int __lightColumnReadyRed;
        private int __lightColumnReadyBuzzer;

        //Data buffers
        private int __barcodeAndWeight;
        private int __streclemeSonuc;
        private int __streclemeHata;
        private int __etiketSonuc = 0;
        private int __etiketHata;
        private int __moving;
        private int __mode;
        private int __security;
        private int __automaticModeActive;

        //Input fields
        private int _lightBarrier1;
        private int _lightBarrier2;
        private int _btnStart;
        private int _btnReset;
        private int _btnEmergency1;
        private int _btnEmergency2;
        private int _rfSensor1;
        private int _rfSensor2;
        private int _rfSensor3;
        private int _rfSensor4;
        private int _door1;
        private int _door2;

        //Output fields
        private int _conveyor1;
        private int _conveyor2;
        private int _conveyor3;
        private int _conveyor4;
        private int _turntable;
        private int _lightColumnBarcodeYellow;
        private int _lightColumnBarcodeGreen;
        private int _lightColumnBarcodeRed;
        private int _lightColumnBarcodeBuzzer;
        private int _lightColumnWeighingGreen;
        private int _lightColumnWeighingRed;
        private int _lightColumnWeighingBuzzer;
        private int _lightColumnReadyGreen;
        private int _lightColumnReadyRed;
        private int _lightColumnReadyBuzzer;

        //Data fields
        private int _barcodeAndWeight;
        private int _streclemeSonuc;
        private int _streclemeHata;
        private int _etiketSonuc = 0;
        private int _etiketHata;
        private int _moving;
        private int _mode;
        private int _security;
        private int _automaticModeActive;
        private int _sensor = 0;

        public ModbusClient modbusClient { get; set; }
        public Form1 mainForm { get; set; }
        int[] registers = new int[41];
        int counter = 0;
        public bool IOException = false;
        string exceptionMessage;

        public Plc(string ipAddress, int port)
        {
            modbusClient = new ModbusClient(ipAddress, port);
        }

        public void writeMultiplePlc(int startingAddress, int[] values)
        {
            try
            {
                modbusClient.Connect();
                Thread.Sleep(100);
                modbusClient.WriteMultipleRegisters(startingAddress, values);
                Thread.Sleep(100);
                modbusClient.Disconnect();
                if (counter != 0)
                    counter = 0;
            }
            catch (System.IO.IOException)
            {
                if (counter < 3)
                {
                    counter++;
                    writeMultiplePlc(startingAddress, values);
                }
                else
                {
                    MessageBox.Show("PLC ye veri gönderilemedi!\nLütfen tekrar deneyin.");
                    mainForm.tryAgain = true;
                    counter = 0;
                }
            }
            catch (EasyModbus.Exceptions.ConnectionException Ex)
            {
                exceptionMessage = Ex.Message;
                string ErrorMessage = "PLC ye bağlanırken hata ile karşılaşıldı.";
                ErrorMessage += Environment.NewLine;
                ErrorMessage += Environment.NewLine;
                ErrorMessage += Ex.Message;
                mainForm.tryAgain = true;
                MessageBox.Show(ErrorMessage, "Bağlantı Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (System.Net.Sockets.SocketException Ex)
            {
                exceptionMessage = Ex.Message;
                string ErrorMessage = "PLC ye bağlanırken hata ile karşılaşıldı.";
                ErrorMessage += Environment.NewLine;
                ErrorMessage += Environment.NewLine;
                ErrorMessage += Ex.Message;
                mainForm.tryAgain = true;
                MessageBox.Show(ErrorMessage, "Bağlantı Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void writePlc(int startingAddress, int value)
        {
            try
            {
                modbusClient.Connect();
                Thread.Sleep(100);
                modbusClient.WriteSingleRegister(startingAddress, value);
                Thread.Sleep(100);
                modbusClient.Disconnect();
                if (counter != 0)
                    counter = 0;
            }
            catch (System.IO.IOException)
            {
                if (counter < 3)
                {
                    counter++;
                    IOException = true;
                }
                else
                {
                    counter = 0;
                    MessageBox.Show("PLC ye veri gönderilemedi!\nLütfen tekrar deneyin.");
                    mainForm.tryAgain = true;
                }
            }
            catch (EasyModbus.Exceptions.ConnectionException Ex)
            {
                exceptionMessage = Ex.Message;
                string ErrorMessage = "PLC ye bağlanırken hata ile karşılaşıldı.";
                ErrorMessage += Environment.NewLine;
                ErrorMessage += Environment.NewLine;
                ErrorMessage += Ex.Message;
                mainForm.tryAgain = true;
                MessageBox.Show(ErrorMessage, "Bağlantı Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (System.Net.Sockets.SocketException Ex)
            {
                exceptionMessage = Ex.Message;
                string ErrorMessage = "PLC ye bağlanırken hata ile karşılaşıldı.";
                ErrorMessage += Environment.NewLine;
                ErrorMessage += Environment.NewLine;
                ErrorMessage += Ex.Message;
                mainForm.tryAgain = true;
                MessageBox.Show(ErrorMessage, "Bağlantı Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void readAllInputs()
        {
            try
            {
                modbusClient.Connect();
                Thread.Sleep(100);
                registers = modbusClient.ReadHoldingRegisters(0, 36);
                Thread.Sleep(100);
                modbusClient.Disconnect();

                //Reading inputs
                //lightBarrier1 = registers[0];
                //lightBarrier2 = registers[1];
                btnStart = registers[2];
                btnReset = registers[3];
                //btnEmergency1 = registers[4];
                //btnEmergency2 = registers[5];
                //rfSensor1 = registers[6];
                //rfSensor2 = registers[7];
                //rfSensor3 = registers[8];
                //rfSensor4 = registers[9];
                //door1 = registers[10];
                //door2 = registers[11];

                //Reading outputs
                //conveyor1 = registers[12];
                //conveyor2 = registers[13];
                //conveyor3 = registers[14];
                //conveyor4 = registers[15];
                //turntable = registers[16];
                lightColumnBarcodeYellow = registers[17];
                lightColumnBarcodeGreen = registers[18];
                lightColumnBarcodeRed = registers[19];
                lightColumnBarcodeBuzzer = registers[20];
                lightColumnWeighingGreen = registers[21];
                lightColumnWeighingRed = registers[22];
                lightColumnWeighingBuzzer = registers[23];
                lightColumnReadyGreen = registers[24];
                lightColumnReadyRed = registers[25];
                lightColumnReadyBuzzer = registers[26];

                //Reading datas
                //barcodeAndWeight = registers[27];
                streclemeSonuc = registers[28];
                streclemeHata = registers[29];
                etiketSonuc = registers[30];
                etiketHata = registers[31];
                moving = registers[32];
                //mode = registers[33];
                security = registers[34];
                automaticModeActive = registers[35];

                if (counter != 0)
                    counter = 0;
            }
            catch (System.IO.IOException)
            {
                if (counter < 3)
                {
                    counter++;
                    IOException = true;
                }
                else
                {
                    counter = 0;
                    MessageBox.Show("PLC ye veri gönderilemedi!\nLütfen tekrar deneyin.");
                    mainForm.tryAgain = true;
                }
            }
            catch (EasyModbus.Exceptions.ConnectionException Ex)
            {
                exceptionMessage = Ex.Message;
                string ErrorMessage = "PLC ye bağlanırken hata ile karşılaşıldı.";
                ErrorMessage += Environment.NewLine;
                ErrorMessage += Environment.NewLine;
                ErrorMessage += Ex.Message;
                mainForm.tryAgain = true;
                MessageBox.Show(ErrorMessage, "Bağlantı Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (System.Net.Sockets.SocketException Ex)
            {
                exceptionMessage = Ex.Message;
                string ErrorMessage = "PLC ye bağlanırken hata ile karşılaşıldı.";
                ErrorMessage += Environment.NewLine;
                ErrorMessage += Environment.NewLine;
                ErrorMessage += Ex.Message;
                mainForm.tryAgain = true;
                MessageBox.Show(ErrorMessage, "Bağlantı Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void readPlcManualMode()
        {
            try
            {
                modbusClient.Connect();
                Thread.Sleep(100);
                registers = modbusClient.ReadHoldingRegisters(0, 41);
                Thread.Sleep(100);
                modbusClient.Disconnect();

                //Reading inputs
                //lightBarrier1 = registers[0];
                //lightBarrier2 = registers[1];
                //btnStart = registers[2];
                //btnReset = registers[3];
                //btnEmergency1 = registers[4];
                //btnEmergency2 = registers[5];
                //rfSensor1 = registers[6];
                //rfSensor2 = registers[7];
                //rfSensor3 = registers[8];
                //rfSensor4 = registers[9];
                //door1 = registers[10];
                //door2 = registers[11];

                //Reading outputs
                //conveyor1 = registers[12];
                //conveyor2 = registers[13];
                //conveyor3 = registers[14];
                //conveyor4 = registers[15];
                //turntable = registers[16];
                lightColumnBarcodeYellow = registers[17];
                lightColumnBarcodeGreen = registers[18];
                lightColumnBarcodeRed = registers[19];
                lightColumnBarcodeBuzzer = registers[20];
                lightColumnWeighingGreen = registers[21];
                lightColumnWeighingRed = registers[22];
                lightColumnWeighingBuzzer = registers[23];
                lightColumnReadyGreen = registers[24];
                lightColumnReadyRed = registers[25];
                lightColumnReadyBuzzer = registers[26];

                //Reading datas
                //barcodeAndWeight = registers[27];
                //streclemeSonuc = registers[28];
                streclemeHata = registers[29];
                //etiketSonuc = registers[30];
                etiketHata = registers[31];
                moving = registers[32];
                //mode = registers[33];
                security = registers[34];
                automaticModeActive = registers[35];
                sensor = registers[40];

                if (counter != 0)
                    counter = 0;
            }
            catch (System.IO.IOException)
            {
                if (counter < 3)
                {
                    counter++;
                    IOException = true;
                }
                else
                {
                    counter = 0;
                    MessageBox.Show("PLC ye veri gönderilemedi!\nLütfen tekrar deneyin.");
                    mainForm.tryAgain = true;
                }
            }
            catch (EasyModbus.Exceptions.ConnectionException Ex)
            {
                exceptionMessage = Ex.Message;
                string ErrorMessage = "PLC ye bağlanırken hata ile karşılaşıldı.";
                ErrorMessage += Environment.NewLine;
                ErrorMessage += Environment.NewLine;
                ErrorMessage += Ex.Message;
                mainForm.tryAgain = true;
                MessageBox.Show(ErrorMessage, "Bağlantı Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (System.Net.Sockets.SocketException Ex)
            {
                exceptionMessage = Ex.Message;
                string ErrorMessage = "PLC ye bağlanırken hata ile karşılaşıldı.";
                ErrorMessage += Environment.NewLine;
                ErrorMessage += Environment.NewLine;
                ErrorMessage += Ex.Message;
                mainForm.tryAgain = true;
                MessageBox.Show(ErrorMessage, "Bağlantı Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
