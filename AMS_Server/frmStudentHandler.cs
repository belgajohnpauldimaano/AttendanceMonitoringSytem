using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
namespace AMS_Server
{
    public partial class frmStudentHandler : Form
    {

        clsData_Access DataAccess;
        DataSet DS = new DataSet();
        string stud_id = "";
        string old_rfid = "";
        Byte[] img = new Byte[0];
        System.IO.MemoryStream ms;

        public frmStudentHandler(string id = "")
        {
            InitializeComponent();
            DataAccess = new clsData_Access("server=127.0.0.1;user id=root;password=;database=sms_app");
            stud_id = id;
            lblNotification.Text = "";

            DS = DataAccess.executeQuery("select yr_details from tbl_yr where yr_active=1");
            for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
            {
                cboYr.Items.Add(DS.Tables[0].Rows[i][0].ToString());
            }
            cboYr.SelectedIndex = 0;
            DS = DataAccess.executeQuery("select sec_details from tbl_sec where sec_active=1");
            for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
            {
                cboSec.Items.Add(DS.Tables[0].Rows[i][0].ToString());
            }
            cboSec.SelectedIndex = 0;

            if (id != "")
            {
                this.Text = "Edit Student Information";
                lblTitle.Text = "Edit Student Information";


                DS = DataAccess.executeQuery("select * from view_students_information where stud_id='" + id + "'");

                tbStudIDNumber.Text = DS.Tables[0].Rows[0][1].ToString();
                tbFn.Text = DS.Tables[0].Rows[0][2].ToString();
                tbMn.Text = DS.Tables[0].Rows[0][3].ToString();
                tbLn.Text = DS.Tables[0].Rows[0][4].ToString();
                tbPCNumber.Text = DS.Tables[0].Rows[0][5].ToString();
                cboYr.SelectedItem =  DS.Tables[0].Rows[0][6].ToString();
                cboSec.SelectedItem =  DS.Tables[0].Rows[0][7].ToString();
                old_rfid = DS.Tables[0].Rows[0][8].ToString();
                lblRFID.Text = DS.Tables[0].Rows[0][8].ToString();
                cboRFTagID.SelectedItem = DS.Tables[0].Rows[0][8].ToString();
                
                //cboRFTagID.Items.Add(DS.Tables[0].Rows[0][8].ToString());
                try
                {
                    img = (Byte[])(DS.Tables[0].Rows[0][12]);
                    ms = new System.IO.MemoryStream(img);
                    pic.Image = Image.FromStream(ms);
                    img = null;
                }
                catch { }
            }

        }

