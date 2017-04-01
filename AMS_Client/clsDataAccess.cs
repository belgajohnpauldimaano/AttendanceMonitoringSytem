using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
namespace AMS_Server
{
    class clsDataAccess /*:Form*/
    {
        private ErrorLogger errLog;
        private string connectionString = "";
        public clsDataAccess(string connStr) {
            try{
                errLog = new ErrorLogger(@"C:\ProgramData\AMS_Client.txt");
                con_db = new MySqlConnection(connStr);
                connectionString = connStr;
                _connStatus = "Database connection successfully established.";
                _dbConnected = true;
                open_db();
                close_db();
            }catch(Exception ex){
                _connStatus = ex.Message.ToString();
                errLog.LogError(String.Format("Date : {2} {3}Message : {0} {3}Stack Trace : {1}{3}", ex.Message.ToString(), ex.StackTrace.ToString(), DateTime.Now.ToString(),Environment.NewLine));
                _dbConnected = false;
            }
        }



        private DataSet DS = new DataSet();
        private MySqlConnection con_db;//= new MySqlConnection("server=127.0.0.1;user id=root;password=;database=sms_app");
        private MySqlCommand cmd; // command
        //private MySqlCommand tempcmd; // temporary command
        private MySqlDataAdapter da; // data adapter

        private void close_db()
        {
            try{
                if (con_db.State == ConnectionState.Open) con_db.Close(); // method that close the open database conneciton
            }
            catch (Exception ex)
            {
                _connStatus = ex.Message.ToString();
                errLog.LogError(String.Format("Date : {2} {3}Message : {0} {3}Stack Trace : {1}{3}", ex.Message.ToString(), ex.StackTrace.ToString(), DateTime.Now.ToString(),Environment.NewLine));
                _dbConnected = false;
            }
        }
        private void open_db()
        {
            try {
                if (con_db.State == ConnectionState.Closed) con_db.Open(); // method that opens the database connection whenever it is close
            }
            catch (Exception ex)
            {
                _connStatus = ex.Message.ToString();
                errLog.LogError(String.Format("Date : {2} {3}Message : {0} {3}Stack Trace : {1}{3}", ex.Message.ToString(), ex.StackTrace.ToString(), DateTime.Now.ToString(),Environment.NewLine));
                _dbConnected = false;
            }
        }
        public DataSet executeQuery(string qry)
        { //method the query to database and return its value to dataset
            try
            {
                //open_db(); // opend database

                DS.Reset(); // reset the dataset to erase the content
                da = new MySqlDataAdapter(qry, con_db); // // query to database
                da.Fill(DS); // pass the adapter to dataset

                //close_db(); // close dataset
                return DS; // return 
            }
            catch (Exception ex)
            {
                _connStatus = ex.Message.ToString();
                errLog.LogError(String.Format("Date : {2} {3}Message : {0} {3}Stack Trace : {1}{3}", ex.Message.ToString(), ex.StackTrace.ToString(), DateTime.Now.ToString(),Environment.NewLine));
                return DS; //return the valaue of dataset
            }
        }


