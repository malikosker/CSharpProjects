using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using Modbus;
using Modbus.Data;
using Modbus.Device;
using System.Net.Sockets;
using System.Threading;
using System.IO;
using Microsoft.VisualBasic.FileIO;

namespace PaletlemeUygulaması
{
    public partial class Form1 : Form
    {
        int x, y, i, j;
        public Form1()
        {
            InitializeComponent();
            KeyDown += new KeyEventHandler(tabControl1_KeyDown);
            //KeyDown += new KeyEventHandler(pictureBox_KeyDown);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            textBox4.Location = new Point(tabControl1.Location.X + tabControl1.Size.Width / 2 - textBox5.Size.Width - 150, tabControl1.Location.Y - 10);
            textBox5.Location = new Point(tabControl1.Location.X + tabControl1.Size.Width / 2 - 140, tabControl1.Location.Y - 10);
            button3.Location = new Point(tabControl1.Location.X + tabControl1.Size.Width / 2 + textBox5.Size.Width - 130, tabControl1.Location.Y - 10);
            label32.Location = new Point(tabControl1.Location.X + tabControl1.Size.Width / 2 + textBox5.Size.Width - 50, tabControl1.Location.Y - 6);
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            toolStripComboBox1.DropDownStyle = ComboBoxStyle.DropDownList;

            try
            {
                TextFieldParser datReader = new TextFieldParser(Application.StartupPath + @"\images\paletDatas.dat");
                datReader.SetDelimiters(new string[] { ";" });
                datReader.HasFieldsEnclosedInQuotes = true;
                string[] colFields = datReader.ReadFields();
                for (int i = 0; i < colFields.Count(s => s != null); i++)
                {
                    try
                    {
                        comboBox1.Items[i] = colFields[i];
                    }
                    catch (System.ArgumentOutOfRangeException)
                    {
                        comboBox1.Items.Add(colFields[i]);
                    }
                }
            }
            catch (System.IO.FileNotFoundException)
            {
            }
            if (comboBox1.Items.Count > 0)
            {
                comboBox1.SelectedIndex = 0;
            }
            else
            {
                paletVallues[0] = 1000;
                paletVallues[1] = 1000;
            }

            toolStripComboBox1.Items.Clear();
            DirectoryInfo di = new DirectoryInfo(Application.StartupPath + @"\recipe datas");
            FileInfo[] TXTFiles = di.GetFiles("*.dat");

            foreach (FileInfo fi in TXTFiles)
            {
                toolStripComboBox1.Items.Add(fi.Name);
            }
        }

