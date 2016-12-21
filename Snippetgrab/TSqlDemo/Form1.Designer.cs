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
            this.lbUser = new System.Windows.Forms.ListBox();
            this.lbMessage = new System.Windows.Forms.ListBox();
            this.users = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnGetOffenders = new System.Windows.Forms.Button();
            this.btnMarkApproved = new System.Windows.Forms.Button();
            this.btnSendWarningMessage = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lbUser
            // 
            this.lbUser.FormattingEnabled = true;
            this.lbUser.ItemHeight = 16;
            this.lbUser.Location = new System.Drawing.Point(31, 72);
            this.lbUser.Margin = new System.Windows.Forms.Padding(4);
            this.lbUser.Name = "lbUser";
            this.lbUser.Size = new System.Drawing.Size(159, 196);
            this.lbUser.TabIndex = 0;
            this.lbUser.SelectedIndexChanged += new System.EventHandler(this.lbUser_SelectedIndexChanged);
            // 
            // lbMessage
            // 
            this.lbMessage.FormattingEnabled = true;
            this.lbMessage.ItemHeight = 16;
            this.lbMessage.Location = new System.Drawing.Point(221, 72);
            this.lbMessage.Margin = new System.Windows.Forms.Padding(4);
            this.lbMessage.Name = "lbMessage";
            this.lbMessage.Size = new System.Drawing.Size(159, 196);
            this.lbMessage.TabIndex = 5;
            // 
            // users
            // 
            this.users.AutoSize = true;
            this.users.Location = new System.Drawing.Point(28, 51);
            this.users.Name = "users";
            this.users.Size = new System.Drawing.Size(45, 17);
            this.users.TabIndex = 6;
            this.users.Text = "Users";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(218, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 17);
            this.label1.TabIndex = 7;
            this.label1.Text = "Messages";
            // 
            // btnGetOffenders
            // 
            this.btnGetOffenders.Location = new System.Drawing.Point(31, 321);
            this.btnGetOffenders.Name = "btnGetOffenders";
            this.btnGetOffenders.Size = new System.Drawing.Size(108, 26);
            this.btnGetOffenders.TabIndex = 8;
            this.btnGetOffenders.Text = "Get offenders";
            this.btnGetOffenders.UseVisualStyleBackColor = true;
            this.btnGetOffenders.Click += new System.EventHandler(this.btnGetOffenders_Click);
            // 
            // btnMarkApproved
            // 
            this.btnMarkApproved.Location = new System.Drawing.Point(221, 321);
            this.btnMarkApproved.Name = "btnMarkApproved";
            this.btnMarkApproved.Size = new System.Drawing.Size(123, 26);
            this.btnMarkApproved.TabIndex = 9;
            this.btnMarkApproved.Text = "Mark approved";
            this.btnMarkApproved.UseVisualStyleBackColor = true;
            this.btnMarkApproved.Click += new System.EventHandler(this.btnMarkApproved_Click);
            // 
            // btnSendWarningMessage
            // 
            this.btnSendWarningMessage.Location = new System.Drawing.Point(498, 321);
            this.btnSendWarningMessage.Name = "btnSendWarningMessage";
            this.btnSendWarningMessage.Size = new System.Drawing.Size(123, 26);
            this.btnSendWarningMessage.TabIndex = 10;
            this.btnSendWarningMessage.Text = "Send warning";
            this.btnSendWarningMessage.UseVisualStyleBackColor = true;
            this.btnSendWarningMessage.Click += new System.EventHandler(this.btnSendWarningMessage_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(498, 72);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(194, 196);
            this.textBox1.TabIndex = 11;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(495, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(128, 17);
            this.label2.TabIndex = 12;
            this.label2.Text = "Your warning here:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 474);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.btnSendWarningMessage);
            this.Controls.Add(this.btnMarkApproved);
            this.Controls.Add(this.btnGetOffenders);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.users);
            this.Controls.Add(this.lbMessage);
            this.Controls.Add(this.lbUser);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lbUser;
        private System.Windows.Forms.ListBox lbMessage;
        private System.Windows.Forms.Label users;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnGetOffenders;
        private System.Windows.Forms.Button btnMarkApproved;
        private System.Windows.Forms.Button btnSendWarningMessage;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label2;
    }
}

