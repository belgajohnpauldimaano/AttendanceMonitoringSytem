using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MySql.Data.MySqlClient;
using System.Data;
namespace AMS_Server
{
    class SMSDataHandler
    {
        private ErrorLogger errLog;
        private string connectionString;
        private MySqlConnection con_db;
        public SMSDataHandler(string _connectionString) {
            try
            {
                errLog = new ErrorLogger(@"C:\ProgramData\AMS_Serverv.txt");
                connectionString = _connectionString;
                con_db = new MySqlConnection(_connectionString);
            }
            catch (Exception ex)
            {
                errLog.LogError(String.Format("Date : {2} {3}Message : {0} {3}Stack Trace : {1}{3}", ex.Message.ToString(), ex.StackTrace.ToString(), DateTime.Now.ToString(), Environment.NewLine));
            }
        }
        public void close_db()
        {
            if (con_db.State == ConnectionState.Open) con_db.Close(); // method that close the open database conneciton
        }
        public void open_db()
        {
            if (con_db.State == ConnectionState.Closed) con_db.Open(); // method that opens when
        }
        public void Send_Msg(string id, int logtype)
        {
            try
            {
                using( MySqlConnection con = new MySqlConnection(connectionString)){
                    con.Open();
                    using (MySqlCommand cmd = new MySqlCommand("update tbl_sms_sender set sms_stat = 1 where stud_id= '" + id + "' and log_date = '" + String.Format("{0:yyyy/MM/dd}", DateTime.Now.Date) + "' and log_type = '" + logtype + "'", con))
                    {
                        cmd.ExecuteNonQuery();
                    }
                    con.Close();
                }
                //update tbl_sms_sender set sms_stat = 1 where stud_id= studid and log_date = dt and log_type = logtype;
                //MySqlCommand cmd_num = new MySqlCommand("sp_update_sms_status", con_db);
                //cmd_num.CommandType = CommandType.StoredProcedure;
                //cmd_num.Parameters.AddWithValue("@studid", id);
                //cmd_num.Parameters.AddWithValue("@logtype", logtype);

            }
            catch (Exception ex)
            {
                errLog.LogError(String.Format("Date : {2} {3}Message : {0} {3}Stack Trace : {1}{3}", ex.Message.ToString(), ex.StackTrace.ToString(), DateTime.Now.ToString(), Environment.NewLine));
            }
        }
        public void ExecuteCommand(string qry)
        {
            try
            {
                using (MySqlConnection con = new MySqlConnection(connectionString))
                {
                    con.Open();
                    //using (MySqlCommand cmd = new MySqlCommand("update tbl_sms_sender set sms_stat = '1' where stud_id in (" + qry + ") and log_date = '" + String.Format("{0:yyyy/MM/dd}", DateTime.Now.Date) + "'", con))
                    using (MySqlCommand cmd = new MySqlCommand("delete from tbl_sms_sender where stud_id in (" + qry + ") and log_date = '" + String.Format("{0:yyyy/MM/dd}", DateTime.Now.Date) + "'", con))
                    {
                        cmd.ExecuteNonQuery();
                    }
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                errLog.LogError(String.Format("Date : {2} {3}Message : {0} {3}Stack Trace : {1}{3}", ex.Message.ToString(), ex.StackTrace.ToString(), DateTime.Now.ToString(), Environment.NewLine));
            }
        }
        public bool GetNumbers(int terminal, ref DataSet d)
        {
            try
            {


                using (MySqlConnection con = new MySqlConnection(connectionString))
                {
                    using (MySqlCommand cmd = new MySqlCommand("sp_search_numbers", con)) 
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@term",terminal);
                        cmd.Parameters.AddWithValue("@currdate", String.Format("{0:yyyy/MM/dd}", DateTime.Now.Date));

                        using(MySqlDataAdapter da = new MySqlDataAdapter())
                        {
                            da.SelectCommand = cmd;
                            d.Reset();
                            da.Fill(d);
                        }
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                errLog.LogError(String.Format("Date : {2} {3}Message : {0} {3}Stack Trace : {1}{3}", ex.Message.ToString(), ex.StackTrace.ToString(), DateTime.Now.ToString(), Environment.NewLine));
                return false;
            }
        }
    }
}
