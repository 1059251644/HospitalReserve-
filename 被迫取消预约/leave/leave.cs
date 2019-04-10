using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace leave
{
    public partial class Leave : Form
    {
        private int leaveType;
        private string docno = "20000006";
        public Leave()
        {
            InitializeComponent();
        }

        private void DateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
           
            dateTimePicker1.MinDate = DateTime.Now.AddDays(1);
        }

        private void DateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
           
            dateTimePicker2.MinDate = DateTime.Parse(dateTimePicker1.Value.ToString());
        }

        private void RadioButton1_CheckedChanged(object sender, EventArgs e)
        {
            leaveType = 0;
            
        }

        private void RadioButton2_CheckedChanged(object sender, EventArgs e)
        {
            leaveType = 1;
            
        }

        private void RadioButton3_CheckedChanged(object sender, EventArgs e)
        {
            leaveType = 2;
            
        }

        private void Button1_Click(object sender, EventArgs e)
        {
           
            DateTime begin = dateTimePicker1.Value;
            DateTime end = dateTimePicker2.Value;
            TimeSpan t1 =new TimeSpan(begin.Ticks);
            TimeSpan t2 = new TimeSpan(end.Ticks);
            TimeSpan t3 = t2.Subtract(t1);
            int sumday = Convert.ToInt32(t3.TotalDays);

            
            string count = "select MAX([leave_number]) from [dbo].[t_leave]";
            int leaveNumber;

            if (string.IsNullOrEmpty(DataBass.GetStringResult(count).ToString()))
            {
                leaveNumber = 1;
            }
            else
                leaveNumber = Convert.ToInt32(DataBass.GetStringResult(count)) + 1;

           
            string sql = "insert [dbo].[t_leave] values(@leave_number,@doctor_number,@begin_time,@end_time,@leave_type)";

            SqlParameter[] parameters = new SqlParameter[]
            {
                    new SqlParameter("@leave_number",leaveNumber),
                    new SqlParameter("@doctor_number",docno),
                    new SqlParameter("@begin_time",begin),
                    new SqlParameter("@end_time",end),
                    new SqlParameter("@leave_type",leaveType),
            };
            try
            {
                DataBass.UPdate(sql, parameters);
                MessageBox.Show("添加成功");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void Leave_Load(object sender, EventArgs e)
        {
            dateTimePicker1.Value = DateTime.Now.AddDays(1);
            dateTimePicker2.Value = DateTime.Now.AddDays(1);
            leaveType = 2;
            
            label3.Visible = false;
        }
    }
}
