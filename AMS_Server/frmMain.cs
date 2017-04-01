using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;


using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;

using Z.IconLibrary.FarmFresh;
//using GsmComm.GsmCommunication;
//using GsmComm.RS232;
//using GsmComm.PduConverter;

using WindowsFormsControlLibrary1;
namespace AMS_Server
{
    public partial class frmMain : Form
    {
        //private GsmCommMain smsComm;
        //SmsSubmitPdu pdu;
        //private string isModemOrGW = ""; // modem or gateway


        private DataSet DS = new DataSet();
        private clsData_Access dataAccess;

        public string userRole = "";
        public string userName = "";
        public string userID = "";

        private string studentRecord;
        string studentlogtype;


       

        frmLogin frmLog;
        frmAnnouncer frmannouncer;
        private void resetImgBtn()
        {
            
        }
        private void resetCBtn(CustomButton cbtn) {
            cbtnStudentLogs.resetBtn(cbtn);
            cbtnStudentInfo.resetBtn(cbtn);
            cbtnSettings.resetBtn(cbtn);
            cbtnReports.resetBtn(cbtn);
            cbtnAbout.resetBtn(cbtn);
            cbtnBroadcast.resetBtn(cbtn);
        }

        public frmMain(Form _frmlog, string _userid, string _name, string _role, Form announcer)
        {
            InitializeComponent();

            this.userID = _userid;
            this.userName = _name;
            this.userRole = _role;
            frmLog =  (frmLogin)_frmlog;
            frmannouncer = (frmAnnouncer)announcer;
            resetImgBtn();

            if(this.userRole != "Administrator"){
                cbtnSettings.Enabled = false;
            }

            
            //greet
            lblGreet.Text  = "Welcome " + this.userName + "! You are logged in as system " + userRole.ToLower();

            //ready
            tsslblHover.Text = "Ready";
            

            cboTagStatus.SelectedIndex = 0;
            
            pnlSettingsContiner.Dock = DockStyle.Fill;
            pnlStudentInformation.Dock = DockStyle.Fill;
            pnlStudentLogs.Dock = DockStyle.Fill;
            pnlMsgBroadcaster.Dock = DockStyle.Fill;
            pnlStudentLogs.BringToFront();
            //pnlMsgBroadcaster.BringToFront();
            //settings icon
            
            //settings header icon
            picSettingsImage.Image = Z.IconLibrary.FarmFresh.Icon.SettingTools.GetImage32();
            picSettingsSubImage.Image = Z.IconLibrary.FarmFresh.Icon.Information.GetImage32();

            //user icon
            btnUserAdd.Image = Z.IconLibrary.FarmFresh.Icon.UserAdd.GetImage16();
            btnUserEdit.Image = Z.IconLibrary.FarmFresh.Icon.UserEdit.GetImage16();
            btnUserDeactivate.Image = Z.IconLibrary.FarmFresh.Icon.UserDelete.GetImage16();

            //tag icon
            btnTagAdd.Image = Z.IconLibrary.FarmFresh.Icon.TagBlueAdd.GetImage16();
            btnTagDelete.Image = Z.IconLibrary.FarmFresh.Icon.TagBlueDelete.GetImage16();
            btnTagSearch.Image = Z.IconLibrary.FarmFresh.Icon.ThreeTags.GetImage16();

            //student info icon
            picStudentImage.Image = Z.IconLibrary.FarmFresh.Icon.UserStudent.GetImage32();
            picStudentSubImage.Image = Z.IconLibrary.FarmFresh.Icon.Information.GetBitmap32();
            btnStudInfoSearch.Image = Z.IconLibrary.FarmFresh.Icon.Magnifier.GetImage16();
            btnStudAdd.Image = Z.IconLibrary.FarmFresh.Icon.Add.GetImage16();
            btnStudDelete.Image = Z.IconLibrary.FarmFresh.Icon.Delete.GetImage16();
            btnStudEdit.Image = Z.IconLibrary.FarmFresh.Icon.EditButton.GetImage16();
            btnStudPrint.Image = Z.IconLibrary.FarmFresh.Icon.PrinterEmpty.GetImage16();

            //student logs

            //student logs header
            picStudentLogsImage.Image = Z.IconLibrary.FarmFresh.Icon.RawAccessLogs.GetImage32();
            picStudentLogsSubImage.Image = Z.IconLibrary.FarmFresh.Icon.Information.GetImage32();


            //student section 
            btnSecAdd.Image = Z.IconLibrary.FarmFresh.Icon.Add.GetImage16();
            btnSearchSection.Image = Z.IconLibrary.FarmFresh.Icon.Add.GetImage16();
            btnSecDeactivate.Image = Z.IconLibrary.FarmFresh.Icon.Delete.GetImage16();
            btnSecEdit.Image = Z.IconLibrary.FarmFresh.Icon.EditButton.GetImage16();

            picBroadcastImage.Image = Z.IconLibrary.FarmFresh.Icon.OmsNewTextMessage.GetImage32();
            picBroadcastsubimage.Image = Z.IconLibrary.FarmFresh.Icon.Information.GetImage32();


            btnAnnounceAdd.Image = Z.IconLibrary.FarmFresh.Icon.Add.GetImage16();
            btnAnnounceDelete.Image = Z.IconLibrary.FarmFresh.Icon.Delete.GetImage16();
            btnAnnounceEdit.Image = Z.IconLibrary.FarmFresh.Icon.EditButton.GetImage16();
            btnAnnounceSearch.Image = Z.IconLibrary.FarmFresh.Icon.Magnifier.GetBitmap16();


            //setting the modem
            //sms_com_connector();
        }


