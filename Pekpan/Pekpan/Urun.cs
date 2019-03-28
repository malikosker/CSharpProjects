using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pekpan
{
    public partial class Urun : UserControl
    {
        public Urun()
        {
            InitializeComponent();
            Active = true;
            SensorNo = 0;
            initialX = -1;
        }        

        [System.ComponentModel.Browsable(true)]
        [System.ComponentModel.Category("TaraControls")]
        //[DisplayName("UrunNo")]
        [System.ComponentModel.RefreshProperties(RefreshProperties.All)]
        [System.ComponentModel.Description("Ürün Barkodu")]
        public string ProdBarcode { get { return lbProdBar.Text; } set { lbProdBar.Text = value; } }

        [System.ComponentModel.Browsable(true)]
        [System.ComponentModel.Category("TaraControls")]
        //[DisplayName("UrunNo")]
        [System.ComponentModel.RefreshProperties(RefreshProperties.All)]
        [System.ComponentModel.Description("Sensör No")]
        int fSensorNo;
        public int SensorNo
        {
            get
            {
                return fSensorNo;
            }
            set
            {
                fSensorNo = value;
                if (fSensorNo == 14) fModbusAddr = 111;
                else if (fSensorNo == 12) fModbusAddr = 149;
                else if (fSensorNo == 10) fModbusAddr = 206;
                else if (fSensorNo == 9) fModbusAddr = 225;
                else if (fSensorNo == 7) fModbusAddr = 282;
                else if (fSensorNo == 6) fModbusAddr = 301;
                else if (fSensorNo == 4) fModbusAddr = 396;
                else if (fSensorNo == 3) fModbusAddr = 415;
                else if (fSensorNo == 1) fModbusAddr = 453;
                else fModbusAddr = 0;
            }
        }

        int initialX;
        public int InitialX
        {
            get { return initialX; }
            set { if (initialX ==-1) initialX = value; }
        }

        [System.ComponentModel.Browsable(true)]
        [System.ComponentModel.Category("TaraControls")]
        //[DisplayName("UrunNo")]
        [System.ComponentModel.RefreshProperties(RefreshProperties.All)]
        [System.ComponentModel.Description("Sensör No")]
        int fIndex;
        public int Index { get { return fIndex; } set { fIndex = value; lbIdx.Text = fIndex.ToString(); } }

        int fHucreNo;
        public int HucreNo { get { return fHucreNo; } set { fHucreNo = value; lbHucreNo.Text = fHucreNo.ToString(); } }

        public string PaletBar { get { return lbPaletBar.Text; } set { lbPaletBar.Text = value; } }

        public Color HucreColor { get { return panel2.BackColor; } set { panel2.BackColor = value; } }

        private bool active;
        private Label lbIdx;
        private Panel panel1;
        private Panel panel2;
        private Panel panel7;
        private Panel panel4;
        private Panel panel6;
        private Label lbPaletBar;
        private Panel panel5;
        private Label lbHucreNo;
        private Panel panel3;
        private Label lbProdBar;

        public bool Active { get { return active; } set { active = value; } }

        private ushort fModbusAddr;
        public ushort ModbusAddr
        {
            get
            {
                return fModbusAddr;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void rot_Click(object sender, EventArgs e)
        {
        }

        private void InitializeComponent()
        {
            this.lbIdx = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel7 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.lbPaletBar = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.lbHucreNo = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.lbProdBar = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel7.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbIdx
            // 
            this.lbIdx.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbIdx.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lbIdx.Location = new System.Drawing.Point(0, 0);
            this.lbIdx.Name = "lbIdx";
            this.lbIdx.Size = new System.Drawing.Size(144, 75);
            this.lbIdx.TabIndex = 1;
            this.lbIdx.Text = "000000";
            this.lbIdx.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(150, 150);
            this.panel1.TabIndex = 8;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Pink;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.panel7);
            this.panel2.Controls.Add(this.panel4);
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(148, 148);
            this.panel2.TabIndex = 0;
            // 
            // panel7
            // 
            this.panel7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel7.Controls.Add(this.lbIdx);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel7.Location = new System.Drawing.Point(0, 35);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(146, 77);
            this.panel7.TabIndex = 2;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.panel6);
            this.panel4.Controls.Add(this.panel5);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel4.Location = new System.Drawing.Point(0, 112);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(146, 34);
            this.panel4.TabIndex = 1;
            // 
            // panel6
            // 
            this.panel6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel6.Controls.Add(this.lbPaletBar);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel6.Location = new System.Drawing.Point(40, 0);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(106, 34);
            this.panel6.TabIndex = 1;
            // 
            // lbPaletBar
            // 
            this.lbPaletBar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbPaletBar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lbPaletBar.Location = new System.Drawing.Point(0, 0);
            this.lbPaletBar.Name = "lbPaletBar";
            this.lbPaletBar.Size = new System.Drawing.Size(104, 32);
            this.lbPaletBar.TabIndex = 1;
            this.lbPaletBar.Text = "0003063408";
            this.lbPaletBar.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel5
            // 
            this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel5.Controls.Add(this.lbHucreNo);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(40, 34);
            this.panel5.TabIndex = 0;
            // 
            // lbHucreNo
            // 
            this.lbHucreNo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbHucreNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lbHucreNo.Location = new System.Drawing.Point(0, 0);
            this.lbHucreNo.Name = "lbHucreNo";
            this.lbHucreNo.Size = new System.Drawing.Size(38, 32);
            this.lbHucreNo.TabIndex = 1;
            this.lbHucreNo.Text = "1";
            this.lbHucreNo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.lbProdBar);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(146, 35);
            this.panel3.TabIndex = 0;
            // 
            // lbProdBar
            // 
            this.lbProdBar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbProdBar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lbProdBar.Location = new System.Drawing.Point(0, 0);
            this.lbProdBar.Name = "lbProdBar";
            this.lbProdBar.Size = new System.Drawing.Size(144, 33);
            this.lbProdBar.TabIndex = 0;
            this.lbProdBar.Text = "301710930243";
            this.lbProdBar.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Urun
            // 
            this.Controls.Add(this.panel1);
            this.Name = "Urun";
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }
    }
}