        public string TakeDatValues(int a)
        {
            DirectoryInfo di = new DirectoryInfo(Application.StartupPath + @"\images\");
            FileInfo[] datFiles = di.GetFiles("*.dat");
            if (!(a < datFiles.Length))
                goto jmp;

            TextFieldParser datReader = new TextFieldParser(Application.StartupPath + @"\images\" + datFiles[a].Name);
            datReader.SetDelimiters(new string[] { ";" });
            string[] colFields = datReader.ReadFields();
            for (int i = 0; i < colFields.Length; i++)
            {
                if (colFields[i] == "")
                {
                    colFields[i] = null;
                }
            }
            return colFields[a++];
        jmp:
            return null;
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            PaletPanlel();
            OptionPanel();
            MouseLocationLabels();
        }

        public void OptionPanel()
        {
            tabControl2.Size = new Size(this.Width * 37 / 100, this.Height - 100);
            tabControl2.Location = new Point(0, 40);
            pictureBox2.Location = new Point(tabPage6.Size.Width / 2 - pictureBox2.Size.Width / 2, 110);
        }

        public void PaletPanlel()
        {
            tabControl1.Size = new Size(this.Width * 62 / 100, this.Height - 100);
            tabControl1.Location = new Point(this.Width - tabControl1.Width - 15, 40);
            panel1.Size = new Size((int)(Math.Round(PanelDimensions(paletVallues[0], paletVallues[1], 0), 0)), (int)(Math.Round(PanelDimensions(paletVallues[0], paletVallues[1], 1), 0)));
            panel1.Location = new Point(tabPage1.Size.Width / 2 - panel1.Size.Width / 2, tabPage1.Size.Height / 2 - panel1.Size.Height / 2);
            tabPage1.Invalidate();
        }
        public void PaletPanel2()
        {

            panel2.Size = new Size((int)(Math.Round(PanelDimensions(paletVallues[0], paletVallues[1], 0), 0)), (int)(Math.Round(PanelDimensions(paletVallues[0], paletVallues[1], 1), 0)));
            panel2.Location = new Point(tabPage2.Size.Width / 2 - panel2.Size.Width / 2, tabPage1.Size.Height / 2 - panel2.Size.Height / 2);
            tabPage2.Invalidate();
        }
        public void MouseLocationLabels()
        {
            label3.Location = new Point(tabControl1.Location.X + tabControl1.Size.Width / 2, tabControl1.Location.Y + tabControl1.Size.Height + 2);
        }


        private void button1_Click(object sender, EventArgs e)
        {
            string directory = Application.StartupPath + @"\images";
            string fileName = "paletDatas.dat";
            string memoryPath = Path.Combine(directory, fileName);
            StreamWriter sw = File.CreateText(memoryPath);

            int comboCounter = 0;
            int k = 0;
            string[] forSorting = new string[comboBox1.Items.Count + 1];
            try
            {
                for (int i = 0; i < comboBox1.Items.Count; i++)
                {
                    if (comboBox1.Items[i].ToString() == Convert.ToInt32(textBox1.Text) + " mm x " + Convert.ToInt32(textBox2.Text) + " mm")
                    {
                        comboCounter++;
                    }
                }
                if (comboCounter == 0)
                {
                    comboBox1.Items.Add(Convert.ToInt32(textBox1.Text) + " mm x " + Convert.ToInt32(textBox2.Text) + " mm");
                    for (int i = 0; i < comboBox1.Items.Count; i++)
                    {
                        forSorting[i] = comboBox1.Items[i].ToString();
                    }
                    foreach (string s in SortByLength(forSorting))
                    {
                        comboBox1.Items[k] = s;
                        k++;
                    }
                    for (int i = 0; i < comboBox1.Items.Count; i++)
                    {
                        forSorting[i] = comboBox1.Items[i].ToString();
                    }
                    sw.WriteLine(string.Join(";", forSorting));
                }
                else
                {
                    MessageBox.Show("Eklemek istediğiniz palet boyutları zaten mevcut.");
                }
            }
            catch (System.FormatException)
            {
                MessageBox.Show("Lütfen eklemek istediğiniz palet boyutlarını anlamlı tam sayılar olarak giriniz.");
            }
            sw.Close();
        }

        static IEnumerable<string> SortByLength(IEnumerable<string> e)
        {
            // Use LINQ to sort the array received and return a copy.
            var sorted = from s in e
                         orderby s.Length ascending
                         select s;
            return sorted;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string directory = Application.StartupPath + @"\images";
            string fileName = "paletDatas.dat";
            string memoryPath = Path.Combine(directory, fileName);
            StreamWriter sw = File.CreateText(memoryPath);
            string[] forSorting = new string[comboBox1.Items.Count - 1];

            comboBox1.Items.RemoveAt(comboBox1.SelectedIndex);

            for (int i = 0; i < comboBox1.Items.Count; i++)
            {
                forSorting[i] = comboBox1.Items[i].ToString();
            }
            sw.WriteLine(string.Join(";", forSorting));
            sw.Close();
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            Point p = new Point(e.X, e.Y);
            i = p.X;
            j = p.Y;
            double pv0, pv1, pd0, pd1;
            pv0 = (double)(paletVallues[0]);
            pv1 = (double)(paletVallues[1]);
            pd0 = PanelDimensions(paletVallues[0], paletVallues[1], 0);
            pd1 = PanelDimensions(paletVallues[0], paletVallues[1], 1);
            double a = pv0 / pd0;
            double b = pv1 / pd1;
            label3.Text = Convert.ToString((int)(i * a)) + " , " + Convert.ToString((int)(j * b));
        }

        int[] paletVallues = new int[2];
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Split on one or more non-digit characters.
            string[] numbers = Regex.Split(comboBox1.Text, @"\D+");
            int index = 0;
            foreach (string value in numbers)
            {
                if (!string.IsNullOrEmpty(value))
                {
                    int i = int.Parse(value);
                    paletVallues[index] = i;
                    index++;
                }
            }
            PaletPanlel();
            if (checkBox1.Checked)
            {
                PaletPanel2();
            }
            label31.Visible = false;
            paletPictureLayout();

            for (int i = 0; i < a; i++)
            {
                pictureBox[i].Size = new Size((int)(Math.Round(pictureBoxDimensions(variable_X[i], variable_Y[i], 0), 0)), (int)(Math.Round(pictureBoxDimensions(variable_X[i], variable_Y[i], 1), 0)));
            }
            //panel2 control
            if (checkBox1.Checked)
            {
                for (int i = 0; i < a; i++)
                {
                    pictureBoxSecond[i].Size = new Size((int)(Math.Round(pictureBoxDimensions(variable_X_Second[i], variable_Y_Second[i], 0), 0)), (int)(Math.Round(pictureBoxDimensions(variable_X_Second[i], variable_Y_Second[i], 1), 0)));
                }
            }
            lineUp();
        }
        public void paletPictureLayout()
        {
            if (paletVallues[0] < paletVallues[1] && pictureBox2.Size.Width > pictureBox2.Size.Height)
            {
                int changedPic_X = pictureBox2.Size.Height;
                int changedPic_Y = pictureBox2.Size.Width;
                pictureBox2.Size = new Size(changedPic_X, changedPic_Y);
                pictureBox2.Image.RotateFlip(RotateFlipType.Rotate90FlipY);
                if (pictureBox2.Size.Width > pictureBox2.Size.Height)
                {
                    pictureBox2.Location = new Point(tabPage6.Size.Width / 2 - pictureBox2.Size.Width / 2, pictureBox2.Location.Y + (pictureBox2.Size.Width - pictureBox2.Size.Height) / 2);
                }
                else
                    pictureBox2.Location = new Point(tabPage6.Size.Width / 2 - pictureBox2.Size.Width / 2, pictureBox2.Location.Y - (pictureBox2.Size.Height - pictureBox2.Size.Width) / 2);
            }
            if (paletVallues[0] > paletVallues[1] && pictureBox2.Size.Width < pictureBox2.Size.Height)
            {
                int changedPic_X = pictureBox2.Size.Height;
                int changedPic_Y = pictureBox2.Size.Width;
                pictureBox2.Size = new Size(changedPic_X, changedPic_Y);
                pictureBox2.Image.RotateFlip(RotateFlipType.Rotate90FlipY);
                if (pictureBox2.Size.Width > pictureBox2.Size.Height)
                {
                    pictureBox2.Location = new Point(tabPage6.Size.Width / 2 - pictureBox2.Size.Width / 2, pictureBox2.Location.Y + (pictureBox2.Size.Width - pictureBox2.Size.Height) / 2);
                }
                else
                    pictureBox2.Location = new Point(tabPage6.Size.Width / 2 - pictureBox2.Size.Width / 2, pictureBox2.Location.Y - (pictureBox2.Size.Height - pictureBox2.Size.Width) / 2);
            }
        }

        public void lineUp()
        {
            int comboRow = 0;
            for (int i = 0; i < a; i++)
            {
                if (i == 0)
                {
                    pictureBox[i].Location = new System.Drawing.Point(i * (int)(Math.Round(pictureBoxDimensions(variable_X[a], variable_Y[a], 0), 0)), 0);
                }
                else
                {
                    if (pictureBox[i].Size.Width <= panel1.Size.Width - pictureBox[i - 1].Location.X - pictureBox[i - 1].Size.Width)
                    {
                        pictureBox[i].Location = new System.Drawing.Point(pictureBox[i - 1].Location.X + pictureBox[i - 1].Size.Width, comboRow * pictureBox[i - 1].Size.Height);
                    }
                    else
                    {
                        comboRow++;
                        column = 0;
                        row = pictureBox[i - 1].Location.Y + pictureBox[i - 1].Size.Height;
                        pictureBox[i].Location = new System.Drawing.Point(column, row);
                    }
                }
            }
            writeCoordinateLabes();
            //panel2 control
            if (checkBox1.Checked)
            {
                try
                {
                    comboRow = 0;
                    for (int i = 0; i < b; i++)
                    {
                        if (i == 0)
                        {
                            pictureBoxSecond[i].Location = new System.Drawing.Point(i * (int)(Math.Round(pictureBoxDimensions(variable_X[a], variable_Y_Second[a], 0), 0)), 0);
                        }
                        else
                        {
                            if (pictureBoxSecond[i].Size.Width <= panel2.Size.Width - pictureBoxSecond[i - 1].Location.X - pictureBoxSecond[i - 1].Size.Width)
                            {
                                pictureBoxSecond[i].Location = new System.Drawing.Point(pictureBoxSecond[i - 1].Location.X + pictureBoxSecond[i - 1].Size.Width, comboRow * pictureBoxSecond[i - 1].Size.Height);
                            }
                            else
                            {
                                comboRow++;
                                columnSecond = 0;
                                rowSecond = pictureBoxSecond[i - 1].Location.Y + pictureBoxSecond[i - 1].Size.Height;
                                pictureBoxSecond[i].Location = new System.Drawing.Point(columnSecond, rowSecond);
                            }
                        }
                    }
                    writeCoordinateLabes();
                }
                catch (System.NullReferenceException)
                {
                }
            }
        }

        int b = 0;
        int rowSecond = 0;
        int columnSecond = 0;
        PictureBox[] pictureBoxSecond = new PictureBox[100];
        Label[] coordinateLabelSecond = new Label[100];
        Button[] pictureBoxButtonSecond = new Button[100];
        CheckBox[] pictureCheckBoxSecond = new CheckBox[100];
        int[] variable_X_Second = new int[100];
        int[] variable_Y_Second = new int[100];
        int[] variable_Z_Second = new int[100];

        int a = 0;
        int row = 0;
        int column = 0;
        PictureBox[] pictureBox = new PictureBox[100];
        Label[] coordinateLabel = new Label[100];
        Button[] pictureBoxButton = new Button[100];
        CheckBox[] pictureCheckBox = new CheckBox[100];
        int[] variable_X = new int[100];
        int[] variable_Y = new int[100];
        int[] variable_Z = new int[100];
        int productX = 0;
        int productY = 0;
        int productZ = 0;
        int productWeight = 0;
        int addCounter = 0;
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            try
            {
                if ((productX == Convert.ToInt32(domainUpDown1.Text) && productY == Convert.ToInt32(domainUpDown2.Text) && productZ == Convert.ToInt32(domainUpDown3.Text) && productWeight == Convert.ToInt32(domainUpDown4.Text)) | addCounter == 0)
                {
                    if (label33.ForeColor == Color.Green | label34.ForeColor == Color.Green)
                    {
                        //panel2 control
                        if (tabControl1.SelectedTab == tabPage2)
                        {
                            variable_X_Second[b] = Convert.ToInt32(domainUpDown1.Text);
                            variable_Y_Second[b] = Convert.ToInt32(domainUpDown2.Text);
                            variable_Z_Second[b] = Convert.ToInt32(domainUpDown3.Text);
                            if (addCounter == 0)
                            {
                                productX = variable_X_Second[0];
                                productY = variable_Y_Second[0];
                                productZ = variable_Z_Second[0];
                                productWeight = Convert.ToInt32(domainUpDown4.Text);
                            }
                            pictureBoxSecond[b] = new PictureBox();
                            pictureBoxSecond[b].Name = "pictureBox" + b;
                            pictureBoxSecond[b].Size = new System.Drawing.Size((int)(Math.Round(pictureBoxDimensions(Convert.ToInt32(domainUpDown1.Text), Convert.ToInt32(domainUpDown2.Text), 0), 0)), (int)(Math.Round(pictureBoxDimensions(Convert.ToInt32(domainUpDown1.Text), Convert.ToInt32(domainUpDown2.Text), 1), 0)));

                            if (b == 0)
                            {
                                pictureBoxSecond[b].Location = new System.Drawing.Point(b * (int)(Math.Round(pictureBoxDimensions(variable_X_Second[b], variable_Y_Second[b], 0), 0)), 0);
                            }
                            else
                            {
                                if (pictureBoxSecond[b].Size.Width <= panel2.Size.Width - pictureBoxSecond[b - 1].Location.X - pictureBoxSecond[b - 1].Size.Width)
                                {
                                    pictureBoxSecond[b].Location = new System.Drawing.Point(pictureBoxSecond[b - 1].Location.X + pictureBoxSecond[b - 1].Size.Width, rowSecond);
                                }
                                else
                                {
                                    columnSecond = 0;
                                    rowSecond = pictureBoxSecond[b - 1].Location.Y + pictureBoxSecond[b - 1].Size.Height;
                                    pictureBoxSecond[b].Location = new System.Drawing.Point(columnSecond, rowSecond);
                                }
                            }
                            string path = "";
                            if (label33.ForeColor == Color.Green)
                            {
                                path = Application.StartupPath + @"\images\koliiçi.jpg";
                            }
                            if (label34.ForeColor == Color.Green)
                            {
                                path = Application.StartupPath + @"\images\çuval.jpg";
                            }
                            Bitmap image = new Bitmap(path, true);
                            pictureBoxSecond[b].Image = image;
                            pictureBoxSecond[b].SizeMode = PictureBoxSizeMode.StretchImage;
                            panel2.Controls.Add(pictureBoxSecond[b]);
                            pictureBoxSecond[b].MouseDown += new MouseEventHandler(pictureBox_MouseDown);
                            pictureBoxSecond[b].MouseMove += new MouseEventHandler(pictureBox_MouseMove);
                            pictureBoxSecond[b].MouseUp += new MouseEventHandler(pictureBox_MouseUp);

                            coordinateLabelSecond[b] = new Label();
                            coordinateLabelSecond[b].Size = new Size(60, 26);
                            coordinateLabelSecond[b].BackColor = Color.Transparent;
                            coordinateLabelSecond[b].ForeColor = Color.White;
                            coordinateLabelSecond[b].Location = new System.Drawing.Point(0, 0);
                            pictureBoxSecond[b].Controls.Add(coordinateLabelSecond[b]);

                            pictureBoxButtonSecond[b] = new Button();
                            pictureBoxButtonSecond[b].Size = new Size(30, 30);
                            pictureBoxButtonSecond[b].BackColor = Color.CadetBlue;
                            path = Application.StartupPath + @"\images\döndürme ikonu.jpg";
                            image = new Bitmap(path, true);
                            image = new Bitmap(image, new Size(19, 19));
                            pictureBoxButtonSecond[b].Image = image;
                            pictureBoxButtonSecond[b].ImageAlign = ContentAlignment.MiddleCenter;
                            pictureBoxButtonSecond[b].Location = new System.Drawing.Point(0, 27);
                            pictureBoxSecond[b].Controls.Add(pictureBoxButtonSecond[b]);
                            pictureBoxButtonSecond[b].Click += new EventHandler(pictureBoxButton_Clicked);

                            pictureCheckBoxSecond[b] = new CheckBox();
                            pictureCheckBoxSecond[b].BackColor = Color.Transparent;
                            if (pictureBoxSecond[b].Width > pictureBoxSecond[b].Height)
                            {
                                pictureCheckBoxSecond[b].Location = new System.Drawing.Point(pictureBoxSecond[b].Size.Width - 38, 13);
                            }
                            else
                            {
                                pictureCheckBoxSecond[b].Location = new System.Drawing.Point(pictureBoxSecond[b].Size.Width - 29, 22);
                            }
                            pictureBoxSecond[b].Controls.Add(pictureCheckBoxSecond[b]);

                            b++;
                            addCounter = a + b;
                        }
                        else
                        {
                            variable_X[a] = Convert.ToInt32(domainUpDown1.Text);
                            variable_Y[a] = Convert.ToInt32(domainUpDown2.Text);
                            variable_Z[a] = Convert.ToInt32(domainUpDown3.Text);
                            if (addCounter == 0)
                            {
                                productX = variable_X[0];
                                productY = variable_Y[0];
                                productZ = variable_Z[0];
                                productWeight = Convert.ToInt32(domainUpDown4.Text);
                            }
                            pictureBox[a] = new PictureBox();
                            pictureBox[a].Name = "pictureBox" + a;
                            pictureBox[a].Size = new System.Drawing.Size((int)(Math.Round(pictureBoxDimensions(Convert.ToInt32(domainUpDown1.Text), Convert.ToInt32(domainUpDown2.Text), 0), 0)), (int)(Math.Round(pictureBoxDimensions(Convert.ToInt32(domainUpDown1.Text), Convert.ToInt32(domainUpDown2.Text), 1), 0)));

                            if (a == 0)
                            {
                                pictureBox[a].Location = new System.Drawing.Point(a * (int)(Math.Round(pictureBoxDimensions(variable_X[a], variable_Y[a], 0), 0)), 0);
                            }
                            else
                            {
                                if (pictureBox[a].Size.Width <= panel1.Size.Width - pictureBox[a - 1].Location.X - pictureBox[a - 1].Size.Width)
                                {
                                    pictureBox[a].Location = new System.Drawing.Point(pictureBox[a - 1].Location.X + pictureBox[a - 1].Size.Width, row);
                                }
                                else
                                {
                                    column = 0;
                                    row = pictureBox[a - 1].Location.Y + pictureBox[a - 1].Size.Height;
                                    pictureBox[a].Location = new System.Drawing.Point(column, row);
                                }
                            }
                            string path = "";
                            if (label33.ForeColor == Color.Green)
                            {
                                path = Application.StartupPath + @"\images\koliiçi.jpg";
                            }
                            if (label34.ForeColor == Color.Green)
                            {
                                path = Application.StartupPath + @"\images\çuval.jpg";
                            }
                            Bitmap image = new Bitmap(path, true);
                            pictureBox[a].Image = image;
                            pictureBox[a].SizeMode = PictureBoxSizeMode.StretchImage;
                            panel1.Controls.Add(pictureBox[a]);
                            pictureBox[a].MouseDown += new MouseEventHandler(pictureBox_MouseDown);
                            pictureBox[a].MouseMove += new MouseEventHandler(pictureBox_MouseMove);
                            pictureBox[a].MouseUp += new MouseEventHandler(pictureBox_MouseUp);
                            //pictureBox[a].PreviewKeyDown += new PreviewKeyDownEventHandler(pictureBox_KeyDown);

                            coordinateLabel[a] = new Label();
                            coordinateLabel[a].Size = new Size(60, 26);
                            coordinateLabel[a].BackColor = Color.Transparent;
                            coordinateLabel[a].ForeColor = Color.White;
                            coordinateLabel[a].Location = new System.Drawing.Point(0, 0);
                            pictureBox[a].Controls.Add(coordinateLabel[a]);

                            pictureBoxButton[a] = new Button();
                            pictureBoxButton[a].Size = new Size(30, 30);
                            pictureBoxButton[a].BackColor = Color.CadetBlue;
                            path = Application.StartupPath + @"\images\döndürme ikonu.jpg";
                            image = new Bitmap(path, true);
                            image = new Bitmap(image, new Size(19, 19));
                            pictureBoxButton[a].Image = image;
                            pictureBoxButton[a].ImageAlign = ContentAlignment.MiddleCenter;
                            pictureBoxButton[a].Location = new System.Drawing.Point(0, 27);
                            pictureBox[a].Controls.Add(pictureBoxButton[a]);
                            pictureBoxButton[a].Click += new EventHandler(pictureBoxButton_Clicked);

                            pictureCheckBox[a] = new CheckBox();
                            pictureCheckBox[a].BackColor = Color.Transparent;
                            if (pictureBox[a].Width > pictureBox[a].Height)
                            {
                                pictureCheckBox[a].Location = new System.Drawing.Point(pictureBox[a].Size.Width - 38, 13);
                            }
                            else
                            {
                                pictureCheckBox[a].Location = new System.Drawing.Point(pictureBox[a].Size.Width - 29, 22);
                            }
                            pictureBox[a].Controls.Add(pictureCheckBox[a]);

                            a++;
                            addCounter = a + b;
                        }

                        writeCoordinateLabes();

                    }
                    else
                    {
                        MessageBox.Show("Lütfen yükleme tipini seçin. (Koli yada Çuval)");
                    }
                }
                else
                {
                    MessageBox.Show("Bir paletteki ürünlerin boyutları farklı olamaz");
                }
            }
            catch (System.FormatException)
            {
                MessageBox.Show("Lütfen ürün boyutlarını, ağırlığını ve adedini anlamlı doğal sayılar olarak giriniz.");
            }
        }

        public void fillBlanks()
        {
            //if (a == 0)
            //{
            //    pictureBox[a].Location = new System.Drawing.Point(a * (int)(Math.Round(pictureBoxDimensions(variable_X[a], variable_Y[a], 0), 0)), 0);
            //}
            //else
            //{
            //    if (pictureBox[a].Size.Width <= panel1.Size.Width - pictureBox[a - 1].Location.X - pictureBox[a - 1].Size.Width)
            //    {
            //        pictureBox[a].Location = new System.Drawing.Point(pictureBox[a - 1].Location.X + pictureBox[a - 1].Size.Width, row);
            //    }
            //    else
            //    {
            //        column = 0;
            //        row = pictureBox[a - 1].Location.Y + pictureBox[a - 1].Size.Height;
            //        pictureBox[a].Location = new System.Drawing.Point(column, row);
            //    }
            //}
        }

