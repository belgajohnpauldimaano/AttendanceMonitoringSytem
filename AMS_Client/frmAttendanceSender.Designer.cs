namespace AMS_Server
{
    partial class frmAttendanceSender
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
            this.bgwAttSender = new System.ComponentModel.BackgroundWorker();
            this.bgwAttSenderTerminal2 = new System.ComponentModel.BackgroundWorker();
            this.bgwAttSenderTerminal3 = new System.ComponentModel.BackgroundWorker();
            this.bgwAttSenderTerminal4 = new System.ComponentModel.BackgroundWorker();
            this.bgwAttSenderTerminal5 = new System.ComponentModel.BackgroundWorker();
            this.SuspendLayout();
            // 
            // bgwAttSender
            // 
            this.bgwAttSender.WorkerSupportsCancellation = true;
            this.bgwAttSender.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwAttSender_DoWork);
            this.bgwAttSender.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgwAttSender_RunWorkerCompleted);
            // 
            // bgwAttSenderTerminal2
            // 
            this.bgwAttSenderTerminal2.WorkerSupportsCancellation = true;
            this.bgwAttSenderTerminal2.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwAttSenderTerminal2_DoWork);
            this.bgwAttSenderTerminal2.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgwAttSenderTerminal2_RunWorkerCompleted);
            // 
            // bgwAttSenderTerminal3
            // 
            this.bgwAttSenderTerminal3.WorkerSupportsCancellation = true;
            this.bgwAttSenderTerminal3.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwAttSenderTerminal3_DoWork);
            this.bgwAttSenderTerminal3.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgwAttSenderTerminal3_RunWorkerCompleted);
            // 
            // bgwAttSenderTerminal4
            // 
            this.bgwAttSenderTerminal4.WorkerSupportsCancellation = true;
            this.bgwAttSenderTerminal4.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwAttSenderTerminal4_DoWork);
            this.bgwAttSenderTerminal4.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgwAttSenderTerminal4_RunWorkerCompleted);
            // 
            // bgwAttSenderTerminal5
            // 
            this.bgwAttSenderTerminal5.WorkerSupportsCancellation = true;
            this.bgwAttSenderTerminal5.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwAttSenderTerminal5_DoWork);
            this.bgwAttSenderTerminal5.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgwAttSenderTerminal5_RunWorkerCompleted);
            // 
            // frmAttendanceSender
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(375, 101);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmAttendanceSender";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmAttendanceSender";
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmAttendanceSender_FormClosing);
            this.Load += new System.EventHandler(this.frmAttendanceSender_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.ComponentModel.BackgroundWorker bgwAttSender;
        private System.ComponentModel.BackgroundWorker bgwAttSenderTerminal2;
        private System.ComponentModel.BackgroundWorker bgwAttSenderTerminal3;
        private System.ComponentModel.BackgroundWorker bgwAttSenderTerminal4;
        private System.ComponentModel.BackgroundWorker bgwAttSenderTerminal5;
    }
}