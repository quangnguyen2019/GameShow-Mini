using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameShowAdmin
{
    public partial class ucAnswerVer : UserControl
    {
        public ucAnswerVer()
        {
            InitializeComponent();
        }

        public TextBox AnswerA => txtA;
        public TextBox AnswerB => txtB;
        public TextBox AnswerC => txtC;
        public TextBox AnswerD => txtD;

        public RadioButton RadioBtnA => rbCorrectA;
        public RadioButton RadioBtnB => rbCorrectB;
        public RadioButton RadioBtnC => rbCorrectC;
        public RadioButton RadioBtnD => rbCorrectD;
    }
}
