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
        private clsDataAccess dataAccess;
        private DataSet dsSender = new DataSet();
        private SMSSender smssender = new SMSSender();
        private string studentlogtype;
        private void frmAttendanceSender_Load(object sender, EventArgs e)
        {
            this.Hide();
            dataAccess = new clsDataAccess("server=127.0.0.1;user id=root;password=;database=sms_app");
            dataAccess.setSendDBConnection();
            bgwAttSender.RunWorkerAsync();
            bgwAttSenderTerminal2.RunWorkerAsync();
            bgwAttSenderTerminal3.RunWorkerAsync();
            bgwAttSenderTerminal4.RunWorkerAsync();
            //bgwAttSenderTerminal5.RunWorkerAsync();
        }

        private void bgwSendFx(int terminal) {
            DataSet ds = new DataSet();
            if (dataAccess.dbConnected)
            {
                if (terminal == 1)
                {
                    ds = dataAccess.GetNumbers(terminal, ref ds);
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
                        Sms_Log_Sender(ds.Tables[0].Rows[i][3].ToString(), ds.Tables[0].Rows[i][7].ToString(), ds.Tables[0].Rows[i][4].ToString(), ds.Tables[0].Rows[i][2].ToString(), ds.Tables[0].Rows[i][1].ToString());
                        //MessageBox.Show(dsSender.Tables[0].Rows[i][3].ToString() + " " + dsSender.Tables[0].Rows[i][7].ToString()+ " " + dsSender.Tables[0].Rows[i][4].ToString() + " " +  dsSender.Tables[0].Rows[i][2].ToString() + " " + dsSender.Tables[0].Rows[i][1].ToString() + " " + i);
                        System.Threading.Thread.Sleep(1000);
                    }
                    //MessageBox.Show("triggerd " + terminal + " " + ds.Tables.Count.ToString() + " " + ds.Tables[0].Rows.Count.ToString() + " Finished : " + DateTime.Now.ToShortTimeString());
                }
                else
                {
                    System.Threading.Thread.Sleep(1000 * 3);
                }
            }
        }

        private void Sms_Log_Sender(string parent_number, string student_number, string name, string logtype, string logtime)
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

            int x = smssender.sendmsg(parent_number, "ST. JOHN ACADEMY : Hello Mr/Mrs " + name + ", your son/daughter " + studentlogtype + " " + logtime);

            if (x < 0) // seems no internet connection
            {
                Thread.Sleep(1000 * 5);
                return;
            }
            dataAccess.Send_Msg(student_number, int.Parse(logtype));
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
