using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using MySql.Data.MySqlClient;
namespace AMS_Server
{
    public class clsData_Access
    {
        private ErrorLogger errLog;
        private bool disposed = false;

        public void Dispose(){
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing) {
            if (disposed) { 
                connectionString = "";
                ErrorDB = "";

                DS.Dispose();
                DS_Send.Dispose();
                dsSender.Dispose();

                con_db.Dispose();
                conS.Dispose();

                da.Dispose();
                daSender.Dispose();

                cmd.Dispose();
                cmdSender.Dispose();

            }
        }
        public bool dbConnected = false;
        private string connectionString = "";
        public clsData_Access(string conStr) { //constructor of this class the accept an string value 
            try
            {
                errLog = new ErrorLogger(@"C:\ProgramData\AMS_Server.txt");
                connectionString = conStr;
                con_db = new MySqlConnection(conStr);
                dbConnected = true;
            }
            catch(Exception ex)
            {
                errLog.LogError(String.Format("Date : {2} {3}Message : {0} {3}Stack Trace : {1}{3}", ex.Message.ToString(), ex.StackTrace.ToString(), DateTime.Now.ToString(), Environment.NewLine));
                dbConnected = false;
            }
        }

        string ErrorDB;
        public string getErrorDB() {
            return ErrorDB;
        }
        private DataSet DS = new DataSet();
        private MySqlConnection con_db ;//= new MySqlConnection("server=127.0.0.1;user id=root;password=;database=sms_app");
        private MySqlCommand cmd; // command
        //private MySqlCommand tempcmd; // temporary command
        private MySqlDataAdapter da; // data adapter

        public void close_db()
        {
            if (con_db.State == ConnectionState.Open) con_db.Close(); // method that close the open database conneciton
        }
        public void open_db()
        {
            if (con_db.State == ConnectionState.Closed) con_db.Open(); // method that opens when
        }


        public DataSet executeQuery(string qry) //method the query to database and return its value to dataset
        {
            DS.Dispose();
            DS = new DataSet();
            try
            {
                DS.Reset(); // reset the dataset to erase the content
                //open_db(); // opend database
                da = new MySqlDataAdapter(qry,con_db); // // query to database
                da.Fill(DS); // pass the adapter to dataset

                //close_db(); // close dataset
                da.Dispose();
                return DS; // return 
            }
            catch (Exception ex)
            {
                errLog.LogError(String.Format("Date : {2} {3}Message : {0} {3}Stack Trace : {1}{3}", ex.Message.ToString(), ex.StackTrace.ToString(), DateTime.Now.ToString(), Environment.NewLine));
                return DS; //return the valaue of dataset
            }
        }
       

        public bool ExecuteCommand(string qry) {
            try
            {
                open_db(); // open the database

                cmd = new MySqlCommand(qry,con_db); //query to database
                cmd.ExecuteNonQuery();

                close_db(); // close

                return true; // return
            }
            catch (Exception ex)
            {
                errLog.LogError(String.Format("Date : {2} {3}Message : {0} {3}Stack Trace : {1}{3}", ex.Message.ToString(), ex.StackTrace.ToString(), DateTime.Now.ToString(), Environment.NewLine));
                ErrorDB = ex.Message.ToString();
                return false;
            }
        }
        public bool ExecuteProcedureCommand_Insert_Tag(string parm1)
        {
            try
            {
                open_db();

                cmd = new MySqlCommand("insert_tags",con_db);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@tag_id",parm1);
                cmd.ExecuteNonQuery();

                close_db();

                return true;
            }
            catch(Exception ex)
            {
                errLog.LogError(String.Format("Date : {2} {3}Message : {0} {3}Stack Trace : {1}{3}", ex.Message.ToString(), ex.StackTrace.ToString(), DateTime.Now.ToString(), Environment.NewLine));
                return false;
            }
        }

        public bool ExecuteProcedureCommand_Handle_User(string un, string pw, string fn, string mn, string ln, string role, string id = "")
        { 
            try
            {
                open_db();

                if (id == "")
                { // 0 is equivalent to adding new user
                    cmd = new MySqlCommand("sp_insert_user",con_db);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@un", un);
                    cmd.Parameters.AddWithValue("@pw", pw);
                    cmd.Parameters.AddWithValue("@fn", fn);
                    cmd.Parameters.AddWithValue("@mn", mn);
                    cmd.Parameters.AddWithValue("@ln", ln);
                    cmd.Parameters.AddWithValue("@urole", role);

                    cmd.ExecuteNonQuery();
                }
                else { // if not 0 then it is editing 

                    cmd = new MySqlCommand("sp_edit_user", con_db);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@uid", id);
                    cmd.Parameters.AddWithValue("@un", un);
                    cmd.Parameters.AddWithValue("@pw", pw);
                    cmd.Parameters.AddWithValue("@fn", fn);
                    cmd.Parameters.AddWithValue("@mn", mn);
                    cmd.Parameters.AddWithValue("@ln", ln);
                    cmd.Parameters.AddWithValue("@urole", role);

                    cmd.ExecuteNonQuery();
                }


                close_db();
                return true;
            }
            catch(Exception ex)
            {
                errLog.LogError(String.Format("Date : {2} {3}Message : {0} {3}Stack Trace : {1}{3}", ex.Message.ToString(), ex.StackTrace.ToString(), DateTime.Now.ToString(), Environment.NewLine));
                return false;
            }
        }

