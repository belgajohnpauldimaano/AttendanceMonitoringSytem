namespace AMS_Server
{
    partial class frmMainClient
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
            this.components = new System.ComponentModel.Container();
            this.tmrDeviceConn = new System.Windows.Forms.Timer(this.components);
            this.ssStatus = new System.Windows.Forms.StatusStrip();
            this.tsslblDevice = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslblDbConn = new System.Windows.Forms.ToolStripStatusLabel();
            this.tmrReadTags = new System.Windows.Forms.Timer(this.components);
            this.tmrDbConn = new System.Windows.Forms.Timer(this.components);
            this.tmrTime = new System.Windows.Forms.Timer(this.components);
            this.lblLogResult = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.tmrQueue = new System.Windows.Forms.Timer(this.components);
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.lblTime = new System.Windows.Forms.Label();
            this.lblDate = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel7 = new System.Windows.Forms.Panel();
            this.picStud = new System.Windows.Forms.PictureBox();
            this.panel6 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblYrSec = new System.Windows.Forms.Label();
            this.lblStudentName = new System.Windows.Forms.Label();
            this.lblStudentNumber = new System.Windows.Forms.Label();
            this.lblTagID = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tmrRefresh = new System.Windows.Forms.Timer(this.components);
            this.ssStatus.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picStud)).BeginInit();
            this.panel6.SuspendLayout();
            this.SuspendLayout();
            // 
            // tmrDeviceConn
            // 
            this.tmrDeviceConn.Interval = 10000;
            this.tmrDeviceConn.Tick += new System.EventHandler(this.tmrDeviceConn_Tick);
            // 
            // ssStatus
            // 
            this.ssStatus.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsslblDevice,
            this.tsslblDbConn});
            this.ssStatus.Location = new System.Drawing.Point(0, 626);
            this.ssStatus.Name = "ssStatus";
            this.ssStatus.Size = new System.Drawing.Size(998, 22);
            this.ssStatus.SizingGrip = false;
            this.ssStatus.Stretch = false;
            this.ssStatus.TabIndex = 0;
            this.ssStatus.Text = "x";
            // 
            // tsslblDevice
            // 
            this.tsslblDevice.Name = "tsslblDevice";
            this.tsslblDevice.Size = new System.Drawing.Size(42, 17);
            this.tsslblDevice.Text = "Device";
            this.tsslblDevice.Click += new System.EventHandler(this.tsslblDevice_Click);
            this.tsslblDevice.DoubleClick += new System.EventHandler(this.tsslblDevice_DoubleClick);
            // 
            // tsslblDbConn
            // 
            this.tsslblDbConn.Name = "tsslblDbConn";
            this.tsslblDbConn.Size = new System.Drawing.Size(120, 17);
            this.tsslblDbConn.Text = "Database Connection";
            // 
            // tmrReadTags
            // 
            this.tmrReadTags.Enabled = true;
            this.tmrReadTags.Interval = 1000;
            this.tmrReadTags.Tick += new System.EventHandler(this.tmrReadTags_Tick);
            // 
            // tmrDbConn
            // 
            this.tmrDbConn.Interval = 10000;
            this.tmrDbConn.Tick += new System.EventHandler(this.tmrDbConn_Tick);
            // 
            // tmrTime
            // 
            this.tmrTime.Enabled = true;
            this.tmrTime.Interval = 1000;
            this.tmrTime.Tick += new System.EventHandler(this.tmrTime_Tick);
            // 
            // lblLogResult
            // 
            this.lblLogResult.BackColor = System.Drawing.Color.Transparent;
            this.lblLogResult.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblLogResult.Font = new System.Drawing.Font("Segoe UI Light", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLogResult.ForeColor = System.Drawing.Color.Black;
            this.lblLogResult.Location = new System.Drawing.Point(0, 0);
            this.lblLogResult.Name = "lblLogResult";
            this.lblLogResult.Size = new System.Drawing.Size(998, 70);
            this.lblLogResult.TabIndex = 22;
            this.lblLogResult.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblLogResult.TextChanged += new System.EventHandler(this.lblLogResult_TextChanged);
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.Transparent;
            this.panel4.BackgroundImage = global::AMS_Server.Properties.Resources.curve;
            this.panel4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel4.Controls.Add(this.lblLogResult);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel4.Location = new System.Drawing.Point(0, 506);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(998, 120);
            this.panel4.TabIndex = 27;
            // 
            // tmrQueue
            // 
            this.tmrQueue.Enabled = true;
            this.tmrQueue.Interval = 3000;
            this.tmrQueue.Tick += new System.EventHandler(this.tmrQueue_Tick);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Transparent;
            this.panel2.BackgroundImage = global::AMS_Server.Properties.Resources.bar1;
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel2.Controls.Add(this.panel5);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 143);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(998, 120);
            this.panel2.TabIndex = 31;
            // 
            // panel5
            // 
            this.panel5.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.panel5.BackgroundImage = global::AMS_Server.Properties.Resources.bar2;
            this.panel5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel5.Controls.Add(this.lblTime);
            this.panel5.Controls.Add(this.lblDate);
            this.panel5.Location = new System.Drawing.Point(235, 1);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(627, 123);
            this.panel5.TabIndex = 0;
            // 
            // lblTime
            // 
            this.lblTime.BackColor = System.Drawing.Color.Transparent;
            this.lblTime.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTime.Font = new System.Drawing.Font("Calibri Light", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTime.ForeColor = System.Drawing.Color.White;
            this.lblTime.Location = new System.Drawing.Point(0, 55);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(627, 64);
            this.lblTime.TabIndex = 2;
            this.lblTime.Text = "Time";
            this.lblTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblDate
            // 
            this.lblDate.BackColor = System.Drawing.Color.Transparent;
            this.lblDate.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblDate.Font = new System.Drawing.Font("Calibri Light", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDate.ForeColor = System.Drawing.Color.White;
            this.lblDate.Location = new System.Drawing.Point(0, 0);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(627, 55);
            this.lblDate.TabIndex = 3;
            this.lblDate.Text = "Time";
            this.lblDate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Top;
            //this.pictureBox1.Image = global::AMS_Server.Properties.Resources.banner;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(998, 143);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 30;
            this.pictureBox1.TabStop = false;
            // 
            // panel7
            // 
            this.panel7.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.panel7.BackgroundImage = global::AMS_Server.Properties.Resources.ID;
            this.panel7.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel7.Controls.Add(this.picStud);
            this.panel7.Font = new System.Drawing.Font("Segoe UI", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel7.Location = new System.Drawing.Point(26, -24);
            this.panel7.Name = "panel7";
            this.panel7.Padding = new System.Windows.Forms.Padding(10);
            this.panel7.Size = new System.Drawing.Size(418, 338);
            this.panel7.TabIndex = 9;
            // 
            // picStud
            // 
            this.picStud.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picStud.Location = new System.Drawing.Point(10, 10);
            this.picStud.Name = "picStud";
            this.picStud.Size = new System.Drawing.Size(398, 318);
            this.picStud.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picStud.TabIndex = 5;
            this.picStud.TabStop = false;
            // 
            // panel6
            // 
            this.panel6.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel6.BackColor = System.Drawing.Color.Transparent;
            this.panel6.BackgroundImage = global::AMS_Server.Properties.Resources.Info;
            this.panel6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel6.Controls.Add(this.label1);
            this.panel6.Controls.Add(this.panel7);
            this.panel6.Controls.Add(this.label2);
            this.panel6.Controls.Add(this.label4);
            this.panel6.Controls.Add(this.lblYrSec);
            this.panel6.Controls.Add(this.lblStudentName);
            this.panel6.Controls.Add(this.lblStudentNumber);
            this.panel6.Controls.Add(this.lblTagID);
            this.panel6.Controls.Add(this.label3);
            this.panel6.Font = new System.Drawing.Font("Segoe UI", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel6.Location = new System.Drawing.Point(29, 239);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(942, 301);
            this.panel6.TabIndex = 32;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(502, 221);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(122, 50);
            this.label1.TabIndex = 17;
            this.label1.Text = "Yr/Sec";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(366, 136);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(258, 50);
            this.label2.TabIndex = 16;
            this.label2.Text = "Student Name";
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(366, 271);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(269, 50);
            this.label4.TabIndex = 14;
            this.label4.Text = "ID Tag Number";
            this.label4.Visible = false;
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // lblYrSec
            // 
            this.lblYrSec.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblYrSec.AutoSize = true;
            this.lblYrSec.Font = new System.Drawing.Font("Segoe UI", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblYrSec.ForeColor = System.Drawing.Color.Black;
            this.lblYrSec.Location = new System.Drawing.Point(660, 221);
            this.lblYrSec.Name = "lblYrSec";
            this.lblYrSec.Size = new System.Drawing.Size(0, 50);
            this.lblYrSec.TabIndex = 13;
            // 
            // lblStudentName
            // 
            this.lblStudentName.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblStudentName.AutoSize = true;
            this.lblStudentName.Font = new System.Drawing.Font("Segoe UI", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStudentName.ForeColor = System.Drawing.Color.Black;
            this.lblStudentName.Location = new System.Drawing.Point(660, 136);
            this.lblStudentName.Name = "lblStudentName";
            this.lblStudentName.Size = new System.Drawing.Size(0, 50);
            this.lblStudentName.TabIndex = 12;
            // 
            // lblStudentNumber
            // 
            this.lblStudentNumber.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblStudentNumber.AutoSize = true;
            this.lblStudentNumber.Font = new System.Drawing.Font("Segoe UI", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStudentNumber.ForeColor = System.Drawing.Color.Black;
            this.lblStudentNumber.Location = new System.Drawing.Point(660, 40);
            this.lblStudentNumber.Name = "lblStudentNumber";
            this.lblStudentNumber.Size = new System.Drawing.Size(0, 50);
            this.lblStudentNumber.TabIndex = 11;
            // 
            // lblTagID
            // 
            this.lblTagID.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblTagID.AutoSize = true;
            this.lblTagID.Font = new System.Drawing.Font("Segoe UI", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTagID.ForeColor = System.Drawing.Color.Black;
            this.lblTagID.Location = new System.Drawing.Point(660, 264);
            this.lblTagID.Name = "lblTagID";
            this.lblTagID.Size = new System.Drawing.Size(0, 50);
            this.lblTagID.TabIndex = 10;
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(329, 40);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(295, 50);
            this.label3.TabIndex = 15;
            this.label3.Text = "Student Number";
            // 
            // tmrRefresh
            // 
            this.tmrRefresh.Interval = 10000;
            this.tmrRefresh.Tick += new System.EventHandler(this.tmrRefresh_Tick);
            // 
            // frmMainClient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            //this.BackgroundImage = global::AMS_Server.Properties.Resources.bg;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(998, 648);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.ssStatus);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.panel6);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmMainClient";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Attendance Monitoring System";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMainClient_FormClosing);
            this.Load += new System.EventHandler(this.frmMainClient_Load);
            this.ssStatus.ResumeLayout(false);
            this.ssStatus.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel7.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picStud)).EndInit();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer tmrDeviceConn;
        private System.Windows.Forms.StatusStrip ssStatus;
        private System.Windows.Forms.ToolStripStatusLabel tsslblDevice;
        private System.Windows.Forms.Timer tmrReadTags;
        private System.Windows.Forms.ToolStripStatusLabel tsslblDbConn;
        private System.Windows.Forms.Timer tmrDbConn;
        private System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.Timer tmrTime;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.Label lblLogResult;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Timer tmrQueue;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.PictureBox picStud;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblYrSec;
        private System.Windows.Forms.Label lblStudentName;
        private System.Windows.Forms.Label lblStudentNumber;
        private System.Windows.Forms.Label lblTagID;
        private System.Windows.Forms.Timer tmrRefresh;
    }
}