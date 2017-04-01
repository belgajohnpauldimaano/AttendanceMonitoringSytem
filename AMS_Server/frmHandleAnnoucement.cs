using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace AMS_Server
{
    public partial class frmHandleAnnoucement : Form
    {
        Announcement Announcement = new Announcement();
        bool IsAdd;
        clsData_Access DA = new clsData_Access("server=127.0.0.1;user id=root;password=;database=sms_app");
        public frmHandleAnnoucement(Form announce)
        {
            InitializeComponent();
            IsAdd = true;
            announcer = (frmAnnouncer)announce;
        }
        frmAnnouncer announcer;
        public frmHandleAnnoucement(Form announce,Announcement _Announcement = null)
        {
            InitializeComponent();
            Announcement = _Announcement;
            txtTitle.Text = Announcement.title;
            txtContent.Text = Announcement.content;
            dtpAnnouceDate.Value = new DateTime(Announcement.senddate.Year, Announcement.senddate.Month, Announcement.senddate.Day);
            announcer = (frmAnnouncer)announce;
            if (Announcement.recipient == "All")
            {
                chkAll.Checked = true;
                chklstRecipient.Enabled = false;
            }
            else 
            {

                string[] recipients = Announcement.recipient.Split(';');
                foreach (string rec in recipients)
                {
                    if (rec != "")
                    {
                        for (int a = 0; a < chklstRecipient.Items.Count; a++)
                        {
                            if (chklstRecipient.Items[a].ToString() == "Grade" + rec)
                            {
                                chklstRecipient.SetItemChecked(int.Parse(rec) - 7, true);
                            }
                        }
                    }
                }
            }
            IsAdd = false;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(this, "Warning : The announcement that you entered will\nbe send during the date that was set and cannot be\ninterrupt the sending\nDo you want to save?", "Cofirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.No)
            {
                return;
            }

            Announcement.title = txtTitle.Text.Trim();
            Announcement.content = txtContent.Text.Trim();
            Announcement.senddate = dtpAnnouceDate.Value;
            Announcement.recipient = "";
            if (chkAll.Checked)
            {
                Announcement.recipient = "All";
            }
            else
            {
                foreach (string a in chklstRecipient.CheckedItems)
                {
                    Announcement.recipient += a.Replace("Grade", ";");
                }
                Announcement.recipient += ";";
            }


            if (IsAdd == false) // Add
            {
                if (DA.HandleAnnouncement(1, Announcement))
                {
                    MessageBox.Show(this, "Announcement successfully editted", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(this, "There is an error during execution", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                if (DA.HandleAnnouncement(0, Announcement))
                {
                    MessageBox.Show(this, "Announcement successfully added", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(this, "There is an error during execution", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            if (!announcer.bgwBroadcaster.IsBusy)
            {
                announcer.bgwBroadcaster.RunWorkerAsync();
            }
            this.Close();
        }

        private void chklstRecipient_ItemCheck(object sender, ItemCheckEventArgs e)
        {
        }

        private void frmHandleAnnoucement_Load(object sender, EventArgs e)
        {

        }

        private void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAll.Checked)
            {
                chklstRecipient.Enabled = false;
            }
            else
            {
                chklstRecipient.Enabled = true;
            }
        }

        private void txtContent_TextChanged(object sender, EventArgs e)
        {
            if (txtContent.Text.Length > 400)
            {
                lblMsg.Text = "The message content should not exceeds to 400 characters.";
                btnSave.Enabled = false;
                return;
            }
            lblMsg.Text = "";
            btnSave.Enabled = true;
        }
    }
}