        public bool sp_update_img(byte[] arrImg, string stud_id_number) {
            try
            {
                open_db();
                cmd = new MySqlCommand("update tbl_student_info set img=@img where stud_id_number='"+ stud_id_number +"'", con_db);
                cmd.Parameters.AddWithValue("@img", arrImg);
                cmd.ExecuteNonQuery();
                close_db();
                return true;
            }
            catch (Exception ex)
            {
                errLog.LogError(String.Format("Date : {2} {3}Message : {0} {3}Stack Trace : {1}{3}", ex.Message.ToString(), ex.StackTrace.ToString(), DateTime.Now.ToString(), Environment.NewLine));
                return false;
            }
            //arrImg = null;
        }

        public DataSet sp_search_stud_logs_sum(string id,string fn,string mn, string ln, int yr1, int yr2, string sec, string tag_id, DateTime dt1, DateTime dt2){ //this is for searching students logs
            DS = new DataSet();
            try{
                open_db();

                cmd = new MySqlCommand("sp_search_stud_logs_sum", con_db);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@id_number", id);
                cmd.Parameters.AddWithValue("@fn", fn);
                cmd.Parameters.AddWithValue("@mn", mn);
                cmd.Parameters.AddWithValue("@ln", ln);
                cmd.Parameters.AddWithValue("@yr1", yr1);
                cmd.Parameters.AddWithValue("@yr2", yr2);
                cmd.Parameters.AddWithValue("@sec", sec);
                cmd.Parameters.AddWithValue("@tag_id", tag_id);
                cmd.Parameters.AddWithValue("@dt1", dt1);
                cmd.Parameters.AddWithValue("@dt2", dt2);

                da = new MySqlDataAdapter();
                da.SelectCommand = cmd;
                DS.Reset();
                da.Fill(DS);

                close_db();
                return DS;
            }
            catch (Exception ex)
            {
                errLog.LogError(String.Format("Date : {2} {3}Message : {0} {3}Stack Trace : {1}{3}", ex.Message.ToString(), ex.StackTrace.ToString(), DateTime.Now.ToString(), Environment.NewLine));
                return DS;
            }
        }
        public DataSet dsSender = new DataSet();
        public MySqlDataAdapter daSender;
        public MySqlCommand cmdSender;



        private DataSet DS_Send = new DataSet();
        MySqlConnection conS,conStatMsg;//, conS2, conS3, conS4;
        public void setSendDBConnection() {
            conS = new MySqlConnection(connectionString);
            conStatMsg = new MySqlConnection(connectionString);
            //conS2 = new MySqlConnection(connectionString);
            //conS3 = new MySqlConnection(connectionString);
            //conS4 = new MySqlConnection(connectionString);
            conS.Open();
            conStatMsg.Open();
            //conS2.Open();
            //conS3.Open();
            //conS4.Open();
        }

