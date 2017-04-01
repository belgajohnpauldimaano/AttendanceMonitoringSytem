using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

using System.Configuration;
using System.IO;

//using ReaderA;
namespace AMS_Server
{
    public partial class frmMainClient : Form
    {
        //private Queue StudentQueue;
        /*
         * 
         * acr122u reader error return code
         * 2146434967 no card availabel
         * 2146435042 resource manager has shut down
         * 
         */
        int retCode;
        int retCode1;

        int hCard;
        int hContext;
        int Protocol;
        public bool connActive = false;

        //string readername = "ACS ACR122 0";      // change depending on reader

        //public byte[] SendBuff = new byte[263];
        //public byte[] RecvBuff = new byte[263];
        public int SendLen, RecvLen, nBytesRet, reqType, Aprotocol, dwProtocol, cbPciLength;
        //ACR122uCard.SCARD_READERSTATE RdrState;
        //ACR122uCard.SCARD_IO_REQUEST pioSendRequest;
        //private int id;
        //private string cardno;


        ACR122uCard.SCARD_READERSTATE rdrState1 = new ACR122uCard.SCARD_READERSTATE();
        ACR122uCard.SCARD_READERSTATE rdrState2 = new ACR122uCard.SCARD_READERSTATE();
        //List<ACR122uCard.SCARD_READERSTATE> rdrStates = new List<ACR122uCard.SCARD_READERSTATE>();
        //List<String> rdrNames = new List<string>();

        int DeviceCount = 0;
        public void SelectDevice()
        {
            //MessageBox.Show(this.ListReaders().Count.ToString());
            if (this.ListReaders().Count > 0)
            {
                 List<string> availableReaders = new List<string>();
                 availableReaders = this.ListReaders();
                //this.RdrState = new ACR122uCard.SCARD_READERSTATE();
                //readername = availableReaders[0].ToString();//selecting first device
                //this.RdrState.RdrName = readername;
                 DeviceCount = availableReaders.Count;
                 if (availableReaders.Count == 1)
                 {
                     rdrState1.RdrName = availableReaders[0].ToString();
                 }
                 else
                 {
                     rdrState1.RdrName = availableReaders[0].ToString();
                     //rdrState2.RdrName = availableReaders[1].ToString();
                 }

                //foreach (string rdrName in availableReaders)
                //{
                //    ACR122uCard.SCARD_READERSTATE rdrState = new ACR122uCard.SCARD_READERSTATE();
                //    rdrState.RdrName = rdrName;
                //    rdrStates.Add(rdrState);
                //}

                //deviceStatusUpdater(retCode);
            }
        }
        public List<string> ListReaders()
        {
            int ReaderCount = 0;
            List<string> AvailableReaderList = new List<string>();

            //Make sure a context has been established before 
            //retrieving the list of smartcard readers.
            retCode = ACR122uCard.SCardListReaders(hContext, null, null, ref ReaderCount);
            if (retCode != ACR122uCard.SCARD_S_SUCCESS)
            {
                //MessageBox.Show(ACR122uCard.GetScardErrMsg(retCode));
                //connActive = false;
            }

            byte[] ReadersList = new byte[ReaderCount];

            //Get the list of reader present again but this time add sReaderGroup, retData as 2rd & 3rd parameter respectively.
            retCode1 = ACR122uCard.SCardListReaders(hContext, null, ReadersList, ref ReaderCount);
            if (retCode != ACR122uCard.SCARD_S_SUCCESS)
            {
                //MessageBox.Show(ACR122uCard.GetScardErrMsg(retCode));
            }

            string rName = "";
            int indx = 0;
            if (ReaderCount > 0)
            {
                // Convert reader buffer to string
                while (ReadersList[indx] != 0)
                {

                    while (ReadersList[indx] != 0)
                    {
                        rName = rName + (char)ReadersList[indx];
                        indx = indx + 1;
                    }

                    //Add reader name to list

                    AvailableReaderList.Add(rName);
                    rName = "";
                    indx = indx + 1;

                }
            }
            GC.Collect();
            return AvailableReaderList;

        }

