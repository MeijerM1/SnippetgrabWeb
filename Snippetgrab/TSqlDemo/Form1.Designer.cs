namespace TSqlDemo
{
    partial class Form1
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
            this.lbTags = new System.Windows.Forms.ListBox();
            this.Notify = new System.Windows.Forms.Button();
            this.nudUploader = new System.Windows.Forms.NumericUpDown();
            this.tbLink = new System.Windows.Forms.TextBox();
            this.lbUsers = new System.Windows.Forms.ListBox();
            ((System.ComponentModel.ISupportInitialize)(this.nudUploader)).BeginInit();
            this.SuspendLayout();
            // 
            // lbTags
            // 
            this.lbTags.FormattingEnabled = true;
            this.lbTags.Location = new System.Drawing.Point(136, 43);
            this.lbTags.Name = "lbTags";
            this.lbTags.Size = new System.Drawing.Size(120, 160);
            this.lbTags.TabIndex = 0;
            // 
            // Notify
            // 
            this.Notify.Location = new System.Drawing.Point(136, 310);
            this.Notify.Name = "Notify";
            this.Notify.Size = new System.Drawing.Size(90, 28);
            this.Notify.TabIndex = 2;
            this.Notify.Text = "button1";
            this.Notify.UseVisualStyleBackColor = true;
            this.Notify.Click += new System.EventHandler(this.Notify_Click);
            // 
            // nudUploader
            // 
            this.nudUploader.Location = new System.Drawing.Point(136, 227);
            this.nudUploader.Name = "nudUploader";
            this.nudUploader.Size = new System.Drawing.Size(120, 20);
            this.nudUploader.TabIndex = 3;
            // 
            // tbLink
            // 
            this.tbLink.Location = new System.Drawing.Point(136, 266);
            this.tbLink.Name = "tbLink";
            this.tbLink.Size = new System.Drawing.Size(124, 20);
            this.tbLink.TabIndex = 4;
            // 
            // lbUsers
            // 
            this.lbUsers.FormattingEnabled = true;
            this.lbUsers.Location = new System.Drawing.Point(323, 43);
            this.lbUsers.Name = "lbUsers";
            this.lbUsers.Size = new System.Drawing.Size(120, 160);
            this.lbUsers.TabIndex = 5;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(594, 385);
            this.Controls.Add(this.lbUsers);
            this.Controls.Add(this.tbLink);
            this.Controls.Add(this.nudUploader);
            this.Controls.Add(this.Notify);
            this.Controls.Add(this.lbTags);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.nudUploader)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lbTags;
        private System.Windows.Forms.Button Notify;
        private System.Windows.Forms.NumericUpDown nudUploader;
        private System.Windows.Forms.TextBox tbLink;
        private System.Windows.Forms.ListBox lbUsers;
    }
}

