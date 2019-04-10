using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 被迫取消预约
{
    public partial class show : Form
    {
        public string dutyno;
        public show(string str)
        {
            dutyno = str;
            InitializeComponent();
        }

        private void Show_Load(object sender, EventArgs e)
        {
            string t_reserve = "select* from[dbo].[t_reserve] where[duty_number]=@dutyno";
            DataTable dataTable=Database.
        }
    }
}
