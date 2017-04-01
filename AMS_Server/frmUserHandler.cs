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
    public partial class frmUserHandler : Form
    {
        public string user_id = "";
        DataSet DS = new DataSet();
        clsData_Access DataAccess;
        public frmUserHandler(string id = "")
        {
            InitializeComponent();
            DataAccess = new clsData_Access("server=127.0.0.1;user id=root;password=;database=sms_app");
            if (id != ""){
                user_id = id;
                if (!DataAccess.dbConnected){
                    MessageBox.Show(this, "Unable to connect to database.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                DS = DataAccess.executeQuery("select * from tbl_sys_user where user_id='"+ user_id +"'");

                tbUn.Text = DS.Tables[0].Rows[0][1].ToString();
                tbPw.Text = DS.Tables[0].Rows[0][2].ToString();
                tbFn.Text = DS.Tables[0].Rows[0][3].ToString();
                tbMn.Text = DS.Tables[0].Rows[0][4].ToString();
                tbLn.Text = DS.Tables[0].Rows[0][5].ToString();
               cboRole.SelectedItem = DS.Tables[0].Rows[0][6].ToString();
            }else { 
            
            }
        }

        private void frmUserHandler_Load(object sender, EventArgs e)
        {
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //compare if the value is null
            if (tbFn.Text == "" || tbLn.Text == "" || tbMn.Text == "" || tbPw.Text == "" || tbUn.Text == "" || cboRole.SelectedItem.ToString() == ""){
                MessageBox.Show(this, "Please fill all required fields.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            if (user_id == ""){ //for adding new user
                DS = DataAccess.executeQuery("select * from tbl_sys_user where user_un ='" + tbUn.Text.Trim() + "' and user_active=1");
                if (DS.Tables[0].Rows.Count > 0){
                    MessageBox.Show(this, "Usename already in used. Please change the username.", "Existing", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }
                if (DataAccess.ExecuteProcedureCommand_Handle_User(tbUn.Text.Trim(), tbPw.Text.Trim(), tbFn.Text.Trim(), tbMn.Text.Trim(), tbLn.Text.Trim(), cboRole.SelectedItem.ToString())){
                    MessageBox.Show(this, "User successfully added", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }else{
                    MessageBox.Show(this, "Error in adding new user.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }else { //for editing an existing user
                DS = DataAccess.executeQuery("select * from tbl_sys_user where user_un ='" + tbUn.Text.Trim() + "' and user_active=1");
                if (DS.Tables[0].Rows.Count > 0){
                    MessageBox.Show(this, "Usename already in used. Please change the username.", "Existing", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }
                if (DataAccess.ExecuteProcedureCommand_Handle_User(tbUn.Text.Trim(), tbPw.Text.Trim(), tbFn.Text.Trim(), tbMn.Text.Trim(), tbLn.Text.Trim(), cboRole.SelectedItem.ToString(),user_id)){
                    MessageBox.Show(this, "User successfully edited", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }else{
                    MessageBox.Show(this, "Error in editing user.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private string ProperCase_Converter(string str){
            return str;
        }
    }
}
