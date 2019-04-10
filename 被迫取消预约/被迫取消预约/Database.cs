using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 被迫取消预约
{
    class Database
    {
        //Server=.;user = admin;pwd = admin;database =HospitalReserve
        public static string conStr = "Server=.;user = admin;pwd = admin;database =HospitalReserve";

        public Database()
        {

        }


        public SqlConnection Getcon()
        {

            SqlConnection connection = new SqlConnection(conStr);
            return connection;
        }
        public static DataTable GetDatatable(string sqlstr, string select1, int i)
        {



            SqlDataAdapter dataAdapter = new SqlDataAdapter();
            SqlConnection connection = new SqlConnection(conStr);
            SqlCommand command = new SqlCommand();
            DataTable dt = new DataTable();
            command.CommandText = sqlstr;
            command.Connection = connection;
            command.Parameters.Clear();
            switch (i)
            {
                case 1:
                    command.Parameters.AddWithValue("@Officenumber", Convert.ToInt32(select1));
                    break;
                case 2:
                    command.Parameters.AddWithValue("@doctor_number", select1);
                    break;
                case 3:
                    command.Parameters.AddWithValue("@dutyno", select1);
                    break;
                default: break;


            }

            dataAdapter.SelectCommand = command;
            connection.Open();
            dataAdapter.Fill(dt);


            dataAdapter.Dispose();
            command.Dispose();
            connection.Close();
            return dt;
        }
        public static void UPdate(DataTable dataTable, string str)
        {


            SqlConnection connection = new SqlConnection(conStr);
            SqlCommand command = new SqlCommand(str, connection);
            SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
            SqlCommandBuilder commandBuilder = new SqlCommandBuilder(dataAdapter);
            connection.Open();
            dataAdapter.Fill(dataTable);
            DataRow dataRow = dataTable.Rows[0];
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                dataRow = dataTable.Rows[i];
            }
            dataAdapter.Update(dataTable);
            connection.Close();
        }
    }
}
