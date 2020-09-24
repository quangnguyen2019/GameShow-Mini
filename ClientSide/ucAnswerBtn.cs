using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClientSide
{
    public partial class ucAnswerBtn : UserControl
    {
        public ucAnswerBtn()
        {
            InitializeComponent();
        }

        public string AnswerA
        {
            get { return btnA.Text; }
            set { btnA.Text = value; }
        }

        public string AnswerB
        {
            get { return btnB.Text; }
            set { btnB.Text = value; }
        }

        public string AnswerC
        {
            get { return btnC.Text; }
            set { btnC.Text = value; }
        }

        public string AnswerD
        {
            get { return btnD.Text; }
            set { btnD.Text = value; }
        }
    }
}