        int picButtonIndex;
        int picButtonIndex2;
        private void pictureBoxButton_Clicked(object sender, EventArgs e)
        {
            Button picButton_active = sender as Button;
            for (int i = 0; i < pictureBoxButton.Count(s => s != null); i++)
            {
                if (picButton_active.Equals(pictureBoxButton[i]))
                {
                    picButtonIndex = i;
                    break;
                }
                else
                    picButtonIndex = 99;
            }
            try
            {
                int changed_X = pictureBox[picButtonIndex].Size.Height;
                int changed_Y = pictureBox[picButtonIndex].Size.Width;
                pictureBox[picButtonIndex].Size = new Size(changed_X, changed_Y);
                variable_X[picButtonIndex] = (int)(pictureBox[picButtonIndex].Size.Width * scaling(0));
                variable_Y[picButtonIndex] = (int)(pictureBox[picButtonIndex].Size.Height * scaling(1));
                pictureBox[picButtonIndex].BringToFront();
                if (pictureBox[picButtonIndex].Width > pictureBox[picButtonIndex].Height)
                {
                    pictureCheckBox[picButtonIndex].Location = new System.Drawing.Point(pictureBox[picButtonIndex].Size.Width - 38, 13);
                }
                else
                {
                    pictureCheckBox[picButtonIndex].Location = new System.Drawing.Point(pictureBox[picButtonIndex].Size.Width - 29, 22);
                }
            }
            catch (System.NullReferenceException)
            {
            }

            for (int i = 0; i < pictureBoxButtonSecond.Count(s => s != null); i++)
            {
                if (picButton_active.Equals(pictureBoxButtonSecond[i]))
                {
                    picButtonIndex2 = i;
                    break;
                }
                else
                    picButtonIndex2 = 99;
            }
            if (checkBox1.Checked)
            {
                try
                {
                    int changed_X = pictureBoxSecond[picButtonIndex2].Size.Height;
                    int changed_Y = pictureBoxSecond[picButtonIndex2].Size.Width;
                    pictureBoxSecond[picButtonIndex2].Size = new Size(changed_X, changed_Y);
                    variable_X_Second[picButtonIndex2] = (int)(pictureBoxSecond[picButtonIndex2].Size.Width * scaling(0));
                    variable_Y_Second[picButtonIndex2] = (int)(pictureBoxSecond[picButtonIndex2].Size.Height * scaling(1));
                    pictureBoxSecond[picButtonIndex2].BringToFront();
                    if (pictureBoxSecond[picButtonIndex2].Width > pictureBoxSecond[picButtonIndex2].Height)
                    {
                        pictureCheckBoxSecond[picButtonIndex2].Location = new System.Drawing.Point(pictureBoxSecond[picButtonIndex2].Size.Width - 38, 13);
                    }
                    else
                    {
                        pictureCheckBoxSecond[picButtonIndex2].Location = new System.Drawing.Point(pictureBoxSecond[picButtonIndex2].Size.Width - 29, 22);
                    }
                }
                catch (System.NullReferenceException)
                {
                }
            }
            writeCoordinateLabes();
        }

        public void writeCoordinateLabes()
        {
            for (int i = 0; i < a; i++)
            {
                coordinateLabel[i].Text = i + 1 + "\n" + Convert.ToString((int)(Math.Round(pictureBox[i].Location.X * scaling(0) + (double)(variable_X[i]) / 2, 0))) + " , " + Convert.ToString((int)(Math.Round(pictureBox[i].Location.Y * scaling(1) + (double)(variable_Y[i]) / 2, 0)));
            }
            //panel2 control
            if (checkBox1.Checked)
            {
                for (int i = 0; i < b; i++)
                {
                    try
                    {
                        coordinateLabelSecond[i].Text = i + 1 + "\n" + Convert.ToString((int)(Math.Round(pictureBoxSecond[i].Location.X * scaling(0) + (double)(variable_X_Second[i]) / 2, 0))) + " , " + Convert.ToString((int)(Math.Round(pictureBoxSecond[i].Location.Y * scaling(1) + (double)(variable_Y_Second[i]) / 2, 0)));
                    }
                    catch (System.NullReferenceException)
                    {
                    }
                }
            }
        }

        public double pictureBoxDimensions(double urunX, double urunY, int index)
        {
            double[] retVal = new double[2];
            retVal[0] = (double)(panel1.Size.Width) * urunX / (double)(paletVallues[0]);
            retVal[1] = (double)(panel1.Size.Height) * urunY / (double)(paletVallues[1]);
            return retVal[index];
        }

        //private void pictureBox_KeyDown(object sender, KeyEventArgs e)
        //{
        //    int x = pictureBox[0].Location.X;
        //    int y = pictureBox[0].Location.Y;

        //    if (e.KeyCode == Keys.Right) x += 1;
        //    else if (e.KeyCode == Keys.Left) x -= 1;
        //    else if (e.KeyCode == Keys.Up) y -= 1;
        //    else if (e.KeyCode == Keys.Down) y += 1;

        //    pictureBox[0].Location = new Point(x, y);
        //}