        internal void establishContext()
        {
            retCode = ACR122uCard.SCardEstablishContext(ACR122uCard.SCARD_SCOPE_SYSTEM, 0, 0, ref hContext);
            if (retCode != ACR122uCard.SCARD_S_SUCCESS)
            {
                //MessageBox.Show("Check your device and please restart again", "Reader not connected");
                connActive = false;
                return;
            }
        }
        public bool connectCard1(int rdrIndex)
        {

            connActive = true;
            //availableReaders[rdrIndex].ToString()
            //this.RdrState.RdrName.ToString()

            if (rdrIndex == 0)
            {
                //availableReaders[0].ToString()
                //retCode = ACR122uCard.SCardConnect(hContext, rdrStates[rdrIndex].RdrName.ToString(), ACR122uCard.SCARD_SHARE_SHARED,
                //      ACR122uCard.SCARD_PROTOCOL_T0 | ACR122uCard.SCARD_PROTOCOL_T1, ref hCard, ref Protocol);
            }
            else
            {
                //retCode1 = ACR122uCard.SCardConnect(hContext, rdrStates[rdrIndex].RdrName.ToString(), ACR122uCard.SCARD_SHARE_SHARED,
                //      ACR122uCard.SCARD_PROTOCOL_T0 | ACR122uCard.SCARD_PROTOCOL_T1, ref hCard, ref Protocol);
            }

            //retCode = Card.SCardConnect(hContext, rdrStates[rdrIndex].RdrName.ToString(), Card.SCARD_SHARE_SHARED,
            //          Card.SCARD_PROTOCOL_T0 | Card.SCARD_PROTOCOL_T1, ref hCard, ref Protocol);

            if (retCode != ACR122uCard.SCARD_S_SUCCESS)
            {
                //MessageBox.Show(Card.GetScardErrMsg(retCode), "Card not available");
                this.Text = ACR122uCard.GetScardErrMsg(retCode).ToString() + " " + "Card not available";
                connActive = false;
                return false;


            }

            if (retCode1 != ACR122uCard.SCARD_S_SUCCESS)
            {
                //MessageBox.Show(Card.GetScardErrMsg(retCode), "Card not available");
                this.Text = ACR122uCard.GetScardErrMsg(retCode).ToString() + " " + "Card not available";
                connActive = false;
                return false;


            }
            return true;

        }

        public bool connectCard(int rdrIndex)
        {

            connActive = true;

            if (rdrIndex == 0)
            {
                //retCode = ACR122uCard.SCardConnect(hContext, rdrStates[rdrIndex].RdrName.ToString(), ACR122uCard.SCARD_SHARE_SHARED,
                  //        ACR122uCard.SCARD_PROTOCOL_T0 | ACR122uCard.SCARD_PROTOCOL_T1, ref hCard, ref Protocol);
            }
            else if (rdrIndex == 1)
            {
                //retCode1 = ACR122uCard.SCardConnect(hContext, rdrStates[rdrIndex].RdrName.ToString(), ACR122uCard.SCARD_SHARE_SHARED,
                //          ACR122uCard.SCARD_PROTOCOL_T0 | ACR122uCard.SCARD_PROTOCOL_T1, ref hCard, ref Protocol);
            }



            if (retCode != ACR122uCard.SCARD_S_SUCCESS)
            {
                //MessageBox.Show(Card.GetScardErrMsg(retCode), "Card not available");
                //this.Text = ACR122uCard.GetScardErrMsg(retCode).ToString() + " " + "Card not available 0 " + retCode.ToString();
                connActive = false;
                return false;
            }

            if (retCode1 != ACR122uCard.SCARD_S_SUCCESS)
            {
                //MessageBox.Show(Card.GetScardErrMsg(retCode), "Card not available");
                this.Text = ACR122uCard.GetScardErrMsg(retCode).ToString() + " " + "Card not available 1 " + retCode.ToString();
                connActive = false;
                return false;
            }
            return true;

        }

