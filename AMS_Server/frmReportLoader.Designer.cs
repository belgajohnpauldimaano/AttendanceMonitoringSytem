namespace AMS_Server
{
    partial class frmReportLoader
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
            this.crvRptLoader = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.SuspendLayout();
            // 
            // crvRptLoader
            // 
            this.crvRptLoader.ActiveViewIndex = -1;
            this.crvRptLoader.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.crvRptLoader.Cursor = System.Windows.Forms.Cursors.Default;
            this.crvRptLoader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.crvRptLoader.Location = new System.Drawing.Point(0, 0);
            this.crvRptLoader.Name = "crvRptLoader";
            this.crvRptLoader.ShowGroupTreeButton = false;
            this.crvRptLoader.ShowParameterPanelButton = false;
            this.crvRptLoader.ShowRefreshButton = false;
            this.crvRptLoader.Size = new System.Drawing.Size(839, 403);
            this.crvRptLoader.TabIndex = 0;
            this.crvRptLoader.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None;
            // 
            // frmReportLoader
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(839, 403);
            this.Controls.Add(this.crvRptLoader);
            this.Name = "frmReportLoader";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Report";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmReportLoader_Load);
            this.ResumeLayout(false);

        }

        #endregion

        public CrystalDecisions.Windows.Forms.CrystalReportViewer crvRptLoader;
    }
}