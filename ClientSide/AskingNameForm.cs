using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClientSide
{
    public partial class AskingNameForm : Form
    {
        public AskingNameForm()
        {
            InitializeComponent();
            lbNotification.Text = "";
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (txtName.Text.Trim() == string.Empty)
            {
                return;
            }

            Close();
        }

        public event EventHandler ClosingClientFrm;

        private void AskingNameForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (txtName.Text.Trim() == string.Empty)
            {
                ClosingClientFrm(sender, e);
            }
        }

        private void txtName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtName.Text.Trim() == string.Empty)
                {
                    return;
                }

                Close();
            }
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            if (txtName.Text.Length > 0)
            {
                lbNotification.Text = "";
            }
            else
            {
                lbNotification.Text = "*Tên còn trống !!";
            }
        }
    }
}
