using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;

namespace Shenon_Fano
{
    public partial class ResultForm : Form
    {
        Model coding;
        public ResultForm()
        {
            InitializeComponent();
        }

        public ResultForm(string str)
        {
            InitializeComponent();
            coding = new Model(str);
            
            foreach(Character c in coding.myCharacters)
            {
                label1.Text += c.ToString();
            }
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
