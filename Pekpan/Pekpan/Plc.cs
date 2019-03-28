using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using EasyModbus;
using System.Windows.Forms;

namespace Pekpan
{
    public delegate void ConnectingController();
    public delegate void HucreDurumController();
    public delegate void WatchProductsController();
    public delegate void SensorInformationsController();
    public delegate void ProductDroppedController1();
    public delegate void ProductDroppedController2();
    public delegate void DensityController2();
    class Plc
    {
        public event ConnectingController ConnectingEvent;
        public event HucreDurumController HucreDurumEvent;
        public event WatchProductsController WatchProductsEvent;
        public event SensorInformationsController SensorInformationsEvent;
        public event ProductDroppedController1 ProductDroppedEvent1;
        public event ProductDroppedController2 ProductDroppedEvent2;
        public event DensityController2 DensityEvent2;
        public bool Connecting
        {
            get
            {
                return _connecting;
            }
            set
            {
                _connecting = value;
                if (_connecting != __connecting && ConnectingEvent != null)
                {
                    __connecting = _connecting;
                    ConnectingEvent();
                }
            }
        }
        public int hucreDurum
        {
            get
            {
                return _hucreDurum;
            }
            set
            {
                _hucreDurum = value;
                if (_hucreDurum != __hucreDurum && HucreDurumEvent != null)
                {
                    __hucreDurum = _hucreDurum;
                    HucreDurumEvent();
                }
            }
        }
        public int watchProducts
        {
            get
            {
                return _watchProducts;
            }
            set
            {
                _watchProducts = value;
                if (_watchProducts != __watchProducts && WatchProductsEvent != null)
                {
                    __watchProducts = _watchProducts;
                    WatchProductsEvent();
                }
            }
        }
        public int sensorInformations
        {
            get
            {
                return _sensorInformations;
            }
            set
            {
                _sensorInformations = value;
                if (_sensorInformations != __sensorInformations && SensorInformationsEvent != null)
                {
                    __sensorInformations = _sensorInformations;
                    SensorInformationsEvent();
                }
            }
        }
        public string productDropped1
        {
            get
            {
                return _productDropped1;
            }
            set
            {
                _productDropped1 = value;
                if (_productDropped1 == __productDropped1 && ProductDroppedEvent1 != null)
                {
                    ProductDroppedEvent1();
                }
            }
        }
        public string productDropped2
        {
            get
            {
                return _productDropped2;
            }
            set
            {
                _productDropped2 = value;
                if (_productDropped2 != __productDropped2 && ProductDroppedEvent2 != null)
                {
                    ProductDroppedEvent2();
                }
            }
        }
        public int density2
        {
            get
            {
                return _density2;
            }
            set
            {
                _density2 = value;
                if (_density2 != __density2 && DensityEvent2 != null)
                {
                    __density2 = _density2;
                    DensityEvent2();
                }
            }
        }

        //fields
        private bool _connecting;
        private int _hucreDurum = 65536;
        private int _watchProducts = 65536;
        private int _sensorInformations = 65536;
        private string _productDropped1 = "0000";
        private string _productDropped2 = "0000";
        private int _density2 = 11;
        //buffers
        private bool __connecting;
        private int __hucreDurum = 65536;
        private int __watchProducts = 65536;
        private int __sensorInformations = 65536;
        public string __productDropped1 = "0000";
        public string __productDropped2 = "0000";
        private int __density2 = 11;

        public ModbusClient modbusClient { get; set; }
        string exceptionMessage;
        public bool tryAgain = false;
        public int counter = 0;

        public Plc(string ipAddress, int port)
        {
            modbusClient = new ModbusClient(ipAddress, port);
        }

        public void writeMultiplePlc(int startingAddress, int[] values)
        {
            try
            {
                modbusClient.Connect();
                modbusClient.WriteMultipleRegisters(startingAddress, values);
                modbusClient.Disconnect();
                Connecting = true;
                if (counter != 0)
                    counter = 0;
            }
            catch (System.IO.IOException)
            {
                if (counter < 3)
                {
                    counter++;
                    tryAgain = true;
                }
                else
                {
                    counter = 0;
                    MessageBox.Show("PLC ye veri gönderilemedi!\nLütfen tekrar deneyin.");
                }
            }
            catch (EasyModbus.Exceptions.ConnectionException Ex)
            {
                if (Connecting)
                {
                    exceptionMessage = Ex.Message;
                    string ErrorMessage = "PLC ye bağlanırken hata ile karşılaşıldı.";
                    ErrorMessage += Environment.NewLine;
                    ErrorMessage += Environment.NewLine;
                    ErrorMessage += Ex.Message;
                    MessageBox.Show(ErrorMessage, "Bağlantı Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Connecting = false;
                }

            }
            catch (System.Net.Sockets.SocketException Ex)
            {
                if (Connecting)
                {
                    exceptionMessage = Ex.Message;
                    string ErrorMessage = "PLC ye bağlanırken hata ile karşılaşıldı.";
                    ErrorMessage += Environment.NewLine;
                    ErrorMessage += Environment.NewLine;
                    ErrorMessage += Ex.Message;
                    MessageBox.Show(ErrorMessage, "Bağlantı Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Connecting = false;
                }
            }
        }

        public void readSensorDataInPlc(int startingAddress)
        {
            try
            {
                modbusClient.Connect();
                int[] a = modbusClient.ReadHoldingRegisters(startingAddress, 5);
                modbusClient.Disconnect();
                Connecting = true;
                hucreDurum = a[0];
                watchProducts = a[1];
                sensorInformations = a[2];
                productDropped1 = Convert.ToString((ushort)a[3] + 16, 2).Remove(0, 1);
                //productDropped2 = Convert.ToString((ushort)a[4] + 16, 2).Remove(0, 1);
                density2 = a[4];
                if (counter != 0)
                    counter = 0;
            }
            catch (System.IO.IOException)
            {
                if (counter < 3)
                {
                    counter++;
                    tryAgain = true;
                }
                else
                {
                    counter = 0;
                    MessageBox.Show("PLC ye veri gönderilemedi!\nLütfen tekrar deneyin.");
                }
            }
            catch (EasyModbus.Exceptions.ConnectionException Ex)
            {
                if (Connecting)
                {
                    exceptionMessage = Ex.Message;
                    string ErrorMessage = "PLC ye bağlanırken hata ile karşılaşıldı.";
                    ErrorMessage += Environment.NewLine;
                    ErrorMessage += Environment.NewLine;
                    ErrorMessage += Ex.Message;
                    MessageBox.Show(ErrorMessage, "Bağlantı Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Connecting = false;
                }
            }
            catch (System.Net.Sockets.SocketException Ex)
            {
                if (Connecting)
                {
                    exceptionMessage = Ex.Message;
                    string ErrorMessage = "PLC ye bağlanırken hata ile karşılaşıldı.";
                    ErrorMessage += Environment.NewLine;
                    ErrorMessage += Environment.NewLine;
                    ErrorMessage += Ex.Message;
                    MessageBox.Show(ErrorMessage, "Bağlantı Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Connecting = false;
                }
            }
        }
    }
}

