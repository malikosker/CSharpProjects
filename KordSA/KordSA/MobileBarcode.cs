using System;
using System.IO.Ports;
using System.Windows.Forms;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KordSA
{
    public delegate void DataController();
    class MobileBarcode
    {
        public event DataController DataEvent;
        SerialPort _mobileBarcode;
        private string _data;
        private string __data;
        private bool connected;
        public string data
        {
            get
            {
                return _data;
            }
            set
            {
                _data = value;
                if (__data != _data && DataEvent != null)
                {
                    __data = _data;
                    DataEvent();
                }
            }
        }

        public MobileBarcode(SerialPort serialPort)
        {
            _mobileBarcode = serialPort;
            _mobileBarcode.DataReceived += _mobileBarcode_DataReceived;
        }

        private void _mobileBarcode_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            Thread.Sleep(50);
            data = _mobileBarcode.ReadExisting();
        }

        public bool open()
        {
            if (_mobileBarcode.IsOpen != true)
            {
                try
                {
                    _mobileBarcode.Open();
                    bool r = (_mobileBarcode.IsOpen == true);
                    connected = r;
                    return r;
                }
                catch (System.IO.IOException Ex)
                {
                    string ErrorMessage = "Mobil barkod okuyucuya bağlanırken hata ile karşılaşıldı.";
                    ErrorMessage += Environment.NewLine;
                    ErrorMessage += Environment.NewLine;
                    ErrorMessage += Ex.Message;
                    MessageBox.Show(ErrorMessage, "Bağlantı Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            return false;
        }

        public void close()
        {
            if (_mobileBarcode.IsOpen == true)
            {
                try
                {
                    _mobileBarcode.Close();
                }
                catch (System.IO.IOException Ex)
                {
                    string ErrorMessage = "Mobil barkod okuyucuya bağlanırken hata ile karşılaşıldı.";
                    ErrorMessage += Environment.NewLine;
                    ErrorMessage += Environment.NewLine;
                    ErrorMessage += Ex.Message;
                    MessageBox.Show(ErrorMessage, "Bağlantı Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
