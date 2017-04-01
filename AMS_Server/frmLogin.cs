using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Z.IconLibrary;
namespace AMS_Server
{
    public partial class frmLogin : Form
    {

        public frmMain Main;
        public clsData_Access dataAccess = new clsData_Access("server=127.0.0.1;user id=root;password=;database=sms_app");
        public DataSet ds = new DataSet();
            
        public frmLogin(/*frmMain frm*/)
        {
            InitializeComponent();

            picIcon.Image = Z.IconLibrary.FarmFresh.Icon.LockOpen.GetImage32();
            btnLogin.Image = Z.IconLibrary.FarmFresh.Icon.Key.GetImage16();
            btnCancel.Image = Z.IconLibrary.FarmFresh.Icon.Cancel.GetImage16();
            this.Icon = Z.IconLibrary.FarmFresh.Icon.LockGo.GetIcon32();

            //Main = frm;
            
        }
        frmAnnouncer frm;
        private void frmLogin_Load(object sender, EventArgs e)
        {
            frm = new frmAnnouncer();
            frmAttendanceSender frmAttendance = new frmAttendanceSender();
            frm.Show();
            frmAttendance.Show();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                ds = dataAccess.executeQuery("select * from tbl_sys_user where user_un ='" + txtUsername.Text.Trim() + "' and user_pw='" + txtPassword.Text.Trim() + "' and user_active=1");
                if (ds.Tables[0].Rows.Count > 0)
                { // row that is greater than 1 means there is an existing user in the given username and password 
                    string id, name, role;
                    id = ds.Tables[0].Rows[0][0].ToString();
                    name = ds.Tables[0].Rows[0][3].ToString() + " " + ds.Tables[0].Rows[0][4].ToString() + " " + ds.Tables[0].Rows[0][5].ToString();
                    role = ds.Tables[0].Rows[0][6].ToString();
                    txtUsername.Clear();
                    txtPassword.Clear();
                    this.Hide();
                    this.WindowState = FormWindowState.Minimized;
                    Main = new frmMain(this, id, name, role, frm);
                    Main.Show();
                }
                else
                {
                    MessageBox.Show(this, "Invalid username or password.", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }catch(Exception ex){
                MessageBox.Show(this, ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