        public bool ExecuteCommand(string qry)
        {
            try
            {
                open_db(); // open the database

                cmd = new MySqlCommand(qry, con_db); //query to database
                cmd.ExecuteNonQuery();

                close_db(); // close

                return true; // return
            }
            catch (Exception ex)
            {
                _connStatus = ex.Message.ToString();
                _dbConnected = false;
                return false;
            }
        }
        public void sp_log_student_attendance() {
            try
            {

                 DataSet studds = new DataSet();
                //studds = this.executeQuery("select stud_ln,  stud_parent_number from tbl_student_info where stud_id_number = '"+ student_id +"'");
                string ln = studds.Tables[0].Rows[0][0].ToString();
                string number = studds.Tables[0].Rows[0][1].ToString();
            }
            catch (Exception ex)
            {
                errLog.LogError(String.Format("Date : {2} {3}Message : {0} {3}Stack Trace : {1}{3}", ex.Message.ToString(), ex.StackTrace.ToString(), DateTime.Now.ToString(),Environment.NewLine));
            }
        }
        public bool sp_log_student(string student_id, int logtype, int terminal, bool hasrec) {
            try
            {

                string time = DateTime.Now.ToLongTimeString();
                string dt = String.Format("{0:yyyy-MM-dd}", DateTime.Now);
                //DataSet studds = new DataSet();
                //studds = this.executeQuery("select stud_fn,  stud_parent_number from tbl_student_info where stud_id_number = '" + student_id + "'");
                //string ln = studds.Tables[0].Rows[0][0].ToString();
                //string number = studds.Tables[0].Rows[0][1].ToString();
                string qry1="";
                string qry2 ="";
                
                qry2 = "INSERT INTO `sms_app`.`tbl_sms_sender` VALUES (null, '" + time + "', '" + logtype + "', '" + studNumber + "', '" + studFn + "', 0, '" + dt + "', '" + student_id + "', '" + terminal + "')";
                if (hasrec == false)
                {
                    if (logtype == 0)
                    {
                        qry1 = "insert into tbl_student_logs(stud_id,slog_am_in,slog_date) values('" + student_id + "','" + time + "','" + dt + "')";
                    }
                    //else if (logtype == 1)
                    //{
                    //    qry1 = "update tbl_student_logs set slog_am_out = '" + time + "' WHERE slog_date = '" + dt + "' AND stud_id = '" + student_id + "'";
                    //}
                    else if (logtype == 2)
                    {
                        qry1 = "insert into tbl_student_logs(stud_id,slog_pm_in,slog_date) values('" + student_id + "','" + time + "','" + dt + "')";
                        //qry1 = "update tbl_student_logs set slog_pm_in = '" + time + "' WHERE slog_date = '" + dt + "' AND stud_id = '" + student_id + "'";
                    }
                    else if (logtype == 3)
                    {
                        qry1 = "insert into tbl_student_logs(stud_id,slog_pm_out,slog_date) values('" + student_id + "','" + time + "','" + dt + "')";
                        //qry1 = "update tbl_student_logs set slog_pm_out = '" + time + "' WHERE slog_date = '" + dt + "' AND stud_id = '" + student_id + "'";
                    }
                }
                else
                {
                    if (logtype == 2)
                    {
                        qry1 = "update tbl_student_logs set slog_pm_in = '" + time + "' WHERE slog_date = '" + dt + "' AND stud_id = '" + student_id + "'";
                    }
                    else if (logtype == 3)
                    {
                        //qry1 = "insert into tbl_student_logs(stud_id,slog_pm_out,slog_date) values('" + student_id + "','" + time + "','" + dt + "')";
                        qry1 = "update tbl_student_logs set slog_pm_out = '" + time + "' WHERE slog_date = '" + dt + "' AND stud_id = '" + student_id + "'";
                    }
                }
                //open_db();
                using (MySqlConnection dbconnection = new MySqlConnection(connectionString))
                {
                    dbconnection.Open();
                    using(MySqlCommand command = new MySqlCommand(qry1,dbconnection))
                    {
                        command.ExecuteNonQuery();
                    }
                    dbconnection.Close();
                }
                using (MySqlConnection dbconnection = new MySqlConnection(connectionString))
                {
                    dbconnection.Open();
                    using (MySqlCommand command = new MySqlCommand(qry2, dbconnection))
                    {
                        command.ExecuteNonQuery();
                    }
                    dbconnection.Close();
                }

                //cmd = new MySqlCommand("sp_log_student_attendance", con_db);
                //cmd.CommandType = CommandType.StoredProcedure;

                //cmd.Parameters.AddWithValue("@studid", student_id);
                //cmd.Parameters.AddWithValue("@logtype", logtype);
                //cmd.Parameters.AddWithValue("@terminal", terminal);

                //cmd.ExecuteNonQuery();
                //cmd.Dispose();
                //studds.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                _connStatus = ex.Message.ToString();
                errLog.LogError(String.Format("Date : {2} {3}Message : {0} {3}Stack Trace : {1}{3}", ex.Message.ToString(), ex.StackTrace.ToString(), DateTime.Now.ToString(),Environment.NewLine));
                _dbConnected = false;
                return false;
            }
            finally
            {
                close_db();
            }
        }


