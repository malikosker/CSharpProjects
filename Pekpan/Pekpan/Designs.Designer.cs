namespace Pekpan
{
    partial class Designs
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.grDesigns = new System.Windows.Forms.DataGridView();
            this.colKimlik = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAck = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colXB = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colYB = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colXOf = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colYOff = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colZ = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.genMin = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.genmax = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.boymin = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.boymax = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Tork = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colComp = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colUser = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDeg = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnEkle = new System.Windows.Forms.Button();
            this.btnSil = new System.Windows.Forms.Button();
            this.btnDegistir = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.grDesigns)).BeginInit();
            this.SuspendLayout();
            // 
            // grDesigns
            // 
            this.grDesigns.AllowUserToAddRows = false;
            this.grDesigns.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.LightGray;
            this.grDesigns.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.grDesigns.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grDesigns.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colKimlik,
            this.colAck,
            this.colXB,
            this.colYB,
            this.colXOf,
            this.colYOff,
            this.colZ,
            this.genMin,
            this.genmax,
            this.boymin,
            this.boymax,
            this.Tork,
            this.colComp,
            this.colUser,
            this.colTT,
            this.colDeg,
            this.colDT});
            this.grDesigns.Location = new System.Drawing.Point(13, 13);
            this.grDesigns.Margin = new System.Windows.Forms.Padding(4);
            this.grDesigns.MultiSelect = false;
            this.grDesigns.Name = "grDesigns";
            this.grDesigns.ReadOnly = true;
            this.grDesigns.RowTemplate.Height = 24;
            this.grDesigns.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grDesigns.Size = new System.Drawing.Size(1340, 667);
            this.grDesigns.TabIndex = 1;
            // 
            // colKimlik
            // 
            this.colKimlik.HeaderText = "Kimlik";
            this.colKimlik.Name = "colKimlik";
            this.colKimlik.ReadOnly = true;
            this.colKimlik.Visible = false;
            // 
            // colAck
            // 
            this.colAck.HeaderText = "Açıklama";
            this.colAck.Name = "colAck";
            this.colAck.ReadOnly = true;
            // 
            // colXB
            // 
            this.colXB.HeaderText = "XBaş";
            this.colXB.Name = "colXB";
            this.colXB.ReadOnly = true;
            this.colXB.Width = 90;
            // 
            // colYB
            // 
            this.colYB.HeaderText = "YBaş";
            this.colYB.Name = "colYB";
            this.colYB.ReadOnly = true;
            this.colYB.Width = 90;
            // 
            // colXOf
            // 
            this.colXOf.HeaderText = "XOffset";
            this.colXOf.Name = "colXOf";
            this.colXOf.ReadOnly = true;
            this.colXOf.Width = 90;
            // 
            // colYOff
            // 
            this.colYOff.HeaderText = "YOffset";
            this.colYOff.Name = "colYOff";
            this.colYOff.ReadOnly = true;
            this.colYOff.Width = 90;
            // 
            // colZ
            // 
            this.colZ.HeaderText = "PaletZ";
            this.colZ.Name = "colZ";
            this.colZ.ReadOnly = true;
            // 
            // genMin
            // 
            this.genMin.HeaderText = "Gen. Min";
            this.genMin.Name = "genMin";
            this.genMin.ReadOnly = true;
            // 
            // genmax
            // 
            this.genmax.HeaderText = "Gen. Max";
            this.genmax.Name = "genmax";
            this.genmax.ReadOnly = true;
            this.genmax.Width = 110;
            // 
            // boymin
            // 
            this.boymin.HeaderText = "BoyMin";
            this.boymin.Name = "boymin";
            this.boymin.ReadOnly = true;
            // 
            // boymax
            // 
            this.boymax.HeaderText = "BoyMax";
            this.boymax.Name = "boymax";
            this.boymax.ReadOnly = true;
            // 
            // Tork
            // 
            this.Tork.HeaderText = "Tork";
            this.Tork.Name = "Tork";
            this.Tork.ReadOnly = true;
            // 
            // colComp
            // 
            this.colComp.HeaderText = "Kullanan Firmalar";
            this.colComp.Name = "colComp";
            this.colComp.ReadOnly = true;
            this.colComp.Width = 180;
            // 
            // colUser
            // 
            this.colUser.HeaderText = "Tasarlayan";
            this.colUser.Name = "colUser";
            this.colUser.ReadOnly = true;
            // 
            // colTT
            // 
            this.colTT.HeaderText = "Tasarlanma Tarihi";
            this.colTT.Name = "colTT";
            this.colTT.ReadOnly = true;
            // 
            // colDeg
            // 
            this.colDeg.HeaderText = "Değiştiren";
            this.colDeg.Name = "colDeg";
            this.colDeg.ReadOnly = true;
            // 
            // colDT
            // 
            this.colDT.HeaderText = "Değ. Tarihi";
            this.colDT.Name = "colDT";
            this.colDT.ReadOnly = true;
            this.colDT.Width = 150;
            // 
            // btnEkle
            // 
            this.btnEkle.Location = new System.Drawing.Point(13, 687);
            this.btnEkle.Name = "btnEkle";
            this.btnEkle.Size = new System.Drawing.Size(80, 46);
            this.btnEkle.TabIndex = 2;
            this.btnEkle.Text = "EKLE";
            this.btnEkle.UseVisualStyleBackColor = true;
            this.btnEkle.Click += new System.EventHandler(this.btnEkle_Click);
            // 
            // btnSil
            // 
            this.btnSil.Location = new System.Drawing.Point(99, 687);
            this.btnSil.Name = "btnSil";
            this.btnSil.Size = new System.Drawing.Size(80, 46);
            this.btnSil.TabIndex = 2;
            this.btnSil.Text = "SİL";
            this.btnSil.UseVisualStyleBackColor = true;
            this.btnSil.Click += new System.EventHandler(this.btnSil_Click);
            // 
            // btnDegistir
            // 
            this.btnDegistir.Location = new System.Drawing.Point(185, 687);
            this.btnDegistir.Name = "btnDegistir";
            this.btnDegistir.Size = new System.Drawing.Size(80, 46);
            this.btnDegistir.TabIndex = 2;
            this.btnDegistir.Text = "DEĞİŞTİR";
            this.btnDegistir.UseVisualStyleBackColor = true;
            this.btnDegistir.Click += new System.EventHandler(this.btnDegistir_Click);
            // 
            // Designs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1366, 745);
            this.Controls.Add(this.btnDegistir);
            this.Controls.Add(this.btnSil);
            this.Controls.Add(this.btnEkle);
            this.Controls.Add(this.grDesigns);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Designs";
            this.Text = "Designs";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Designs_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grDesigns)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView grDesigns;
        private System.Windows.Forms.Button btnEkle;
        private System.Windows.Forms.Button btnSil;
        private System.Windows.Forms.Button btnDegistir;
        private System.Windows.Forms.DataGridViewTextBoxColumn colKimlik;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAck;
        private System.Windows.Forms.DataGridViewTextBoxColumn colXB;
        private System.Windows.Forms.DataGridViewTextBoxColumn colYB;
        private System.Windows.Forms.DataGridViewTextBoxColumn colXOf;
        private System.Windows.Forms.DataGridViewTextBoxColumn colYOff;
        private System.Windows.Forms.DataGridViewTextBoxColumn colZ;
        private System.Windows.Forms.DataGridViewTextBoxColumn genMin;
        private System.Windows.Forms.DataGridViewTextBoxColumn genmax;
        private System.Windows.Forms.DataGridViewTextBoxColumn boymin;
        private System.Windows.Forms.DataGridViewTextBoxColumn boymax;
        private System.Windows.Forms.DataGridViewTextBoxColumn Tork;
        private System.Windows.Forms.DataGridViewTextBoxColumn colComp;
        private System.Windows.Forms.DataGridViewTextBoxColumn colUser;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTT;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDeg;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDT;
    }
}