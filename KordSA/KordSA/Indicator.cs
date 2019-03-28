using System;
using System.IO.Ports;
using DmitryBrant.CustomControls;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KordSA
{
    class Indicator
    {
        public string weight;
        public double dWeight;
        SerialPort _serialPortIndicator;
        SevenSegmentArray _sevenSegmentArray;

        public Indicator(SerialPort serialPort, SevenSegmentArray sevenSegmentArray)
        {
            _sevenSegmentArray = sevenSegmentArray;
            _serialPortIndicator = serialPort;
            _serialPortIndicator.DataReceived += _serialPortIndicator_DataReceived;
        }

        private void _serialPortIndicator_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            weight = _serialPortIndicator.ReadLine();
            weight = weight.Remove(0, 5).Remove(8);
            //if (weight[6] == ' ')
            //    weight = weight.Remove(0, 7).Remove(3);
            //else if (weight[5] == ' ')
            //    weight = weight.Remove(0, 6).Remove(4);
            //else if (weight[4] == ' ')
            //    weight = weight.Remove(0, 5).Remove(5);
            //else if (weight[3] == ' ')
            //    weight = weight.Remove(0, 4).Remove(6);
            _sevenSegmentArray.Value = weight;
            if (weight[0] != '-')
                dWeight = Convert.ToDouble(weight);
        }

        public void open()
        {
            _serialPortIndicator.Open();
        }

        public void close()
        {
            _serialPortIndicator.Close();
        }
    }
}