        public bool sp_log_student_am_in(string student_id)
        {
            try
            {
                open_db();

                //double x = DateTime.Compare(
                string time;
                int stats;
                //string curTime = DateTime.Now.Hour.ToString() + "." + ((double)(DateTime.Now.Minute) / 60).ToString("0.00");
                double dblTime = double.Parse(DateTime.Now.Hour.ToString()) + double.Parse(((double)DateTime.Now.Minute / 60).ToString("0.00"));

                time = DateTime.Now.ToLongTimeString();
                stats = dblTime > 7.00 ? 1 : 0;

                cmd = new MySqlCommand("sp_log_student_am_in", con_db);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@stud_id", student_id);
                cmd.Parameters.AddWithValue("@log_time", time);
                cmd.Parameters.AddWithValue("@log_status", stats);
                cmd.Parameters.AddWithValue("@dt", DateTime.Now.Date);

                cmd.ExecuteNonQuery();

                close_db();

                return true;
            }
            catch (Exception ex)
            {
                _connStatus = ex.Message.ToString();
                errLog.LogError(String.Format("Date : {2} {3}Message : {0} {3}Stack Trace : {1}{3}", ex.Message.ToString(), ex.StackTrace.ToString(), DateTime.Now.ToString(),Environment.NewLine));
                _dbConnected = false;
                return false;
            }
        } 

        public bool sp_log_student_pm_in(string student_id,bool hasRecord) {

            try{
                open_db();

                string time;
                int stats;
                string curTime = DateTime.Now.Hour.ToString() + "." + DateTime.Now.Minute.ToString();
                double dblTime = double.Parse(curTime);

                time = DateTime.Now.ToLongTimeString();
                stats = dblTime > 13.00 ? 1 : 0;

                if(hasRecord){ //if the student has already a record
                    cmd = new MySqlCommand("sp_log_student_pm_in", con_db);
                }else{ //if the student dont have a current record
                    cmd = new MySqlCommand("sp_log_student_pm_in_first", con_db);
                }

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@stud_id", student_id);
                cmd.Parameters.AddWithValue("@log_time", time);
                cmd.Parameters.AddWithValue("@log_status", stats);
                cmd.Parameters.AddWithValue("@dt", DateTime.Now.Date);

                cmd.ExecuteNonQuery();

                close_db();

                return true;

            }catch (Exception ex){
                _connStatus = ex.Message.ToString();
                errLog.LogError(String.Format("Date : {2} {3}Message : {0} {3}Stack Trace : {1}{3}", ex.Message.ToString(), ex.StackTrace.ToString(), DateTime.Now.ToString(),Environment.NewLine));
                _dbConnected = false;
                return false;
            }
        }
        public bool sp_log_student_am_out(string student_id) {
            try
            {
                open_db();

                string time;
                int stats;
                /*string curTime = DateTime.Now.Hour.ToString() + "." + DateTime.Now.Minute.ToString();
                double dblTime = double.Parse(curTime);*/

                time = DateTime.Now.ToLongTimeString();
                stats = 0;//dblTime > 8.00 ? 1 : 0;
                
                cmd = new MySqlCommand("sp_log_student_am_out", con_db);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@stud_id", student_id);
                cmd.Parameters.AddWithValue("@log_time", time);
                cmd.Parameters.AddWithValue("@log_status", stats);
                cmd.Parameters.AddWithValue("@dt", DateTime.Now.Date);

                cmd.ExecuteNonQuery();

                close_db();

                return true;

            }catch (Exception ex) {
                _connStatus = ex.Message.ToString();
                errLog.LogError(String.Format("Date : {2} {3}Message : {0} {3}Stack Trace : {1}{3}", ex.Message.ToString(), ex.StackTrace.ToString(), DateTime.Now.ToString(),Environment.NewLine));
                _dbConnected = false;
                return false;
            }
        }

