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

namespace 被迫取消预约
{
    public partial class ShortLeave : Form
    {
       public int Office_number = 2;
        int i ;
        string Doctor_number;
        string docno = "doctor_number";
        DataTable t_do;
        public ShortLeave()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {

            Doctor_number = textBox1.Text;
            
            try
            {
                i = 1;
                string t_doctor = "select [doctor_number],[doctor_name],[title_name] from[dbo].[t_doctor]left join[dbo].[t_title] on [dbo].[t_title].title_id=[dbo].[t_doctor].title_id where [office_number]=@Officenumber";
                t_do = Database.GetDatatable(t_doctor, Office_number.ToString(),i);
              //  MessageBox.Show(t_do.Rows[0][docno].ToString(), Doctor_number);
               if(t_do.Rows[0][docno].ToString().Trim().Equals(Doctor_number))
                {
                    MessageBox.Show(t_do.Rows[0][docno].ToString(), Doctor_number);
                    ShortLeave_from frmMain = new ShortLeave_from(Doctor_number);
                    frmMain.Show();
                    
                }
               else
                {
                    MessageBox.Show("没有该医生");
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show("错误信息" + ex.Message, "出现错误");
            }

        }

        private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           if(e.ColumnIndex==dataGridView1.Columns[3].Index)
            {

                int i = e.RowIndex;
                Doctor_number = t_do.Rows[i][0].ToString().Trim();
                MessageBox.Show(Doctor_number);
                ShortLeave_from frmMain = new ShortLeave_from(Doctor_number);
                frmMain.Show();
                
            }
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

            MessageBox.Show(Office_number.ToString());
            try
            {
                i = 1;
                DataGridViewButtonColumn search = new DataGridViewButtonColumn();
                search.Text = "查询";
                search.Width = 50;
                search.Name = "btnsearch";
                search.HeaderText = "查询";
                search.UseColumnTextForButtonValue = true ;
                
                string t_doctor = "select [doctor_number],[doctor_name],[title_name] from[dbo].[t_doctor]left join[dbo].[t_title] on[dbo].[t_title].title_id=[dbo].[t_doctor].title_id where [office_number]=@Officenumber";
                t_do = Database.GetDatatable(t_doctor, Office_number.ToString(),i);
                
                t_do.Columns["doctor_number"].ColumnName="医生工作号";
                t_do.Columns["doctor_name"].ColumnName = "医生姓名";
                t_do.Columns["title_name"].ColumnName = "医生职称";
                dataGridView1.DataSource = t_do;
                dataGridView1.Columns.Add(search);
            }

            catch (Exception ex)
            {
                MessageBox.Show("错误信息" + ex.Message, "出现错误");
            }
        }

        private void GroupBox2_Enter(object sender, EventArgs e)
        {

        }
    }
}
