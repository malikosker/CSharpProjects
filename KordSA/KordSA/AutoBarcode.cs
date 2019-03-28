using System;
using System.Net;
using System.Net.Sockets;
using System.IO.Ports;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KordSA
{
    class AutoBarcode
    {
        Plc _plc = new Plc("192.168.0.1", 502);
        public string _barcode1;
        public string _barcode2;

        public void runSabitBarcod()
        {
            _barcode1 = null;
            _barcode2 = null;

            byte[] arti = new byte[1];
            byte[] eksi = new byte[1];
            byte[] received1 = new byte[9];
            byte[] received2 = new byte[12];
            arti[0] = 43;
            eksi[0] = 45;
            try
            {
                Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
                s.Connect(new IPEndPoint(IPAddress.Parse("192.168.0.5"), 10003));
                s.Send(arti);
                s.ReceiveTimeout = 4000;
                int c = 0;
                try
                {
                    //c = s.Receive(received1);
                    //string a = System.Text.Encoding.UTF8.GetString(received1);
                    //c = s.Receive(received2);
                    //string b = System.Text.Encoding.UTF8.GetString(received2);
                    //if (a.Length == 9)
                    //{
                    //    _barcode1 = a;
                    //    _barcode2 = b;
                    //}
                    //else
                    //{
                    //    _barcode2 = a;
                    //    _barcode1 = b;
                    //}
                    c = s.Receive(received1);
                    if (c == 9)
                    {
                        _barcode1 = System.Text.Encoding.UTF8.GetString(received1);
                    }
                    else if (c == 12)
                    {
                        _barcode2 = System.Text.Encoding.UTF8.GetString(received1);
                    }
                    c = s.Receive(received2);
                    if (c == 9)
                    {
                        _barcode1 = System.Text.Encoding.UTF8.GetString(received2);
                    }
                    else if (c == 12)
                    {
                        _barcode2 = System.Text.Encoding.UTF8.GetString(received2);
                    }
                    if (!(_barcode1 != null && _barcode1.Length == 9 && _barcode2 != null && _barcode2.Length == 12))
                    {
                        MessageBox.Show("Sabit barkod okuyucu barkodları okuyamadı.\nLütfen mobil barcod okuyucuyu kullanarak devam ediniz", "Okuma Hatası", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        _barcode1 = null;
                        _barcode2 = null;
                    }
                }
                catch (Exception)
                {
                    s.Send(eksi);
                    _plc.writePlc(27, 2);
                    DialogResult dialog = MessageBox.Show("Sabit barkod okuyucu barkodları okuyamadı.\nLütfen mobil barcod okuyucuyu kullanarak devam ediniz", "Okuma Hatası", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                s.Disconnect(false);
            }
            catch (System.Net.Sockets.SocketException Ex)
            {
                string ErrorMessage = "Sabit barkod okuyucuya bağlanırken hata ile karşılaşıldı.";
                ErrorMessage += Environment.NewLine;
                ErrorMessage += Environment.NewLine;
                ErrorMessage += Ex.Message;

                MessageBox.Show(ErrorMessage, "Bağlantı Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
