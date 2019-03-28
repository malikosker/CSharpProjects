namespace PekpanBoyahane
{
    partial class Login
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Login));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.txbSifre = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cbxUsers = new System.Windows.Forms.ComboBox();
            this.btnEkle = new System.Windows.Forms.Button();
            this.btnDegistir = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txbEskiEskiSifre = new System.Windows.Forms.TextBox();
            this.txbEskiYeniSifre = new System.Windows.Forms.TextBox();
            this.txbEskiYeniSifreTekrar = new System.Windows.Forms.TextBox();
            this.btnEskiTamam = new System.Windows.Forms.Button();
            this.btnEskiVazgec = new System.Windows.Forms.Button();
            this.txbEskiKullanici = new System.Windows.Forms.TextBox();
            this.pnlEski = new System.Windows.Forms.Panel();
            this.pnlYeni = new System.Windows.Forms.Panel();
            this.txbYeniKullaniciAdi = new System.Windows.Forms.TextBox();
            this.btnYeniVazgec = new System.Windows.Forms.Button();
            this.btnYeniTamam = new System.Windows.Forms.Button();
            this.txbYeniSifreTekrar = new System.Windows.Forms.TextBox();
            this.txbYeniSifre = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.btnGiris = new System.Windows.Forms.Button();
            this.btnVazgec = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.pnlEski.SuspendLayout();
            this.pnlYeni.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(75, 66);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // txbSifre
            // 
            this.txbSifre.Location = new System.Drawing.Point(185, 67);
            this.txbSifre.Name = "txbSifre";
            this.txbSifre.Size = new System.Drawing.Size(137, 20);
            this.txbSifre.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label1.Location = new System.Drawing.Point(102, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Kullanıcı Adı";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label2.Location = new System.Drawing.Point(146, 70);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(33, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Şirfe";
            // 
            // cbxUsers
            // 
            this.cbxUsers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxUsers.FormattingEnabled = true;
            this.cbxUsers.Location = new System.Drawing.Point(185, 28);
            this.cbxUsers.Name = "cbxUsers";
            this.cbxUsers.Size = new System.Drawing.Size(137, 21);
            this.cbxUsers.TabIndex = 3;
            this.cbxUsers.SelectedIndexChanged += new System.EventHandler(this.cbxUsers_SelectedIndexChanged);
            // 
            // btnEkle
            // 
            this.btnEkle.Location = new System.Drawing.Point(328, 26);
            this.btnEkle.Name = "btnEkle";
            this.btnEkle.Size = new System.Drawing.Size(66, 23);
            this.btnEkle.TabIndex = 4;
            this.btnEkle.Text = "EKLE";
            this.btnEkle.UseVisualStyleBackColor = true;
            this.btnEkle.Click += new System.EventHandler(this.btnEkle_Click);
            // 
            // btnDegistir
            // 
            this.btnDegistir.Location = new System.Drawing.Point(328, 65);
            this.btnDegistir.Name = "btnDegistir";
            this.btnDegistir.Size = new System.Drawing.Size(66, 23);
            this.btnDegistir.TabIndex = 4;
            this.btnDegistir.Text = "DEĞİŞTİR";
            this.btnDegistir.UseVisualStyleBackColor = true;
            this.btnDegistir.Click += new System.EventHandler(this.btnDegistir_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label3.Location = new System.Drawing.Point(49, 39);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Eski Şifre";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label6.Location = new System.Drawing.Point(55, 13);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(55, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "Kullanıcı";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label4.Location = new System.Drawing.Point(49, 65);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(62, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Yeni Şifre";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label5.Location = new System.Drawing.Point(8, 91);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(103, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "Yeni Şifre Tekrar";
            // 
            // txbEskiEskiSifre
            // 
            this.txbEskiEskiSifre.Location = new System.Drawing.Point(127, 36);
            this.txbEskiEskiSifre.Name = "txbEskiEskiSifre";
            this.txbEskiEskiSifre.Size = new System.Drawing.Size(154, 20);
            this.txbEskiEskiSifre.TabIndex = 1;
            // 
            // txbEskiYeniSifre
            // 
            this.txbEskiYeniSifre.Location = new System.Drawing.Point(127, 62);
            this.txbEskiYeniSifre.Name = "txbEskiYeniSifre";
            this.txbEskiYeniSifre.Size = new System.Drawing.Size(154, 20);
            this.txbEskiYeniSifre.TabIndex = 1;
            // 
            // txbEskiYeniSifreTekrar
            // 
            this.txbEskiYeniSifreTekrar.Location = new System.Drawing.Point(127, 88);
            this.txbEskiYeniSifreTekrar.Name = "txbEskiYeniSifreTekrar";
            this.txbEskiYeniSifreTekrar.Size = new System.Drawing.Size(154, 20);
            this.txbEskiYeniSifreTekrar.TabIndex = 1;
            // 
            // btnEskiTamam
            // 
            this.btnEskiTamam.Location = new System.Drawing.Point(127, 123);
            this.btnEskiTamam.Name = "btnEskiTamam";
            this.btnEskiTamam.Size = new System.Drawing.Size(71, 30);
            this.btnEskiTamam.TabIndex = 2;
            this.btnEskiTamam.Text = "TAMAM";
            this.btnEskiTamam.UseVisualStyleBackColor = true;
            this.btnEskiTamam.Click += new System.EventHandler(this.btnEskiTamam_Click);
            // 
            // btnEskiVazgec
            // 
            this.btnEskiVazgec.Location = new System.Drawing.Point(210, 123);
            this.btnEskiVazgec.Name = "btnEskiVazgec";
            this.btnEskiVazgec.Size = new System.Drawing.Size(71, 30);
            this.btnEskiVazgec.TabIndex = 2;
            this.btnEskiVazgec.Text = "VAZGEÇ";
            this.btnEskiVazgec.UseVisualStyleBackColor = true;
            this.btnEskiVazgec.Click += new System.EventHandler(this.btnEskiVazgec_Click);
            // 
            // txbEskiKullanici
            // 
            this.txbEskiKullanici.Location = new System.Drawing.Point(127, 10);
            this.txbEskiKullanici.Name = "txbEskiKullanici";
            this.txbEskiKullanici.Size = new System.Drawing.Size(154, 20);
            this.txbEskiKullanici.TabIndex = 3;
            // 
            // pnlEski
            // 
            this.pnlEski.Controls.Add(this.txbEskiKullanici);
            this.pnlEski.Controls.Add(this.btnEskiVazgec);
            this.pnlEski.Controls.Add(this.btnEskiTamam);
            this.pnlEski.Controls.Add(this.txbEskiYeniSifreTekrar);
            this.pnlEski.Controls.Add(this.txbEskiYeniSifre);
            this.pnlEski.Controls.Add(this.txbEskiEskiSifre);
            this.pnlEski.Controls.Add(this.label5);
            this.pnlEski.Controls.Add(this.label4);
            this.pnlEski.Controls.Add(this.label6);
            this.pnlEski.Controls.Add(this.label3);
            this.pnlEski.Location = new System.Drawing.Point(105, 12);
            this.pnlEski.Name = "pnlEski";
            this.pnlEski.Size = new System.Drawing.Size(289, 162);
            this.pnlEski.TabIndex = 5;
            this.pnlEski.Visible = false;
            // 
            // pnlYeni
            // 
            this.pnlYeni.Controls.Add(this.txbYeniKullaniciAdi);
            this.pnlYeni.Controls.Add(this.btnYeniVazgec);
            this.pnlYeni.Controls.Add(this.btnYeniTamam);
            this.pnlYeni.Controls.Add(this.txbYeniSifreTekrar);
            this.pnlYeni.Controls.Add(this.txbYeniSifre);
            this.pnlYeni.Controls.Add(this.label8);
            this.pnlYeni.Controls.Add(this.label9);
            this.pnlYeni.Controls.Add(this.label10);
            this.pnlYeni.Location = new System.Drawing.Point(105, 12);
            this.pnlYeni.Name = "pnlYeni";
            this.pnlYeni.Size = new System.Drawing.Size(290, 175);
            this.pnlYeni.TabIndex = 6;
            this.pnlYeni.Visible = false;
            // 
            // txbYeniKullaniciAdi
            // 
            this.txbYeniKullaniciAdi.Location = new System.Drawing.Point(127, 10);
            this.txbYeniKullaniciAdi.Name = "txbYeniKullaniciAdi";
            this.txbYeniKullaniciAdi.Size = new System.Drawing.Size(154, 20);
            this.txbYeniKullaniciAdi.TabIndex = 3;
            // 
            // btnYeniVazgec
            // 
            this.btnYeniVazgec.Location = new System.Drawing.Point(210, 96);
            this.btnYeniVazgec.Name = "btnYeniVazgec";
            this.btnYeniVazgec.Size = new System.Drawing.Size(71, 30);
            this.btnYeniVazgec.TabIndex = 2;
            this.btnYeniVazgec.Text = "VAZGEÇ";
            this.btnYeniVazgec.UseVisualStyleBackColor = true;
            this.btnYeniVazgec.Click += new System.EventHandler(this.btnYeniVazgec_Click);
            // 
            // btnYeniTamam
            // 
            this.btnYeniTamam.Location = new System.Drawing.Point(127, 96);
            this.btnYeniTamam.Name = "btnYeniTamam";
            this.btnYeniTamam.Size = new System.Drawing.Size(71, 30);
            this.btnYeniTamam.TabIndex = 2;
            this.btnYeniTamam.Text = "TAMAM";
            this.btnYeniTamam.UseVisualStyleBackColor = true;
            this.btnYeniTamam.Click += new System.EventHandler(this.btnYeniTamam_Click);
            // 
            // txbYeniSifreTekrar
            // 
            this.txbYeniSifreTekrar.Location = new System.Drawing.Point(127, 62);
            this.txbYeniSifreTekrar.Name = "txbYeniSifreTekrar";
            this.txbYeniSifreTekrar.Size = new System.Drawing.Size(154, 20);
            this.txbYeniSifreTekrar.TabIndex = 1;
            // 
            // txbYeniSifre
            // 
            this.txbYeniSifre.Location = new System.Drawing.Point(127, 36);
            this.txbYeniSifre.Name = "txbYeniSifre";
            this.txbYeniSifre.Size = new System.Drawing.Size(154, 20);
            this.txbYeniSifre.TabIndex = 1;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label8.Location = new System.Drawing.Point(37, 62);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(74, 13);
            this.label8.TabIndex = 0;
            this.label8.Text = "Şifre Tekrar";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label9.Location = new System.Drawing.Point(27, 13);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(84, 13);
            this.label9.TabIndex = 0;
            this.label9.Text = "Yeni Kullanıcı";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label10.Location = new System.Drawing.Point(73, 39);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(33, 13);
            this.label10.TabIndex = 0;
            this.label10.Text = "Şifre";
            // 
            // btnGiris
            // 
            this.btnGiris.Location = new System.Drawing.Point(185, 104);
            this.btnGiris.Name = "btnGiris";
            this.btnGiris.Size = new System.Drawing.Size(66, 23);
            this.btnGiris.TabIndex = 4;
            this.btnGiris.Text = "GİRİŞ";
            this.btnGiris.UseVisualStyleBackColor = true;
            this.btnGiris.Click += new System.EventHandler(this.btnGiris_Click);
            // 
            // btnVazgec
            // 
            this.btnVazgec.Location = new System.Drawing.Point(256, 104);
            this.btnVazgec.Name = "btnVazgec";
            this.btnVazgec.Size = new System.Drawing.Size(66, 23);
            this.btnVazgec.TabIndex = 4;
            this.btnVazgec.Text = "VAZGEÇ";
            this.btnVazgec.UseVisualStyleBackColor = true;
            this.btnVazgec.Click += new System.EventHandler(this.btnVazgec_Click);
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(404, 195);
            this.Controls.Add(this.pnlYeni);
            this.Controls.Add(this.pnlEski);
            this.Controls.Add(this.btnDegistir);
            this.Controls.Add(this.btnVazgec);
            this.Controls.Add(this.btnGiris);
            this.Controls.Add(this.btnEkle);
            this.Controls.Add(this.cbxUsers);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txbSifre);
            this.Controls.Add(this.pictureBox1);
            this.Name = "Login";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Login";
            this.Load += new System.EventHandler(this.Login_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.pnlEski.ResumeLayout(false);
            this.pnlEski.PerformLayout();
            this.pnlYeni.ResumeLayout(false);
            this.pnlYeni.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox txbSifre;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbxUsers;
        private System.Windows.Forms.Button btnEkle;
        private System.Windows.Forms.Button btnDegistir;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txbEskiEskiSifre;
        private System.Windows.Forms.TextBox txbEskiYeniSifre;
        private System.Windows.Forms.TextBox txbEskiYeniSifreTekrar;
        private System.Windows.Forms.Button btnEskiTamam;
        private System.Windows.Forms.Button btnEskiVazgec;
        private System.Windows.Forms.TextBox txbEskiKullanici;
        private System.Windows.Forms.Panel pnlEski;
        private System.Windows.Forms.Panel pnlYeni;
        private System.Windows.Forms.TextBox txbYeniKullaniciAdi;
        private System.Windows.Forms.Button btnYeniVazgec;
        private System.Windows.Forms.Button btnYeniTamam;
        private System.Windows.Forms.TextBox txbYeniSifreTekrar;
        private System.Windows.Forms.TextBox txbYeniSifre;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button btnGiris;
        private System.Windows.Forms.Button btnVazgec;
    }
}