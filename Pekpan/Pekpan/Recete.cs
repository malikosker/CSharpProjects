using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pekpan
{
    public partial class Recete : Form
    {
        public Recete(List<string> receteDatas)
        {
            InitializeComponent();
            label11.Text = receteDatas[0];
            label12.Text = receteDatas[1];
            label13.Text = receteDatas[2];
            label14.Text = receteDatas[3];
            label15.Text = receteDatas[4];
            label16.Text = receteDatas[5];
            label17.Text = receteDatas[6];
            label18.Text = receteDatas[7];
            label19.Text = receteDatas[8];
            label10.Text = receteDatas[9];
        }
    }
}
