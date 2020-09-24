using GameShowAdmin;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Broardcast_ScheduleAdmin
{
    public partial class BrdScheduleForm : Form
    {
        public BrdScheduleForm()
        {
            InitializeComponent();
        }

        private void SetUIGridview()
        {
            MaximizeBox = false;

            var col_0 = grvGameShow.Columns[0];
            var col_1 = grvGameShow.Columns[1];
            var col_2 = grvGameShow.Columns[2];
            var col_3 = grvGameShow.Columns[3];

            col_0.HeaderText = "ID";
            col_1.HeaderText = "Tên Gameshow";
            col_2.HeaderText = "Bắt Đầu";
            col_3.HeaderText = "Kết Thúc";

            col_0.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            col_1.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            col_2.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            col_3.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // set width of columns
            col_0.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            col_1.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            col_2.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            col_3.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void BrdScheduleForm_Load(object sender, EventArgs e)
        {
            List<Broardcast_Schedule> lst = new List<Broardcast_Schedule>();

            lst.Add(new Broardcast_Schedule()
            {
                Id = 1,
                Name = "Nhanh như chớp",
                StartDate = DateTime.Now.AddSeconds(10),
                EndDate = DateTime.Now.AddMinutes(30)
            });

            lst.Add(new Broardcast_Schedule()
            {
                Id = 2,
                Name = "Nhanh như chớp",
                StartDate = DateTime.Now.AddDays(1),
                EndDate = DateTime.Now.AddDays(1).AddMinutes(30)
            });

            lst.Add(new Broardcast_Schedule()
            {
                Id = 3,
                Name = "Nhanh như chớp",
                StartDate = DateTime.Now.AddDays(2),
                EndDate = DateTime.Now.AddDays(2).AddMinutes(30)
            });

            lst.Add(new Broardcast_Schedule()
            {
                Id = 4,
                Name = "Nhanh như chớp",
                StartDate = DateTime.Now.AddDays(3),
                EndDate = DateTime.Now.AddDays(3).AddMinutes(30)
            });

            grvGameShow.DataSource = lst;
            SetUIGridview();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
