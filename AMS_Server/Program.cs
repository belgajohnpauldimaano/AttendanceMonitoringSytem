using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Threading.Tasks;

namespace AMS_Server
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //Application.Run(new frmMain("Guest","Guest",""));
            Application.Run(new frmLogin());
            //Application.Run(new frmAddTag());
            return;
//RESTART_LOGIN:
//            frmLogin frmlogin = new frmLogin();
//            DataSet ds = new DataSet();
//            clsData_Access dataAccess;

//             //make a new instance of clsData_Access
//            try {
                
//                dataAccess = new clsData_Access("server=127.0.0.1;user id=root;password=;database=sms_app");
                
                
//                if (frmlogin.ShowDialog() == DialogResult.OK)
//                {
//                    ds = dataAccess.executeQuery("select * from tbl_sys_user where user_un ='" + frmlogin.txtUsername.Text.Trim() + "' and user_pw='" + frmlogin.txtPassword.Text.Trim() + "' and user_active=1");
//                    if (ds.Tables[0].Rows.Count > 0)
//                    { // row that is greater than 1 means there is an existing user in the given username and password 

//                        Application.Run(new frmMain(ds.Tables[0].Rows[0][6].ToString(), ds.Tables[0].Rows[0][3].ToString() + " " + ds.Tables[0].Rows[0][4].ToString() + " " + ds.Tables[0].Rows[0][5].ToString(), ds.Tables[0].Rows[0][0].ToString()));

//                    }
//                    else
//                    {

//                        MessageBox.Show(frmlogin, "Invalid username or password.", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Information);
//                        goto RESTART_LOGIN;

//                    }
//                }

//            }catch (Exception ex){
//                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
//                Application.Exit();
//            }
        }
    }
}