        public bool sp_log_student_pm_out(string student_id)
        {
            try
            {
                open_db();

                string time;
                int stats;
                /*string curTime = DateTime.Now.Hour.ToString() + "." + DateTime.Now.Minute.ToString();
                double dblTime = double.Parse(curTime);*/

                time = DateTime.Now.ToLongTimeString();
                stats = 0;//dblTime > 8.00 ? 1 : 0;

                cmd = new MySqlCommand("sp_log_student_pm_out", con_db);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@stud_id", student_id);
                cmd.Parameters.AddWithValue("@log_time", time);
                cmd.Parameters.AddWithValue("@log_status", stats);
                cmd.Parameters.AddWithValue("@dt", DateTime.Now.Date);

                cmd.ExecuteNonQuery();

                close_db();

                return true;

            }
            catch (Exception ex)
            {
                _connStatus = ex.Message.ToString();
                errLog.LogError(String.Format("Date : {2} {3}Message : {0} {3}Stack Trace : {1}{3}", ex.Message.ToString(), ex.StackTrace.ToString(), DateTime.Now.ToString(),Environment.NewLine));
                _dbConnected = false;
                return false;
            }
        }

        string studName = "";
        string studFn = "";
        string studNumber = "";
        string studIdNum = "";
        string studSec = "";
        string studYr = "";
        string imgfile = "";
        string _connStatus = "";
        //object img = "";




        bool _dbConnected = false;

        //public object student_image {
        //    get {
        //        return img;
        //    }
        //}
        public void clearInfo() {
            //img = null;
            studName = "";
            studFn = "";
            studIdNum = "";
            studSec = "";
            studYr = "";
        }
        public string imageFileName {
            get {
                return imgfile;
            }
        }
        public bool dbConnected {
            get {
                return _dbConnected;
            }
        }
        public string connStatus {
            get {
                return _connStatus;
            }
        }
        public string student_name {
            get {
                return studName;
            }
        }
        public string student_fn {
            get {
                return studFn;
            }
        }
        public string student_ID {
            get {
                return studIdNum;
            }
        }
        public string student_Number {
            get {
                return studNumber;
            }
        }
        public string student_Sec {
            get {
                return studSec;
            }
        }
        public string student_Yr {
            get {
                return studYr;
            }
        }

        public int sp_check_stud_info_for_log(string rfid) {
            try
            {
                open_db();
                DataSet dsCheck = new DataSet();
                cmd = new MySqlCommand("sp_check_stud_info_for_log", con_db);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@rfid", rfid);
                da = new MySqlDataAdapter();
                da.SelectCommand = cmd;
                dsCheck.Reset();
                da.Fill(dsCheck);


                if (dsCheck.Tables[0].Rows.Count > 0)
                {
                    studIdNum = dsCheck.Tables[0].Rows[0][0].ToString();
                    studName = dsCheck.Tables[0].Rows[0][1].ToString() + " " + dsCheck.Tables[0].Rows[0][2].ToString() + " " + dsCheck.Tables[0].Rows[0][3].ToString();
                    studFn = dsCheck.Tables[0].Rows[0][1].ToString();
                    studYr = dsCheck.Tables[0].Rows[0][4].ToString();
                    studNumber = dsCheck.Tables[0].Rows[0][6].ToString();
                    studSec = dsCheck.Tables[0].Rows[0][5].ToString();
                    imgfile = dsCheck.Tables[0].Rows[0][3].ToString() + ", " + dsCheck.Tables[0].Rows[0][1].ToString() + " " + dsCheck.Tables[0].Rows[0][2].ToString() + ".png";
                    //if (dsCheck.Tables[0].Rows[0][6] != null)
                    //{
                    //    img = dsCheck.Tables[0].Rows[0][6];
                    //}
                    //else
                    //{
                    //    img = null;
                    //}

                    close_db();
                    cmd.Dispose();
                    da.Dispose();
                    dsCheck.Dispose();
                    return 1;
                }
                else
                {
                    close_db();
                    studName = "";
                    studFn = "";
                    studNumber = "";
                    studIdNum = "";
                    studSec = "";
                    studYr = "";
                    //img = null;
                    return 0;
                }
            }
            catch (Exception ex)
            {
                _connStatus = ex.Message.ToString();
                errLog.LogError(String.Format("Date : {2} {3}Message : {0} {3}Stack Trace : {1}{3}", ex.Message.ToString(), ex.StackTrace.ToString(), DateTime.Now.ToString(),Environment.NewLine));
                return -1;
            }
            finally
            {
                da.Dispose();
                cmd.Dispose();
            }
        }