        private string getcardUID()//only for mifare 1k cards
        {
            string cardUID = "";
            byte[] receivedUID = new byte[256];
            ACR122uCard.SCARD_IO_REQUEST request = new ACR122uCard.SCARD_IO_REQUEST();
            request.dwProtocol = ACR122uCard.SCARD_PROTOCOL_T1;
            request.cbPciLength = System.Runtime.InteropServices.Marshal.SizeOf(typeof(ACR122uCard.SCARD_IO_REQUEST));
            byte[] sendBytes = new byte[] { 0xFF, 0xCA, 0x00, 0x00, 0x00 }; //get UID command      for Mifare cards
            int outBytes = receivedUID.Length;
            int status = ACR122uCard.SCardTransmit(hCard, ref request, ref sendBytes[0], sendBytes.Length, ref request, ref receivedUID[0], ref outBytes);

            if (status != ACR122uCard.SCARD_S_SUCCESS)
            {
                cardUID = "Error";
            }
            else
            {
                cardUID = BitConverter.ToString(receivedUID.Take(4).ToArray()).Replace("-", string.Empty).ToLower();
            }

            return cardUID;
        }



        string Path = "";

        private clsDataAccess dataAccess;
        public frmMainClient()
        {
            InitializeComponent();

            //StudentQueue = new Queue();
            SelectDevice();
            establishContext();
            //MessageBox.Show(connActive.ToString());
            openDevice();
            dbConn();
            Path = Application.StartupPath.ToString();


            //frmAttendanceSender frmSender = new frmAttendanceSender();
            //frmSender.Show();

            //picStud.Image = Image.FromFile(Application.StartupPath + @"\images\Buensuceso, Aiza C..png");
        }





        private void frmMainClient_Load(object sender, EventArgs e)
        {
            //checkStudentTag("833de0d5");
        }

        private void deviceStatusUpdater(int retVal) { 
            if(retVal == 0){
                //tsslblDevice.Text = "Connected to " + this.RdrState.RdrName.ToString() + " device.";
                //tsslblDevice.Text = "Connected to " + this.rdrStates[0].RdrName.ToString() + " device.";
                tsslblDevice.Image = Z.IconLibrary.FarmFresh.Icon.AcceptButton.GetImage16();
                /*tmrDeviceConn.Enabled = false;
                tmrReadTags.Enabled = true;*/
            }else{
                tsslblDevice.Text = ACR122uCard.GetScardErrMsg(retCode); 
                tsslblDevice.Image = Z.IconLibrary.FarmFresh.Icon.Error.GetImage16();
                /*tmrDeviceConn.Enabled = true;
                tmrReadTags.Enabled = false;*/
            }
        }

        private void openDevice() {
            if (!connActive)
            {
                SelectDevice();
                establishContext();
            }


            /*int portNum = -1;

            closeDevice();

            fcmdRetVal = StaticClassReaderA.AutoOpenComPort(ref portNum, ref reader, ref portIndex);

            deviceStatusUpdater(fcmdRetVal);*/
        }
        private void closeDevice(){
            /*int fcmdRetVal = 0x30;
            fcmdRetVal = StaticClassReaderA.CloseComPort();*/
        }

        private void monitorDbConn() { 
            if (dataAccess.dbConnected == false){
                dbConn();
                tmrDbConn.Enabled = true;// Reconnect to the database
            }
        }
        private void dbConn() { // Connecting to the database
            //dataAccess = new clsDataAccess(ConfigurationManager.ConnectionStrings["Local"].ConnectionString);
            dataAccess = new clsDataAccess(ConfigurationManager.ConnectionStrings["Server"].ConnectionString);
            if (dataAccess.dbConnected == true){ // database connection successfully established
                tsslblDbConn.Text = dataAccess.connStatus.ToString();
                tsslblDbConn.Image = Z.IconLibrary.FarmFresh.Icon.DatabaseConnect.GetImage16();
                tmrDbConn.Enabled = false;
            }else{ // database connection error
                tsslblDbConn.Text = dataAccess.connStatus.ToString();
                tsslblDbConn.Image = Z.IconLibrary.FarmFresh.Icon.DatabaseError.GetImage16();
                //tmrDbConn.Enabled = true;
            }
        }
        
