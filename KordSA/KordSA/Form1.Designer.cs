namespace KordSA
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        public void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.btnStart = new System.Windows.Forms.Button();
            this.plcListener = new System.Windows.Forms.Timer(this.components);
            this.btnConveyor4 = new System.Windows.Forms.Button();
            this.btnConveyor3 = new System.Windows.Forms.Button();
            this.btnConveyor2 = new System.Windows.Forms.Button();
            this.btnConveyorTurn = new System.Windows.Forms.Button();
            this.btnConveyor1 = new System.Windows.Forms.Button();
            this.btnManualMode = new System.Windows.Forms.Button();
            this.btnAutoMode = new System.Windows.Forms.Button();
            this.btnLightColumn1Red = new System.Windows.Forms.Button();
            this.btnLightColumn1Yellow = new System.Windows.Forms.Button();
            this.btnLightColumn1Green = new System.Windows.Forms.Button();
            this.btnLightColumn2Red = new System.Windows.Forms.Button();
            this.btnLightColumn2Green = new System.Windows.Forms.Button();
            this.btnLightColumn3Red = new System.Windows.Forms.Button();
            this.btnLightColumn3Green = new System.Windows.Forms.Button();
            this.btnLightColumn1Buzzer = new System.Windows.Forms.Button();
            this.btnLightColumn2Buzzer = new System.Windows.Forms.Button();
            this.btnLightColumn3Buzzer = new System.Windows.Forms.Button();
            this.ımageList1 = new System.Windows.Forms.ImageList(this.components);
            this.lblBarkod = new System.Windows.Forms.Label();
            this.lblWeighing = new System.Windows.Forms.Label();
            this.lblSystemReady = new System.Windows.Forms.Label();
            this.btnReset = new System.Windows.Forms.Button();
            this.pbxMarka = new System.Windows.Forms.PictureBox();
            this.serialPortMobileBarcod = new System.IO.Ports.SerialPort(this.components);
            this.serialPortIndicator = new System.IO.Ports.SerialPort(this.components);
            this.sevenSegmentArray1 = new DmitryBrant.CustomControls.SevenSegmentArray();
            this.serialPortStickerMachine = new System.IO.Ports.SerialPort(this.components);
            this.pnlManualMode = new System.Windows.Forms.Panel();
            this.pbSensor3 = new System.Windows.Forms.PictureBox();
            this.pbSensor1 = new System.Windows.Forms.PictureBox();
            this.pbSensor2 = new System.Windows.Forms.PictureBox();
            this.lbSil = new System.Windows.Forms.Label();
            this.nmConveyor = new System.Windows.Forms.NumericUpDown();
            this.btnSil = new System.Windows.Forms.Button();
            this.lblMessage = new System.Windows.Forms.Label();
            this.dataBaseListener = new System.Windows.Forms.Timer(this.components);
            this.btnExit = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pbxMarka)).BeginInit();
            this.pnlManualMode.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbSensor3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbSensor1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbSensor2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmConveyor)).BeginInit();
            this.SuspendLayout();
            // 
            // btnStart
            // 
            this.btnStart.BackColor = System.Drawing.Color.DarkGreen;
            this.btnStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnStart.ForeColor = System.Drawing.Color.White;
            this.btnStart.Location = new System.Drawing.Point(54, 12);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 44);
            this.btnStart.TabIndex = 0;
            this.btnStart.Text = "START";
            this.btnStart.UseVisualStyleBackColor = false;
            this.btnStart.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnStart_MouseDown);
            this.btnStart.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnStart_MouseUp);
            // 
            // plcListener
            // 
            this.plcListener.Enabled = true;
            this.plcListener.Interval = 300;
            this.plcListener.Tick += new System.EventHandler(this.plcListener_Tick);
            // 
            // btnConveyor4
            // 
            this.btnConveyor4.BackColor = System.Drawing.Color.Red;
            this.btnConveyor4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnConveyor4.ForeColor = System.Drawing.Color.White;
            this.btnConveyor4.Location = new System.Drawing.Point(88, 179);
            this.btnConveyor4.Name = "btnConveyor4";
            this.btnConveyor4.Size = new System.Drawing.Size(55, 41);
            this.btnConveyor4.TabIndex = 1;
            this.btnConveyor4.Text = "OFF";
            this.btnConveyor4.UseVisualStyleBackColor = false;
            this.btnConveyor4.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnConveyor4_MouseDown);
            this.btnConveyor4.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnConveyor4_MouseUp);
            // 
            // btnConveyor3
            // 
            this.btnConveyor3.BackColor = System.Drawing.Color.Red;
            this.btnConveyor3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnConveyor3.ForeColor = System.Drawing.Color.White;
            this.btnConveyor3.Location = new System.Drawing.Point(283, 179);
            this.btnConveyor3.Name = "btnConveyor3";
            this.btnConveyor3.Size = new System.Drawing.Size(55, 41);
            this.btnConveyor3.TabIndex = 1;
            this.btnConveyor3.Text = "OFF";
            this.btnConveyor3.UseVisualStyleBackColor = false;
            this.btnConveyor3.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnConveyor3_MouseDown);
            this.btnConveyor3.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnConveyor3_MouseUp);
            // 
            // btnConveyor2
            // 
            this.btnConveyor2.BackColor = System.Drawing.Color.Red;
            this.btnConveyor2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnConveyor2.ForeColor = System.Drawing.Color.White;
            this.btnConveyor2.Location = new System.Drawing.Point(479, 179);
            this.btnConveyor2.Name = "btnConveyor2";
            this.btnConveyor2.Size = new System.Drawing.Size(55, 41);
            this.btnConveyor2.TabIndex = 1;
            this.btnConveyor2.Text = "OFF";
            this.btnConveyor2.UseVisualStyleBackColor = false;
            this.btnConveyor2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnConveyor2_MouseDown);
            this.btnConveyor2.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnConveyor2_MouseUp);
            // 
            // btnConveyorTurn
            // 
            this.btnConveyorTurn.BackColor = System.Drawing.Color.Red;
            this.btnConveyorTurn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnConveyorTurn.ForeColor = System.Drawing.Color.White;
            this.btnConveyorTurn.Location = new System.Drawing.Point(540, 179);
            this.btnConveyorTurn.Name = "btnConveyorTurn";
            this.btnConveyorTurn.Size = new System.Drawing.Size(55, 41);
            this.btnConveyorTurn.TabIndex = 1;
            this.btnConveyorTurn.Text = "TURN OFF";
            this.btnConveyorTurn.UseVisualStyleBackColor = false;
            this.btnConveyorTurn.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnConveyorTurn_MouseDown);
            this.btnConveyorTurn.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnConveyorTurn_MouseUp);
            // 
            // btnConveyor1
            // 
            this.btnConveyor1.BackColor = System.Drawing.Color.Red;
            this.btnConveyor1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnConveyor1.ForeColor = System.Drawing.Color.White;
            this.btnConveyor1.Location = new System.Drawing.Point(509, 405);
            this.btnConveyor1.Name = "btnConveyor1";
            this.btnConveyor1.Size = new System.Drawing.Size(55, 41);
            this.btnConveyor1.TabIndex = 1;
            this.btnConveyor1.Text = "OFF";
            this.btnConveyor1.UseVisualStyleBackColor = false;
            this.btnConveyor1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnConveyor1_MouseDown);
            this.btnConveyor1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnConveyor1_MouseUp);
            // 
            // btnManualMode
            // 
            this.btnManualMode.BackColor = System.Drawing.Color.Lime;
            this.btnManualMode.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnManualMode.ForeColor = System.Drawing.Color.White;
            this.btnManualMode.Location = new System.Drawing.Point(273, 12);
            this.btnManualMode.Name = "btnManualMode";
            this.btnManualMode.Size = new System.Drawing.Size(132, 44);
            this.btnManualMode.TabIndex = 0;
            this.btnManualMode.Text = "MANUAL MOD";
            this.btnManualMode.UseVisualStyleBackColor = false;
            this.btnManualMode.Click += new System.EventHandler(this.btnManualMode_Click);
            // 
            // btnAutoMode
            // 
            this.btnAutoMode.BackColor = System.Drawing.Color.Red;
            this.btnAutoMode.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnAutoMode.ForeColor = System.Drawing.Color.White;
            this.btnAutoMode.Location = new System.Drawing.Point(135, 12);
            this.btnAutoMode.Name = "btnAutoMode";
            this.btnAutoMode.Size = new System.Drawing.Size(132, 44);
            this.btnAutoMode.TabIndex = 0;
            this.btnAutoMode.Text = "OTOMATİK MOD";
            this.btnAutoMode.UseVisualStyleBackColor = false;
            this.btnAutoMode.Click += new System.EventHandler(this.btnAutoMode_Click);
            // 
            // btnLightColumn1Red
            // 
            this.btnLightColumn1Red.BackColor = System.Drawing.Color.DarkRed;
            this.btnLightColumn1Red.Location = new System.Drawing.Point(25, 353);
            this.btnLightColumn1Red.Name = "btnLightColumn1Red";
            this.btnLightColumn1Red.Size = new System.Drawing.Size(34, 65);
            this.btnLightColumn1Red.TabIndex = 3;
            this.btnLightColumn1Red.UseVisualStyleBackColor = false;
            // 
            // btnLightColumn1Yellow
            // 
            this.btnLightColumn1Yellow.BackColor = System.Drawing.Color.DarkGoldenrod;
            this.btnLightColumn1Yellow.Location = new System.Drawing.Point(25, 415);
            this.btnLightColumn1Yellow.Name = "btnLightColumn1Yellow";
            this.btnLightColumn1Yellow.Size = new System.Drawing.Size(34, 65);
            this.btnLightColumn1Yellow.TabIndex = 3;
            this.btnLightColumn1Yellow.UseVisualStyleBackColor = false;
            // 
            // btnLightColumn1Green
            // 
            this.btnLightColumn1Green.BackColor = System.Drawing.Color.DarkGreen;
            this.btnLightColumn1Green.Location = new System.Drawing.Point(25, 477);
            this.btnLightColumn1Green.Name = "btnLightColumn1Green";
            this.btnLightColumn1Green.Size = new System.Drawing.Size(34, 65);
            this.btnLightColumn1Green.TabIndex = 3;
            this.btnLightColumn1Green.UseVisualStyleBackColor = false;
            // 
            // btnLightColumn2Red
            // 
            this.btnLightColumn2Red.BackColor = System.Drawing.Color.DarkRed;
            this.btnLightColumn2Red.Location = new System.Drawing.Point(100, 415);
            this.btnLightColumn2Red.Name = "btnLightColumn2Red";
            this.btnLightColumn2Red.Size = new System.Drawing.Size(34, 65);
            this.btnLightColumn2Red.TabIndex = 3;
            this.btnLightColumn2Red.UseVisualStyleBackColor = false;
            // 
            // btnLightColumn2Green
            // 
            this.btnLightColumn2Green.BackColor = System.Drawing.Color.DarkGreen;
            this.btnLightColumn2Green.Location = new System.Drawing.Point(100, 477);
            this.btnLightColumn2Green.Name = "btnLightColumn2Green";
            this.btnLightColumn2Green.Size = new System.Drawing.Size(34, 65);
            this.btnLightColumn2Green.TabIndex = 3;
            this.btnLightColumn2Green.UseVisualStyleBackColor = false;
            // 
            // btnLightColumn3Red
            // 
            this.btnLightColumn3Red.BackColor = System.Drawing.Color.DarkRed;
            this.btnLightColumn3Red.Location = new System.Drawing.Point(181, 415);
            this.btnLightColumn3Red.Name = "btnLightColumn3Red";
            this.btnLightColumn3Red.Size = new System.Drawing.Size(34, 65);
            this.btnLightColumn3Red.TabIndex = 3;
            this.btnLightColumn3Red.UseVisualStyleBackColor = false;
            // 
            // btnLightColumn3Green
            // 
            this.btnLightColumn3Green.BackColor = System.Drawing.Color.DarkGreen;
            this.btnLightColumn3Green.Location = new System.Drawing.Point(181, 477);
            this.btnLightColumn3Green.Name = "btnLightColumn3Green";
            this.btnLightColumn3Green.Size = new System.Drawing.Size(34, 65);
            this.btnLightColumn3Green.TabIndex = 3;
            this.btnLightColumn3Green.UseVisualStyleBackColor = false;
            // 
            // btnLightColumn1Buzzer
            // 
            this.btnLightColumn1Buzzer.BackColor = System.Drawing.Color.DarkGreen;
            this.btnLightColumn1Buzzer.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnLightColumn1Buzzer.BackgroundImage")));
            this.btnLightColumn1Buzzer.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnLightColumn1Buzzer.Location = new System.Drawing.Point(25, 539);
            this.btnLightColumn1Buzzer.Name = "btnLightColumn1Buzzer";
            this.btnLightColumn1Buzzer.Size = new System.Drawing.Size(34, 42);
            this.btnLightColumn1Buzzer.TabIndex = 3;
            this.btnLightColumn1Buzzer.UseVisualStyleBackColor = false;
            // 
            // btnLightColumn2Buzzer
            // 
            this.btnLightColumn2Buzzer.BackColor = System.Drawing.Color.DarkGreen;
            this.btnLightColumn2Buzzer.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnLightColumn2Buzzer.BackgroundImage")));
            this.btnLightColumn2Buzzer.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnLightColumn2Buzzer.Location = new System.Drawing.Point(100, 539);
            this.btnLightColumn2Buzzer.Name = "btnLightColumn2Buzzer";
            this.btnLightColumn2Buzzer.Size = new System.Drawing.Size(34, 42);
            this.btnLightColumn2Buzzer.TabIndex = 3;
            this.btnLightColumn2Buzzer.UseVisualStyleBackColor = false;
            // 
            // btnLightColumn3Buzzer
            // 
            this.btnLightColumn3Buzzer.BackColor = System.Drawing.Color.DarkGreen;
            this.btnLightColumn3Buzzer.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnLightColumn3Buzzer.BackgroundImage")));
            this.btnLightColumn3Buzzer.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnLightColumn3Buzzer.Location = new System.Drawing.Point(181, 539);
            this.btnLightColumn3Buzzer.Name = "btnLightColumn3Buzzer";
            this.btnLightColumn3Buzzer.Size = new System.Drawing.Size(34, 42);
            this.btnLightColumn3Buzzer.TabIndex = 3;
            this.btnLightColumn3Buzzer.UseVisualStyleBackColor = false;
            // 
            // ımageList1
            // 
            this.ımageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ımageList1.ImageStream")));
            this.ımageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.ımageList1.Images.SetKeyName(0, "ses.png");
            this.ımageList1.Images.SetKeyName(1, "ses açık.png");
            // 
            // lblBarkod
            // 
            this.lblBarkod.AutoSize = true;
            this.lblBarkod.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblBarkod.Location = new System.Drawing.Point(16, 593);
            this.lblBarkod.Name = "lblBarkod";
            this.lblBarkod.Size = new System.Drawing.Size(58, 13);
            this.lblBarkod.TabIndex = 4;
            this.lblBarkod.Text = "BARKOD";
            // 
            // lblWeighing
            // 
            this.lblWeighing.AutoSize = true;
            this.lblWeighing.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblWeighing.Location = new System.Drawing.Point(87, 593);
            this.lblWeighing.Name = "lblWeighing";
            this.lblWeighing.Size = new System.Drawing.Size(56, 13);
            this.lblWeighing.TabIndex = 4;
            this.lblWeighing.Text = "AĞIRLIK";
            // 
            // lblSystemReady
            // 
            this.lblSystemReady.AutoSize = true;
            this.lblSystemReady.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblSystemReady.Location = new System.Drawing.Point(156, 593);
            this.lblSystemReady.Name = "lblSystemReady";
            this.lblSystemReady.Size = new System.Drawing.Size(95, 13);
            this.lblSystemReady.TabIndex = 4;
            this.lblSystemReady.Text = "SİSTEM HAZIR";
            // 
            // btnReset
            // 
            this.btnReset.BackColor = System.Drawing.Color.DarkGoldenrod;
            this.btnReset.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnReset.ForeColor = System.Drawing.Color.White;
            this.btnReset.Location = new System.Drawing.Point(411, 12);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(160, 44);
            this.btnReset.TabIndex = 0;
            this.btnReset.Text = "RESET";
            this.btnReset.UseVisualStyleBackColor = false;
            this.btnReset.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnReset_MouseDown);
            this.btnReset.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnReset_MouseUp);
            // 
            // pbxMarka
            // 
            this.pbxMarka.Image = ((System.Drawing.Image)(resources.GetObject("pbxMarka.Image")));
            this.pbxMarka.Location = new System.Drawing.Point(781, 612);
            this.pbxMarka.Name = "pbxMarka";
            this.pbxMarka.Size = new System.Drawing.Size(152, 125);
            this.pbxMarka.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbxMarka.TabIndex = 5;
            this.pbxMarka.TabStop = false;
            // 
            // serialPortMobileBarcod
            // 
            this.serialPortMobileBarcod.BaudRate = 115200;
            this.serialPortMobileBarcod.PortName = "COM3";
            // 
            // serialPortIndicator
            // 
            this.serialPortIndicator.PortName = "COM4";
            // 
            // sevenSegmentArray1
            // 
            this.sevenSegmentArray1.ArrayCount = 5;
            this.sevenSegmentArray1.ColorBackground = System.Drawing.Color.Black;
            this.sevenSegmentArray1.ColorDark = System.Drawing.Color.Maroon;
            this.sevenSegmentArray1.ColorLight = System.Drawing.Color.Red;
            this.sevenSegmentArray1.DecimalShow = true;
            this.sevenSegmentArray1.ElementPadding = new System.Windows.Forms.Padding(4);
            this.sevenSegmentArray1.ElementWidth = 10;
            this.sevenSegmentArray1.ItalicFactor = 0F;
            this.sevenSegmentArray1.Location = new System.Drawing.Point(646, 12);
            this.sevenSegmentArray1.Name = "sevenSegmentArray1";
            this.sevenSegmentArray1.Size = new System.Drawing.Size(231, 78);
            this.sevenSegmentArray1.TabIndex = 6;
            this.sevenSegmentArray1.TabStop = false;
            this.sevenSegmentArray1.Value = null;
            // 
            // serialPortStickerMachine
            // 
            this.serialPortStickerMachine.PortName = "COM2";
            // 
            // pnlManualMode
            // 
            this.pnlManualMode.BackColor = System.Drawing.Color.Transparent;
            this.pnlManualMode.Controls.Add(this.label1);
            this.pnlManualMode.Controls.Add(this.button1);
            this.pnlManualMode.Controls.Add(this.pbSensor3);
            this.pnlManualMode.Controls.Add(this.pbSensor1);
            this.pnlManualMode.Controls.Add(this.pbSensor2);
            this.pnlManualMode.Controls.Add(this.lbSil);
            this.pnlManualMode.Controls.Add(this.nmConveyor);
            this.pnlManualMode.Controls.Add(this.btnSil);
            this.pnlManualMode.Controls.Add(this.btnConveyor4);
            this.pnlManualMode.Controls.Add(this.btnConveyor3);
            this.pnlManualMode.Controls.Add(this.btnConveyor2);
            this.pnlManualMode.Controls.Add(this.lblSystemReady);
            this.pnlManualMode.Controls.Add(this.btnConveyorTurn);
            this.pnlManualMode.Controls.Add(this.lblWeighing);
            this.pnlManualMode.Controls.Add(this.btnConveyor1);
            this.pnlManualMode.Controls.Add(this.lblBarkod);
            this.pnlManualMode.Controls.Add(this.btnLightColumn1Red);
            this.pnlManualMode.Controls.Add(this.btnLightColumn3Green);
            this.pnlManualMode.Controls.Add(this.btnLightColumn2Red);
            this.pnlManualMode.Controls.Add(this.btnLightColumn2Green);
            this.pnlManualMode.Controls.Add(this.btnLightColumn3Red);
            this.pnlManualMode.Controls.Add(this.btnLightColumn3Buzzer);
            this.pnlManualMode.Controls.Add(this.btnLightColumn1Yellow);
            this.pnlManualMode.Controls.Add(this.btnLightColumn2Buzzer);
            this.pnlManualMode.Controls.Add(this.btnLightColumn1Green);
            this.pnlManualMode.Controls.Add(this.btnLightColumn1Buzzer);
            this.pnlManualMode.Location = new System.Drawing.Point(12, 96);
            this.pnlManualMode.Name = "pnlManualMode";
            this.pnlManualMode.Size = new System.Drawing.Size(921, 641);
            this.pnlManualMode.TabIndex = 7;
            // 
            // pbSensor3
            // 
            this.pbSensor3.BackColor = System.Drawing.Color.DarkRed;
            this.pbSensor3.Location = new System.Drawing.Point(479, 48);
            this.pbSensor3.Name = "pbSensor3";
            this.pbSensor3.Size = new System.Drawing.Size(117, 10);
            this.pbSensor3.TabIndex = 8;
            this.pbSensor3.TabStop = false;
            this.pbSensor3.Visible = false;
            // 
            // pbSensor1
            // 
            this.pbSensor1.BackColor = System.Drawing.Color.DarkRed;
            this.pbSensor1.Location = new System.Drawing.Point(479, 240);
            this.pbSensor1.Name = "pbSensor1";
            this.pbSensor1.Size = new System.Drawing.Size(117, 10);
            this.pbSensor1.TabIndex = 8;
            this.pbSensor1.TabStop = false;
            this.pbSensor1.Visible = false;
            // 
            // pbSensor2
            // 
            this.pbSensor2.BackColor = System.Drawing.Color.DarkRed;
            this.pbSensor2.Location = new System.Drawing.Point(439, 84);
            this.pbSensor2.Name = "pbSensor2";
            this.pbSensor2.Size = new System.Drawing.Size(10, 117);
            this.pbSensor2.TabIndex = 8;
            this.pbSensor2.TabStop = false;
            this.pbSensor2.Visible = false;
            // 
            // lbSil
            // 
            this.lbSil.AutoSize = true;
            this.lbSil.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lbSil.Location = new System.Drawing.Point(284, 269);
            this.lbSil.Name = "lbSil";
            this.lbSil.Size = new System.Drawing.Size(84, 13);
            this.lbSil.TabIndex = 7;
            this.lbSil.Text = "Conveyor No:";
            // 
            // nmConveyor
            // 
            this.nmConveyor.Location = new System.Drawing.Point(374, 267);
            this.nmConveyor.Maximum = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.nmConveyor.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nmConveyor.Name = "nmConveyor";
            this.nmConveyor.Size = new System.Drawing.Size(61, 20);
            this.nmConveyor.TabIndex = 6;
            this.nmConveyor.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // btnSil
            // 
            this.btnSil.BackColor = System.Drawing.SystemColors.Highlight;
            this.btnSil.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnSil.ForeColor = System.Drawing.Color.White;
            this.btnSil.Location = new System.Drawing.Point(283, 302);
            this.btnSil.Name = "btnSil";
            this.btnSil.Size = new System.Drawing.Size(152, 46);
            this.btnSil.TabIndex = 5;
            this.btnSil.Text = "SİL";
            this.btnSil.UseVisualStyleBackColor = false;
            this.btnSil.Click += new System.EventHandler(this.btnSil_Click);
            // 
            // lblMessage
            // 
            this.lblMessage.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.lblMessage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblMessage.Location = new System.Drawing.Point(55, 59);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(516, 82);
            this.lblMessage.TabIndex = 8;
            this.lblMessage.Text = "Manual mod aktif";
            // 
            // dataBaseListener
            // 
            this.dataBaseListener.Interval = 15000;
            this.dataBaseListener.Tick += new System.EventHandler(this.dataBaseListener_Tick);
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExit.BackColor = System.Drawing.Color.White;
            this.btnExit.FlatAppearance.BorderSize = 2;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExit.Image = ((System.Drawing.Image)(resources.GetObject("btnExit.Image")));
            this.btnExit.Location = new System.Drawing.Point(882, 11);
            this.btnExit.Margin = new System.Windows.Forms.Padding(2);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(44, 44);
            this.btnExit.TabIndex = 97;
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 1500;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // timer2
            // 
            this.timer2.Interval = 1500;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.DarkViolet;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.button1.Location = new System.Drawing.Point(716, 267);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(118, 53);
            this.button1.TabIndex = 9;
            this.button1.Text = "MANUAL MOD";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label1.ForeColor = System.Drawing.Color.LimeGreen;
            this.label1.Location = new System.Drawing.Point(713, 323);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(180, 26);
            this.label1.TabIndex = 10;
            this.label1.Text = "Sadece sistem otomatikte iken\r\nacile düştüğünde kullnınız";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(935, 609);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.lblMessage);
            this.Controls.Add(this.pnlManualMode);
            this.Controls.Add(this.sevenSegmentArray1);
            this.Controls.Add(this.pbxMarka);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.btnAutoMode);
            this.Controls.Add(this.btnManualMode);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Form1";
            this.Text = "Form1";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbxMarka)).EndInit();
            this.pnlManualMode.ResumeLayout(false);
            this.pnlManualMode.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbSensor3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbSensor1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbSensor2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmConveyor)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnConveyor4;
        private System.Windows.Forms.Button btnConveyor3;
        private System.Windows.Forms.Button btnConveyor2;
        private System.Windows.Forms.Button btnConveyorTurn;
        private System.Windows.Forms.Button btnConveyor1;
        private System.Windows.Forms.Button btnManualMode;
        private System.Windows.Forms.Button btnLightColumn1Red;
        private System.Windows.Forms.Button btnLightColumn1Yellow;
        private System.Windows.Forms.Button btnLightColumn1Green;
        private System.Windows.Forms.Button btnLightColumn2Red;
        private System.Windows.Forms.Button btnLightColumn2Green;
        private System.Windows.Forms.Button btnLightColumn3Red;
        private System.Windows.Forms.Button btnLightColumn3Green;
        private System.Windows.Forms.Button btnLightColumn1Buzzer;
        private System.Windows.Forms.Button btnLightColumn2Buzzer;
        private System.Windows.Forms.Button btnLightColumn3Buzzer;
        private System.Windows.Forms.ImageList ımageList1;
        private System.Windows.Forms.Label lblBarkod;
        private System.Windows.Forms.Label lblWeighing;
        private System.Windows.Forms.Label lblSystemReady;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.PictureBox pbxMarka;
        private System.IO.Ports.SerialPort serialPortMobileBarcod;
        private System.IO.Ports.SerialPort serialPortIndicator;
        private DmitryBrant.CustomControls.SevenSegmentArray sevenSegmentArray1;
        private System.IO.Ports.SerialPort serialPortStickerMachine;
        public System.Windows.Forms.Timer plcListener;
        public System.Windows.Forms.Button btnStart;
        public System.Windows.Forms.Button btnAutoMode;
        private System.Windows.Forms.Panel pnlManualMode;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.Timer dataBaseListener;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.Label lbSil;
        private System.Windows.Forms.NumericUpDown nmConveyor;
        private System.Windows.Forms.Button btnSil;
        private System.Windows.Forms.PictureBox pbSensor3;
        private System.Windows.Forms.PictureBox pbSensor1;
        private System.Windows.Forms.PictureBox pbSensor2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
    }
}

