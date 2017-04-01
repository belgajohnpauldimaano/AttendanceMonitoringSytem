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
    public partial class frmChangePassword : Form
    {
        private string id = "";
        private clsData_Access DA = new clsData_Access("server=127.0.0.1;user id=root;password=;database=sms_app");
        private DataSet DS = new DataSet();
        public frmChangePassword(string id)
        {
            this.id = id;
            InitializeComponent();
        }

        bool xx;
        private void btnSave_Click(object sender, EventArgs e)
        {
            DS = DA.executeQuery("select user_pw from tbl_sys_user where user_id='"+ id +"'");
            if(DS.Tables[0].Rows.Count > 0){
                if (DS.Tables[0].Rows[0][0].ToString() == tbOldPass.Text)
                {
                    if (tbNewPass.Text == tbConfirmPass.Text)
                    {
                         xx = DA.ExecuteCommand("update tbl_sys_user set user_pw='" + tbNewPass.Text + "' where user_id='" + id + "'");

                         MessageBox.Show(this, "Password successfully changed." + xx.ToString(), "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                         this.Close();
                    }
                    else
                    {
                        MessageBox.Show(this, "New password and confirmaiton didn't matched.", "Incorrect", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
                else {
                    MessageBox.Show(this,"Old password is incorrect.","Incorrect",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                }
            }
        }
    }
}
