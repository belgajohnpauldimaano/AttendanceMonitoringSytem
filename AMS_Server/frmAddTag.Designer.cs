namespace AMS_Server
{
    partial class frmAddTag
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
            this.panel6 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.gbTag = new System.Windows.Forms.GroupBox();
            this.btnTagCancel = new System.Windows.Forms.Button();
            this.tbTagID = new System.Windows.Forms.TextBox();
            this.btnTagSave = new System.Windows.Forms.Button();
            this.btnCloseOpenDevice = new System.Windows.Forms.Button();
            this.tmrTagReader = new System.Windows.Forms.Timer(this.components);
            this.panel6.SuspendLayout();
            this.gbTag.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.panel6.Controls.Add(this.label2);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel6.Location = new System.Drawing.Point(0, 0);
            this.panel6.Margin = new System.Windows.Forms.Padding(4);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(399, 47);
            this.panel6.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Honeydew;
            this.label2.Location = new System.Drawing.Point(13, 7);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(167, 40);
            this.label2.TabIndex = 0;
            this.label2.Text = "Add New Tag ID";
            // 
            // gbTag
            // 
            this.gbTag.Controls.Add(this.btnTagCancel);
            this.gbTag.Controls.Add(this.tbTagID);
            this.gbTag.Controls.Add(this.btnTagSave);
            this.gbTag.Location = new System.Drawing.Point(12, 55);
            this.gbTag.Margin = new System.Windows.Forms.Padding(4);
            this.gbTag.Name = "gbTag";
            this.gbTag.Padding = new System.Windows.Forms.Padding(4);
            this.gbTag.Size = new System.Drawing.Size(373, 85);
            this.gbTag.TabIndex = 3;
            this.gbTag.TabStop = false;
            this.gbTag.Text = "Tag";
            // 
            // btnTagCancel
            // 
            this.btnTagCancel.Location = new System.Drawing.Point(287, 34);
            this.btnTagCancel.Name = "btnTagCancel";
            this.btnTagCancel.Size = new System.Drawing.Size(74, 32);
            this.btnTagCancel.TabIndex = 2;
            this.btnTagCancel.Text = "Cancel";
            this.btnTagCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnTagCancel.UseVisualStyleBackColor = true;
            this.btnTagCancel.Click += new System.EventHandler(this.btnTagCancel_Click);
            // 
            // tbTagID
            // 
            this.tbTagID.Location = new System.Drawing.Point(15, 38);
            this.tbTagID.Name = "tbTagID";
            this.tbTagID.ReadOnly = true;
            this.tbTagID.Size = new System.Drawing.Size(191, 26);
            this.tbTagID.TabIndex = 1;
            // 
            // btnTagSave
            // 
            this.btnTagSave.Location = new System.Drawing.Point(212, 34);
            this.btnTagSave.Name = "btnTagSave";
            this.btnTagSave.Size = new System.Drawing.Size(69, 32);
            this.btnTagSave.TabIndex = 0;
            this.btnTagSave.Text = "Save";
            this.btnTagSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnTagSave.UseVisualStyleBackColor = true;
            this.btnTagSave.Click += new System.EventHandler(this.btnTagSave_Click);
            // 
            // btnCloseOpenDevice
            // 
            this.btnCloseOpenDevice.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCloseOpenDevice.Location = new System.Drawing.Point(12, 1);
            this.btnCloseOpenDevice.Name = "btnCloseOpenDevice";
            this.btnCloseOpenDevice.Size = new System.Drawing.Size(373, 38);
            this.btnCloseOpenDevice.TabIndex = 4;
            this.btnCloseOpenDevice.Text = "Open Device";
            this.btnCloseOpenDevice.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnCloseOpenDevice.UseVisualStyleBackColor = true;
            this.btnCloseOpenDevice.Click += new System.EventHandler(this.btnCloseOpenDevice_Click);
            // 
            // tmrTagReader
            // 
            this.tmrTagReader.Enabled = true;
            this.tmrTagReader.Interval = 500;
            this.tmrTagReader.Tick += new System.EventHandler(this.tmrTagReader_Tick);
            // 
            // frmAddTag
            // 
            this.AcceptButton = this.btnTagSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(399, 149);
            this.Controls.Add(this.gbTag);
            this.Controls.Add(this.panel6);
            this.Controls.Add(this.btnCloseOpenDevice);
            this.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmAddTag";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "RFID Tag";
            this.Load += new System.EventHandler(this.frmAddTag_Load);
            this.panel6.ResumeLayout(false);
            this.gbTag.ResumeLayout(false);
            this.gbTag.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox gbTag;
        private System.Windows.Forms.Button btnCloseOpenDevice;
        private System.Windows.Forms.Button btnTagCancel;
        private System.Windows.Forms.TextBox tbTagID;
        private System.Windows.Forms.Button btnTagSave;
        private System.Windows.Forms.Timer tmrTagReader;
    }
}