        private void frmStudentHandler_Load(object sender, EventArgs e)
        {
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            DS = DataAccess.executeQuery("select stud_id from tbl_student_info where stud_id_number='" + tbStudIDNumber.Text.Trim() + "'");
            if (/*cboRFTagID.SelectedIndex == 0 ||*/ cboSec.SelectedIndex == 0 || cboYr.SelectedIndex == 0)
            {
                lblNotification.Text = "Please select Year level or section.";
            }
            else if (tbStudIDNumber.Text.Trim() == "" || tbPCNumber.Text.Trim() == "" ||  tbLn.Text.Trim() == "" || tbFn.Text.Trim() == "")
            {
                lblNotification.Text = "Please fill all fields.";
            }
            else if (DS.Tables[0].Rows.Count > 0 && stud_id == "")
            {
                lblNotification.Text = "Student ID Number already existing.";
            }
            else if (lblRFID.Text == "")
            {
                lblNotification.Text = "Student should have RFID.";

            }
            else {

                lblNotification.Text = "";
                string rfid = "", yr = "", sec = "", rfid_id = "";


                //DS = DataAccess.executeQuery("select rfid_id from tbl_rfid_tag where rfid_tag_id='" + cboRFTagID.SelectedItem.ToString() + "' and rfid_tag_active=1");
                //rfid = DS.Tables[0].Rows[0][0].ToString();

                DS = DataAccess.executeQuery("select sec_id from tbl_sec where sec_details='" + cboSec.SelectedItem.ToString() + "' and sec_active=1");
                sec = DS.Tables[0].Rows[0][0].ToString();
                
                DS = DataAccess.executeQuery("select yr_id from tbl_yr where yr_details='" + cboYr.SelectedItem.ToString() + "' and yr_active=1");
                yr = DS.Tables[0].Rows[0][0].ToString();

                DataAccess.ExecuteCommand("insert tbl_rfid_tag values(null, '"+ lblRFID.Text +"', '1', '1')");


                DS = DataAccess.executeQuery("select rfid_id from tbl_rfid_tag where rfid_tag_id='" + lblRFID.Text + "' and rfid_tag_active='1'");
                rfid_id = DS.Tables[0].Rows[0][0].ToString();

                if (stud_id == "")
                {
                    if (DataAccess.ExecuteCommand("insert into tbl_student_info values(null,'" + tbStudIDNumber.Text.Trim() + "','" + tbFn.Text.Trim() + "','" + tbMn.Text.Trim() + "','" + tbLn.Text.Trim() + "','" + tbPCNumber.Text.Trim() + "',1,'" + yr + "','" + sec + "','" + rfid_id + "',null)"))
                    {
                        DataAccess.ExecuteCommand("update tbl_rfid_tag set rfid_tag_assigned=1 where rfid_id='" + rfid + "'");


                        //System.IO.MemoryStream mstream = new System.IO.MemoryStream();
                        //pic.Image.Save(mstream, System.Drawing.Imaging.ImageFormat.Png);
                        //byte[] arrImg = mstream.GetBuffer();
                        //mstream.Close();
                        //mstream.Flush();
                        //mstream.Dispose();
                        //DataAccess.sp_update_img(arrImg, tbStudIDNumber.Text);
                        //arrImg = null;
                        //GC.Collect();
                        MessageBox.Show(this, "New student information succesfully added.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show(this, "Error in adding student information", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else 
                {
                    if (DataAccess.ExecuteCommand("update tbl_student_info set stud_id_number='" + tbStudIDNumber.Text.Trim() + "',stud_fn='" + tbFn.Text.Trim() + "',stud_mn='" + tbMn.Text.Trim() + "',stud_ln='" + tbLn.Text.Trim() + "',stud_parent_number='" + tbPCNumber.Text.Trim() + "',stud_active=1,yr_id='" + yr + "',sec_id='" + sec + "',rfid_id='" + rfid_id + "' where stud_id='" + stud_id + "'"))
                    {
                        if (old_rfid != lblRFID.Text) //cboRFTagID.SelectedItem.ToString())
                        {

                            DataAccess.ExecuteCommand("update tbl_rfid_tag set rfid_tag_assigned=0 where rfid_tag_id='" + old_rfid + "' and rfid_tag_active=1");
                            DataAccess.ExecuteCommand("update tbl_rfid_tag set rfid_tag_assigned=1 where rfid_id='" + rfid + "'");
                        }
                        MessageBox.Show(this, "Student information succesfully edited.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //if (pic.Image != null)
                        //{
                        //    System.IO.MemoryStream mstream = new System.IO.MemoryStream();
                        //    pic.Image.Save(mstream, System.Drawing.Imaging.ImageFormat.Bmp);
                        //    byte[] arrImg = mstream.GetBuffer();
                        //    mstream.Close();
                        //    mstream.Flush();
                        //    mstream.Dispose();
                        //    DataAccess.sp_update_img(arrImg, tbStudIDNumber.Text);
                        //    arrImg = null;
                        //}
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show(this, "Error in editing student information", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            opnPic.Filter = "(PNG File )|*.png";
            if (opnPic.ShowDialog() == DialogResult.Cancel)
            {
                //return;
            }
            else
            {
                System.IO.FileStream picstream = new System.IO.FileStream(opnPic.FileName, System.IO.FileMode.Open, System.IO.FileAccess.Read);

                using (Image img = Image.FromStream(picstream))
                {
                    //Image img;
                    //img1 = Image.FromStream(picstream);
                    Bitmap img1 = new Bitmap(img);
                    pic.Image = img1;

                    lblFilename.Text = opnPic.FileName.ToString();

                    img1 = null;
                    picstream.Flush();
                    picstream = null;
                    GC.Collect();
                }
            }
        }
        private void btnChooseRFID_Click(object sender, EventArgs e)
        {
            frmChooseRFID frm = new frmChooseRFID();
            frm.ShowDialog();
            if(frm.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                lblRFID.Text = frm.rfid;
            }
        }

        private void frmStudentHandler_FormClosing(object sender, FormClosingEventArgs e)
        {
            pic.Image = null;
            DataAccess.Dispose();
            DataAccess = null;
            pic.Dispose();
            DS.Dispose();
            img = null;

            Dispose(true);
            GC.Collect();
            this.Dispose();
        }
    }
}
