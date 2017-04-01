using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace AMS_Server
{
    public partial class frmAnnouncer : Form
    {
        private clsData_Access da = new clsData_Access("server=127.0.0.1;user id=root;password=;database=sms_app");
        public frmAnnouncer()
        {
            InitializeComponent();
        }

        private void frmAnnouncer_Load(object sender, EventArgs e)
        {
            this.Hide();
            if (!bgwBroadcaster.IsBusy)
            {
                bgwBroadcaster.RunWorkerAsync();
            }   
        }
        SMSSender AnnounceSender = new SMSSender();
        DataSet dsMessage = new DataSet();
        DataSet dsStudent = new DataSet();
        private void bgwBroadcaster_DoWork(object sender, DoWorkEventArgs e)
        {
            Thread.Sleep((1000 * 60) * 10);
            if(DateTime.Now.Hour < 4){
                return;
            }
            dsMessage = da.executeQuery("select m_id, m_content, m_recipient from tbl_message where issent=0 and m_senddate='"+ String.Format("{0:yyyy/MM/dd}", DateTime.Now.Date ) +"'");


            if (dsMessage.Tables.Count < 1)
            {
                return;
            }
            //MessageBox.Show(dsMessage.Tables[0].Rows.Count.ToString());
            if (dsMessage.Tables[0].Rows.Count > 0)
            {
                DataTable dt = dsMessage.Tables[0];

                for (int i = 0; i < dsMessage.Tables[0].Rows.Count; i++)
                {
                    string[] years = dt.Rows[i][2].ToString().Split(';');
                    string message = dt.Rows[i][1].ToString();
                    string m_id = dt.Rows[i][0].ToString();

                    if (dt.Rows[i][2].ToString() == "All")
                    {
                        dsStudent = da.executeQuery("select stud_ln, stud_parent_number from tbl_student_info join tbl_yr on tbl_student_info.yr_id = tbl_yr.yr_id and stud_active='1'");

                        if (dsStudent.Tables.Count < 1)
                        {
                            return;
                        }
                        if (dsStudent.Tables[0].Rows.Count > 0)
                        {
                            //MessageBox.Show(dsStudent.Tables[0].Rows.Count.ToString());
                            foreach (DataRow drStud in dsStudent.Tables[0].Rows)
                            {
                                //MessageBox.Show(String.Format("{0} {1}", drStud[1].ToString(), message));
                                int a = AnnounceSender.sendmsg(drStud[1].ToString(), message);
                                //MessageBox.Show(a.ToString());
                                if (a < 0) // seems no internet connection
                                {
                                    //Thread.Sleep(1000 * 100);
                                    //bgwBroadcaster.CancelAsync();
                                    return;
                                }
                                Thread.Sleep(1500);
                            }
                        }
                    }
                    else
                    {
                        foreach (string year in years)
                        {
                            if (year != "")
                            {
                                dsStudent = da.executeQuery("select stud_ln, stud_parent_number from tbl_student_info join tbl_yr on tbl_student_info.yr_id = tbl_yr.yr_id where yr_details='" + year + "' and stud_active='1'");
                                if (dsStudent.Tables[0].Rows.Count > 0)
                                {
                                    //MessageBox.Show(dsStudent.Tables[0].Rows.Count.ToString());
                                    foreach (DataRow drStud in dsStudent.Tables[0].Rows)
                                    {
                                        //MessageBox.Show(drStud[0].ToString());
                                        int a = AnnounceSender.sendmsg(drStud[1].ToString(), message);
                                        //MessageBox.Show(a.ToString());
                                        if (a < 0) // seems no internet connection
                                        {
                                            //Thread.Sleep(1000);
                                            //bgwBroadcaster.CancelAsync();
                                            return;
                                        }
                                        Thread.Sleep(1500);
                                    }
                                }
                            }
                        }
                    }

                    da.ExecuteCommand("update tbl_message set issent='1' where m_id='" + m_id + "'");
                }
            }
            Thread.Sleep((1000 * 60) * 2);
        }

        private void bgwBroadcaster_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (!bgwBroadcaster.IsBusy)
            {
                bgwBroadcaster.RunWorkerAsync();
                //Thread.Sleep(10000);
            }


        }

        private void frmAnnouncer_FormClosing(object sender, FormClosingEventArgs e)
        {
            bgwBroadcaster.CancelAsync();
        }
    }
}
