namespace AMS_Server
{
    partial class frmStudentHandler
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel6 = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblRFID = new System.Windows.Forms.Label();
            this.btnChooseRFID = new System.Windows.Forms.Button();
            this.lblFilename = new System.Windows.Forms.Label();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pic = new System.Windows.Forms.PictureBox();
            this.lblNotification = new System.Windows.Forms.Label();
            this.cboSec = new System.Windows.Forms.ComboBox();
            this.cboYr = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.tbPCNumber = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tbLn = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tbMn = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbFn = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbStudIDNumber = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cboRFTagID = new System.Windows.Forms.ComboBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.opnPic = new System.Windows.Forms.OpenFileDialog();
            this.panel6.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic)).BeginInit();
            this.SuspendLayout();
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(45)))), ((int)(((byte)(50)))));
            this.panel6.Controls.Add(this.lblTitle);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel6.Location = new System.Drawing.Point(0, 0);
            this.panel6.Margin = new System.Windows.Forms.Padding(4);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(609, 70);
            this.panel6.TabIndex = 3;
            // 
            // lblTitle
            // 
            this.lblTitle.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.Honeydew;
            this.lblTitle.Location = new System.Drawing.Point(13, 24);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(338, 43);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Add New Student Information";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 70);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(15, 16, 15, 16);
            this.panel1.Size = new System.Drawing.Size(609, 290);
            this.panel1.TabIndex = 4;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblRFID);
            this.groupBox1.Controls.Add(this.btnChooseRFID);
            this.groupBox1.Controls.Add(this.lblFilename);
            this.groupBox1.Controls.Add(this.btnBrowse);
            this.groupBox1.Controls.Add(this.panel2);
            this.groupBox1.Controls.Add(this.lblNotification);
            this.groupBox1.Controls.Add(this.cboSec);
            this.groupBox1.Controls.Add(this.cboYr);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.tbPCNumber);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.tbLn);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.tbMn);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.tbFn);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.tbStudIDNumber);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(15, 16);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(579, 258);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Student Information";
            // 
            // lblRFID
            // 
            this.lblRFID.AutoSize = true;
            this.lblRFID.Location = new System.Drawing.Point(441, 155);
            this.lblRFID.Name = "lblRFID";
            this.lblRFID.Size = new System.Drawing.Size(0, 14);
            this.lblRFID.TabIndex = 26;
            // 
            // btnChooseRFID
            // 
            this.btnChooseRFID.Location = new System.Drawing.Point(444, 184);
            this.btnChooseRFID.Name = "btnChooseRFID";
            this.btnChooseRFID.Size = new System.Drawing.Size(124, 28);
            this.btnChooseRFID.TabIndex = 25;
            this.btnChooseRFID.Text = "RFID...";
            this.btnChooseRFID.UseVisualStyleBackColor = true;
            this.btnChooseRFID.Click += new System.EventHandler(this.btnChooseRFID_Click);
            // 
            // lblFilename
            // 
            this.lblFilename.Location = new System.Drawing.Point(604, 159);
            this.lblFilename.Name = "lblFilename";
            this.lblFilename.Size = new System.Drawing.Size(142, 42);
            this.lblFilename.TabIndex = 24;
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(600, 204);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(146, 28);
            this.btnBrowse.TabIndex = 23;
            this.btnBrowse.Text = "Browse";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.pic);
            this.panel2.Location = new System.Drawing.Point(604, 16);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(142, 140);
            this.panel2.TabIndex = 22;
            // 
            // pic
            // 
            this.pic.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pic.Location = new System.Drawing.Point(0, 0);
            this.pic.Name = "pic";
            this.pic.Size = new System.Drawing.Size(142, 140);
            this.pic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pic.TabIndex = 0;
            this.pic.TabStop = false;
            // 
            // lblNotification
            // 
            this.lblNotification.AutoSize = true;
            this.lblNotification.ForeColor = System.Drawing.Color.Red;
            this.lblNotification.Location = new System.Drawing.Point(25, 212);
            this.lblNotification.Name = "lblNotification";
            this.lblNotification.Size = new System.Drawing.Size(60, 14);
            this.lblNotification.TabIndex = 21;
            this.lblNotification.Text = "Lastname";
            // 
            // cboSec
            // 
            this.cboSec.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSec.FormattingEnabled = true;
            this.cboSec.Items.AddRange(new object[] {
            "Select Section"});
            this.cboSec.Location = new System.Drawing.Point(444, 109);
            this.cboSec.Name = "cboSec";
            this.cboSec.Size = new System.Drawing.Size(124, 22);
            this.cboSec.TabIndex = 19;
            // 
            // cboYr
            // 
            this.cboYr.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboYr.FormattingEnabled = true;
            this.cboYr.Items.AddRange(new object[] {
            "Select Year Level"});
            this.cboYr.Location = new System.Drawing.Point(444, 71);
            this.cboYr.Name = "cboYr";
            this.cboYr.Size = new System.Drawing.Size(124, 22);
            this.cboYr.TabIndex = 18;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(311, 153);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(56, 14);
            this.label8.TabIndex = 15;
            this.label8.Text = "RF Tag ID";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(311, 112);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(46, 14);
            this.label7.TabIndex = 13;
            this.label7.Text = "Section";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(311, 75);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(61, 14);
            this.label6.TabIndex = 11;
            this.label6.Text = "Year Level";
            // 
            // tbPCNumber
            // 
            this.tbPCNumber.Location = new System.Drawing.Point(444, 34);
            this.tbPCNumber.Name = "tbPCNumber";
            this.tbPCNumber.Size = new System.Drawing.Size(124, 22);
            this.tbPCNumber.TabIndex = 10;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(311, 37);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(131, 14);
            this.label5.TabIndex = 9;
            this.label5.Text = "Parent Contact Number";
            // 
            // tbLn
            // 
            this.tbLn.Location = new System.Drawing.Point(156, 155);
            this.tbLn.Name = "tbLn";
            this.tbLn.Size = new System.Drawing.Size(124, 22);
            this.tbLn.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(23, 158);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 14);
            this.label4.TabIndex = 7;
            this.label4.Text = "Lastname";
            // 
            // tbMn
            // 
            this.tbMn.Location = new System.Drawing.Point(156, 115);
            this.tbMn.Name = "tbMn";
            this.tbMn.Size = new System.Drawing.Size(124, 22);
            this.tbMn.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(23, 118);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 14);
            this.label3.TabIndex = 5;
            this.label3.Text = "Middlename";
            // 
            // tbFn
            // 
            this.tbFn.Location = new System.Drawing.Point(156, 76);
            this.tbFn.Name = "tbFn";
            this.tbFn.Size = new System.Drawing.Size(124, 22);
            this.tbFn.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(23, 79);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 14);
            this.label2.TabIndex = 3;
            this.label2.Text = "Firstname";
            // 
            // tbStudIDNumber
            // 
            this.tbStudIDNumber.Location = new System.Drawing.Point(156, 37);
            this.tbStudIDNumber.Name = "tbStudIDNumber";
            this.tbStudIDNumber.Size = new System.Drawing.Size(124, 22);
            this.tbStudIDNumber.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 14);
            this.label1.TabIndex = 1;
            this.label1.Text = "Student ID Number";
            // 
            // cboRFTagID
            // 
            this.cboRFTagID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboRFTagID.FormattingEnabled = true;
            this.cboRFTagID.Items.AddRange(new object[] {
            "Select Tag ID"});
            this.cboRFTagID.Location = new System.Drawing.Point(236, 370);
            this.cboRFTagID.Name = "cboRFTagID";
            this.cboRFTagID.Size = new System.Drawing.Size(124, 22);
            this.cboRFTagID.TabIndex = 20;
            this.cboRFTagID.Visible = false;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(426, 366);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(81, 38);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(513, 366);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(81, 38);
            this.btnCancel.TabIndex = 20;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // opnPic
            // 
            this.opnPic.FileName = "openFileDialog1";
            // 
            // frmStudentHandler
            // 
            this.AcceptButton = this.btnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(609, 410);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel6);
            this.Controls.Add(this.cboRFTagID);
            this.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmStudentHandler";
            this.Padding = new System.Windows.Forms.Padding(0, 0, 0, 50);
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Add Student Inforamtion";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmStudentHandler_FormClosing);
            this.Load += new System.EventHandler(this.frmStudentHandler_Load);
            this.panel6.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pic)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TextBox tbPCNumber;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbLn;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbMn;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbFn;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbStudIDNumber;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.ComboBox cboRFTagID;
        private System.Windows.Forms.ComboBox cboSec;
        private System.Windows.Forms.ComboBox cboYr;
        private System.Windows.Forms.Label lblNotification;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.PictureBox pic;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.Label lblFilename;
        private System.Windows.Forms.OpenFileDialog opnPic;
        private System.Windows.Forms.Button btnChooseRFID;
        private System.Windows.Forms.Label lblRFID;
    }
}