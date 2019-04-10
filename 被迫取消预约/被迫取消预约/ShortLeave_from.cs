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
    public partial class ShortLeave_from : Form
    {
        private String dotno;
        private string docsi;
        private DataTable t_do;
        DataTable dt;
        public ShortLeave_from(String DoctorNumber)
        {
            InitializeComponent();
            dotno = DoctorNumber;
        }

        private void 医生值班情况_Load(object sender, EventArgs e)
        {
            try
            {
                DataGridViewButtonColumn vocate = new DataGridViewButtonColumn
                {
                    Text = "请假",
                    Width = 50,
                    Name = "btnsearch",
                    HeaderText = "请假",
                    UseColumnTextForButtonValue = true
                };
                string t_doctor = "select[doctor_number],[duty_time],[resource_type],[duty_situation],[duty_number] from [dbo].[t_onDuty]where [doctor_number]=@doctor_number";
                t_do = Database.GetDatatable(t_doctor, dotno, 2);
                dataGridView1.DataSource = t_do;
                DataSet ds = new DataSet("database in menory");
                
                dt = new DataTable("doctoc duty table");
                ds.Tables.Add(dt);
               
                dt.Columns.Add("医生工作号", typeof(System.String));
                dt.Columns.Add("值班星期", typeof(System.String));
                dt.Columns.Add("值班班次", typeof(System.String));
                dt.Columns.Add("号源情况", typeof(System.String));
                dt.Columns.Add("值班编号", typeof(System.String));

                for (int i = 0; i < t_do.Rows.Count; i++)
                {
                    DataRow dr = dt.NewRow();
                    
                    dr["医生工作号"] = t_do.Rows[i][0];
                    dr["值班星期"] = Changeweek(Convert.ToInt32(t_do.Rows[i][1].ToString()));
                    dr["值班班次"] = ChangeType(Convert.ToInt32(t_do.Rows[i][2].ToString()));
                    dr["号源情况"] = t_do.Rows[i][3];
                    dr["值班编号"] = t_do.Rows[i][4];
                    dt.Rows.Add(dr);
                }
               
                dataGridView1.DataSource = ds.Tables["doctoc duty table"];
                dataGridView1.Columns.Add(vocate);
            }

            catch (Exception ex)
            {
                MessageBox.Show("错误信息" + ex.Message, "出现错误");
            }
        }
        private string Changeweek(int weekday)
        {
            string week = "error";
            if (weekday >= 0 && weekday <= 6)
            {

                switch (weekday)
                {
                    case 0:
                        week = "星期天";
                        break;
                    case 1:
                        week = "星期一";
                        break;
                    case 2:
                        week = "星期二";
                        break;
                    case 3:
                        week = "星期三";
                        break;
                    case 4:
                        week = "星期四";
                        break;
                    case 5:
                        week = "星期五";
                        break;
                    case 6:
                        week = "星期六";
                        break;

                }
            }
            else
                MessageBox.Show("error!");
           
            return week;
        }
        private string ChangeType(int type)
        {
            string Type = "error";
            if (type ==0 ||type== 1)
            {

                switch (type)
                {
                    case 0:
                        Type = "上午";
                        break;
                    case 1:
                        Type = "上午";
                        break;
                    

                }
            }
            else
                MessageBox.Show("error!");

            return Type;
        }
        private string DATAchange()
        {
            DateTime dateTime = new DateTime();
            dateTime = DateTime.Now;
            string iweek =Convert.ToInt32(dateTime.DayOfWeek).ToString();
            return iweek;
        }
        private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.ColumnIndex == dataGridView1.Columns[5].Index)
            {
               
                
                int i = e.RowIndex;
                DataTable t_reserve;
               
                if (t_do.Rows[i][1].ToString()==DATAchange())
                {
                    MessageBox.Show("不可取消预约");
                }
                else
                {
                    bool IS = true;
                    docsi = t_do.Rows[i][3].ToString().Trim();
                    string dutyno = t_do.Rows[i][4].ToString().Trim();
                    string t_doctor = "select[doctor_number],[duty_time],[resource_type],[duty_situation],[duty_number] from [dbo].[t_onDuty]where [doctor_number]=" + dotno;
                    string reserve = "select * from [dbo].[t_reserve] where  [reserve_State]=0 and [duty_number]=@dutyno";

                    t_reserve = Database.GetDatatable(reserve, dutyno, 3);
                    MessageBox.Show(docsi);
                    char[] vs = docsi.ToArray();
                    for (int j = 0; j < vs.Length; j++)
                    {
                        if (vs[j] == '1')
                        {
                            MessageBox.Show("需要取消患者预约");
                            IS = false;
                            break;
                        }
                    }
                    if (IS == false)
                    {

                        
                        string reserve1 = "select * from [dbo].[t_reserve] where  [reserve_State]=0 and [duty_number]=" + dutyno;

                        for (int j = 0; j < t_reserve.Rows.Count; j++)
                        {
                            t_reserve.Rows[j][2] = 3;
                            string reserverNo = t_reserve.Rows[j][0].ToString().Trim();
                            string treat = "select * from [dbo].[t_treat] where  [reserve_number]=" + reserverNo;
                            DataTable t_treat = Database.GetDatatable(treat, null, 9);
                            for (int k = 0; k < t_treat.Rows.Count; k++)
                            {
                                t_treat.Rows[k][2] = 3;

                            }
                            Database.UPdate(t_treat, treat);
                        }
                        Database.UPdate(t_reserve, reserve1);
                        MessageBox.Show("成功取消患者预约，请通知患者");
                    }
                    else
                    {
                        MessageBox.Show("没有患者，可以正常取消");
                    }

                    show show = new show(dutyno);
                    show.ShowDialog();
                    docsi = "22222222222222222222";
                    t_do.Rows[i][3] = docsi;
                    dt.Rows[i][3] = docsi;
                    Database.UPdate(t_do, t_doctor);
                    

                }



            }
        }
    }
}
