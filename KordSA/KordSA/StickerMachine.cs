using System;
using System.IO.Ports;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KordSA
{
    class StickerMachine
    {
        public Sticker _sticker = new Sticker();
        SerialPort _serialPortStickerMachine;
        string _text;
        public struct Sticker
        {
            public string TIP;
            public string REF;
            public string PAKET;
            public string GERCEKMERGE;
            public string DENYE;
            public string DTEX;
            public string DENIER;
            public string FILAMENT;
            public string PACKDATE;
            public string UNIT;
            public string REFERANS;
            public string TICAGKG;
            public string TICAGLB;
            public string TOPLAMAGKG;
            public string TOPLAMAGLB;
            public string SAPKOD;
        }
        public void adjustProperties(List<string> DBdatas)
        {
            _sticker.TIP = DBdatas[0];
            _sticker.REF = DBdatas[1];
            _sticker.PAKET = DBdatas[2];
            _sticker.GERCEKMERGE = DBdatas[3];
            _sticker.DENYE = DBdatas[4];
            _sticker.DTEX = DBdatas[5];
            _sticker.DENIER = DBdatas[6];
            _sticker.FILAMENT = DBdatas[7];
            _sticker.PACKDATE = DBdatas[8];
            _sticker.UNIT = DBdatas[9];
            _sticker.REFERANS = DBdatas[10];
            _sticker.TICAGKG = DBdatas[11];
            _sticker.TICAGLB = DBdatas[12];
            _sticker.TOPLAMAGKG = DBdatas[13];
            _sticker.TOPLAMAGLB = DBdatas[14];
            _sticker.SAPKOD = DBdatas[15];
        }
        public StickerMachine(SerialPort serialPort)
        {
            _serialPortStickerMachine = serialPort;
        }

        public void open()
        {
            _serialPortStickerMachine.Open();
        }

        public void close()
        {
            _serialPortStickerMachine.Close();
        }

        public void sendData(string text)
        {
            _serialPortStickerMachine.Write(text);
            _serialPortStickerMachine.Write(text);
        }

        public string readData()
        {
            return _serialPortStickerMachine.ReadExisting();
        }

        public string orderText(Sticker sticker)
        {
            _text =
            "#!A1#DC" +
            "#IMSR150.08/210.16" +
            "#RX0" +
            "#ERN/1//0" +
            "#R0/0" +

            "#T18.00 #J80.83" +
            "#YG/1///C:\\GRAPHICS\\kordsa_logo.gif#G" +

            "#T143.41 #J204.50 #YB13/2P2.0O/10.16/2///" + sticker.PAKET + "#G" +
            "#T143.58 #J66.50 #YB13/2P2.0O/10.16/2///" + sticker.PAKET + "#G" +
            "#T143.66 #J139.41 #YB13/2P2.0O/10.16/2///" + sticker.PAKET + "#G" +
            "#T115.58 #J205.00 #YB13/2P2.0O/20.00/13///" + sticker.PAKET + "#G" +
            "#T03.83 #J181.00 #YL0/0/01.00/116.33" +
            "#T122.16 #J04.16 #YL0/1/01.00/200.16" +
            "#T39.58 #J05.66 #YL0/1/01.00/176.50" +
            "#T65.41 #J05.66 #YL0/1/01.00/176.50" +
            "#T81.91 #J05.08 #YL0/1/01.00/176.41" +
            "#T97.25 #J05.66 #YL0/1/01.00/176.50" +
            "#T22.91 #J05.08 #YL0/1/01.00/176.41" +
            "#T122.41 #J69.83 #YL0/0/01.00/27.25" +
            "#T122.25 #J143.33 #YL0/0/01.00/26.91" +
            "#T44.66 #J12.16 #YN101/1B/59///PALLET NO#G" +
            "#T28.75 #J12.16 #YN101/1B/59///MATERIAL#G" +
            "#T36.16 #J12.16 #YN101/1B/85///" + sticker.TIP + "#G" +
            "#T50.08 #J136.83 #YN101/1B/59///MERGE#G" +
            "#T27.50 #J137.58 #YN101/1B/59///REF-PIN#G" +
            "#T37.66 #J137.00 #YN101/1B/80///" + sticker.REF + "#G" +
            "#T19.41 #J82.91 #YN101/1B/55///Made in TURKEY#G" +
            "#T62.58 #J11.08 #YN101/1B/288///" + sticker.PAKET + "#G" +
            "#T61.00 #J136.58 #YN101/1B/119///" + sticker.GERCEKMERGE + "#G" +
            "#T71.50 #J11.25 #YN101/1B/59///TYPE#G" +
            "#T78.58 #J11.25 #YN101/1B/85///" + sticker.DENYE + "#G" +
            "#T71.16 #J78.75 #YN101/1B/59///DTEX/DENIER#G" +
            "#T78.58 #J78.75 #YN101/1B/85///" + sticker.DTEX + "/" + sticker.DENIER + "#G" +
            "#T70.58 #J136.66 #YN101/1B/59///FILAMENTS#G" +
            "#T78.91 #J137.08 #YN101/1B/102///" + sticker.FILAMENT + "#G" +
            "#T101.91 #J10.16 #YN101/1B/59///COMMERCIAL WEIGHT#G" +
            "#T110.50 #J10.25 #YN101/1B/85///" + sticker.TICAGKG + " KG#G" +
            "#T119.00 #J10.25 #YN101/1B/85///" + sticker.TICAGLB + " LB#G" +
            "#T87.00 #J10.41 #YN101/1B/59///PACK DATE#G" +
            "#T93.41 #J10.41 #YN101/1B/85///" + sticker.PACKDATE + "#G" +
            "#T86.50 #J78.08 #YN101/1B/59///## OF BOBBINS#G" +
            "#T93.50 #J78.16 #YN101/1B/85///" + sticker.UNIT + "#G" +
            "#T86.66 #J136.50 #YN101/1B/59///ICN#G" +
            "#T95.58 #J136.16 #YN101/1B/102///" + sticker.REFERANS + "#G" +
            "#T101.91 #J77.58 #YN101/1B/59///GROSS WEIGHT#G" +
            "#T110.50 #J77.58 #YN101/1B/85///" + sticker.TOPLAMAGKG + " KG#G" +
            "#T119.00 #J77.58 #YN101/1B/85///" + sticker.TOPLAMAGLB + " LB#G" +
            "#T102.66 #J136.16 #YN101/1B/59///ERP CODE#G" +
            "#T130.66 #J10.50 #YN101/1B/85///" + sticker.PAKET + "#G" +
            "#T130.91 #J76.33 #YN101/1B/85///" + sticker.PAKET + "#G" +
            "#T136.41 #J10.91 #YN101/1B/42///" + sticker.GERCEKMERGE + "-" + sticker.DTEX + "/" + sticker.DENYE + "#G" +
            "#T113.75 #J135.33 #YN101/1B/102///" + sticker.SAPKOD + "#G" +
            "#T142.41 #J11.25 #YN101/1B/59///" + sticker.SAPKOD + "#G" +
            "#T136.75 #J76.75 #YN101/1B/42///" + sticker.GERCEKMERGE + "-" + sticker.DTEX + "/" + sticker.DENYE + "#G" +
            "#T146.58 #J11.75 #YN101/1B/42///" + sticker.PACKDATE + "#G" +
            "#T132.91 #J148.83 #YN101/1B/102///" + sticker.PAKET + "#G" +
            "#T142.66 #J77.08 #YN101/1B/59///" + sticker.SAPKOD + "#G" +
            "#T137.33 #J149.33 #YN101/1B/42///" + sticker.GERCEKMERGE + "-" + sticker.DTEX + "/" + sticker.DENYE + "#G" +
            "#T146.91 #J77.58 #YN101/1B/42///" + sticker.PACKDATE + "#G" +
            "#T143.33 #J149.66 #YN101/1B/59///" + sticker.SAPKOD + "#G" +
            "#T147.50 #J150.08 #YN101/1B/42///" + sticker.PACKDATE + "#G" +
            "#Q1#G" +
            "#!P1";
            return _text;
        }
    }
}