        public void setCloseSendDBConnection()
        {
            conS.Close();
            conStatMsg.Close();
            //conS2.Close();
            //conS3.Close();
            //conS4.Close();
        }
        public DataSet GetNumbers(int terminal, ref DataSet d)
        {
            try
            {
                //open_db();

                MySqlCommand cmd_num = new MySqlCommand("sp_search_numbers", conS);
                cmd_num.CommandType = CommandType.StoredProcedure;
                cmd_num.Parameters.AddWithValue("@term",terminal);

                MySqlDataAdapter da_num = new MySqlDataAdapter();
                da_num.SelectCommand = cmd_num;
                d.Reset();
                da_num.Fill(d);

                return d;
            }
            catch (Exception ex)
            {
                errLog.LogError(String.Format("Date : {2} {3}Message : {0} {3}Stack Trace : {1}{3}", ex.Message.ToString(), ex.StackTrace.ToString(), DateTime.Now.ToString(), Environment.NewLine));
                return d;
            }
        }
        public DataSet GetNumbers_terminal2(int terminal, ref DataSet d)
        {
            //try
            //{
            ////open_db();
            //MySqlCommand cmd_num = new MySqlCommand("sp_search_numbers", conS2);
            //cmd_num.CommandType = CommandType.StoredProcedure;
            //cmd_num.Parameters.AddWithValue("@term", terminal);

            //MySqlDataAdapter da_num = new MySqlDataAdapter();
            //da_num.SelectCommand = cmd_num;
            //d.Reset();
            //da_num.Fill(d);


            //return d;
            //}
            //catch
            //{
               return d;
            //}
        }
        public DataSet GetNumbers_terminal3(int terminal, ref DataSet d)
        {
            //try
            //{
            ////open_db();
            //MySqlCommand cmd_num = new MySqlCommand("sp_search_numbers", conS3);
            //cmd_num.CommandType = CommandType.StoredProcedure;
            //cmd_num.Parameters.AddWithValue("@term", terminal);

            //MySqlDataAdapter da_num = new MySqlDataAdapter();
            //da_num.SelectCommand = cmd_num;
            //d.Reset();
            //da_num.Fill(d);


            //return d;
            //}
            //catch
            //{
               return d;
            //}
        }
        public DataSet GetNumbers_terminal4(int terminal, ref DataSet d)
        {
            //try
            //{
            ////open_db();
            //MySqlCommand cmd_num = new MySqlCommand("sp_search_numbers", conS4);
            //cmd_num.CommandType = CommandType.StoredProcedure;
            //cmd_num.Parameters.AddWithValue("@term", terminal);

            //MySqlDataAdapter da_num = new MySqlDataAdapter();
            //da_num.SelectCommand = cmd_num;
            //d.Reset();
            //da_num.Fill(d);


            //return d;
            //}
            //catch
            //{
               return d;
            //}
        }
        public void Send_Msg(string id, int logtype) {
            try
            {
                conStatMsg.Open();
                //update tbl_sms_sender set sms_stat = 1 where stud_id= studid and log_date = dt and log_type = logtype;
                //MySqlCommand cmd_num = new MySqlCommand("sp_update_sms_status", con_db);
                //cmd_num.CommandType = CommandType.StoredProcedure;
                //cmd_num.Parameters.AddWithValue("@studid", id);
                //cmd_num.Parameters.AddWithValue("@logtype", logtype);

                MySqlCommand cmd_num = new MySqlCommand("update tbl_sms_sender set sms_stat = 1 where stud_id= '" + id + "' and log_date = '" + String.Format("{0:yyyy/MM/dd}", DateTime.Now.Date) + "' and log_type = '" + logtype + "'", conStatMsg);
                cmd_num.ExecuteNonQuery();
                cmd_num.Dispose();
                conStatMsg.Close();
            }
            catch (Exception ex)
            {
                errLog.LogError(String.Format("Date : {2} {3}Message : {0} {3}Stack Trace : {1}{3}", ex.Message.ToString(), ex.StackTrace.ToString(), DateTime.Now.ToString(), Environment.NewLine));
            }
        }
        public DataSet sp_sms_log_info() {
            dsSender = new DataSet();
            try {
                open_db();

                cmdSender = new MySqlCommand("sp_sms_log_info", con_db);
                cmdSender.CommandType = CommandType.StoredProcedure;

                cmdSender.Parameters.AddWithValue("@dt",String.Format("{0:yyyy/MM/dd}",DateTime.Now.Date));

                daSender = new MySqlDataAdapter();
                daSender.SelectCommand = cmdSender;
                dsSender.Reset();
                daSender.Fill(dsSender);

                close_db();
                return dsSender;
            }
            catch (Exception ex)
            {
                errLog.LogError(String.Format("Date : {2} {3}Message : {0} {3}Stack Trace : {1}{3}", ex.Message.ToString(), ex.StackTrace.ToString(), DateTime.Now.ToString(), Environment.NewLine));
                return dsSender;
            }
        }
        public bool HandleAnnouncement(int type, Announcement Announce) {
            try{
            open_db();
                if (type == 0)//Add
                {
                    cmd = new MySqlCommand("insert into tbl_message values(null, @title, @content, @senddate, @recipient, @issent)",con_db);
                    cmd.Parameters.AddWithValue("@title",Announce.title);
                    cmd.Parameters.AddWithValue("@content", Announce.content);
                    cmd.Parameters.AddWithValue("@senddate", Announce.senddate);
                    cmd.Parameters.AddWithValue("@recipient", Announce.recipient);
                    cmd.Parameters.AddWithValue("@issent", "0");
                    cmd.ExecuteNonQuery();
                }
                else if (type == 1) // Edit
                {  

                    cmd = new MySqlCommand("update tbl_message set m_title=@title, m_content=@content, m_senddate=@senddate, m_recipient=@recipient, issent=@sent where m_id=@id", con_db);
                    cmd.Parameters.AddWithValue("@title", Announce.title);
                    cmd.Parameters.AddWithValue("@content", Announce.content);
                    cmd.Parameters.AddWithValue("@senddate", Announce.senddate);
                    cmd.Parameters.AddWithValue("@recipient", Announce.recipient);
                    cmd.Parameters.AddWithValue("@sent", "0");
                    cmd.Parameters.AddWithValue("@id", Announce.m_id);
                    cmd.ExecuteNonQuery();
                }
                else // delete
                {
                    cmd = new MySqlCommand("delete from tbl_message where m_id=@id", con_db);
                    cmd.Parameters.AddWithValue("@id", Announce.m_id);
                    cmd.ExecuteNonQuery();
                }
                close_db();
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