        private void frmMain_Load(object sender, EventArgs e)
        {
            // make a new instance of clsData_Access
            dataAccess = new clsData_Access("server=127.0.0.1;user id=root;password=;database=sms_app");
            //dataAccess = new clsData_Access("server=169.254.207.9;user id=root;password=;database=sms_app");
            try
            {
                if (dataAccess.dbConnected) //determine if the connection to database was successfully configured
                {
                    DS =  dataAccess.executeQuery("select yr_details from tbl_yr where yr_active=1");
                    for (int i = 0; i < DS.Tables[0].Rows.Count;i++ )
                    {
                        cboStudYrFilter.Items.Add(DS.Tables[0].Rows[i][0].ToString());
                        cboStudYrToFilter.Items.Add(DS.Tables[0].Rows[i][0].ToString());

                        cboSLogYrFromFilter.Items.Add(DS.Tables[0].Rows[i][0].ToString());
                        cboSLogYrToFilter.Items.Add(DS.Tables[0].Rows[i][0].ToString());
                    }
                    cboStudYrFilter.SelectedIndex = 0;
                    cboStudYrToFilter.SelectedIndex = 0;

                    cboSLogYrFromFilter.SelectedIndex = 0;
                    cboSLogYrToFilter.SelectedIndex = 0;

                    DS = dataAccess.executeQuery("select sec_details from tbl_sec where sec_active=1");
                    for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                    {
                        cboStudSecFilter.Items.Add(DS.Tables[0].Rows[i][0].ToString());
                        cboSLogSecFilter.Items.Add(DS.Tables[0].Rows[i][0].ToString());
                    }
                    cboStudSecFilter.SelectedIndex = 0;
                    cboSLogSecFilter.SelectedIndex = 0;


                    cboAnnounceGrade.SelectedIndex = 0;
                }
                //if (!bgwSender.IsBusy) bgwSender.RunWorkerAsync();
            }
            catch (Exception ex) { //catche if not
                MessageBox.Show(this,ex.Message.ToString(),"Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }

            //status strip for date and time width
            //tsslblDateTime.Width = this.Width - tsslblHover.Width;
        }

        private void btnTagSearch_Click(object sender, EventArgs e)
        {

            btnTagDelete.Enabled = (chkTagActive.Checked ? true : false);//disable or enable button
            string assigned = (cboTagStatus.SelectedItem.ToString() == "All" ? "" : (cboTagStatus.SelectedItem.ToString() == "Assigned" ? "1" : "0"));
            if (dataAccess.dbConnected) //determine if the connection to database was successfully configured
            {
                DS = dataAccess.executeQuery("select * from tbl_rfid_tag where rfid_tag_id like '" + tbTagID.Text.Trim() + "%' and rfid_tag_active='" + (chkTagActive.Checked ? 1 : 0) + "' and rfid_tag_assigned like '%" + assigned + "%'");
                blvwTagList.SuspendSort();
                blvwTagList.Items.Clear();
                if (DS.Tables[0].Rows.Count > 0)
                {

                    blvwTagList.BeginUpdate();
                    for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                    {
                        blvwTagList.Items.Add(DS.Tables[0].Rows[i][0].ToString());
                        blvwTagList.Items[blvwTagList.Items.Count - 1].SubItems.Add(DS.Tables[0].Rows[i][1].ToString());
                    }
                    blvwTagList.EndUpdate();
                }   
            }
        }

        private void btnTagDelete_Click(object sender, EventArgs e)
        {
            try
            {
                string id = blvwTagList.SelectedItems[0].Text;
                if (dataAccess.dbConnected) //determine if the connection to database was successfully configured
                {
                    DS = dataAccess.executeQuery("select * from tbl_rfid_tag where rfid_id='" + id + "' and rfid_tag_assigned=1");
                    if (DS.Tables[0].Rows.Count > 0) //this code is used for determining if the tag is currently assigned to student
                    {
                        MessageBox.Show(this, "Unable to deactivate tag, it is currently assigned to student.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    //this line is used to confirm the deactivation of tag id
                    if (MessageBox.Show(this, "Are you sure you want to deactivate the tag id?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                    {
                        // deactivation of tag id
                        dataAccess.executeQuery("update tbl_rfid_tag set rfid_tag_active=0 where rfid_id = " + id);
                        MessageBox.Show(this, "Tag ID successfully deactivated", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        btnTagSearch.PerformClick();
                    }
                }
            }
            catch //catch the error if there is 
            {
                MessageBox.Show(this, "You must first select item to list", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnTagAdd_Click(object sender, EventArgs e)
        {
            frmAddTag frm = new frmAddTag();
            frm.ShowDialog();
            btnTagSearch.PerformClick();
        }

        private void btnUserSearch_Click(object sender, EventArgs e)
        {
            if (dataAccess.dbConnected) //determine if the connection to database was successfully configured
            {
                DS = dataAccess.executeQuery("select * from tbl_sys_user where (user_un like '" + tbUserSearch.Text.Trim() + "%' or user_fn like '" + tbUserSearch.Text.Trim() + "%' or user_mn like '" + tbUserSearch.Text.Trim() + "%' or user_ln like '" + tbUserSearch.Text.Trim() + "%' ) and user_role like '" + (cboUserRole.Text == "User" ? "User" : (cboUserRole.Text == "Administrator" ? "Administrator" : "")) + "%' and user_active=1");
                blvwUserList.SuspendSort();
                blvwUserList.Items.Clear();
                if (DS.Tables[0].Rows.Count > 0)
                {
                    blvwUserList.BeginUpdate();
                    for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                    {
                        blvwUserList.Items.Add(DS.Tables[0].Rows[i][0].ToString());
                        blvwUserList.Items[blvwUserList.Items.Count - 1].SubItems.Add(DS.Tables[0].Rows[i][1].ToString());
                        blvwUserList.Items[blvwUserList.Items.Count - 1].SubItems.Add(DS.Tables[0].Rows[i][3].ToString());
                        blvwUserList.Items[blvwUserList.Items.Count - 1].SubItems.Add(DS.Tables[0].Rows[i][4].ToString());
                        blvwUserList.Items[blvwUserList.Items.Count - 1].SubItems.Add(DS.Tables[0].Rows[i][5].ToString());
                        blvwUserList.Items[blvwUserList.Items.Count - 1].SubItems.Add(DS.Tables[0].Rows[i][6].ToString());
                    }
                    blvwUserList.EndUpdate();
                }

                tbUserSearch.Focus();
            }
        }

        private void btnUserAdd_Click(object sender, EventArgs e)
        {
            frmUserHandler frm = new frmUserHandler("");
            frm.ShowDialog();
            btnUserSearch.PerformClick();
        }

        private void btnUserEdit_Click(object sender, EventArgs e)
        {
            try{
                string id = blvwUserList.SelectedItems[0].Text;
                MessageBox.Show(id);
                frmUserHandler frm = new frmUserHandler("2");
                frm.ShowDialog();
                btnUserSearch.PerformClick();
            }catch{//catch the error if there is 
                MessageBox.Show(this, "You must first select item to list", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void tbUserSearch_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter){
                btnUserSearch.PerformClick();
            }
        }

        private void tbTagID_KeyUp(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {
                btnTagSearch.PerformClick();
            }
        }

        private void btnUserDeactivate_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataAccess.dbConnected) //determine if the connection to database was successfully configured
                {
                    string id = blvwUserList.SelectedItems[0].Text;

                    if (MessageBox.Show(this, "Are you sure you want to delete the user?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                    {
                        dataAccess.ExecuteCommand("update tbl_sys_user set user_active=0 where user_id='" + id + "'");
                        MessageBox.Show(this, "User successfully deactivated", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        btnUserSearch.PerformClick();
                    }
                }

            }
            catch //catch the error if there is 
            {
                MessageBox.Show(this, "You must first select item to list", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnStudInfoSearch_Click(object sender, EventArgs e)
        {
            try
            {
                studentRecord = "select * from view_students_information  where (stud_id_number like '" + tbStudFilter.Text.Trim() + "%' or stud_fn  like '" + tbStudFilter.Text.Trim() + "%' or stud_mn  like '" + tbStudFilter.Text.Trim() + "%' or stud_ln like '" + tbStudFilter.Text.Trim() + "%') and ( yr_details >= '" + cboStudYrFilter.SelectedItem.ToString() + "' and yr_details <= '" + cboStudYrToFilter.SelectedItem.ToString() + "' )" + (cboStudSecFilter.SelectedIndex == 0 ? " and sec_details like '%%'" : " and sec_details = '" + cboStudSecFilter.SelectedItem.ToString() + "'") + " and stud_active='" + (chkStudOld.Checked ? "0" : "1") + "'";
                DS = dataAccess.executeQuery(studentRecord + " " + sOrderBy);//"select * from view_students_information  where (stud_id_number like '" + tbStudFilter.Text.Trim() + "%' or stud_fn  like '" + tbStudFilter.Text.Trim() + "%' or stud_mn  like '" + tbStudFilter.Text.Trim() + "%' or stud_ln like '" + tbStudFilter.Text.Trim() + "%') and ( yr_details >= '" + cboStudYrFilter.SelectedItem.ToString() + "' and yr_details <= '" + cboStudYrToFilter.SelectedItem.ToString() + "' )" + (cboStudSecFilter.SelectedIndex == 0 ? " and sec_details like '%%'" : " and sec_details = '" + cboStudSecFilter.SelectedItem.ToString() + "'") + " and stud_active='" + (chkStudOld.Checked ? "0" : "1") + "'");
                blvwStudList.Items.Clear();
                //blvwStudList.SuspendSort();
                blvwStudList.BeginUpdate();
                for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                {
                    blvwStudList.Items.Add(DS.Tables[0].Rows[i][0].ToString());
                    blvwStudList.Items[blvwStudList.Items.Count - 1].SubItems.Add(DS.Tables[0].Rows[i][1].ToString());
                    blvwStudList.Items[blvwStudList.Items.Count - 1].SubItems.Add(DS.Tables[0].Rows[i][4].ToString() + ", " + DS.Tables[0].Rows[i][2].ToString() + " " + DS.Tables[0].Rows[i][3].ToString());
                    blvwStudList.Items[blvwStudList.Items.Count - 1].SubItems.Add(DS.Tables[0].Rows[i][6].ToString());
                    blvwStudList.Items[blvwStudList.Items.Count - 1].SubItems.Add(DS.Tables[0].Rows[i][7].ToString());
                    blvwStudList.Items[blvwStudList.Items.Count - 1].SubItems.Add(DS.Tables[0].Rows[i][5].ToString());
                    blvwStudList.Items[blvwStudList.Items.Count - 1].SubItems.Add(DS.Tables[0].Rows[i][8].ToString());
                    blvwStudList.Items[blvwStudList.Items.Count - 1].SubItems.Add(DS.Tables[0].Rows[i][9].ToString());
                }
                blvwStudList.EndUpdate();
                if(!System.IO.Directory.Exists(@"C:\ProgramData\AMS_Reports")){
                    System.IO.Directory.CreateDirectory(@"C:\ProgramData\AMS_Reports");
                }
                DS.WriteXml(@"C:\ProgramData\AMS_Reports\Student_Information.xml");
                studentRecord = null;
                //dsstudentinfo = DS;
                lblStudInfoCount.Text = "Showing " + DS.Tables[0].Rows.Count.ToString() + " records.";
                DS.Dispose();
            }
            catch (Exception ex)//catch the error if there is 
            {
                MessageBox.Show(this, ex.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnStudDelete_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < blvwStudList.SelectedItems.Count; i++)
            {
                try
                {
                    string id = blvwStudList.SelectedItems[i].Text;
                    string rfid_tag = blvwStudList.SelectedItems[i].SubItems[7].Text;
                    //string rfid = blvwStudList.SelectedItems[0].SubItems;
                    if (dataAccess.dbConnected) //determine if the connection to database was successfully configured
                    {
                        //this line is used to confirm the deactivation of tag id
                        if (MessageBox.Show(this, "Are you sure you want to delete the record of " + blvwStudList.SelectedItems[i].SubItems[2].Text + " ?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                        {
                            // delete student information
                            if (dataAccess.ExecuteCommand("update tbl_student_info set stud_active=0 where stud_id = " + id))
                            {
                                dataAccess.ExecuteCommand("update tbl_rfid_tag set rfid_tag_active = 0 where rfid_id='" + rfid_tag + "'");
                                MessageBox.Show(this, "Student successfully deleted", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                
                            }
                            else
                            {
                                MessageBox.Show(this, "Error in deleting student information.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
                catch //catch the error if there is 
                {
                    MessageBox.Show(this, "You must first select item to list", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            btnStudInfoSearch.PerformClick();

            //try
            //{
            //    string id = blvwStudList.SelectedItems[0].Text;
            //    string rfid_tag = blvwStudList.SelectedItems[0].SubItems[7].Text;
            //    //string rfid = blvwStudList.SelectedItems[0].SubItems;
            //    if (dataAccess.dbConnected) //determine if the connection to database was successfully configured
            //    {
            //        //this line is used to confirm the deactivation of tag id
            //        if (MessageBox.Show(this, "Are you sure you want to delete the student information?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            //        {
            //            // delete student information
            //            if (dataAccess.ExecuteCommand("update tbl_student_info set stud_active=0 where stud_id = " + id))
            //            {
            //                dataAccess.ExecuteCommand("update tbl_rfid_tag set rfid_tag_active = 0 where rfid_id='" + rfid_tag + "'");
            //                MessageBox.Show(this, "Student successfully deleted", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //                btnStudInfoSearch.PerformClick();
            //            }
            //            else
            //            {
            //                MessageBox.Show(this, "Error in deleting student information.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //            }
            //        }
            //    }
            //}
            //catch //catch the error if there is 
            //{
            //    MessageBox.Show(this, "You must first select item to list", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //}
        }

        private void btnStudAdd_Click(object sender, EventArgs e)
        {
            frmStudentHandler frm = new frmStudentHandler();
            frm.ShowDialog();
            btnStudInfoSearch.PerformClick();
        }

        private void btnStudEdit_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < blvwStudList.SelectedItems.Count;i++ )
            {
                try
                {
                    string id =blvwStudList.SelectedItems[i].Text ;//blvwStudList.CheckedItems[i].Text.ToString();
                    frmStudentHandler frm = new frmStudentHandler(id);
                    frm.ShowDialog();
                    frm.Dispose();
                    //GC.Collect();
                }
                catch //catch the error if there is 
                {
                    MessageBox.Show(this, "You must first select item to list", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }

            //btnStudInfoSearch.PerformClick();
        }
        //DataSet dsstudentinfo = new DataSet();
        private void btnStudPrint_Click(object sender, EventArgs e)
        {
            try
            {
                frmReportLoader frm = new frmReportLoader();
                ReportDocument rd = new ReportDocument();
                rd.Load(AppDomain.CurrentDomain.BaseDirectory.ToString() + @"Reports\rptStudentInformation.rpt");
                //rptStudentInformation rpt = new rptStudentInformation();
                frm.crvRptLoader.ReportSource = rd; //rpt;
                frm.ShowDialog();
                frm.crvRptLoader.Dispose();
                rd.Dispose();
                frm.Dispose();
            }
            catch (Exception ex) {
                MessageBox.Show(this,ex.ToString(),"Error",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
        }

        private void btnSaveSMSTechnology_Click(object sender, EventArgs e)
        {
            //if (radGateway.Checked){
            //    try {

            //        DS = dataAccess.executeQuery("select * from tbl_devices_gateway where device_name = 'gateway'"); // query to know if it has a record for gateway
            //        if (DS.Tables[0].Rows.Count < 1){ // no rows selected.. ready for adding new data
            //            if (isModemOrGW == "modem")
            //            { // if the current device that is using is a modem
            //                dataAccess.ExecuteCommand("update tbl_devices_gateway set device_status='false' where device_name ='modem'"); //set the device status to false
            //            }
            //            dataAccess.ExecuteCommand("insert into tbl_devices_gateway values(null,'gateway','" + txtAPI.Text.Trim() + "','true')"); //add gateway info
            //        }
            //        else{ // it has a selected row.. for updating
            //            if (isModemOrGW == "modem")
            //            { // if the current device that is using is a modem
            //                dataAccess.ExecuteCommand("update tbl_devices_gateway set device_status='false' where device_name ='modem'"); //set the device status to false
            //            }
            //            dataAccess.ExecuteCommand("update tbl_devices_gateway set device_info='" + txtAPI.Text.Trim() + "',device_status='true' where device_name = 'gateway'"); //add gateway info
            //        }
            //        MessageBox.Show(this, "Gateway information successfully updated.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    }catch(Exception ex){
            //        MessageBox.Show(this,ex.Message,"Invalid",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
            //    }
            //}else {
            //    try {
                
            //        DS = dataAccess.executeQuery("select * from tbl_devices_gateway where device_name = 'modem'");
            //        if (DS.Tables[0].Rows.Count < 1){ // no modem yet
            //            if(isModemOrGW == "gateway"){
            //                dataAccess.ExecuteCommand("update tbl_devices_gateway set device_status = 'false' where device_name = 'gateway'");
            //            }
            //            dataAccess.ExecuteCommand("insert into tbl_devices_gateway values(null,'modem','"+ cboAvailablePorts.SelectedItem.ToString() +"','true')");
            //        }else{ // there is an existing row 
            //            if (isModemOrGW == "gateway"){ // set the devices status of gateway to false
            //                dataAccess.ExecuteCommand("update tbl_devices_gateway set device_status = 'false' where device_name='gateway'");
            //            }
            //            dataAccess.ExecuteCommand("update tbl_devices_gateway set device_info='"+ cboAvailablePorts.SelectedItem.ToString() +"',device_status='true' where device_name = 'modem'");
            //            MessageBox.Show(cboAvailablePorts.SelectedItem.ToString());
            //        }

            //        MessageBox.Show(this, "Modem information successfully updated.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show(this, ex.Message, "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //    }
            //}

            //btnSaveSMSTechnology.Enabled = false;
        }

        private void radModem_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void radGateway_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void blvwStudList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        int currentHeaderClicked,lastHeaderClicked;
        string sOrderBy = "",toggleOrder="";


        private void blvwStudList_ColumnClick(object sender, ComponentOwl.BetterListView.BetterListViewColumnClickEventArgs eventArgs)
        {

            if(eventArgs.ColumnHeader.Index > 4){
                return;
            }

            lastHeaderClicked = currentHeaderClicked;
            currentHeaderClicked = eventArgs.ColumnHeader.Index;

            if (lastHeaderClicked == currentHeaderClicked)
            {
                if (toggleOrder == "asc")
                {
                    toggleOrder = "desc";
                }
                else
                {
                    toggleOrder = "asc";
                }
            }
            else
            {
                toggleOrder = "asc";
            }

            switch (eventArgs.ColumnHeader.Index) { 
                case 0:
                    sOrderBy = " stud_id " + toggleOrder;
                    break;
                case 1:
                    sOrderBy = " stud_id_number " + toggleOrder;
                    break;
                case 2 :
                    sOrderBy = " stud_ln " + toggleOrder;
                    break;
                case 3:
                    sOrderBy = " yr_details " + toggleOrder;
                    break;
                case 4:
                    sOrderBy = " sec_details " + toggleOrder;
                    break;
            }

            sOrderBy = "ORDER BY " + sOrderBy;
            btnStudInfoSearch.PerformClick();
        }

        private void btnComPortRefresh_Click(object sender, EventArgs e)
        {
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
        }

        private void cboAvailablePorts_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void btnTestGateway_Click(object sender, EventArgs e)
        {

        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {

            if (MessageBox.Show(this, "Are you sure you want to exit?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                e.Cancel = true;
            }
            else
            {
                //bgwSender.CancelAsync();
                frmLog.Show();
                frmLog.WindowState = FormWindowState.Normal;
            }
        }

        private void chkSLogTodayFilter_CheckedChanged(object sender, EventArgs e)
        {
            if(chkSLogSpecifyDateFilter.Checked){
                pnlSLogDateFilter.Enabled = true;
            }else{
                pnlSLogDateFilter.Enabled = false;
            }
        }

        private void btnSlogSearch_Click(object sender, EventArgs e)
        {
            blvwStudentLogs.Items.Clear();

            string tbfilter = tbSLogFilter.Text.Trim();
            int yrFrom = int.Parse(cboSLogYrFromFilter.SelectedItem.ToString());
            int yrTo = int.Parse(cboSLogYrToFilter.SelectedItem.ToString());
            string sec = cboSLogSecFilter.SelectedItem.ToString();

            DateTime dtFrom;
            DateTime dtTo;
            if (chkSLogSpecifyDateFilter.Enabled){
                dtFrom = dtpSLogDateFromFilter.Value;
                dtTo = dtpSLogDateToFilter.Value;
            }else{
                dtFrom = DateTime.Now.Date;
                dtTo = DateTime.Now.Date;
            }
            tbfilter = tbfilter.Replace("'", "");
            DS = dataAccess.executeQuery("SELECT * FROM view_student_logs_summary WHERE (stud_id_number like '%" + tbfilter + "%' OR stud_fn  like '%" + tbfilter + "%' OR stud_mn like '%" + tbfilter + "%' OR stud_ln like '%" + tbfilter + "%' OR rfid_tag_id like '%" + tbfilter + "%') AND (slog_date >= '" + String.Format("{0: yyyy/MM/dd}", dtFrom) + "' AND slog_date <= '" + String.Format("{0: yyyy/MM/dd}", dtTo) + "') AND (yr_details >='" + yrFrom + "' AND yr_details <= '" + yrTo + "') " + (sec == "All" ? "" : " AND  (sec_details = '" + sec + "')") + (chkSlogActiveFilter.Checked ? " AND stud_active='0'" : " AND stud_active='1'") + " " + SLogOrder);
            if (DS.Tables[0].Rows.Count > 0)
            {
                lblLogsCount.Text = "Showing " + DS.Tables[0].Rows.Count.ToString() + " records.";
                //SLogCurrHeader = 0;
                blvwStudentLogs.SuspendSort();
                blvwStudentLogs.BeginUpdate();
                for (int x = 0; x < DS.Tables[0].Rows.Count;x++ )
                {
                    DataTable dt = DS.Tables[0];

                    blvwStudentLogs.Items.Add(dt.Rows[x][0].ToString());
                    blvwStudentLogs.Items[blvwStudentLogs.Items.Count - 1].SubItems.Add(dt.Rows[x][1].ToString() + " " + dt.Rows[x][2].ToString() + " " + dt.Rows[x][3].ToString());
                    blvwStudentLogs.Items[blvwStudentLogs.Items.Count - 1].SubItems.Add(dt.Rows[x][4].ToString());
                    blvwStudentLogs.Items[blvwStudentLogs.Items.Count - 1].SubItems.Add(dt.Rows[x][5].ToString());
                    //blvwStudentLogs.Items[blvwStudentLogs.Items.Count - 1].SubItems.Add(dt.Rows[x][6].ToString());
                    blvwStudentLogs.Items[blvwStudentLogs.Items.Count - 1].SubItems.Add(dt.Rows[x][8].ToString());
                    blvwStudentLogs.Items[blvwStudentLogs.Items.Count - 1].SubItems.Add(dt.Rows[x][10].ToString());
                    blvwStudentLogs.Items[blvwStudentLogs.Items.Count - 1].SubItems.Add(dt.Rows[x][12].ToString());
                    blvwStudentLogs.Items[blvwStudentLogs.Items.Count - 1].SubItems.Add(dt.Rows[x][14].ToString());
                    blvwStudentLogs.Items[blvwStudentLogs.Items.Count - 1].SubItems.Add(String.Format("{0:MM/dd/yyyy}",dt.Rows[x][16]));

                }
                blvwStudentLogs.EndUpdate();

                if (!System.IO.Directory.Exists(@"C:\ProgramData\AMS_Reports"))
                {
                    System.IO.Directory.CreateDirectory(@"C:\ProgramData\AMS_Reports");
                }
                DS.WriteXml(@"C:\ProgramData\AMS_Reports\Students_Log_Information.xml");
                DS.Dispose();
                tbfilter = null;
                sec = null;
            }
            //DS = dataAccess.sp_search_stud_logs_sum(tbfilter, tbfilter, tbfilter, tbfilter, yrFrom, yrTo, sec, tbfilter, dtFrom, dtTo);
        }

        string SLogsOrderToggle = "", SLogOrder = " ORDER BY stud_id_number  asc";
        int SLogCurrHeader = 0, SLogLastHeader = 0;
        private void blvwStudentLogs_ColumnClick(object sender, ComponentOwl.BetterListView.BetterListViewColumnClickEventArgs eventArgs)
        {
            SLogLastHeader = SLogCurrHeader;
            SLogCurrHeader = eventArgs.ColumnHeader.Index;

            if (SLogLastHeader == SLogCurrHeader)
            {
                if (SLogsOrderToggle == "asc")
                {
                    SLogsOrderToggle = "desc";
                }
                else
                {
                    SLogsOrderToggle = "asc";
                }
            }else {
                SLogsOrderToggle = "asc";
            }
            SLogOrder = " ORDER BY ";
            switch (eventArgs.ColumnHeader.Index) { 
                case 0:
                    SLogOrder += " stud_id_number " + SLogsOrderToggle;
                    break;
                case 1:
                    SLogOrder += "stud_ln " + SLogsOrderToggle;
                    break;
                case 2:
                    SLogOrder += "yr_details " + SLogsOrderToggle;
                    break;
                case 3:
                    SLogOrder += "sec_details " + SLogsOrderToggle;
                    break;
                case 4:
                    SLogOrder += "slog_am_in " + SLogsOrderToggle;
                    break;
                case 5:
                    SLogOrder += "slog_am_out " + SLogsOrderToggle;
                    break;
                case 6:
                    SLogOrder += "slog_pm_in " + SLogsOrderToggle;
                    break;
                case 7:
                    SLogOrder += "slog_pm_out " + SLogsOrderToggle;
                    break;
                case 8:
                    SLogOrder += "slog_date " + SLogsOrderToggle;
                    break;
            }
            btnSlogSearch.PerformClick();
        }

        private void btnSLogPrint_Click(object sender, EventArgs e)
        {
            try
            {
                ReportDocument rd = new ReportDocument();
                //MessageBox.Show(AppDomain.CurrentDomain.BaseDirectory.ToString() + @"Reports\rptStudentsLogInfo.rpt");
                rd.Load(AppDomain.CurrentDomain.BaseDirectory.ToString() + @"Reports\rptStudentsLogInfo.rpt");
                //rptStudentsLogInfo rpt = new rptStudentsLogInfo();
                frmReportLoader frm = new frmReportLoader();
                ParameterValues crpv = new ParameterValues();
                ParameterDiscreteValue dtPeriod = new ParameterDiscreteValue();

                if (chkSLogSpecifyDateFilter.Checked)
                {
                    dtPeriod.Value = "Period from " + String.Format("{0:MM/dd/yyyy}", dtpSLogDateFromFilter.Value) + " To " + String.Format("{0:MM/dd/yyyy}", dtpSLogDateToFilter.Value);
                }
                else
                {
                    dtPeriod.Value = "For the record of " + String.Format("{0:MM/dd/yyyy}", DateTime.Now.Date) + ".";
                }


                crpv.Add(dtPeriod);
                //rpt.DataDefinition.ParameterFields["dtPeriod"].ApplyCurrentValues(crpv);
                rd.DataDefinition.ParameterFields["dtPeriod"].ApplyCurrentValues(crpv);
                frm.crvRptLoader.ReportSource = rd;//rpt;
                frm.ShowDialog();
                rd.Dispose();
                frm.Dispose();
                crpv = null;
                dtPeriod = null;

            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lblChangePass_MouseMove(object sender, MouseEventArgs e)
        {
            tsslblHover.Text = "Change Password";
        }


        private void lblLogout_MouseMove(object sender, MouseEventArgs e)
        {
        }

        private void lblAbout_MouseMove(object sender, MouseEventArgs e)
        {
            tsslblHover.Text = lblAbout.Text; ;
        }

        private void lblChangePass_Click(object sender, EventArgs e)
        {
            frmChangePassword frm = new frmChangePassword(userID);
            frm.ShowDialog();
        }

        private void lblExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lblExit_MouseMove(object sender, MouseEventArgs e)
        {
            tsslblHover.Text = lblExit.Text;
        }

        private void tmrDateTime_Tick(object sender, EventArgs e)
        {
            tsslblDateTime.Text = DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString();
        }
        DataSet dsSender = new DataSet();
        private void bgwSender_DoWork(object sender, DoWorkEventArgs e)
        {
            //DataSet dsx = new DataSet();
            //dsSender = dataAccess.GetNumbers(1);
            //int count = dsSender.Tables[0].Rows.Count;
            //try
            //{
            //    this.statSystem.Items[2].Text = "";
            //}
            //catch { }
            //if (count > 0)
            //{
            //    for (int i = 0; i < count; i++)
            //    {
            //        Sms_Log_Sender(dsSender.Tables[0].Rows[i][3].ToString(), dsSender.Tables[0].Rows[i][7].ToString(), dsSender.Tables[0].Rows[i][4].ToString(), dsSender.Tables[0].Rows[i][2].ToString(), dsSender.Tables[0].Rows[i][1].ToString());
            //        //MessageBox.Show(dsSender.Tables[0].Rows[i][3].ToString() + " " + dsSender.Tables[0].Rows[i][7].ToString()+ " " + dsSender.Tables[0].Rows[i][4].ToString() + " " +  dsSender.Tables[0].Rows[i][2].ToString() + " " + dsSender.Tables[0].Rows[i][1].ToString() + " " + i);
                    
            //            try
            //            {

            //                BeginInvoke((MethodInvoker)delegate
            //                {
            //                        this.statSystem.Items[2].Text = "Sending " + (i + 1) + "/" + count;
            //                });
            //            }
            //            catch
            //            {

            //            }
            //        System.Threading.Thread.Sleep(1000);
            //    }
            //}
            //else {
            //    System.Threading.Thread.Sleep(1000 * 10);
            //}
        }
        SMSSender smssender = new SMSSender();
        private void Sms_Log_Sender(string parent_number,string student_number, string name, string logtype, string logtime) {
            if(logtype == "0"){
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

            int x = smssender.sendmsg(parent_number, "Hello Mr/Mrs " + name + ", your son/daughter " + studentlogtype);
            dataAccess.Send_Msg(student_number, int.Parse(logtype));
            //MessageBox.Show(x.ToString() + "Hello Mr/Mrs " + name + ", your son/daughter " + studentlogtype);
            //MessageBox.Show(x.ToString());
            //if (smssender.sendmsg(parent_number, "Hello Mr/Mrs " + name + ", your son/daughter " + studentlogtype) != 0)
            //{
            //    dataAccess.Send_Msg(student_number, int.Parse(logtype));
            //}
            
            //MessageBox.Show(smssender.itexmo_bal().ToString());
            //string x = smssender.itexmo_bal().ToString();

        }

        private void sms_com_connector()
        {
        //    int i = 0;
        //taas:

        //    try
        //    {
        //        smsComm = new GsmCommMain(3, 9600, 300);
        //        smsComm.Open();
        //        if (!smsComm.IsConnected())
        //        {
        //            goto taas;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(this, ex.Message.ToString() + "\n Please restart the application", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        i++;
        //        //smsComm.Close();
        //        Application.Exit() ;
        //    }
        }
        frmSMSBroadcast frmsmsbroadcast;
        private void picSMSBroadcast_Click(object sender, EventArgs e)
        {

        }

        private void bgwSender_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (!bgwSender.IsBusy) bgwSender.RunWorkerAsync();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void customButton3_Load(object sender, EventArgs e)
        {
        }

        private void customButton1_Load(object sender, EventArgs e)
        {
        }

        private void customButton2_Load(object sender, EventArgs e)
        {
        }

        private void customButton1_Click(object sender, EventArgs e)
        {
        }

        private void cbtnStudentLogs_Click(object sender, EventArgs e)
        {
            pnlStudentLogs.BringToFront();
            resetCBtn((CustomButton) sender);
        }

        private void cbtnStudentInfo_Click(object sender, EventArgs e)
        {
            pnlStudentInformation.BringToFront();
            resetCBtn((CustomButton)sender);
        }

        private void cbtnSettings_Click(object sender, EventArgs e)
        {
            pnlSettingsContiner.BringToFront();
            resetCBtn((CustomButton)sender);
        }

        private void cbtnStudentLogs_Load(object sender, EventArgs e)
        {

        }

        private void cbtnReports_Click(object sender, EventArgs e)
        {
            resetCBtn((CustomButton)sender);
        }

        private void cbtnAbout_Click(object sender, EventArgs e)
        {
            resetCBtn((CustomButton)sender);
            frmAbout frm = new frmAbout();
            frm.ShowDialog();
        }

        private void cbtnBroadcast_Click(object sender, EventArgs e)
        {
            pnlMsgBroadcaster.BringToFront();
            resetCBtn((CustomButton)sender);
        }

        private void btnSearchSection_Click(object sender, EventArgs e)
        {
            btnSecDeactivate.Enabled = (chkSecActive.Checked ? true : false);
            DS = dataAccess.executeQuery("select sec_id, sec_details from tbl_sec where sec_details like '%" + txtSecSearch.Text + "%' and sec_active = " + (chkSecActive.Checked ? "1" : "0"));
            try {
                blvwSection.Items.Clear();
                blvwSection.BeginUpdate();
                foreach(System.Data.DataRow dr in DS.Tables[0].Rows){
                    blvwSection.Items.Add(dr[0].ToString());
                    blvwSection.Items[blvwSection.Items.Count - 1].SubItems.Add(dr[1].ToString());
                }
                blvwSection.EndUpdate();
                DS.Dispose();
            }catch(Exception ex){
                MessageBox.Show(this,ex.Message.ToString(),"Error",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
        }

        private void btnSecAdd_Click(object sender, EventArgs e)
        {
            frmHandleSection frm = new frmHandleSection();
            frm.ShowDialog();
            btnSearchSection.PerformClick();
        }

        private void btnSecEdit_Click(object sender, EventArgs e)
        {
            if (blvwSection.SelectedItems.Count > 0)
            {
                ClassSection section = new ClassSection(blvwSection.SelectedItems[0].Text, blvwSection.SelectedItems[0].SubItems[1].Text);
                frmHandleSection frm = new frmHandleSection(section);
                frm.ShowDialog();
                btnSearchSection.PerformClick();
            }
        }

        private void txtSecSearch_TextChanged(object sender, EventArgs e)
        {
            btnSearchSection.PerformClick();
        }

        private void btnSecDeactivate_Click(object sender, EventArgs e)
        {
            if (blvwSection.SelectedItems.Count > 0)
            {
                if (MessageBox.Show(this, "Are you sure you want to deactivate?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    if (dataAccess.ExecuteCommand("update tbl_sec set sec_active=0 where sec_id=" + blvwSection.SelectedItems[0].Text))
                    {
                        btnSearchSection.PerformClick();
                    }
                    else {
                        MessageBox.Show(this, "There is a problem during deactivating section","Problem", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
            }
        }

        private void btnAnnounceDelete_Click(object sender, EventArgs e)
        {
            if(lvwAnnounceList.SelectedItems.Count > 0){
                if(MessageBox.Show(this,"Are you sure you want to delete the annoucement?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes){
                    Announcement Announce = new Announcement();
                    Announce.m_id = lvwAnnounceList.SelectedItems[0].Text;
                    if(dataAccess.HandleAnnouncement(2,Announce))
                    {
                        MessageBox.Show(this, "Announcement successfully deleted.","Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    btnAnnounceSearch.PerformClick();  
                }
            }
        }

        private void btnAnnounceEdit_Click(object sender, EventArgs e)
        {
            if (lvwAnnounceList.SelectedItems.Count > 0)
            {
                    string m_id = lvwAnnounceList.SelectedItems[0].Text;
                    DataSet ds_announce = new DataSet();
                    ds_announce = dataAccess.executeQuery("select * from tbl_message where m_id = '"+ m_id +"'");
                    Announcement Annoucement = new Announcement();
                    Annoucement.m_id = ds_announce.Tables[0].Rows[0][0].ToString();
                    Annoucement.title = ds_announce.Tables[0].Rows[0][1].ToString();
                    Annoucement.content = ds_announce.Tables[0].Rows[0][2].ToString();
                    Annoucement.senddate = Convert.ToDateTime(ds_announce.Tables[0].Rows[0][3].ToString());
                    Annoucement.recipient = ds_announce.Tables[0].Rows[0][4].ToString();
                    frmHandleAnnoucement frmAnnoucement = new frmHandleAnnoucement(frmannouncer, Annoucement);
                    frmAnnoucement.ShowDialog();
                    ds_announce.Dispose();
                    btnAnnounceSearch.PerformClick();
            }
        }

        private void btnAnnounceAdd_Click(object sender, EventArgs e)
        {
            frmHandleAnnoucement frmAnnoucement = new frmHandleAnnoucement(frmannouncer);
            frmAnnoucement.ShowDialog();
            btnAnnounceSearch.PerformClick();
        }

        private void btnAnnounceSearch_Click(object sender, EventArgs e)
        {
            DataSet DSAnnouncementList = new DataSet();

            lvwAnnounceList.Items.Clear();
            DSAnnouncementList = dataAccess.executeQuery("select * from tbl_message where m_recipient like '%"+ cboAnnounceGrade.SelectedItem.ToString() +"%' and m_title like '%"+ txtAnnounceSearch.Text.Trim() +"%' order by m_senddate desc");
            try { 
                if(DSAnnouncementList.Tables[0].Rows.Count > 0){
                    foreach( DataRow dr in DSAnnouncementList.Tables[0].Rows ){
                        lvwAnnounceList.Items.Add(dr[0].ToString());
                        lvwAnnounceList.Items[lvwAnnounceList.Items.Count - 1].SubItems.Add(dr[1].ToString());
                        string r = dr[4].ToString();
                        string recipient = "All";
                        if (r != "All")
                        {
                            recipient = "";
                            string[] recipients = r.Split(';');
                            foreach (string rec in recipients)
                            {
                                if (rec != "")
                                {
                                    recipient += "Grade " + rec + " ";
                                }
                            }
                        }
                        lvwAnnounceList.Items[lvwAnnounceList.Items.Count - 1].SubItems.Add(recipient);
                        lvwAnnounceList.Items[lvwAnnounceList.Items.Count - 1].SubItems.Add(String.Format("{0:MM/dd/yyyy}", dr[3]));
                        string status = (dr[5].ToString() == "0" ? "Not yet sent" : "Already sent");
                        lvwAnnounceList.Items[lvwAnnounceList.Items.Count - 1].SubItems.Add(status);
                    }
                }
            }
            catch (Exception ex)
            {
            }

            DSAnnouncementList.Dispose();
        }

        private void txtAnnounceSearch_TextChanged(object sender, EventArgs e)
        {
            btnAnnounceSearch.PerformClick();
        }

        private void tbStudFilter_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void tbStudFilter_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.ToString() == "Return")
            {
                btnStudInfoSearch.PerformClick();
            }
        }

        private void cmsStudInfo_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text == "Edit")
            {
                try
                {
                    string id = blvwStudList.SelectedItems[0].Text;//blvwStudList.CheckedItems[i].Text.ToString();
                    frmStudentHandler frm = new frmStudentHandler(id);
                    frm.ShowDialog();
                    frm.Dispose();
                    //GC.Collect();
                }
                catch //catch the error if there is 
                {
                    MessageBox.Show(this, "You must first select item to list", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                } 
            }
        }

    }
}
