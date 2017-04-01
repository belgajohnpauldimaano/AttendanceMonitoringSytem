using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AMS_Server
{
    public partial class frmAttendanceSender : Form
    {
        public frmAttendanceSender()
        {
            InitializeComponent();
        }
        private clsData_Access dataAccess;
        private DataSet dsSender = new DataSet();
        private SMSSender smssender = new SMSSender();
        private string studentlogtype;
        private SMSDataHandler smsDataHandler;
        private ErrorLogger errLog;
        private void frmAttendanceSender_Load(object sender, EventArgs e)
        {
            this.Hide();
            errLog = new ErrorLogger(@"C:\ProgramData\AMS_Server.txt");
            smsDataHandler = new SMSDataHandler("server=127.0.0.1;user id=root;password=;database=sms_app");
            //dataAccess.setSendDBConnection();
            bgwAttSender.RunWorkerAsync();
            //bgwAttSenderTerminal2.RunWorkerAsync();
            //bgwAttSenderTerminal3.RunWorkerAsync();
            //bgwAttSenderTerminal4.RunWorkerAsync();
            //bgwAttSenderTerminal5.RunWorkerAsync();
        }

        //sending will starts here
        private void bgwSendFx(int terminal) {
            System.Threading.Thread.Sleep(10000);
            result.Clear();
            try
            {
                DataSet ds = new DataSet();
                if (terminal == 1) // this will always triggered. i set the terminal to 1 
                {
                    if (!smsDataHandler.GetNumbers(1, ref ds))
                    {
                        return;
                    }
                }
                else if (terminal == 2)
                {
                    ds = dataAccess.GetNumbers_terminal2(terminal, ref ds);
                }
                else if (terminal == 3)
                {
                    ds = dataAccess.GetNumbers_terminal3(terminal, ref ds);
                }
                else if (terminal == 4)
                {
                    ds = dataAccess.GetNumbers_terminal4(terminal, ref ds);
                }

                if (ds.Tables.Count < 1)
                {
                    return;
                }
                //MessageBox.Show("triggerd " + terminal + " " + ds.Tables.Count.ToString() + " " + ds.Tables[0].Rows.Count.ToString());
                int count = ds.Tables[0].Rows.Count;
                if (count > 0)
                {
                    for (int i = 0; i < count; i++)
                    {
                        //MessageBox.Show(ds.Tables[0].Rows[i][3].ToString() + " " + ds.Tables[0].Rows[i][7].ToString() + " " + ds.Tables[0].Rows[i][4].ToString() + " " + ds.Tables[0].Rows[i][2].ToString() + " " + ds.Tables[0].Rows[i][1].ToString() + " " + i);

                            
                        int r = Sms_Log_Sender(ds.Tables[0].Rows[i][3].ToString(), ds.Tables[0].Rows[i][7].ToString(), ds.Tables[0].Rows[i][4].ToString(), ds.Tables[0].Rows[i][2].ToString(), ds.Tables[0].Rows[i][1].ToString());
                        if(r<0){
                            //return;
                            break;
                        }

                        System.Threading.Thread.Sleep(3000);
                    }
                    string nums = "";
                    int counter = 0;
                    if (result.Count > 0)
                    {
                        foreach (var pair in result)
                        {
                            nums += "'" + pair.Key + "'";
                            if ((result.Count - 1) != counter && result.Count != 1)
                            {
                                nums += ",";
                            }
                            counter++;
                        }
                        smsDataHandler.ExecuteCommand(nums);
                    }
                    //MessageBox.Show("triggerd " + terminal + " " + ds.Tables.Count.ToString() + " " + ds.Tables[0].Rows.Count.ToString() + " Finished : " + DateTime.Now.ToShortTimeString());
                }
                else
                {
                    System.Threading.Thread.Sleep(1000 * 3);
                }
                
                ds.Dispose();
            }
            catch (Exception ex)
            {
                errLog.LogError(String.Format("Date : {2} {3}Message : {0} {3}Stack Trace : {1}{3}", ex.Message.ToString(), ex.StackTrace.ToString(), DateTime.Now.ToString(), Environment.NewLine));
            }
        }
        Dictionary<string, string> result = new Dictionary<string, string>();
        private int Sms_Log_Sender(string parent_number, string student_number, string name, string logtype, string logtime)
        {
            try
            {
                if (logtype == "0")
                {
                    studentlogtype = "has successfully logged in at ";
                }
                else if (logtype == "1")
                {
                    studentlogtype = "has successfully logged out at";
                }
                else if (logtype == "2")
                {
                    studentlogtype = "has successfully logged in at";
                }
                else if (logtype == "3")
                {
                    studentlogtype = "has successfully logged out at";
                }
                //smssender.sendmsg( number , message)
                int x = smssender.sendmsg(parent_number, "ST. JOHN ACADEMY : Your daughter/son " + name + " " + studentlogtype + " " + logtime);
                //MessageBox.Show("Webserver Response "+x.ToString());
                if (x < 0) // seems no internet connection
                {
                    //Thread.Sleep(1000 * 5);
                    //MessageBox.Show("Webserver Response " + x.ToString());
                    return -1;
                }
                else
                {
                    result.Add(student_number, x.ToString());
                }
                //smsDataHandler.Send_Msg(student_number, int.Parse(logtype));
                return 1;
            }
            catch (Exception ex)
            {
                errLog.LogError(String.Format("Date : {2} {3}Message : {0} {3}Stack Trace : {1}{3}", ex.Message.ToString(), ex.StackTrace.ToString(), DateTime.Now.ToString(), Environment.NewLine));
                return -1;
            }
        }

        private void bgwAttSender_DoWork(object sender, DoWorkEventArgs e)
        {
            bgwSendFx(1);
        }

        private void bgwAttSender_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (!bgwAttSender.IsBusy)
            {
                bgwAttSender.RunWorkerAsync();
            }
        }

        private void frmAttendanceSender_FormClosing(object sender, FormClosingEventArgs e)
        {
            bgwAttSender.CancelAsync();
            bgwAttSenderTerminal2.CancelAsync();
            bgwAttSenderTerminal3.CancelAsync();
            bgwAttSenderTerminal4.CancelAsync();
            bgwAttSenderTerminal5.CancelAsync();
            dataAccess.setCloseSendDBConnection();
        }

        private void bgwAttSenderTerminal2_DoWork(object sender, DoWorkEventArgs e)
        {
            bgwSendFx(2);
        }

        private void bgwAttSenderTerminal3_DoWork(object sender, DoWorkEventArgs e)
        {
            bgwSendFx(3);
        }

        private void bgwAttSenderTerminal2_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

            if (!bgwAttSenderTerminal2.IsBusy)
            {
                bgwAttSenderTerminal2.RunWorkerAsync();
            }
        }

        private void bgwAttSenderTerminal3_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (!bgwAttSenderTerminal3.IsBusy)
            {
                bgwAttSenderTerminal3.RunWorkerAsync();
            }
        }

        private void bgwAttSenderTerminal4_DoWork(object sender, DoWorkEventArgs e)
        {
            bgwSendFx(4);
        }

        private void bgwAttSenderTerminal5_DoWork(object sender, DoWorkEventArgs e)
        {
            bgwSendFx(5);
        }

        private void bgwAttSenderTerminal4_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (!bgwAttSenderTerminal4.IsBusy)
            {
                bgwAttSenderTerminal4.RunWorkerAsync();
            }
        }

        private void bgwAttSenderTerminal5_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (!bgwAttSenderTerminal5.IsBusy)
            {
                bgwAttSenderTerminal5.RunWorkerAsync();
            }
        }
    }
}
