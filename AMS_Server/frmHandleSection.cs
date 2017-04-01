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
    public partial class frmHandleSection : Form
    {
        private ClassSection Section;
        private clsData_Access DataAccess ;
        public frmHandleSection()
        {
            InitializeComponent();
            lblTitle.Text = "Add Section";
            DataAccess = new clsData_Access("server=127.0.0.1;user id=root;password=;database=sms_app");
        }
        public frmHandleSection(ClassSection Section)
        {
            InitializeComponent();
            this.Section = Section;
            lblTitle.Text = "Edit Section";
            lblID.Text = Section.sec_id;
            txtSection.Text = Section.sec_name;
            DataAccess = new clsData_Access("server=127.0.0.1;user id=root;password=;database=sms_app");
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (this.Section == null)
            {
                if (DataAccess.ExecuteCommand("insert into tbl_sec values(null, '" + txtSection.Text.Trim() + "', '1')"))
                {
                    MessageBox.Show(this, "New section information successfully added", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else 
                {
                    MessageBox.Show(this, "Error in adding new section information", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                btnCancel.PerformClick();
            }
            else
            {
                Section.sec_name = txtSection.Text.Trim();
                if (DataAccess.ExecuteCommand("update tbl_sec set sec_details='" + Section.sec_name + "' where sec_id =" + Section.sec_id))
                {
                    MessageBox.Show(this, "Section information successfully editted", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(this, "Error in editting section information", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        private void frmHandleSection_Load(object sender, EventArgs e)
        {

        }
    }
}