        private string _slog_am_in = "";
        private string _slog_am_out = "";
        private string _slog_pm_in = "";
        private string _slog_pm_out = "";

        public string slog_am_in {
            get {
                return _slog_am_in;
            }
        }
        public string slog_am_out{
            get
            {
                return _slog_am_out;
            }
        }
        public string slog_pm_in{
            get
            {
                return _slog_pm_in;
            }
        }
        public string slog_pm_out{
            get
            {
                return _slog_pm_out;
            }
        }
        public string error_msg {
            get {
                return error_msg;
            }
            set {
                error_msg = value;
            }
        }
        public int sp_search_student_logs_for_log(string student_ID, string student_Tag)
        {
            try {
                open_db();
                
                cmd = new MySqlCommand("sp_search_student_logs", con_db);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@stud_id_number", student_ID);
                cmd.Parameters.AddWithValue("@tag_id", student_Tag);
                cmd.Parameters.AddWithValue("@dt",DateTime.Now.Date);

                da = new MySqlDataAdapter();
                da.SelectCommand = cmd;
                DS.Reset();
                da.Fill(DS);

                if(DS.Tables[0].Rows.Count>0){

                    _slog_am_in = DS.Tables[0].Rows[0][2].ToString();
                    _slog_am_out = DS.Tables[0].Rows[0][3].ToString();
                    _slog_pm_in = DS.Tables[0].Rows[0][4].ToString();
                    _slog_pm_out = DS.Tables[0].Rows[0][5].ToString();
                
                
                    close_db();
                    return 1;
                }else{
                    
                    _slog_am_in = "";
                    _slog_am_out = "";
                    _slog_pm_in = "";
                    _slog_pm_out = "";

                    close_db();
                    da.Dispose();
                    cmd.Dispose();
                    DS.Reset();
                    return 0;
                }
                //MessageBox.Show(student_ID + " " + student_Tag);
            }
            catch (Exception ex) {
                _connStatus = ex.Message.ToString();
                errLog.LogError(String.Format("Date : {2} {3}Message : {0} {3}Stack Trace : {1}{3}", ex.Message.ToString(), ex.StackTrace.ToString(), DateTime.Now.ToString(),Environment.NewLine));
                return -1;
            }
        }

