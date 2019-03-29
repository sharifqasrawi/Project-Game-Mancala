namespace Server
{
    partial class Mankala_Server_Form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Mankala_Server_Form));
            this.stop_server_btn = new System.Windows.Forms.Button();
            this.start_server_btn = new System.Windows.Forms.Button();
            this.displayTextBox = new System.Windows.Forms.TextBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.btnAbout = new System.Windows.Forms.Button();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // stop_server_btn
            // 
            this.stop_server_btn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.stop_server_btn.Enabled = false;
            this.stop_server_btn.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.stop_server_btn.Location = new System.Drawing.Point(94, 12);
            this.stop_server_btn.Name = "stop_server_btn";
            this.stop_server_btn.Size = new System.Drawing.Size(95, 23);
            this.stop_server_btn.TabIndex = 1;
            this.stop_server_btn.Text = "End";
            this.stop_server_btn.UseVisualStyleBackColor = true;
            this.stop_server_btn.Click += new System.EventHandler(this.stop_server_btn_Click);
            // 
            // start_server_btn
            // 
            this.start_server_btn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.start_server_btn.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.start_server_btn.Location = new System.Drawing.Point(195, 12);
            this.start_server_btn.Name = "start_server_btn";
            this.start_server_btn.Size = new System.Drawing.Size(111, 23);
            this.start_server_btn.TabIndex = 0;
            this.start_server_btn.Text = "Start";
            this.start_server_btn.UseVisualStyleBackColor = true;
            this.start_server_btn.Click += new System.EventHandler(this.start_server_btn_Click);
            // 
            // displayTextBox
            // 
            this.displayTextBox.Location = new System.Drawing.Point(12, 41);
            this.displayTextBox.Multiline = true;
            this.displayTextBox.Name = "displayTextBox";
            this.displayTextBox.ReadOnly = true;
            this.displayTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.displayTextBox.Size = new System.Drawing.Size(294, 218);
            this.displayTextBox.TabIndex = 2;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 262);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(318, 22);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(118, 17);
            this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            // 
            // btnAbout
            // 
            this.btnAbout.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAbout.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnAbout.Location = new System.Drawing.Point(12, 12);
            this.btnAbout.Name = "btnAbout";
            this.btnAbout.Size = new System.Drawing.Size(76, 23);
            this.btnAbout.TabIndex = 1;
            this.btnAbout.Text = "About";
            this.btnAbout.UseVisualStyleBackColor = true;
            this.btnAbout.Click += new System.EventHandler(this.btnAbout_Click);
            // 
            // Mankala_Server_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(318, 284);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.displayTextBox);
            this.Controls.Add(this.btnAbout);
            this.Controls.Add(this.stop_server_btn);
            this.Controls.Add(this.start_server_btn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Mankala_Server_Form";
            this.Text = "Mankala Server";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Mankala_Server_Form_FormClosing);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button stop_server_btn;
        private System.Windows.Forms.Button start_server_btn;
        private System.Windows.Forms.TextBox displayTextBox;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.Button btnAbout;
    }
}

