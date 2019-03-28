namespace KordSA
{
    partial class Urun
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.etiket = new System.Windows.Forms.Label();
            this.strec = new System.Windows.Forms.Label();
            this.weight = new System.Windows.Forms.Label();
            this.bobinBarcode = new System.Windows.Forms.Label();
            this.refBarcode = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.etiket);
            this.panel1.Controls.Add(this.strec);
            this.panel1.Controls.Add(this.weight);
            this.panel1.Controls.Add(this.bobinBarcode);
            this.panel1.Controls.Add(this.refBarcode);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(150, 150);
            this.panel1.TabIndex = 0;
            // 
            // etiket
            // 
            this.etiket.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.etiket.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.etiket.Location = new System.Drawing.Point(0, 90);
            this.etiket.Name = "etiket";
            this.etiket.Size = new System.Drawing.Size(150, 30);
            this.etiket.TabIndex = 1;
            this.etiket.Text = "ETİKETLEME";
            this.etiket.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // strec
            // 
            this.strec.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.strec.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.strec.Location = new System.Drawing.Point(0, 120);
            this.strec.Name = "strec";
            this.strec.Size = new System.Drawing.Size(150, 30);
            this.strec.TabIndex = 1;
            this.strec.Text = "STREÇLENME";
            this.strec.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // weight
            // 
            this.weight.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.weight.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.weight.Location = new System.Drawing.Point(0, 60);
            this.weight.Name = "weight";
            this.weight.Size = new System.Drawing.Size(150, 30);
            this.weight.TabIndex = 1;
            this.weight.Text = "0,0 kg";
            this.weight.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // bobinBarcode
            // 
            this.bobinBarcode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.bobinBarcode.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.bobinBarcode.Location = new System.Drawing.Point(0, 30);
            this.bobinBarcode.Name = "bobinBarcode";
            this.bobinBarcode.Size = new System.Drawing.Size(150, 30);
            this.bobinBarcode.TabIndex = 2;
            this.bobinBarcode.Text = "000000000000";
            this.bobinBarcode.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // refBarcode
            // 
            this.refBarcode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.refBarcode.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.refBarcode.Location = new System.Drawing.Point(0, 0);
            this.refBarcode.Name = "refBarcode";
            this.refBarcode.Size = new System.Drawing.Size(150, 30);
            this.refBarcode.TabIndex = 3;
            this.refBarcode.Text = "000000000";
            this.refBarcode.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Urun
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.panel1);
            this.Name = "Urun";
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.Label weight;
        public System.Windows.Forms.Label bobinBarcode;
        public System.Windows.Forms.Label refBarcode;
        public System.Windows.Forms.Label strec;
        public System.Windows.Forms.Label etiket;
    }
}
