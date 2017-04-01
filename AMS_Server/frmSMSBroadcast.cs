using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using MySql.Data.MySqlClient;


namespace AMS_Server
{
    public partial class frmSMSBroadcast : Form
    {
        //clsData_Access dataAccess;


        MySqlConnection con = new MySqlConnection("server=127.0.0.1;user id=root;password=;database=sms_app");
        MySqlCommand cmd;
        MySqlCommand cmdupdater;
        MySqlDataAdapter da;
        DataSet ds = new DataSet();
        string message = "";
        
        public frmSMSBroadcast()
        {
            InitializeComponent();
            //GsmCommMain.MessageEventHandler msgeventhadler = new GsmCommMain.MessageEventHandler( this.msghandler) ;

            //comm.MessageSendComplete += ;
            
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void rtbMessage_TextChanged(object sender, EventArgs e)
        {
            if (rtbMessage.Text.Length > 140)
            {

                btnSend.Enabled = false;
                lblNotification.Text = "Your message exceeds to \n140 characters.";
            }
            else {
                btnSend.Enabled = true;
                lblNotification.Text = "";
            }
        }

        private void frmSMSBroadcast_Load(object sender, EventArgs e)
        {
        }
        //public delegate 
        private void bgwSender_DoWork(object sender, DoWorkEventArgs e)
        {
            
            try
            {
                if (count > 0)
                {
                }
                else
                {
                    BeginInvoke((MethodInvoker)delegate
                    {
                        this.Text = "No item to be send.";
                    });
                }

            }
            catch (Exception ex)
            {
                BeginInvoke((MethodInvoker)delegate
                    {
                        this.Text = ex.Message;
                        //MessageBox.Show(this, ex.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    });
                //Application.Exit();
            }
        }
        string[,] info;

        int count;
        private void btnSend_Click(object sender, EventArgs e)
        {

            if (con.State == ConnectionState.Closed) con.Open();

            cmd = new MySqlCommand("sp_student_contact_num", con);
            cmd.CommandType = CommandType.StoredProcedure;

            da = new MySqlDataAdapter(cmd);
            ds.Reset();
            da.Fill(ds);
            count = ds.Tables[0].Rows.Count;
            info = new string[count, count];
            
            for (int i = 0; i < count; i++)
            {
                info[i,0] = ds.Tables[0].Rows[i][0].ToString();
                info[i,1] = ds.Tables[0].Rows[i][1].ToString();
            }
            message = rtbMessage.Text;
            if(!bgwSender.IsBusy) bgwSender.RunWorkerAsync();
        }

        private void bgwSender_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //if (!bgwSender.IsBusy) bgwSender.RunWorkerAsync();
        }

        private void bgwSender_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.Text = e.ProgressPercentage.ToString();
        }

        private void frmSMSBroadcast_FormClosing(object sender, FormClosingEventArgs e)
        {
        }
    }
}
