namespace PekpanBoyahane
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
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.txbIsEmriNo = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnGonder = new System.Windows.Forms.Button();
            this.btnGiris = new System.Windows.Forms.Button();
            this.btnCikis = new System.Windows.Forms.Button();
            this.gvIsEmri = new System.Windows.Forms.DataGridView();
            this.planNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.isEmri = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sipNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.profNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cariIsmi = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.stokKodu = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.stokAdi = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.miktar = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.giden = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.uretim = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.kalan = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblKullaniciAdi = new System.Windows.Forms.Label();
            this.nudMiktar = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txbIsEmriNoG = new System.Windows.Forms.TextBox();
            this.btnGuncelle = new System.Windows.Forms.Button();
            this.nudMiktarG = new System.Windows.Forms.NumericUpDown();
            this.btnSil = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.gvIsEmri)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMiktar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMiktarG)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label1.Location = new System.Drawing.Point(12, 427);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(107, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "İŞ EMRİ NO";
            // 
            // txbIsEmriNo
            // 
            this.txbIsEmriNo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txbIsEmriNo.Location = new System.Drawing.Point(134, 427);
            this.txbIsEmriNo.MaxLength = 15;
            this.txbIsEmriNo.Name = "txbIsEmriNo";
            this.txbIsEmriNo.Size = new System.Drawing.Size(145, 20);
            this.txbIsEmriNo.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label2.Location = new System.Drawing.Point(12, 463);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 20);
            this.label2.TabIndex = 0;
            this.label2.Text = "MİKTAR";
            // 
            // btnGonder
            // 
            this.btnGonder.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnGonder.Location = new System.Drawing.Point(134, 499);
            this.btnGonder.Name = "btnGonder";
            this.btnGonder.Size = new System.Drawing.Size(145, 30);
            this.btnGonder.TabIndex = 2;
            this.btnGonder.Text = "GÖNDER";
            this.btnGonder.UseVisualStyleBackColor = true;
            this.btnGonder.Click += new System.EventHandler(this.btnGonder_Click);
            // 
            // btnGiris
            // 
            this.btnGiris.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnGiris.Location = new System.Drawing.Point(12, 12);
            this.btnGiris.Name = "btnGiris";
            this.btnGiris.Size = new System.Drawing.Size(59, 30);
            this.btnGiris.TabIndex = 2;
            this.btnGiris.Text = "GİRİŞ";
            this.btnGiris.UseVisualStyleBackColor = true;
            this.btnGiris.Click += new System.EventHandler(this.btnGiris_Click);
            // 
            // btnCikis
            // 
            this.btnCikis.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnCikis.Location = new System.Drawing.Point(77, 12);
            this.btnCikis.Name = "btnCikis";
            this.btnCikis.Size = new System.Drawing.Size(59, 30);
            this.btnCikis.TabIndex = 2;
            this.btnCikis.Text = "ÇIKIŞ";
            this.btnCikis.UseVisualStyleBackColor = true;
            this.btnCikis.Click += new System.EventHandler(this.btnCikis_Click);
            // 
            // gvIsEmri
            // 
            this.gvIsEmri.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvIsEmri.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.planNo,
            this.isEmri,
            this.sipNo,
            this.profNo,
            this.cariIsmi,
            this.stokKodu,
            this.stokAdi,
            this.miktar,
            this.giden,
            this.uretim,
            this.kalan,
            this.id});
            this.gvIsEmri.Location = new System.Drawing.Point(12, 48);
            this.gvIsEmri.Name = "gvIsEmri";
            this.gvIsEmri.Size = new System.Drawing.Size(1146, 363);
            this.gvIsEmri.TabIndex = 3;
            this.gvIsEmri.DoubleClick += new System.EventHandler(this.gvIsEmri_DoubleClick);
            // 
            // planNo
            // 
            this.planNo.HeaderText = "PLAN NO";
            this.planNo.Name = "planNo";
            // 
            // isEmri
            // 
            this.isEmri.HeaderText = "İŞ EMRİ";
            this.isEmri.Name = "isEmri";
            // 
            // sipNo
            // 
            this.sipNo.HeaderText = "SİP.NO";
            this.sipNo.Name = "sipNo";
            // 
            // profNo
            // 
            this.profNo.HeaderText = "PROF.NO";
            this.profNo.Name = "profNo";
            // 
            // cariIsmi
            // 
            this.cariIsmi.HeaderText = "CARİ İSMİ";
            this.cariIsmi.Name = "cariIsmi";
            // 
            // stokKodu
            // 
            this.stokKodu.HeaderText = "STOK KODU";
            this.stokKodu.Name = "stokKodu";
            // 
            // stokAdi
            // 
            this.stokAdi.HeaderText = "STOK ADI";
            this.stokAdi.Name = "stokAdi";
            // 
            // miktar
            // 
            this.miktar.HeaderText = "MİKTAR";
            this.miktar.Name = "miktar";
            // 
            // giden
            // 
            this.giden.HeaderText = "GİDEN";
            this.giden.Name = "giden";
            // 
            // uretim
            // 
            this.uretim.HeaderText = "ÜRETİM";
            this.uretim.Name = "uretim";
            // 
            // kalan
            // 
            this.kalan.HeaderText = "KALAN";
            this.kalan.Name = "kalan";
            // 
            // id
            // 
            this.id.HeaderText = "ID";
            this.id.Name = "id";
            this.id.Visible = false;
            // 
            // lblKullaniciAdi
            // 
            this.lblKullaniciAdi.AutoSize = true;
            this.lblKullaniciAdi.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblKullaniciAdi.Location = new System.Drawing.Point(142, 16);
            this.lblKullaniciAdi.Name = "lblKullaniciAdi";
            this.lblKullaniciAdi.Size = new System.Drawing.Size(116, 20);
            this.lblKullaniciAdi.TabIndex = 0;
            this.lblKullaniciAdi.Text = "Kullanıcı Adı :";
            // 
            // nudMiktar
            // 
            this.nudMiktar.Location = new System.Drawing.Point(134, 463);
            this.nudMiktar.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudMiktar.Name = "nudMiktar";
            this.nudMiktar.Size = new System.Drawing.Size(145, 20);
            this.nudMiktar.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label3.Location = new System.Drawing.Point(407, 427);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(107, 20);
            this.label3.TabIndex = 0;
            this.label3.Text = "İŞ EMRİ NO";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label4.Location = new System.Drawing.Point(407, 463);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(75, 20);
            this.label4.TabIndex = 0;
            this.label4.Text = "MİKTAR";
            // 
            // txbIsEmriNoG
            // 
            this.txbIsEmriNoG.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txbIsEmriNoG.Location = new System.Drawing.Point(529, 427);
            this.txbIsEmriNoG.MaxLength = 15;
            this.txbIsEmriNoG.Name = "txbIsEmriNoG";
            this.txbIsEmriNoG.Size = new System.Drawing.Size(145, 20);
            this.txbIsEmriNoG.TabIndex = 0;
            // 
            // btnGuncelle
            // 
            this.btnGuncelle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnGuncelle.Location = new System.Drawing.Point(408, 499);
            this.btnGuncelle.Name = "btnGuncelle";
            this.btnGuncelle.Size = new System.Drawing.Size(130, 30);
            this.btnGuncelle.TabIndex = 2;
            this.btnGuncelle.Text = "GÜNCELLE";
            this.btnGuncelle.UseVisualStyleBackColor = true;
            this.btnGuncelle.Click += new System.EventHandler(this.btnGuncelle_Click);
            // 
            // nudMiktarG
            // 
            this.nudMiktarG.Location = new System.Drawing.Point(529, 463);
            this.nudMiktarG.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudMiktarG.Name = "nudMiktarG";
            this.nudMiktarG.Size = new System.Drawing.Size(145, 20);
            this.nudMiktarG.TabIndex = 4;
            // 
            // btnSil
            // 
            this.btnSil.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnSil.Location = new System.Drawing.Point(544, 499);
            this.btnSil.Name = "btnSil";
            this.btnSil.Size = new System.Drawing.Size(130, 30);
            this.btnSil.TabIndex = 2;
            this.btnSil.Text = "KAYDI SİL";
            this.btnSil.UseVisualStyleBackColor = true;
            this.btnSil.Click += new System.EventHandler(this.btnSil_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1167, 581);
            this.Controls.Add(this.nudMiktarG);
            this.Controls.Add(this.nudMiktar);
            this.Controls.Add(this.gvIsEmri);
            this.Controls.Add(this.btnCikis);
            this.Controls.Add(this.btnGiris);
            this.Controls.Add(this.btnSil);
            this.Controls.Add(this.btnGuncelle);
            this.Controls.Add(this.btnGonder);
            this.Controls.Add(this.txbIsEmriNoG);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txbIsEmriNo);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblKullaniciAdi);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Boyahane Üretim Ekranı";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gvIsEmri)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMiktar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMiktarG)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txbIsEmriNo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnGonder;
        private System.Windows.Forms.Button btnGiris;
        private System.Windows.Forms.Button btnCikis;
        private System.Windows.Forms.DataGridView gvIsEmri;
        private System.Windows.Forms.Label lblKullaniciAdi;
        private System.Windows.Forms.NumericUpDown nudMiktar;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txbIsEmriNoG;
        private System.Windows.Forms.Button btnGuncelle;
        private System.Windows.Forms.NumericUpDown nudMiktarG;
        private System.Windows.Forms.Button btnSil;
        private System.Windows.Forms.DataGridViewTextBoxColumn planNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn isEmri;
        private System.Windows.Forms.DataGridViewTextBoxColumn sipNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn profNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn cariIsmi;
        private System.Windows.Forms.DataGridViewTextBoxColumn stokKodu;
        private System.Windows.Forms.DataGridViewTextBoxColumn stokAdi;
        private System.Windows.Forms.DataGridViewTextBoxColumn miktar;
        private System.Windows.Forms.DataGridViewTextBoxColumn giden;
        private System.Windows.Forms.DataGridViewTextBoxColumn uretim;
        private System.Windows.Forms.DataGridViewTextBoxColumn kalan;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
    }
}