        Point location = Point.Empty;
        private void pictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                location = new Point(e.X, e.Y);
            }
        }

        int picIndex;
        int picIndex2;
        private void pictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (location != Point.Empty)
            {
                PictureBox picBox_active = sender as PictureBox;
                picBox_active.BringToFront();
                Point newlocation = picBox_active.Location;
                newlocation.X += e.X - location.X;
                newlocation.Y += e.Y - location.Y;

                //panel2 control
                if (tabControl1.SelectedTab == tabPage2)
                {
                    if (newlocation.Y < 15)
                    {
                        if (newlocation.X < 15)
                        {
                            picBox_active.Location = new Point(0, 0);
                        }
                        else if (15 > panel1.Size.Width - (newlocation.X + picBox_active.Size.Width))
                        {
                            picBox_active.Location = new Point(panel1.Size.Width - picBox_active.Size.Width, 0);
                        }
                        else
                        {
                            if (b != 1)
                            {
                                for (int j = 0; j < b; j++)
                                {
                                    if (picBox_active.Equals(pictureBoxSecond[j]))
                                    {
                                        j++;
                                    }
                                    try
                                    {
                                        if (15 > Math.Abs(pictureBoxSecond[j].Left - (newlocation.X + picBox_active.Width)))
                                        {
                                            picBox_active.Location = new Point(pictureBoxSecond[j].Left - picBox_active.Width, 0);
                                            break;
                                        }
                                        else if (15 > Math.Abs(pictureBoxSecond[j].Right - newlocation.X))
                                        {
                                            picBox_active.Location = new Point(pictureBoxSecond[j].Right, 0);
                                            break;
                                        }
                                        else
                                        {
                                            picBox_active.Location = new Point(newlocation.X, 0);
                                        }
                                    }
                                    catch (System.NullReferenceException)
                                    {
                                    }
                                }
                            }
                            else
                            {
                                picBox_active.Location = new Point(newlocation.X, 0);
                            }
                        }
                    }
                    else if (newlocation.X < 15)
                    {
                        if (newlocation.Y < 15)
                        {
                            picBox_active.Location = new Point(0, 0);
                        }
                        else if (15 > panel1.Size.Height - (newlocation.Y + picBox_active.Size.Height))
                        {
                            picBox_active.Location = new Point(0, panel1.Size.Height - picBox_active.Size.Height);
                        }
                        else
                        {
                            if (b != 1)
                            {
                                for (int j = 0; j < b; j++)
                                {
                                    if (picBox_active.Equals(pictureBoxSecond[j]))
                                    {
                                        j++;
                                    }
                                    try
                                    {
                                        if (15 > Math.Abs(pictureBoxSecond[j].Top - (newlocation.Y + picBox_active.Height)))
                                        {
                                            picBox_active.Location = new Point(0, pictureBoxSecond[j].Top - picBox_active.Size.Height);
                                            break;
                                        }

                                        else if (15 > Math.Abs(pictureBoxSecond[j].Bottom - newlocation.Y))
                                        {
                                            picBox_active.Location = new Point(0, pictureBoxSecond[j].Bottom);
                                            break;
                                        }
                                        else
                                            picBox_active.Location = new Point(0, newlocation.Y);
                                    }
                                    catch (System.NullReferenceException)
                                    {
                                    }
                                }
                            }
                            else
                            {
                                picBox_active.Location = new Point(0, newlocation.Y);
                            }
                        }
                    }
                    else if (15 > panel1.Size.Width - (newlocation.X + picBox_active.Size.Width))
                    {
                        if (newlocation.Y < 15)
                        {
                            picBox_active.Location = new Point(panel1.Size.Width - picBox_active.Size.Width, 0);
                        }
                        else if (15 > panel1.Size.Height - (newlocation.Y + picBox_active.Size.Height))
                        {
                            picBox_active.Location = new Point(panel1.Size.Width - picBox_active.Size.Width, panel1.Size.Height - picBox_active.Size.Height);
                        }
                        else
                        {
                            if (b != 1)
                            {
                                for (int j = 0; j < b; j++)
                                {
                                    if (picBox_active.Equals(pictureBoxSecond[j]))
                                    {
                                        j++;
                                    }
                                    try
                                    {
                                        if (15 > Math.Abs(pictureBoxSecond[j].Top - (newlocation.Y + picBox_active.Height)))
                                        {
                                            picBox_active.Location = new Point(panel1.Size.Width - picBox_active.Size.Width, pictureBoxSecond[j].Top - picBox_active.Size.Height);
                                            break;
                                        }

                                        else if (15 > Math.Abs(pictureBoxSecond[j].Bottom - newlocation.Y))
                                        {
                                            picBox_active.Location = new Point(panel1.Size.Width - picBox_active.Size.Width, pictureBoxSecond[j].Bottom);
                                            break;
                                        }
                                        else
                                            picBox_active.Location = new Point(panel1.Size.Width - picBox_active.Size.Width, newlocation.Y);
                                    }
                                    catch (System.NullReferenceException)
                                    {
                                    }
                                }
                            }
                            else
                            {
                                picBox_active.Location = new Point(panel1.Size.Width - picBox_active.Size.Width, newlocation.Y);
                            }
                        }
                    }
                    else if (15 > panel1.Size.Height - (newlocation.Y + picBox_active.Size.Height))
                    {
                        if (newlocation.X < 15)
                        {
                            picBox_active.Location = new Point(0, panel1.Size.Height - picBox_active.Size.Height);
                        }
                        else if (15 > panel1.Size.Width - (newlocation.X + picBox_active.Size.Width))
                        {
                            picBox_active.Location = new Point(panel1.Size.Width - picBox_active.Size.Width, panel1.Size.Height - picBox_active.Size.Height);
                        }
                        else
                        {
                            if (b != 1)
                            {
                                for (int j = 0; j < b; j++)
                                {
                                    if (picBox_active.Equals(pictureBoxSecond[j]))
                                    {
                                        j++;
                                    }
                                    try
                                    {
                                        if (15 > Math.Abs(pictureBoxSecond[j].Left - (newlocation.X + picBox_active.Width)))
                                        {
                                            picBox_active.Location = new Point(pictureBoxSecond[j].Left - picBox_active.Width, panel1.Size.Height - picBox_active.Size.Height);
                                            break;
                                        }
                                        else if (15 > Math.Abs(pictureBoxSecond[j].Right - newlocation.X))
                                        {
                                            picBox_active.Location = new Point(pictureBoxSecond[j].Right, panel1.Size.Height - picBox_active.Size.Height);
                                            break;
                                        }
                                        else
                                            picBox_active.Location = new Point(newlocation.X, panel1.Size.Height - picBox_active.Size.Height);
                                    }
                                    catch (System.NullReferenceException)
                                    {
                                    }
                                }
                            }
                            else
                            {
                                picBox_active.Location = new Point(newlocation.X, panel1.Size.Height - picBox_active.Size.Height);
                            }
                        }
                    }
                    else
                    {
                        if (b != 1)
                        {
                            for (int j = 0; j < b; j++)
                            {
                                if (picBox_active.Equals(pictureBoxSecond[j]))
                                {
                                    j++;
                                }
                                try
                                {
                                    if (15 > Math.Abs(pictureBoxSecond[j].Left - (newlocation.X + picBox_active.Width)))
                                    {
                                        for (int i = 0; i < b; i++)
                                        {
                                            if (picBox_active.Equals(pictureBoxSecond[i]))
                                            {
                                                i++;
                                            }
                                            if (15 > Math.Abs(pictureBoxSecond[i].Top - (newlocation.Y + picBox_active.Height)))
                                            {
                                                picBox_active.Location = new Point(pictureBoxSecond[j].Left - picBox_active.Width, pictureBoxSecond[i].Top - picBox_active.Height);
                                                break;
                                            }
                                            else if (15 > Math.Abs(pictureBoxSecond[i].Bottom - newlocation.Y))
                                            {
                                                picBox_active.Location = new Point(pictureBoxSecond[j].Left - picBox_active.Width, pictureBoxSecond[i].Bottom);
                                                break;
                                            }
                                            else
                                                picBox_active.Location = new Point(pictureBoxSecond[j].Left - picBox_active.Width, newlocation.Y);
                                        }
                                        break;
                                    }
                                    else if (15 > Math.Abs(pictureBoxSecond[j].Right - newlocation.X))
                                    {
                                        for (int i = 0; i < b; i++)
                                        {
                                            if (picBox_active.Equals(pictureBoxSecond[i]))
                                            {
                                                i++;
                                            }
                                            if (15 > Math.Abs(pictureBoxSecond[i].Top - (newlocation.Y + picBox_active.Height)))
                                            {
                                                picBox_active.Location = new Point(pictureBoxSecond[j].Right, pictureBoxSecond[i].Top - picBox_active.Height);
                                                break;
                                            }
                                            else if (15 > Math.Abs(pictureBoxSecond[i].Bottom - newlocation.Y))
                                            {
                                                picBox_active.Location = new Point(pictureBoxSecond[j].Right, pictureBoxSecond[i].Bottom);
                                                break;
                                            }
                                            else
                                                picBox_active.Location = new Point(pictureBoxSecond[j].Right, newlocation.Y);
                                        }
                                        break;
                                    }
                                    else if (15 > Math.Abs(pictureBoxSecond[j].Top - (newlocation.Y + picBox_active.Height)))
                                    {
                                        for (int i = 0; i < b; i++)
                                        {
                                            if (picBox_active.Equals(pictureBoxSecond[i]))
                                            {
                                                i++;
                                            }
                                            if (15 > Math.Abs(pictureBoxSecond[i].Left - (newlocation.X + picBox_active.Width)))
                                            {
                                                picBox_active.Location = new Point(pictureBoxSecond[i].Left - picBox_active.Width, pictureBoxSecond[j].Top - picBox_active.Height);
                                                break;
                                            }
                                            else if (15 > Math.Abs(pictureBoxSecond[i].Right - newlocation.X))
                                            {
                                                picBox_active.Location = new Point(pictureBoxSecond[i].Right, pictureBoxSecond[j].Top - picBox_active.Height);
                                                break;
                                            }
                                            else
                                                picBox_active.Location = new Point(newlocation.X, pictureBoxSecond[j].Top - picBox_active.Height);
                                        }
                                        break;
                                    }
                                    else if (15 > Math.Abs(pictureBoxSecond[j].Bottom - newlocation.Y))
                                    {
                                        for (int i = 0; i < b; i++)
                                        {
                                            if (picBox_active.Equals(pictureBoxSecond[i]))
                                            {
                                                i++;
                                            }
                                            if (15 > Math.Abs(pictureBoxSecond[i].Left - (newlocation.X + picBox_active.Width)))
                                            {
                                                picBox_active.Location = new Point(pictureBoxSecond[i].Left - picBox_active.Width, pictureBoxSecond[j].Bottom);
                                                break;
                                            }
                                            else if (15 > Math.Abs(pictureBoxSecond[i].Right - newlocation.X))
                                            {
                                                picBox_active.Location = new Point(pictureBoxSecond[i].Right, pictureBoxSecond[j].Bottom);
                                                break;
                                            }
                                            else
                                                picBox_active.Location = new Point(newlocation.X, pictureBoxSecond[j].Bottom);
                                        }

                                        break;
                                    }
                                    else
                                        picBox_active.Location = newlocation;
                                }
                                catch (System.NullReferenceException)
                                {
                                }
                            }
                        }
                        else
                        {
                            picBox_active.Location = newlocation;
                        }
                    }
                }
                else
                {
                    if (newlocation.Y < 15)
                    {
                        if (newlocation.X < 15)
                        {
                            picBox_active.Location = new Point(0, 0);
                        }
                        else if (15 > panel1.Size.Width - (newlocation.X + picBox_active.Size.Width))
                        {
                            picBox_active.Location = new Point(panel1.Size.Width - picBox_active.Size.Width, 0);
                        }
                        else
                        {
                            if (a != 1)
                            {
                                for (int j = 0; j < a; j++)
                                {
                                    if (picBox_active.Equals(pictureBox[j]))
                                    {
                                        j++;
                                    }
                                    try
                                    {
                                        if (15 > Math.Abs(pictureBox[j].Left - (newlocation.X + picBox_active.Width)))
                                        {
                                            picBox_active.Location = new Point(pictureBox[j].Left - picBox_active.Width, 0);
                                            break;
                                        }
                                        else if (15 > Math.Abs(pictureBox[j].Right - newlocation.X))
                                        {
                                            picBox_active.Location = new Point(pictureBox[j].Right, 0);
                                            break;
                                        }
                                        else
                                        {
                                            picBox_active.Location = new Point(newlocation.X, 0);
                                        }
                                    }
                                    catch (System.NullReferenceException)
                                    {
                                    }
                                }
                            }
                            else
                            {
                                picBox_active.Location = new Point(newlocation.X, 0);
                            }
                        }
                    }
                    else if (newlocation.X < 15)
                    {
                        if (newlocation.Y < 15)
                        {
                            picBox_active.Location = new Point(0, 0);
                        }
                        else if (15 > panel1.Size.Height - (newlocation.Y + picBox_active.Size.Height))
                        {
                            picBox_active.Location = new Point(0, panel1.Size.Height - picBox_active.Size.Height);
                        }
                        else
                        {
                            if (a != 1)
                            {
                                for (int j = 0; j < a; j++)
                                {
                                    if (picBox_active.Equals(pictureBox[j]))
                                    {
                                        j++;
                                    }
                                    try
                                    {
                                        if (15 > Math.Abs(pictureBox[j].Top - (newlocation.Y + picBox_active.Height)))
                                        {
                                            picBox_active.Location = new Point(0, pictureBox[j].Top - picBox_active.Size.Height);
                                            break;
                                        }

                                        else if (15 > Math.Abs(pictureBox[j].Bottom - newlocation.Y))
                                        {
                                            picBox_active.Location = new Point(0, pictureBox[j].Bottom);
                                            break;
                                        }
                                        else
                                            picBox_active.Location = new Point(0, newlocation.Y);
                                    }
                                    catch (System.NullReferenceException)
                                    {
                                    }
                                }
                            }
                            else
                            {
                                picBox_active.Location = new Point(0, newlocation.Y);
                            }
                        }
                    }
                    else if (15 > panel1.Size.Width - (newlocation.X + picBox_active.Size.Width))
                    {
                        if (newlocation.Y < 15)
                        {
                            picBox_active.Location = new Point(panel1.Size.Width - picBox_active.Size.Width, 0);
                        }
                        else if (15 > panel1.Size.Height - (newlocation.Y + picBox_active.Size.Height))
                        {
                            picBox_active.Location = new Point(panel1.Size.Width - picBox_active.Size.Width, panel1.Size.Height - picBox_active.Size.Height);
                        }
                        else
                        {
                            if (a != 1)
                            {
                                for (int j = 0; j < a; j++)
                                {
                                    if (picBox_active.Equals(pictureBox[j]))
                                    {
                                        j++;
                                    }
                                    try
                                    {
                                        if (15 > Math.Abs(pictureBox[j].Top - (newlocation.Y + picBox_active.Height)))
                                        {
                                            picBox_active.Location = new Point(panel1.Size.Width - picBox_active.Size.Width, pictureBox[j].Top - picBox_active.Size.Height);
                                            break;
                                        }

                                        else if (15 > Math.Abs(pictureBox[j].Bottom - newlocation.Y))
                                        {
                                            picBox_active.Location = new Point(panel1.Size.Width - picBox_active.Size.Width, pictureBox[j].Bottom);
                                            break;
                                        }
                                        else
                                            picBox_active.Location = new Point(panel1.Size.Width - picBox_active.Size.Width, newlocation.Y);
                                    }
                                    catch (System.NullReferenceException)
                                    {
                                    }
                                }
                            }
                            else
                            {
                                picBox_active.Location = new Point(panel1.Size.Width - picBox_active.Size.Width, newlocation.Y);
                            }
                        }
                    }
                    else if (15 > panel1.Size.Height - (newlocation.Y + picBox_active.Size.Height))
                    {
                        if (newlocation.X < 15)
                        {
                            picBox_active.Location = new Point(0, panel1.Size.Height - picBox_active.Size.Height);
                        }
                        else if (15 > panel1.Size.Width - (newlocation.X + picBox_active.Size.Width))
                        {
                            picBox_active.Location = new Point(panel1.Size.Width - picBox_active.Size.Width, panel1.Size.Height - picBox_active.Size.Height);
                        }
                        else
                        {
                            if (a != 1)
                            {
                                for (int j = 0; j < a; j++)
                                {
                                    if (picBox_active.Equals(pictureBox[j]))
                                    {
                                        j++;
                                    }
                                    try
                                    {
                                        if (15 > Math.Abs(pictureBox[j].Left - (newlocation.X + picBox_active.Width)))
                                        {
                                            picBox_active.Location = new Point(pictureBox[j].Left - picBox_active.Width, panel1.Size.Height - picBox_active.Size.Height);
                                            break;
                                        }
                                        else if (15 > Math.Abs(pictureBox[j].Right - newlocation.X))
                                        {
                                            picBox_active.Location = new Point(pictureBox[j].Right, panel1.Size.Height - picBox_active.Size.Height);
                                            break;
                                        }
                                        else
                                            picBox_active.Location = new Point(newlocation.X, panel1.Size.Height - picBox_active.Size.Height);
                                    }
                                    catch (System.NullReferenceException)
                                    {
                                    }
                                }
                            }
                            else
                            {
                                picBox_active.Location = new Point(newlocation.X, panel1.Size.Height - picBox_active.Size.Height);
                            }
                        }
                    }
                    else
                    {
                        if (a != 1)
                        {
                            for (int j = 0; j < a; j++)
                            {
                                if (picBox_active.Equals(pictureBox[j]))
                                {
                                    j++;
                                }
                                try
                                {
                                    if (15 > Math.Abs(pictureBox[j].Left - (newlocation.X + picBox_active.Width)))
                                    {
                                        for (int i = 0; i < a; i++)
                                        {
                                            if (picBox_active.Equals(pictureBox[i]))
                                            {
                                                i++;
                                            }
                                            if (15 > Math.Abs(pictureBox[i].Top - (newlocation.Y + picBox_active.Height)))
                                            {
                                                picBox_active.Location = new Point(pictureBox[j].Left - picBox_active.Width, pictureBox[i].Top - picBox_active.Height);
                                                break;
                                            }
                                            else if (15 > Math.Abs(pictureBox[i].Bottom - newlocation.Y))
                                            {
                                                picBox_active.Location = new Point(pictureBox[j].Left - picBox_active.Width, pictureBox[i].Bottom);
                                                break;
                                            }
                                            else
                                                picBox_active.Location = new Point(pictureBox[j].Left - picBox_active.Width, newlocation.Y);
                                        }
                                        break;
                                    }
                                    else if (15 > Math.Abs(pictureBox[j].Right - newlocation.X))
                                    {
                                        for (int i = 0; i < a; i++)
                                        {
                                            if (picBox_active.Equals(pictureBox[i]))
                                            {
                                                i++;
                                            }
                                            if (15 > Math.Abs(pictureBox[i].Top - (newlocation.Y + picBox_active.Height)))
                                            {
                                                picBox_active.Location = new Point(pictureBox[j].Right, pictureBox[i].Top - picBox_active.Height);
                                                break;
                                            }
                                            else if (15 > Math.Abs(pictureBox[i].Bottom - newlocation.Y))
                                            {
                                                picBox_active.Location = new Point(pictureBox[j].Right, pictureBox[i].Bottom);
                                                break;
                                            }
                                            else
                                                picBox_active.Location = new Point(pictureBox[j].Right, newlocation.Y);
                                        }
                                        break;
                                    }
                                    else if (15 > Math.Abs(pictureBox[j].Top - (newlocation.Y + picBox_active.Height)))
                                    {
                                        for (int i = 0; i < a; i++)
                                        {
                                            if (picBox_active.Equals(pictureBox[i]))
                                            {
                                                i++;
                                            }
                                            if (15 > Math.Abs(pictureBox[i].Left - (newlocation.X + picBox_active.Width)))
                                            {
                                                picBox_active.Location = new Point(pictureBox[i].Left - picBox_active.Width, pictureBox[j].Top - picBox_active.Height);
                                                break;
                                            }
                                            else if (15 > Math.Abs(pictureBox[i].Right - newlocation.X))
                                            {
                                                picBox_active.Location = new Point(pictureBox[i].Right, pictureBox[j].Top - picBox_active.Height);
                                                break;
                                            }
                                            else
                                                picBox_active.Location = new Point(newlocation.X, pictureBox[j].Top - picBox_active.Height);
                                        }
                                        break;
                                    }
                                    else if (15 > Math.Abs(pictureBox[j].Bottom - newlocation.Y))
                                    {
                                        for (int i = 0; i < a; i++)
                                        {
                                            if (picBox_active.Equals(pictureBox[i]))
                                            {
                                                i++;
                                            }
                                            if (15 > Math.Abs(pictureBox[i].Left - (newlocation.X + picBox_active.Width)))
                                            {
                                                picBox_active.Location = new Point(pictureBox[i].Left - picBox_active.Width, pictureBox[j].Bottom);
                                                break;
                                            }
                                            else if (15 > Math.Abs(pictureBox[i].Right - newlocation.X))
                                            {
                                                picBox_active.Location = new Point(pictureBox[i].Right, pictureBox[j].Bottom);
                                                break;
                                            }
                                            else
                                                picBox_active.Location = new Point(newlocation.X, pictureBox[j].Bottom);
                                        }

                                        break;
                                    }
                                    else
                                        picBox_active.Location = newlocation;
                                }
                                catch (System.NullReferenceException)
                                {
                                }
                            }
                        }
                        else
                        {
                            picBox_active.Location = newlocation;
                        }
                    }
                }

                for (int i = 0; i < pictureBox.Count(s => s != null); i++)
                {
                    if (picBox_active.Equals(pictureBox[i]))
                    {
                        picIndex = i;
                        break;
                    }
                    else
                        picIndex = 99;
                }

                Point p = new Point(picBox_active.Location.X, picBox_active.Location.Y);
                i = p.X;
                j = p.Y;
                try
                {
                    coordinateLabel[picIndex].Text = picIndex + 1 + "\n" + Convert.ToString((int)(i * scaling(0) + (double)(variable_X[picIndex]) / 2)) + " , " + Convert.ToString((int)(j * scaling(1) + (double)(variable_Y[picIndex]) / 2));
                }
                catch (System.NullReferenceException)
                {
                }

                for (int i = 0; i < pictureBoxSecond.Count(s => s != null); i++)
                {
                    if (picBox_active.Equals(pictureBoxSecond[i]))
                    {
                        picIndex2 = i;
                        break;
                    }
                    else
                        picIndex2 = 99;
                }

                if (checkBox1.Checked)
                {
                    try
                    {
                        coordinateLabelSecond[picIndex2].Text = picIndex2 + 1 + "\n" + Convert.ToString((int)(i * scaling(0) + (double)(variable_X_Second[picIndex2]) / 2)) + " , " + Convert.ToString((int)(j * scaling(1) + (double)(variable_Y_Second[picIndex2]) / 2));
                    }
                    catch (System.NullReferenceException)
                    {
                    }
                }
            }
        }

        private void pictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            location = Point.Empty;
        }

        public double scaling(int index)//location scaling
        {
            double[] retVal = new double[2];
            double pv0, pv1, pd0, pd1;
            pv0 = (double)(paletVallues[0]);
            pv1 = (double)(paletVallues[1]);
            pd0 = PanelDimensions(paletVallues[0], paletVallues[1], 0);
            pd1 = PanelDimensions(paletVallues[0], paletVallues[1], 1);
            retVal[0] = pv0 / pd0;
            retVal[1] = pv1 / pd1;
            return retVal[index];
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if (paletVallues[0] == paletVallues[1])
            {
                label31.Visible = true;
                label31.Location = new Point(tabPage6.Size.Width / 2 - label31.Size.Width / 2, pictureBox2.Location.Y - 20);
                tabPage6.Invalidate();
            }
            else
            {
                label31.Visible = false;
                int changedPic_X = pictureBox2.Size.Height;
                int changedPic_Y = pictureBox2.Size.Width;
                pictureBox2.Size = new Size(changedPic_X, changedPic_Y);
                pictureBox2.Image.RotateFlip(RotateFlipType.Rotate90FlipY);
                if (pictureBox2.Size.Width > pictureBox2.Size.Height)
                {
                    pictureBox2.Location = new Point(tabPage6.Size.Width / 2 - pictureBox2.Size.Width / 2, pictureBox2.Location.Y + (pictureBox2.Size.Width - pictureBox2.Size.Height) / 2);
                }
                else
                    pictureBox2.Location = new Point(tabPage6.Size.Width / 2 - pictureBox2.Size.Width / 2, pictureBox2.Location.Y - (pictureBox2.Size.Height - pictureBox2.Size.Width) / 2);
                int changedPallet_X = paletVallues[1];
                int changedPallet_Y = paletVallues[0];
                paletVallues[0] = changedPallet_X;
                paletVallues[1] = changedPallet_Y;
                PaletPanlel();
                lineUp();
                //panel2 control
                if (checkBox1.Checked)
                {
                    PaletPanel2();
                    lineUp();
                }
            }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            int countCheckBox = 0;
            //panel2 control
            if (tabControl1.SelectedTab == tabPage2)
            {
                for (int i = 0; i < b; i++)
                {
                    if (pictureCheckBoxSecond[i].Checked)
                    {
                        countCheckBox++;
                    }
                }
                if (countCheckBox == 0)
                {
                    MessageBox.Show("Lütfen silmek istediğiniz ürünü seçiniz.");
                }
                else
                {
                    PictureBox[] whichPicture = new PictureBox[b];
                    Label[] whichLabel = new Label[b];
                    Button[] whichButton = new Button[b];
                    CheckBox[] whichCheck = new CheckBox[b];
                    int[] deleteIndex = new int[b];
                    int order = 0;
                    int count = 0;
                    for (int i = 0; i < b; i++)
                    {
                        if (pictureCheckBoxSecond[i].Checked)
                        {
                            deleteIndex[i] = i;
                            count++;
                        }
                        else
                            deleteIndex[i] = 100;
                    }
                    int[] sira = new int[count];
                    int f = 0;
                    int g = 0;
                    for (int i = 0; i < b; i++)
                    {
                        if (deleteIndex[i] != 100)
                        {
                            sira[f] = deleteIndex[i];
                            f++;
                        }
                    }
                    for (int j = 0; j < count; j++)
                    {
                        for (int i = 0; i < b; i++)
                        {
                            if (i == sira[j] - g)
                            {
                                pictureBoxSecond[i].Dispose();
                                coordinateLabelSecond[i].Dispose();
                                pictureBoxButtonSecond[i].Dispose();
                                pictureCheckBoxSecond[i].Dispose();
                                order++;
                            }
                            whichPicture[i] = pictureBoxSecond[i + order];
                            whichLabel[i] = coordinateLabelSecond[i + order];
                            whichButton[i] = pictureBoxButtonSecond[i + order];
                            whichCheck[i] = pictureCheckBoxSecond[i + order];
                            pictureBoxSecond[i] = whichPicture[i];
                            coordinateLabelSecond[i] = whichLabel[i];
                            pictureBoxButtonSecond[i] = whichButton[i];
                            pictureCheckBoxSecond[i] = whichCheck[i];
                        }
                        order = 0;
                        g++;
                        b--;
                    }
                    addCounter = a + b;
                    writeCoordinateLabes();
                }
            }
            else
            {
                for (int i = 0; i < a; i++)
                {
                    if (pictureCheckBox[i].Checked)
                    {
                        countCheckBox++;
                    }
                }
                if (countCheckBox == 0)
                {
                    MessageBox.Show("Lütfen silmek istediğiniz ürünü seçiniz.");
                }
                else
                {
                    PictureBox[] whichPicture = new PictureBox[a];
                    Label[] whichLabel = new Label[a];
                    Button[] whichButton = new Button[a];
                    CheckBox[] whichCheck = new CheckBox[a];
                    int[] deleteIndex = new int[a];
                    int order = 0;
                    int count = 0;
                    for (int i = 0; i < a; i++)
                    {
                        if (pictureCheckBox[i].Checked)
                        {
                            deleteIndex[i] = i;
                            count++;
                        }
                        else
                            deleteIndex[i] = 100;
                    }
                    int[] sira = new int[count];
                    int f = 0;
                    int g = 0;
                    for (int i = 0; i < a; i++)
                    {
                        if (deleteIndex[i] != 100)
                        {
                            sira[f] = deleteIndex[i];
                            f++;
                        }
                    }
                    for (int j = 0; j < count; j++)
                    {
                        for (int i = 0; i < a; i++)
                        {
                            if (i == sira[j] - g)
                            {
                                pictureBox[i].Dispose();
                                coordinateLabel[i].Dispose();
                                pictureBoxButton[i].Dispose();
                                pictureCheckBox[i].Dispose();
                                order++;
                            }
                            whichPicture[i] = pictureBox[i + order];
                            whichLabel[i] = coordinateLabel[i + order];
                            whichButton[i] = pictureBoxButton[i + order];
                            whichCheck[i] = pictureCheckBox[i + order];
                            pictureBox[i] = whichPicture[i];
                            coordinateLabel[i] = whichLabel[i];
                            pictureBoxButton[i] = whichButton[i];
                            pictureCheckBox[i] = whichCheck[i];
                        }
                        order = 0;
                        g++;
                        a--;
                    }
                    addCounter = a + b;
                    writeCoordinateLabes();
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (tabControl1.SelectedTab == tabPage2)
                {
                    int countCheckBox = 0;
                    int checkBoxIndex = 0;
                    for (int i = 0; i < a; i++)
                    {
                        if (pictureCheckBoxSecond[i].Checked)
                        {
                            countCheckBox++;
                        }
                    }
                    if (countCheckBox > 1)
                    {
                        MessageBox.Show("Lütfen sadece göndermek istediğiniz ürünü seçiniz. Bu fonksiyon sadece bir ürünü hareket ettirebilir.");
                    }
                    else if (countCheckBox == 0)
                    {
                        MessageBox.Show("Lütfen bir ürün seçiniz");
                    }
                    else
                    {
                        for (int i = 0; i < a; i++)
                        {
                            if (pictureCheckBoxSecond[i].Checked)
                            {
                                checkBoxIndex = i;
                            }
                        }
                        pictureBoxSecond[checkBoxIndex].Location = new Point((int)(Convert.ToInt32(textBox4.Text) / scaling(0)) - pictureBoxSecond[checkBoxIndex].Size.Width / 2, (int)(Convert.ToInt32(textBox5.Text) / scaling(1)) - pictureBoxSecond[checkBoxIndex].Size.Height / 2);
                        writeCoordinateLabes();
                    }
                }
                else
                {
                    int countCheckBox = 0;
                    int checkBoxIndex = 0;
                    for (int i = 0; i < a; i++)
                    {
                        if (pictureCheckBox[i].Checked)
                        {
                            countCheckBox++;
                        }
                    }
                    if (countCheckBox > 1)
                    {
                        MessageBox.Show("Lütfen sadece göndermek istediğiniz ürünü seçiniz. Bu fonksiyon sadece bir ürünü hareket ettirebilir.");
                    }
                    else if (countCheckBox == 0)
                    {
                        MessageBox.Show("Lütfen bir ürün seçiniz");
                    }
                    else
                    {
                        for (int i = 0; i < a; i++)
                        {
                            if (pictureCheckBox[i].Checked)
                            {
                                checkBoxIndex = i;
                            }
                        }
                        pictureBox[checkBoxIndex].Location = new Point((int)(Convert.ToInt32(textBox4.Text) / scaling(0)) - pictureBox[checkBoxIndex].Size.Width / 2, (int)(Convert.ToInt32(textBox5.Text) / scaling(1)) - pictureBox[checkBoxIndex].Size.Height / 2);
                        writeCoordinateLabes();
                    }
                }
            }
            catch (System.FormatException)
            {
                MessageBox.Show("Lütfen girdiğiniz lokasyonları anlamlı birer doğal sayı olarak giriniz.");
            }
        }

        TabPage tabPage2 = new TabPage();
        Panel panel2 = new Panel();
        private void checkBox1_CheckStateChanged(object sender, EventArgs e)
        {
            tabPage2.Text = "Kat - 2";
            tabPage2.BackColor = Color.White;
            tabPage2.Paint += new PaintEventHandler(tabPage2_Paint);

            panel2.BackColor = Color.BurlyWood;

            if (checkBox1.Checked)
            {
                tabControl1.TabPages.Add(tabPage2);
                tabControl1.SelectedTab = tabPage2;
                tabPage2.Controls.Add(panel2);
                panel2.MouseMove += new MouseEventHandler(panel1_MouseMove);
                PaletPanel2();
                try
                {
                    if (a > 0)
                    {
                        if ((productX == Convert.ToInt32(domainUpDown1.Text) && productY == Convert.ToInt32(domainUpDown2.Text) && productZ == Convert.ToInt32(domainUpDown3.Text)) | addCounter == 0)
                        {
                            if (label33.ForeColor == Color.Green | label34.ForeColor == Color.Green)
                            {
                                for (int i = 0; i < numberProductLayer; i++)
                                {
                                    b = i;
                                    variable_X_Second[b] = Convert.ToInt32(domainUpDown1.Text);
                                    variable_Y_Second[b] = Convert.ToInt32(domainUpDown2.Text);
                                    variable_Z_Second[b] = Convert.ToInt32(domainUpDown3.Text);
                                    pictureBoxSecond[b] = new PictureBox();
                                    pictureBoxSecond[b].Name = "pictureBox" + b;
                                    pictureBoxSecond[b].Size = new System.Drawing.Size((int)(Math.Round(pictureBoxDimensions(Convert.ToInt32(domainUpDown1.Text), Convert.ToInt32(domainUpDown2.Text), 0), 0)), (int)(Math.Round(pictureBoxDimensions(Convert.ToInt32(domainUpDown1.Text), Convert.ToInt32(domainUpDown2.Text), 1), 0)));

                                    if (b == 0)
                                    {
                                        pictureBoxSecond[b].Location = new System.Drawing.Point(b * (int)(Math.Round(pictureBoxDimensions(variable_X_Second[b], variable_Y_Second[b], 0), 0)), 0);
                                    }
                                    else
                                    {
                                        if (pictureBoxSecond[b].Size.Width <= panel2.Size.Width - pictureBoxSecond[b - 1].Location.X - pictureBoxSecond[b - 1].Size.Width)
                                        {
                                            pictureBoxSecond[b].Location = new System.Drawing.Point(pictureBoxSecond[b - 1].Location.X + pictureBoxSecond[b - 1].Size.Width, rowSecond);
                                        }
                                        else
                                        {
                                            columnSecond = 0;
                                            rowSecond = pictureBoxSecond[b - 1].Location.Y + pictureBoxSecond[b - 1].Size.Height;
                                            pictureBoxSecond[b].Location = new System.Drawing.Point(columnSecond, rowSecond);
                                        }
                                    }
                                    string path = "";
                                    if (label33.ForeColor == Color.Green)
                                    {
                                        path = Application.StartupPath + @"\images\koliiçi.jpg";
                                    }
                                    if (label34.ForeColor == Color.Green)
                                    {
                                        path = Application.StartupPath + @"\images\çuval.jpg";
                                    }
                                    Bitmap image = new Bitmap(path, true);
                                    pictureBoxSecond[b].Image = image;
                                    pictureBoxSecond[b].SizeMode = PictureBoxSizeMode.StretchImage;
                                    panel2.Controls.Add(pictureBoxSecond[b]);
                                    pictureBoxSecond[b].MouseDown += new MouseEventHandler(pictureBox_MouseDown);
                                    pictureBoxSecond[b].MouseMove += new MouseEventHandler(pictureBox_MouseMove);
                                    pictureBoxSecond[b].MouseUp += new MouseEventHandler(pictureBox_MouseUp);

                                    coordinateLabelSecond[b] = new Label();
                                    coordinateLabelSecond[b].Size = new Size(60, 26);
                                    coordinateLabelSecond[b].BackColor = Color.Transparent;
                                    coordinateLabelSecond[b].ForeColor = Color.White;
                                    coordinateLabelSecond[b].Location = new System.Drawing.Point(0, 0);
                                    pictureBoxSecond[b].Controls.Add(coordinateLabelSecond[b]);

                                    pictureBoxButtonSecond[b] = new Button();
                                    pictureBoxButtonSecond[b].Size = new Size(30, 30);
                                    pictureBoxButtonSecond[b].BackColor = Color.CadetBlue;
                                    path = Application.StartupPath + @"\images\döndürme ikonu.jpg";
                                    image = new Bitmap(path, true);
                                    image = new Bitmap(image, new Size(19, 19));
                                    pictureBoxButtonSecond[b].Image = image;
                                    pictureBoxButtonSecond[b].ImageAlign = ContentAlignment.MiddleCenter;
                                    pictureBoxButtonSecond[b].Location = new System.Drawing.Point(0, 27);
                                    pictureBoxSecond[b].Controls.Add(pictureBoxButtonSecond[b]);
                                    pictureBoxButtonSecond[b].Click += new EventHandler(pictureBoxButton_Clicked);

                                    pictureCheckBoxSecond[b] = new CheckBox();
                                    pictureCheckBoxSecond[b].BackColor = Color.Transparent;
                                    if (pictureBoxSecond[b].Width > pictureBoxSecond[b].Height)
                                    {
                                        pictureCheckBoxSecond[b].Location = new System.Drawing.Point(pictureBoxSecond[b].Size.Width - 38, 13);
                                    }
                                    else
                                    {
                                        pictureCheckBoxSecond[b].Location = new System.Drawing.Point(pictureBoxSecond[b].Size.Width - 29, 22);
                                    }
                                    pictureBoxSecond[b].Controls.Add(pictureCheckBoxSecond[b]);
                                }
                                b = numberProductLayer;
                                writeCoordinateLabes();
                            }
                            else
                            {
                                MessageBox.Show("Lütfen ürün boyutlarını, ağırlığını ve adedini anlamlı doğal sayılar olarak giriniz.");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Bir paletteki ürünlerin boyutları farklı olamaz. Lütfen ürün boyutlarını kontrol edin.");
                        }
                    }
                }
                catch (System.FormatException)
                {
                    MessageBox.Show("Lütfen ürün boyutlarını, ağırlığını ve adedini anlamlı doğal sayılar olarak giriniz.");
                }
            }
            else
            {
                for (int i = 0; i < pictureBoxSecond.Count(s => s != null); i++)
                {
                    pictureBoxSecond[i].Dispose();
                    coordinateLabelSecond[i].Dispose();
                    pictureBoxButtonSecond[i].Dispose();
                    pictureCheckBoxSecond[i].Dispose();
                }
                b = 0;
                addCounter = a + b;
                rowSecond = 0;
                columnSecond = 0;
                tabControl1.Controls.Remove(tabPage2);
            }
        }

        int numberProductLayer;
        private void button4_Click(object sender, EventArgs e)
        {
            a = 0;
            row = 0;
            column = 0;
            addCounter = 0;

            try
            {
                if ((productX == Convert.ToInt32(domainUpDown1.Text) && productY == Convert.ToInt32(domainUpDown2.Text) && productZ == Convert.ToInt32(domainUpDown3.Text) && productWeight == Convert.ToInt32(domainUpDown4.Text)) | addCounter == 0)
                {
                    if (label33.ForeColor == Color.Green | label34.ForeColor == Color.Green)
                    {
                        numberProductLayer = Convert.ToInt32(domainUpDown5.Text);
                        for (int i = 0; i < pictureBox.Count(s => s != null); i++)
                        {
                            pictureBox[i].Dispose();
                            coordinateLabel[i].Dispose();
                            pictureBoxButton[i].Dispose();
                            pictureCheckBox[i].Dispose();
                        }
                        for (int i = 0; i < numberProductLayer; i++)
                        {
                            a = i;
                            variable_X[a] = Convert.ToInt32(domainUpDown1.Text);
                            variable_Y[a] = Convert.ToInt32(domainUpDown2.Text);
                            variable_Z[a] = Convert.ToInt32(domainUpDown3.Text);

                            if (addCounter == 0)
                            {
                                productX = variable_X[0];
                                productY = variable_Y[0];
                                productZ = variable_Z[0];
                                productWeight = Convert.ToInt32(domainUpDown4.Text);
                            }

                            pictureBox[a] = new PictureBox();
                            pictureBox[a].Name = "pictureBox" + a;
                            pictureBox[a].Size = new System.Drawing.Size((int)(Math.Round(pictureBoxDimensions(Convert.ToInt32(domainUpDown1.Text), Convert.ToInt32(domainUpDown2.Text), 0), 0)), (int)(Math.Round(pictureBoxDimensions(Convert.ToInt32(domainUpDown1.Text), Convert.ToInt32(domainUpDown2.Text), 1), 0)));

                            if (a == 0)
                            {
                                pictureBox[a].Location = new System.Drawing.Point(a * (int)(Math.Round(pictureBoxDimensions(variable_X[a], variable_Y[a], 0), 0)), 0);
                            }
                            else
                            {
                                if (pictureBox[a].Size.Width <= panel1.Size.Width - pictureBox[a - 1].Location.X - pictureBox[a - 1].Size.Width)
                                {
                                    pictureBox[a].Location = new System.Drawing.Point(pictureBox[a - 1].Location.X + pictureBox[a - 1].Size.Width, row);
                                }
                                else
                                {
                                    column = 0;
                                    row = pictureBox[a - 1].Location.Y + pictureBox[a - 1].Size.Height;
                                    pictureBox[a].Location = new System.Drawing.Point(column, row);
                                }
                            }
                            string path = "";
                            if (label33.ForeColor == Color.Green)
                            {
                                path = Application.StartupPath + @"\images\koliiçi.jpg";
                            }
                            if (label34.ForeColor == Color.Green)
                            {
                                path = Application.StartupPath + @"\images\çuval.jpg";
                            }
                            Bitmap image = new Bitmap(path, true);
                            pictureBox[a].Image = image;
                            pictureBox[a].SizeMode = PictureBoxSizeMode.StretchImage;
                            panel1.Controls.Add(pictureBox[a]);
                            pictureBox[a].MouseDown += new MouseEventHandler(pictureBox_MouseDown);
                            pictureBox[a].MouseMove += new MouseEventHandler(pictureBox_MouseMove);
                            pictureBox[a].MouseUp += new MouseEventHandler(pictureBox_MouseUp);

                            coordinateLabel[a] = new Label();
                            coordinateLabel[a].Size = new Size(60, 26);
                            coordinateLabel[a].BackColor = Color.Transparent;
                            coordinateLabel[a].ForeColor = Color.White;
                            coordinateLabel[a].Location = new System.Drawing.Point(0, 0);
                            pictureBox[a].Controls.Add(coordinateLabel[a]);

                            pictureBoxButton[a] = new Button();
                            pictureBoxButton[a].Size = new Size(30, 30);
                            pictureBoxButton[a].BackColor = Color.CadetBlue;
                            path = Application.StartupPath + @"\images\döndürme ikonu.jpg";
                            image = new Bitmap(path, true);
                            image = new Bitmap(image, new Size(19, 19));
                            pictureBoxButton[a].Image = image;
                            pictureBoxButton[a].ImageAlign = ContentAlignment.MiddleCenter;
                            pictureBoxButton[a].Location = new System.Drawing.Point(0, 27);
                            pictureBox[a].Controls.Add(pictureBoxButton[a]);
                            pictureBoxButton[a].Click += new EventHandler(pictureBoxButton_Clicked);

                            pictureCheckBox[a] = new CheckBox();
                            pictureCheckBox[a].BackColor = Color.Transparent;
                            if (pictureBox[a].Width > pictureBox[a].Height)
                            {
                                pictureCheckBox[a].Location = new System.Drawing.Point(pictureBox[a].Size.Width - 38, 13);
                            }
                            else
                            {
                                pictureCheckBox[a].Location = new System.Drawing.Point(pictureBox[a].Size.Width - 29, 22);
                            }
                            pictureBox[a].Controls.Add(pictureCheckBox[a]);
                        }
                        a = numberProductLayer;
                        writeCoordinateLabes();
                        //panel2 control
                        b = 0;
                        rowSecond = 0;
                        columnSecond = 0;
                        addCounter = a + b;
                        if (checkBox1.Checked)
                        {
                            for (int i = 0; i < pictureBoxSecond.Count(s => s != null); i++)
                            {
                                pictureBoxSecond[i].Dispose();
                                coordinateLabelSecond[i].Dispose();
                                pictureBoxButtonSecond[i].Dispose();
                                pictureCheckBoxSecond[i].Dispose();
                            }
                            for (int i = 0; i < numberProductLayer; i++)
                            {
                                b = i;
                                variable_X_Second[b] = Convert.ToInt32(domainUpDown1.Text);
                                variable_Y_Second[b] = Convert.ToInt32(domainUpDown2.Text);
                                variable_Z_Second[b] = Convert.ToInt32(domainUpDown3.Text);

                                if (addCounter == 0)
                                {
                                    productX = variable_X_Second[0];
                                    productY = variable_Y_Second[0];
                                    productZ = variable_Z_Second[0];
                                    productWeight = Convert.ToInt32(domainUpDown4.Text);
                                }

                                pictureBoxSecond[b] = new PictureBox();
                                pictureBoxSecond[b].Name = "pictureBox" + b;
                                pictureBoxSecond[b].Size = new System.Drawing.Size((int)(Math.Round(pictureBoxDimensions(Convert.ToInt32(domainUpDown1.Text), Convert.ToInt32(domainUpDown2.Text), 0), 0)), (int)(Math.Round(pictureBoxDimensions(Convert.ToInt32(domainUpDown1.Text), Convert.ToInt32(domainUpDown2.Text), 1), 0)));

                                if (b == 0)
                                {
                                    pictureBoxSecond[b].Location = new System.Drawing.Point(b * (int)(Math.Round(pictureBoxDimensions(variable_X_Second[b], variable_Y_Second[b], 0), 0)), 0);
                                }
                                else
                                {
                                    if (pictureBoxSecond[b].Size.Width <= panel2.Size.Width - pictureBoxSecond[b - 1].Location.X - pictureBoxSecond[b - 1].Size.Width)
                                    {
                                        pictureBoxSecond[b].Location = new System.Drawing.Point(pictureBoxSecond[b - 1].Location.X + pictureBoxSecond[b - 1].Size.Width, rowSecond);
                                    }
                                    else
                                    {
                                        columnSecond = 0;
                                        rowSecond = pictureBoxSecond[b - 1].Location.Y + pictureBoxSecond[b - 1].Size.Height;
                                        pictureBoxSecond[b].Location = new System.Drawing.Point(columnSecond, rowSecond);
                                    }
                                }
                                string path = "";
                                if (label33.ForeColor == Color.Green)
                                {
                                    path = Application.StartupPath + @"\images\koliiçi.jpg";
                                }
                                if (label34.ForeColor == Color.Green)
                                {
                                    path = Application.StartupPath + @"\images\çuval.jpg";
                                }
                                Bitmap image = new Bitmap(path, true);
                                pictureBoxSecond[b].Image = image;
                                pictureBoxSecond[b].SizeMode = PictureBoxSizeMode.StretchImage;
                                panel2.Controls.Add(pictureBoxSecond[b]);
                                pictureBoxSecond[b].MouseDown += new MouseEventHandler(pictureBox_MouseDown);
                                pictureBoxSecond[b].MouseMove += new MouseEventHandler(pictureBox_MouseMove);
                                pictureBoxSecond[b].MouseUp += new MouseEventHandler(pictureBox_MouseUp);

                                coordinateLabelSecond[b] = new Label();
                                coordinateLabelSecond[b].Size = new Size(60, 26);
                                coordinateLabelSecond[b].BackColor = Color.Transparent;
                                coordinateLabelSecond[b].ForeColor = Color.White;
                                coordinateLabelSecond[b].Location = new System.Drawing.Point(0, 0);
                                pictureBoxSecond[b].Controls.Add(coordinateLabelSecond[b]);

                                pictureBoxButtonSecond[b] = new Button();
                                pictureBoxButtonSecond[b].Size = new Size(30, 30);
                                pictureBoxButtonSecond[b].BackColor = Color.CadetBlue;
                                path = Application.StartupPath + @"\images\döndürme ikonu.jpg";
                                image = new Bitmap(path, true);
                                image = new Bitmap(image, new Size(19, 19));
                                pictureBoxButtonSecond[b].Image = image;
                                pictureBoxButtonSecond[b].ImageAlign = ContentAlignment.MiddleCenter;
                                pictureBoxButtonSecond[b].Location = new System.Drawing.Point(0, 27);
                                pictureBoxSecond[b].Controls.Add(pictureBoxButtonSecond[b]);
                                pictureBoxButtonSecond[b].Click += new EventHandler(pictureBoxButton_Clicked);

                                pictureCheckBoxSecond[b] = new CheckBox();
                                pictureCheckBoxSecond[b].BackColor = Color.Transparent;
                                if (pictureBoxSecond[b].Width > pictureBoxSecond[b].Height)
                                {
                                    pictureCheckBoxSecond[b].Location = new System.Drawing.Point(pictureBoxSecond[b].Size.Width - 38, 13);
                                }
                                else
                                {
                                    pictureCheckBoxSecond[b].Location = new System.Drawing.Point(pictureBoxSecond[b].Size.Width - 29, 22);
                                }
                                pictureBoxSecond[b].Controls.Add(pictureCheckBoxSecond[b]);
                            }
                            b = numberProductLayer;
                            addCounter = a + b;
                            writeCoordinateLabes();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Lütfen yükleme tipini seçin. (Koli yada Çuval)");
                    }
                }
            }
            catch (System.FormatException)
            {
                MessageBox.Show("Lütfen ürün boyutlarını, ağırlığını ve adedini anlamlı doğal sayılar olarak giriniz.");
            }


        }

        //public PlcChecker plcChekcer { get; private set; }
        //private async void toolStripButton2_Click(object sender, EventArgs e)
        //{
        //    plcChekcer = new PlcChecker();
        //    await plcChekcer.Connect("192.168.0.1", 502);
        //    bool result = await plcChekcer.Check();
        //    if (result)
        //    {
        //        //do something   
        //    }

        //}
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            try
            {
                TcpClient client = new TcpClient();
                ModbusIpMaster master = ModbusIpMaster.CreateIp(client);
                client.Connect("192.168.0.1", 502);
                int k = 0;
                ushort[] offsets = new ushort[4];
                ushort[] orderOffset = new ushort[4];
                ushort[] totalOffsets = new ushort[3 * a];
                for (int j = 0; j < a; j++)
                {
                    string[] numbers = Regex.Split(coordinateLabel[j].Text, @"\D+");
                    int index = 0;
                    foreach (string value in numbers)
                    {
                        if (!string.IsNullOrEmpty(value))
                        {
                            int i = int.Parse(value);
                            offsets[index] = (ushort)(i);
                            index++;
                        }
                    }
                    for (int i = 0; i < 3; i++)
                    {
                        if (i == 0 | i == 1)
                        {
                            orderOffset[i] = offsets[i + 1];
                        }
                        else
                        {
                            orderOffset[i] = (ushort)(variable_Z[j]);
                        }
                    }
                    for (int i = 0; i < 3; i++)
                    {
                        if (j == 0)
                        {
                            totalOffsets[i] = orderOffset[i];
                        }
                        else
                        {
                            totalOffsets[(j * 3) + i] = orderOffset[i];
                        }
                    }
                }
                master.WriteMultipleRegisters(0, totalOffsets);
                client.Close();
            }
            catch (System.Net.Sockets.SocketException)
            {
                MessageBox.Show("PLC bağlı değil lütfen bağladıktan sonra tekrar deneyin");
            }
            //ushort[] offsets = new ushort[4];
            //for (int j = 0; j < a; j++)
            //{
            //    string[] numbers = Regex.Split(coordinateLabel[j].Text, @"\D+");
            //    int index = 0;
            //    foreach (string value in numbers)
            //    {
            //        if (!string.IsNullOrEmpty(value))
            //        {
            //            int i = int.Parse(value);
            //            offsets[index] = (ushort)(i);
            //            index++;
            //        }
            //    }
            //    master.WriteSingleRegister(0, offsets[1]);
            //    master.WriteSingleRegister(1, offsets[2]);
            //    master.WriteSingleRegister(2, (ushort)(variable_Z[j]));
            //    while (true)
            //    {
            //        Thread.Sleep(1000);
            //        offsets = master.ReadHoldingRegisters(0, 4);
            //        if (offsets[3] == 0)
            //        {
            //            master.WriteSingleRegister(0, 0);
            //            master.WriteSingleRegister(1, 0);
            //            master.WriteSingleRegister(2, 0);
            //            while (true)
            //            {
            //                Thread.Sleep(1000);
            //                offsets = master.ReadHoldingRegisters(0, 4);
            //                if (offsets[3] == 1)
            //                {
            //                    break;
            //                }
            //            }
            //            break;
            //        }
            //    }
            //}
            //client.Close();
        }

        private void panel3_Click(object sender, EventArgs e)
        {
            label33.Text = "Koli tipi palet seçimi kullanılacak.";
            label33.ForeColor = Color.Green;
            label34.Text = "Çuval tipi palet seçimi için tıklayınız.";
            label34.ForeColor = Color.Black;
            for (int i = 0; i < pictureBox.Count(s => s != null); i++)
            {
                string path = "";
                if (label33.ForeColor == Color.Green)
                {
                    path = Application.StartupPath + @"\images\koliiçi.jpg";
                }
                if (label34.ForeColor == Color.Green)
                {
                    path = Application.StartupPath + @"\images\çuval.jpg";
                }
                Bitmap image = new Bitmap(path, true);
                pictureBox[i].Image = image;
            }
            //panel2 control
            if (checkBox1.Checked)
            {
                for (int i = 0; i < pictureBoxSecond.Count(s => s != null); i++)
                {
                    string path = "";
                    if (label33.ForeColor == Color.Green)
                    {
                        path = Application.StartupPath + @"\images\koliiçi.jpg";
                    }
                    if (label34.ForeColor == Color.Green)
                    {
                        path = Application.StartupPath + @"\images\çuval.jpg";
                    }
                    Bitmap image = new Bitmap(path, true);
                    pictureBoxSecond[i].Image = image;
                }
            }
        }

        private void panel4_Click(object sender, EventArgs e)
        {
            label34.Text = "Çuval tipi palet seçimi kullanılacak.";
            label34.ForeColor = Color.Green;
            label33.Text = "Koli tipi palet seçimi için tıklayınız.";
            label33.ForeColor = Color.Black;
            for (int i = 0; i < pictureBox.Count(s => s != null); i++)
            {
                string path = "";
                if (label33.ForeColor == Color.Green)
                {
                    path = Application.StartupPath + @"\images\koliiçi.jpg";
                }
                if (label34.ForeColor == Color.Green)
                {
                    path = Application.StartupPath + @"\images\çuval.jpg";
                }
                Bitmap image = new Bitmap(path, true);
                pictureBox[i].Image = image;
            }
            //panel2 control
            if (checkBox1.Checked)
            {
                for (int i = 0; i < pictureBoxSecond.Count(s => s != null); i++)
                {
                    string path = "";
                    if (label33.ForeColor == Color.Green)
                    {
                        path = Application.StartupPath + @"\images\koliiçi.jpg";
                    }
                    if (label34.ForeColor == Color.Green)
                    {
                        path = Application.StartupPath + @"\images\çuval.jpg";
                    }
                    Bitmap image = new Bitmap(path, true);
                    pictureBoxSecond[i].Image = image;
                }
            }
        }

        private void tabControl2_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (textBox3.Text == "")
            //{
            //    MessageBox.Show("Program adını girmediniz.\nLütfen bir program adı giriniz.");
            //    tabControl2.SelectedTab = tabPage3;
            //}
            //else if (comboBox1.Text == "")
            //{
            //    MessageBox.Show("Palet boyutlarını seçmediniz.\nLütfen bir palet boyutu seçiniz.");
            //    tabControl2.SelectedTab = tabPage4;
            //}
            //else if (label33.ForeColor != Color.Green && label34.ForeColor != Color.Green)
            //{
            //    MessageBox.Show("Yükleme tipini seçmediniz.\nLütfen yükleme tipini seçiniz.");
            //    tabControl2.SelectedTab = tabPage6;
            //}
            //else if (domainUpDown1.Text == "" | domainUpDown2.Text == "" | domainUpDown3.Text == "" | domainUpDown4.Text == "" | domainUpDown5.Text == "")
            //{
            //    MessageBox.Show("Ürün boyutlarını boş bırakamazsınız.\nLütfen ürün boyutlarını giriniz.");
            //    tabControl2.SelectedTab = tabPage7;
            //}
            //if (tabControl2.SelectedTab == tabPage4)
            //{
            //    if (textBox3.Text == "")
            //    {
            //        MessageBox.Show("Program adını girmediniz.\nLütfen bir program adı giriniz.");
            //        tabControl2.SelectedTab = tabPage3;
            //    }
            //}
            //if (tabControl2.SelectedTab == tabPage5)
            //{
            //    if (comboBox1.Text == "")
            //    {
            //        MessageBox.Show("Palet boyutlarını seçmediniz.\nLütfen bir palet boyutu seçiniz.");
            //        tabControl2.SelectedTab = tabPage4;
            //    }
            //}
            //if (tabControl2.SelectedTab == tabPage7)
            //{
            //    if (label33.ForeColor != Color.Green && label34.ForeColor != Color.Green)
            //    {
            //        MessageBox.Show("Yükleme tipini seçmediniz.\nLütfen yükleme tipini seçiniz.");
            //        tabControl2.SelectedTab = tabPage6;
            //    }
            //}
            //if (tabControl2.SelectedTab == tabPage8)
            //{
            //    if (domainUpDown1.Text == "" | domainUpDown2.Text == "" | domainUpDown3.Text == "" | domainUpDown4.Text == "" | domainUpDown5.Text == "")
            //    {
            //        MessageBox.Show("Ürün boyutlarını boş bırakamazsınız.\nLütfen ürün boyutlarını giriniz.");
            //        tabControl2.SelectedTab = tabPage7;
            //    }
            //}
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            string directory = Application.StartupPath + @"\recipe datas";
            string fileName = textBox3.Text + ".dat";
            string memoryPath = Path.Combine(directory, fileName);
            StreamWriter sw = File.CreateText(memoryPath);
            sw.WriteLine(textBox3.Text);
            sw.WriteLine(richTextBox1.Text);
            sw.WriteLine(comboBox1.Text);
            if (paletVallues[0] == paletVallues[1])
            {
                sw.WriteLine("equilateral");
            }
            else if (paletVallues[0] < paletVallues[1])
            {
                sw.WriteLine("vertical");
            }
            else if (paletVallues[0] > paletVallues[1])
            {
                sw.WriteLine("horizontal");
            }
            if (label33.ForeColor == Color.Green)
            {
                sw.WriteLine("koli");
            }
            else if (label34.ForeColor == Color.Green)
            {
                sw.WriteLine("çuval");
            }
            else
            {
                sw.WriteLine("");
            }
            sw.WriteLine(productX + ";" + productY + ";" + productZ + ";" + productWeight + ";" + a);
            if (!checkBox1.Checked)
            {
                sw.WriteLine(domainUpDown6.Text + ";" + "same");
                sw.WriteLine(b);
            }
            else
            {
                sw.WriteLine(domainUpDown6.Text + ";" + "different");
                sw.WriteLine(b);
            }
            string[] savedCoordinates = new string[a];
            int horizontalVertical;
            for (int i = 0; i < a; i++)
            {
                string[] numbers = Regex.Split(coordinateLabel[i].Text, @"\D+");
                if (pictureBox[i].Width > pictureBox[i].Height)
                {
                    horizontalVertical = 0;
                }
                else
                {
                    horizontalVertical = 1;
                }
                savedCoordinates[i] = numbers[1] + " " + numbers[2] + " " + horizontalVertical;
            }
            sw.WriteLine(string.Join(";", savedCoordinates));

            //panel2 control
            if (checkBox1.Checked)
            {
                string[] savedCoordinatesSecond = new string[b];
                int horizontalVerticalSecond;
                for (int i = 0; i < b; i++)
                {
                    string[] numbers = Regex.Split(coordinateLabelSecond[i].Text, @"\D+");
                    if (pictureBoxSecond[i].Width > pictureBoxSecond[i].Height)
                    {
                        horizontalVerticalSecond = 0;
                    }
                    else
                    {
                        horizontalVerticalSecond = 1;
                    }
                    savedCoordinatesSecond[i] = numbers[1] + " " + numbers[2] + " " + horizontalVerticalSecond;
                }
                sw.WriteLine(string.Join(";", savedCoordinatesSecond));
            }
            else
            {
                sw.WriteLine("");
            }
            sw.Close();
        }

        private void fileSystemWatcher1_Created(object sender, FileSystemEventArgs e)
        {
            toolStripComboBox1.Items.Add(e.Name);
        }

        private void toolStripComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            TextFieldParser datReader = new TextFieldParser(Application.StartupPath + @"\recipe datas\" + toolStripComboBox1.Text);
            datReader.SetDelimiters(new string[] { ";" });
            datReader.HasFieldsEnclosedInQuotes = true;
            string colFields = datReader.ReadToEnd();
            List<string> lineOrder = new List<string>();

            using (StringReader sr = new StringReader(colFields))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    lineOrder.Add(line);
                }
            }
            if (comboBox1.Items.Contains(lineOrder[2]))
            {
                addCounter = 0;

                textBox3.Text = lineOrder[0];
                richTextBox1.Text = lineOrder[1];
                comboBox1.SelectedItem = lineOrder[2];

                if ((lineOrder[3] == "vertical") && (paletVallues[0] > paletVallues[1]))
                {
                    pictureBox2_Click(pictureBox2, EventArgs.Empty);
                }
                if ((lineOrder[3] == "horizontal") && (paletVallues[0] < paletVallues[1]))
                {
                    pictureBox2_Click(pictureBox2, EventArgs.Empty);
                }
                if (lineOrder[4] == "koli")
                {
                    panel3_Click(panel3, EventArgs.Empty);
                }
                if (lineOrder[4] == "çuval")
                {
                    panel4_Click(panel4, EventArgs.Empty);
                }
                if (lineOrder[4] == "")
                {
                    label33.ForeColor = Color.Black;
                    label34.ForeColor = Color.Black;
                }

                for (int i = 0; i < pictureBox.Count(s => s != null); i++)
                {
                    pictureBox[i].Dispose();
                    coordinateLabel[i].Dispose();
                    pictureBoxButton[i].Dispose();
                    pictureCheckBox[i].Dispose();
                }
                a = 0;
                addCounter = a + b;
                row = 0;
                column = 0;

                for (int i = 0; i < pictureBoxSecond.Count(s => s != null); i++)
                {
                    pictureBoxSecond[i].Dispose();
                    coordinateLabelSecond[i].Dispose();
                    pictureBoxButtonSecond[i].Dispose();
                    pictureCheckBoxSecond[i].Dispose();
                }
                b = 0;
                addCounter = a + b;
                rowSecond = 0;
                columnSecond = 0;

                string[] parser = lineOrder[5].Split(';');
                domainUpDown1.Text = parser[0];
                domainUpDown2.Text = parser[1];
                domainUpDown3.Text = parser[2];
                domainUpDown4.Text = parser[3];
                domainUpDown5.Text = parser[4];
                if (tabControl1.SelectedTab != tabPage1)
                {
                    tabControl1.SelectedTab = tabPage1;
                    for (int i = 0; i < Convert.ToInt32(parser[4]); i++)
                    {
                        toolStripButton1_Click(toolStripButton1, EventArgs.Empty);
                    }
                }
                else
                {
                    for (int i = 0; i < Convert.ToInt32(parser[4]); i++)
                    {
                        toolStripButton1_Click(toolStripButton1, EventArgs.Empty);
                    }
                }

                parser = lineOrder[6].Split(';');
                domainUpDown6.Text = parser[0];
                if (parser[1] == "same" && checkBox1.Checked)
                {
                    checkBox1.CheckState = CheckState.Unchecked;
                }
                else if (parser[1] == "different" && !checkBox1.Checked)
                {
                    checkBox1.CheckState = CheckState.Checked;
                }

                if (checkBox1.Checked)
                {
                    tabControl1.SelectedTab = tabPage2;
                    for (int i = 0; i < Convert.ToInt32(lineOrder[7]); i++)
                    {
                        toolStripButton1_Click(toolStripButton1, EventArgs.Empty);
                    }
                }
                string[] parser2 = new string[3];
                parser = lineOrder[8].Split(';');
                for (int i = 0; i < parser.Length; i++)
                {
                    parser2 = Regex.Split(parser[i], " ");
                    if (parser2[2] == "0" && pictureBox[i].Width < pictureBox[i].Height)
                    {
                        pictureBoxButton_Clicked(pictureBoxButton[i], EventArgs.Empty);
                    }
                    else if (parser2[2] == "1" && pictureBox[i].Width > pictureBox[i].Height)
                    {
                        pictureBoxButton_Clicked(pictureBoxButton[i], EventArgs.Empty);
                    }
                    pictureBox[i].Location = new Point((int)(Convert.ToInt32(parser2[0]) / scaling(0)) - pictureBox[i].Size.Width / 2, (int)(Convert.ToInt32(parser2[1]) / scaling(1)) - pictureBox[i].Size.Height / 2);
                }

                parser = lineOrder[9].Split(';');
                for (int i = 0; i < parser.Length; i++)
                {
                    parser2 = Regex.Split(parser[i], " ");
                    if (parser2[2] == "0" && pictureBoxSecond[i].Width < pictureBoxSecond[i].Height)
                    {
                        pictureBoxButton_Clicked(pictureBoxButtonSecond[i], EventArgs.Empty);
                    }
                    else if (parser2[2] == "1" && pictureBoxSecond[i].Width > pictureBoxSecond[i].Height)
                    {
                        pictureBoxButton_Clicked(pictureBoxButtonSecond[i], EventArgs.Empty);
                    }
                    pictureBoxSecond[i].Location = new Point((int)(Convert.ToInt32(parser2[0]) / scaling(0)) - pictureBoxSecond[i].Size.Width / 2, (int)(Convert.ToInt32(parser2[1]) / scaling(1)) - pictureBoxSecond[i].Size.Height / 2);
                }
                writeCoordinateLabes();
            }
            else
            {
                MessageBox.Show("Seçtiğiniz reçetedeki palet boyutları (" + lineOrder[3] + ") programınızda bulunmamaktadır. Ekledikten sonra bu reçeteyi seçebilirsiniz.");
            }
        }

        private void tabControl1_KeyDown(object sender, KeyEventArgs e)
        {
            //int x = pictureBox[0].Location.X;
            //int y = pictureBox[0].Location.Y;

            //if (e.KeyCode == Keys.Right) x += 1;
            //else if (e.KeyCode == Keys.Left) x -= 1;
            //else if (e.KeyCode == Keys.Up) y -= 1;
            //else if (e.KeyCode == Keys.Down) y += 1;

            //pictureBox[0].Location = new Point(x, y);
        }

        public double PanelDimensions(int x, int y, int index)
        {
            double[] dimentions = new double[2];

            int max_x = 500;
            int max_y = 500;

            if (x > y)
            {
                dimentions[0] = max_x;
                dimentions[1] = (double)(y) * (double)(max_x) / (double)(x);
            }
            if (y > x)
            {
                dimentions[1] = max_y;
                dimentions[0] = (double)(x) * (double)(max_y) / (double)(y);
            }
            if (x == y)
            {
                dimentions[0] = max_x;
                dimentions[1] = max_y;
            }
            return dimentions[index];
        }

        private void tabPage1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = tabPage1.CreateGraphics();
            Pen p = new Pen(Color.Black);

            Point Y1 = new Point(panel1.Location.X / 2, panel1.Location.Y);
            Point Y2 = new Point(panel1.Location.X / 2, panel1.Location.Y + panel1.Size.Height);
            Point X1 = new Point(panel1.Location.X, panel1.Location.Y / 2);
            Point X2 = new Point(panel1.Location.X + panel1.Size.Width, panel1.Location.Y / 2);

            g.DrawLine(p, Y1, Y2);
            g.DrawLine(p, X1, X2);

            for (int i = 0; i < paletVallues[0] / 100 + 1; i++)
            {
                double a;
                a = (X2.X - X1.X) / ((double)(paletVallues[0]) / 100);
                g.DrawLine(p, new Point((int)(X1.X + i * a), X1.Y - 3), new Point((int)(X1.X + i * a), X1.Y + 3));
            }
            for (int i = 0; i < paletVallues[1] / 100 + 1; i++)
            {
                double a;
                a = (Y2.Y - Y1.Y) / ((double)(paletVallues[1]) / 100);
                g.DrawLine(p, new Point(Y1.X - 3, (int)(Y1.Y + i * a)), new Point(Y1.X + 3, (int)(Y1.Y + i * a)));
            }
        }
        private void tabPage2_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = tabPage2.CreateGraphics();
            Pen p = new Pen(Color.Black);

            Point Y1 = new Point(panel2.Location.X / 2, panel2.Location.Y);
            Point Y2 = new Point(panel2.Location.X / 2, panel2.Location.Y + panel2.Size.Height);
            Point X1 = new Point(panel2.Location.X, panel2.Location.Y / 2);
            Point X2 = new Point(panel2.Location.X + panel2.Size.Width, panel2.Location.Y / 2);

            g.DrawLine(p, Y1, Y2);
            g.DrawLine(p, X1, X2);

            for (int i = 0; i < paletVallues[0] / 100 + 1; i++)
            {
                double a;
                a = (X2.X - X1.X) / ((double)(paletVallues[0]) / 100);
                g.DrawLine(p, new Point((int)(X1.X + i * a), X1.Y - 3), new Point((int)(X1.X + i * a), X1.Y + 3));
            }
            for (int i = 0; i < paletVallues[1] / 100 + 1; i++)
            {
                double a;
                a = (Y2.Y - Y1.Y) / ((double)(paletVallues[1]) / 100);
                g.DrawLine(p, new Point(Y1.X - 3, (int)(Y1.Y + i * a)), new Point(Y1.X + 3, (int)(Y1.Y + i * a)));
            }
        }
    }
}
