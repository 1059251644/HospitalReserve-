using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leave
{
    class DataBass
    {
        public static string conStr = "Server=.;user = admin;pwd = admin;database =HospitalReserve";
        public SqlConnection Getcon()
        {

            SqlConnection connection = new SqlConnection(conStr);
            return connection;
        }
        public static object GetStringResult(string sql)
        {
            SqlConnection connection = new SqlConnection(conStr);
            SqlCommand command = new SqlCommand(sql,connection);
            try
            {
                connection.Open();
               
                return command.ExecuteScalar();
            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
        }
        public static void UPdate(string str,params SqlParameter[] value)
        {


            SqlConnection connection = new SqlConnection(conStr);
            SqlCommand command = new SqlCommand(str, connection);
            command.Parameters.AddRange(value);
            try
            {
                connection.Open();
                command.ExecuteNonQuery();
              
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
            connection.Close();
        }
        public static DataTable GetDatatable(string sqlstr)
        {



            SqlDataAdapter dataAdapter = new SqlDataAdapter();
            SqlConnection connection = new SqlConnection(conStr);
            SqlCommand command = new SqlCommand();
            DataTable dt = new DataTable();
            command.CommandText = sqlstr;
            
           

            dataAdapter.SelectCommand = command;
            connection.Open();
            dataAdapter.Fill(dt);


            dataAdapter.Dispose();
            command.Dispose();
            connection.Close();
            return dt;
        }
    }
}
