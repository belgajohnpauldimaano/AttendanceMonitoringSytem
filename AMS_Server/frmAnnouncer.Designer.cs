namespace AMS_Server
{
    partial class frmAnnouncer
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
            this.bgwBroadcaster = new System.ComponentModel.BackgroundWorker();
            this.SuspendLayout();
            // 
            // bgwBroadcaster
            // 
            this.bgwBroadcaster.WorkerSupportsCancellation = true;
            this.bgwBroadcaster.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwBroadcaster_DoWork);
            this.bgwBroadcaster.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgwBroadcaster_RunWorkerCompleted);
            // 
            // frmAnnouncer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(336, 83);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmAnnouncer";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Announce Broadcaster";
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmAnnouncer_FormClosing);
            this.Load += new System.EventHandler(this.frmAnnouncer_Load);
            this.ResumeLayout(false);

        }

        #endregion

        public System.ComponentModel.BackgroundWorker bgwBroadcaster;

    }
}