        public bool ExecuteProcedureCommand_Handle_User(string un, string pw, string fn, string mn, string ln, string role, string id = "")
        {
            try
            {
                open_db();

                if (id == "")
                { // 0 is equivalent to adding new user
                    cmd = new MySqlCommand("sp_insert_user", con_db);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@un", un);
                    cmd.Parameters.AddWithValue("@pw", pw);
                    cmd.Parameters.AddWithValue("@fn", fn);
                    cmd.Parameters.AddWithValue("@mn", mn);
                    cmd.Parameters.AddWithValue("@ln", ln);
                    cmd.Parameters.AddWithValue("@urole", role);

                    cmd.ExecuteNonQuery();
                }
                else
                { // if not 0 then it is editing 

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

                cmd = null;
                close_db();
                return true;
            }
            catch(Exception ex)
            {
                _dbConnected = false;
                errLog.LogError(String.Format("Date : {2} {3}Message : {0} {3}Stack Trace : {1}{3}", ex.Message.ToString(), ex.StackTrace.ToString(), DateTime.Now.ToString(),Environment.NewLine));
                return false;
            }
        }


        private DataSet DS_Send = new DataSet();
        MySqlConnection conS, conS2, conS3, conS4;
        public void setSendDBConnection()
        {
            conS = new MySqlConnection(connectionString);
            //conS2 = new MySqlConnection(connectionString);
            //conS3 = new MySqlConnection(connectionString);
            //conS4 = new MySqlConnection(connectionString);
            conS.Open();
            //conS2.Open();
            //conS3.Open();
            //conS4.Open();
        }

        public void setCloseSendDBConnection()
        {
            conS.Close();
            //conS2.Close();
            //conS3.Close();
            //conS4.Close();
        }
        public DataSet GetNumbers(int terminal, ref DataSet d)
        {
            try
            {
                //open_db();

                MySqlCommand cmd_num = new MySqlCommand("sp_search_numbers", con_db);
                cmd_num.CommandType = CommandType.StoredProcedure;
                cmd_num.Parameters.AddWithValue("@term", terminal);

                MySqlDataAdapter da_num = new MySqlDataAdapter();
                da_num.SelectCommand = cmd_num;
                d.Reset();
                da_num.Fill(d);

                return d;
            }
            catch(Exception ex)
            {
                errLog.LogError(String.Format("Date : {2} {3}Message : {0} {3}Stack Trace : {1}{3}", ex.Message.ToString(), ex.StackTrace.ToString(), DateTime.Now.ToString(),Environment.NewLine));
                return d;
            }
        }
        public DataSet GetNumbers_terminal2(int terminal, ref DataSet d)
        {
            try
            {
                //open_db();
                MySqlCommand cmd_num = new MySqlCommand("sp_search_numbers", conS2);
                cmd_num.CommandType = CommandType.StoredProcedure;
                cmd_num.Parameters.AddWithValue("@term", terminal);

                MySqlDataAdapter da_num = new MySqlDataAdapter();
                da_num.SelectCommand = cmd_num;
                d.Reset();
                da_num.Fill(d);


                return d;
            }
            catch(Exception ex)
            {
                errLog.LogError(String.Format("Date : {2} {3}Message : {0} {3}Stack Trace : {1}{3}", ex.Message.ToString(), ex.StackTrace.ToString(), DateTime.Now.ToString(),Environment.NewLine));
                return d;
            }
        }
        public DataSet GetNumbers_terminal3(int terminal, ref DataSet d)
        {
            try
            {
                //open_db();
                MySqlCommand cmd_num = new MySqlCommand("sp_search_numbers", conS3);
                cmd_num.CommandType = CommandType.StoredProcedure;
                cmd_num.Parameters.AddWithValue("@term", terminal);

                MySqlDataAdapter da_num = new MySqlDataAdapter();
                da_num.SelectCommand = cmd_num;
                d.Reset();
                da_num.Fill(d);


                return d;
            }
            catch(Exception ex)
            {
                errLog.LogError(String.Format("Date : {2} {3}Message : {0} {3}Stack Trace : {1}{3}", ex.Message.ToString(), ex.StackTrace.ToString(), DateTime.Now.ToString(),Environment.NewLine));
                return d;
            }
        }
        public DataSet GetNumbers_terminal4(int terminal, ref DataSet d)
        {
            try
            {
                //open_db();
                MySqlCommand cmd_num = new MySqlCommand("sp_search_numbers", conS4);
                cmd_num.CommandType = CommandType.StoredProcedure;
                cmd_num.Parameters.AddWithValue("@term", terminal);

                MySqlDataAdapter da_num = new MySqlDataAdapter();
                da_num.SelectCommand = cmd_num;
                d.Reset();
                da_num.Fill(d);


                return d;
            }
            catch(Exception ex)
            {
                errLog.LogError(String.Format("Date : {2} {3}Message : {0} {3}Stack Trace : {1}{3}", ex.Message.ToString(), ex.StackTrace.ToString(), DateTime.Now.ToString(),Environment.NewLine));
                return d;
            }
        }

        public void Send_Msg(string id, int logtype)
        {
            try
            {
                con_db.Open();

                MySqlCommand cmd_num = new MySqlCommand("sp_update_sms_status", con_db);
                cmd_num.CommandType = CommandType.StoredProcedure;
                cmd_num.Parameters.AddWithValue("@studid", id);
                cmd_num.Parameters.AddWithValue("@logtype", logtype);
                cmd_num.ExecuteNonQuery();
                con_db.Close();
            }
            catch(Exception ex)
            {
                errLog.LogError(String.Format("Date : {2} {3}Message : {0} {3}Stack Trace : {1}{3}", ex.Message.ToString(), ex.StackTrace.ToString(), DateTime.Now.ToString(),Environment.NewLine));
            }
        }
        /*private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // clsDataAccess
            // 
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Name = "clsDataAccess";
            this.Load += new System.EventHandler(this.clsDataAccess_Load);
            this.ResumeLayout(false);

        }
        
        private void clsDataAccess_Load(object sender, EventArgs e)
        {

        }*/
    }
}