        private void checkStudentTag(string tag_id) {
            // check if there is an existing record to the database
            int sqlRetVal = dataAccess.sp_check_stud_info_for_log(tag_id);
            if ( sqlRetVal == 1){ // the system found 1 record 
                lblStudentNumber.Text = dataAccess.student_ID.ToString();
                lblStudentName.Text = dataAccess.student_name.ToString();

                lblYrSec.Text = dataAccess.student_Yr.ToString() + " - " + dataAccess.student_Sec.ToString();

                try
                {
                    using (FileStream stream = new FileStream(Path + @"\images\" + dataAccess.imageFileName, FileMode.Open, FileAccess.Read))
                    //Get a binary reader for the file stream
                    using (BinaryReader reader = new BinaryReader(stream))
                    {
                        //copy the content of the file into a memory stream
                        var memoryStream = new MemoryStream(reader.ReadBytes((int)stream.Length));
                        //make a new Bitmap object the owner of the MemoryStream
                        //Bitmap bmp = new Bitmap(memoryStream);
                        picStud.Image = new Bitmap(memoryStream);
                    }
                }
                catch { }

                bool student_log = checkStudentLogs(dataAccess.student_ID.ToString(), tag_id);

                logStudent(dataAccess.student_ID.ToString(), student_log);
                GC.Collect();
                
            }else if (sqlRetVal == 0){  // the system does not found any record
                lblLogResult.Text = "Student Record does not found.";
            }else {  // the system found error during execution of query
                //monitorDbConn();
            }

            GC.Collect();
        }
        private bool checkStudentLogs(string student_ID, string student_Tag){
            int sqlRetVal = dataAccess.sp_search_student_logs_for_log(student_ID, student_Tag);
            if (sqlRetVal == 0){ // student not yet logged to the system
                return false;
            }else if(sqlRetVal == 1) { // the system found a record .
                return true;
            }
            return false;
        }
        int terminal = 0;
        private void logStudent(string student_ID, bool hasRecord) {
            if(terminal >= 1){
                terminal = 0;
            }
            //string curTime = double.Parse(DateTime.Now.Hour.ToString()) + (double.Parse( DateTime.Now.Minute.ToString())/60);
            double dblTime = double.Parse(DateTime.Now.Hour.ToString()) + double.Parse(((double)DateTime.Now.Minute / 60).ToString("0.00"));
//            MessageBox.Show(dblTime.ToString() + hasRecord.ToString());

            //MessageBox.Show(DateTime.Parse("08:08").ToString("HH:mm") +  DateTime.Parse(DateTime.Now.ToLongTimeString()).ToString("HH:mm"));
            //double timein = double.Parse(DateTime.Parse(dataAccess.slog_am_in).ToString("HH")) + (double.Parse(DateTime.Parse(dataAccess.slog_am_in).ToString("mm")) / 60);
            //double timein = double.Parse(DateTime.Parse("08:50").ToString("HH")) + (double.Parse(DateTime.Parse("08:50").ToString("mm")) / 60);
            //MessageBox.Show((dblTime - timein).ToString());
            //MessageBox.Show(dataAccess.slog_am_in + " " + dataAccess.slog_am_out);


            if (hasRecord == false) // no record yet 
            {
                if (dblTime <= 10)//am in
                {
                    if (dataAccess.sp_log_student(student_ID, 0, 1, hasRecord)) lblLogResult.Text = "Successfully logged in";
                }
                else if (dblTime >= 11 && dblTime <= 13.99)//pm in no record yet
                {
                    if (dataAccess.sp_log_student(student_ID, 2, 1, hasRecord)) lblLogResult.Text = "Successfully logged in";
                }
                else if (dblTime >= 14)
                {
                    if (dataAccess.sp_log_student(student_ID, 3, 1, hasRecord)) lblLogResult.Text = "Successfully logged out";
                }
            }
            else 
            {
                if (dblTime >= 11 && dblTime <= 13.99 && dataAccess.slog_pm_in == "")//pm in has record
                {
                    if (dataAccess.sp_log_student(student_ID, 2, 1, hasRecord)) lblLogResult.Text = "Successfully logged in";
                }
                else if (dblTime >= 14 && dataAccess.slog_pm_out == "")//pm out has record
                {
                    if (dataAccess.sp_log_student(student_ID, 3, 1, hasRecord)) lblLogResult.Text = "Successfully logged out";
                }
            }

            //return;
            //if (hasRecord == false)// student does not have a record for a current date
            //{ 
            //    if(dblTime <= 10.99)
            //    { //am in
            //        if (dataAccess.sp_log_student(student_ID, 0, ++terminal))
            //        {
            //            lblLogResult.Text = "Successfully LoggedIn.";
            //        }
            //        else
            //        {
            //            lblLogResult.Text = "Unable to LogIn";
            //        }
            //    }
            //    else if (dblTime >= 11 && dblTime <= 15)
            //    { //pm in if the system does not found am in
            //        if (dataAccess.sp_log_student(student_ID, 2, ++terminal))
            //        {
            //            lblLogResult.Text = "Successfully LoggedIn";
            //        }
            //        else
            //        {
            //            lblLogResult.Text = "Unable to LogIn";
            //        }
            //    }
            //}
            //else// there is an existing record
            //{
            //    if (dblTime >= 14 && dataAccess.slog_am_out == "" && dataAccess.slog_pm_in == "")
            //    {
            //        if (dataAccess.sp_log_student(student_ID, 3, ++terminal))
            //        {
            //            lblLogResult.Text = "Successfully LoggedOut."; // pm out
            //        }
            //        else
            //        {
            //            lblLogResult.Text = "Unable to LogIn";
            //        }
            //    }
            //    else if (dataAccess.slog_am_in != "" && dataAccess.slog_pm_in == "") //already logged in for the am and no log for pm in
            //    {

            //        double timein = double.Parse(DateTime.Parse(dataAccess.slog_am_in).ToString("HH")) + (double.Parse(DateTime.Parse(dataAccess.slog_am_in).ToString("mm")) / 60);

            //        //MessageBox.Show(dblTime.ToString() + " " + timein.ToString());
            //        if (dblTime - timein <= 2 && dblTime < 11.30)
            //        {
            //            lblLogResult.Text = "Already Logged In";
            //        }
            //        else if (dataAccess.slog_am_out != "")
            //        {
            //            timein = double.Parse(DateTime.Parse(dataAccess.slog_am_out).ToString("HH")) + (double.Parse(DateTime.Parse(dataAccess.slog_am_out).ToString("mm")) / 60);
                        
            //            if (dblTime - timein >= 0.083)
            //            {
            //                if (dataAccess.sp_log_student(student_ID, 2, ++terminal)) // pm in
            //                {
            //                    lblLogResult.Text = "Successfully Logged In";
            //                }
            //                else
            //                {
            //                    lblLogResult.Text = "Already Logged Out";
            //                }
            //            }
            //            else
            //            {
            //                lblLogResult.Text = "Already Logged Out";
            //            }
            //        }
            //        else
            //        {
            //            if (dataAccess.sp_log_student(student_ID, 1, ++terminal)) // am out
            //            {
            //                lblLogResult.Text = "Successfully Logged Out.";
            //            }
            //            else
            //            {
            //                lblLogResult.Text = "Already logged Out";
            //            }
            //        }
            //    }
            //    else
            //    {

            //        double timelog = double.Parse(DateTime.Parse(dataAccess.slog_pm_in).ToString("HH")) + (double.Parse(DateTime.Parse(dataAccess.slog_pm_in).ToString("mm")) / 60);
            //        //MessageBox.Show((timelog).ToString() + " " + dblTime.ToString());
            //        //if (dblTime - timelog >= 2.5 && dataAccess.slog_pm_out == "")
            //        if (dblTime >= 14 && dataAccess.slog_pm_out == "")
            //        {
            //            if (dataAccess.sp_log_student(student_ID, 3, ++terminal))
            //            {
            //                lblLogResult.Text = "Successfully LoggedOut."; // pm out
            //            }
            //            else
            //            {
            //                lblLogResult.Text = "Unable to LogOuta";
            //            }
            //        }
            //        else if (dataAccess.slog_pm_out != "")
            //        {
            //            lblLogResult.Text = "You are already LoggedOut.";
            //        }
            //        else
            //        {
            //            lblLogResult.Text = "Unable to LogOut";
            //        }
            //    }
            //}

            GC.Collect();
        }

        private void tmrReadTags_Tick(object sender, EventArgs e){ // for reading the tags
            //MessageBox.Show(availableReaders[0].ToString() + " " + availableReaders[1].ToString());
            //return;
            try
            {
                if (DeviceCount > 0)
                {
                    retCode = ACR122uCard.SCardConnect(hContext, rdrState1.RdrName.ToString(), ACR122uCard.SCARD_SHARE_SHARED,
                          ACR122uCard.SCARD_PROTOCOL_T0 | ACR122uCard.SCARD_PROTOCOL_T1, ref hCard, ref Protocol);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            if (retCode == ACR122uCard.SCARD_S_SUCCESS)
            {
                string cardUID = getcardUID();
                //this.Text = cardUID; //displaying on text block
                this.Text = ACR122uCard.GetScardErrMsg(retCode);
                if (cardUID != "63000000" && cardUID != "Error")
                {
                    //if (!StudentQueue.Contains(cardUID))
                    //{
                        //StudentQueue.Enqueue(cardUID);

                        checkStudentTag(cardUID);
                    //}
                }
            }

            //try{
            //    if (DeviceCount > 1)
            //    {
            //        retCode1 = ACR122uCard.SCardConnect(hContext, rdrState2.RdrName.ToString(), ACR122uCard.SCARD_SHARE_SHARED,
            //        ACR122uCard.SCARD_PROTOCOL_T0 | ACR122uCard.SCARD_PROTOCOL_T1, ref hCard, ref Protocol);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message.ToString());
            //}
            //if (retCode1 == ACR122uCard.SCARD_S_SUCCESS)
            //{
            //    string cardUID = getcardUID();
            //    //displaying on text block
            //    if (cardUID != "63000000" && cardUID != "Error")
            //    {
            //        //if (!StudentQueue.Contains(cardUID))
            //        //{
            //            //StudentQueue.Enqueue(cardUID);
            //            checkStudentTag(cardUID);
            //        //}
            //    }
            //}
            //retCode = 0;
            //retCode1 = 0;
            //hCard = 0;
            //Protocol = 0;
            //hContext = 0;

        }

        private void tmrDeviceConn_Tick(object sender, EventArgs e)
        {
            //deviceStatusUpdater(retCode);
            //if (retCode == 0)
            //{
            //    tmrReadTags.Enabled = true;
            //    tmrDeviceConn.Enabled = false;
            //}
            //else
            //{
            //    openDevice();
            //}
        }

        private void frmMainClient_FormClosing(object sender, FormClosingEventArgs e)
        {
            //closeDevice();
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void tmrDbConn_Tick(object sender, EventArgs e)
        {
            dbConn();
        }
        double current = double.Parse(string.Format("{0:0.00}", (double.Parse(DateTime.Now.ToString("mm")) / 60)));
        double currents = double.Parse(string.Format("{0:0.00}", (double.Parse(DateTime.Now.ToString("ss")))));

        DateTime dtr = DateTime.Now;
        private void tmrTime_Tick(object sender, EventArgs e)
        {

            lblTime.Text = string.Format("{0}", DateTime.Now.ToLongTimeString());
            lblDate.Text = string.Format("{0}", DateTime.Now.ToLongDateString());
            var dateTwo = DateTime.Now;
            double diffs = dateTwo.Subtract(dtr).TotalSeconds;


            if (diffs >= 120 && diffs <= 123)
            {
                Application.Restart();
            }
        }

        private void lblLogResult_TextChanged(object sender, EventArgs e)
        {
        }

        private void tmrLogResult_Tick(object sender, EventArgs e)
        {
        }

        private void bgwReadTags_DoWork(object sender, DoWorkEventArgs e)
        {
            
        }

        private void tmrQueue_Tick(object sender, EventArgs e)
        {
            lblLogResult.Text = "";
            lblStudentName.Text = "";
            lblStudentNumber.Text = "";
            lblTagID.Text = "";
            lblYrSec.Text = "";
            picStud.Image = null;
            //picStud.Dispose();
            //if(StudentQueue.Count > 0){
            //    string rfid = StudentQueue.Dequeue().ToString();
            //    lblTagID.Text = rfid;
            //    checkStudentTag(rfid);

            //}
        }

        private void tsslblDevice_DoubleClick(object sender, EventArgs e)
        {
        }

        private void tsslblDevice_Click(object sender, EventArgs e)
        {
            SelectDevice();
            establishContext();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void tmrRefresh_Tick(object sender, EventArgs e)
        {
        }
    }
}
