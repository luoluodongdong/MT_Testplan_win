namespace MT_ICT
{
    partial class Welcome
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Welcome));
            this.labStatus = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.passwordTB = new System.Windows.Forms.TextBox();
            this.userTB = new System.Windows.Forms.TextBox();
            this.okBtn = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.loginPanel = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.loginPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // labStatus
            // 
            this.labStatus.AutoSize = true;
            this.labStatus.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labStatus.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.labStatus.Location = new System.Drawing.Point(18, 561);
            this.labStatus.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labStatus.Name = "labStatus";
            this.labStatus.Size = new System.Drawing.Size(73, 28);
            this.labStatus.TabIndex = 0;
            this.labStatus.Text = "label1";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(20, 596);
            this.progressBar1.Margin = new System.Windows.Forms.Padding(4);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(1012, 32);
            this.progressBar1.TabIndex = 1;
            // 
            // passwordTB
            // 
            this.passwordTB.Location = new System.Drawing.Point(130, 137);
            this.passwordTB.Name = "passwordTB";
            this.passwordTB.PasswordChar = '*';
            this.passwordTB.Size = new System.Drawing.Size(180, 35);
            this.passwordTB.TabIndex = 4;
            this.passwordTB.Text = "123";
            this.passwordTB.KeyDown += new System.Windows.Forms.KeyEventHandler(this.PasswordTB_KeyDown);
            // 
            // userTB
            // 
            this.userTB.Location = new System.Drawing.Point(128, 67);
            this.userTB.Name = "userTB";
            this.userTB.Size = new System.Drawing.Size(180, 35);
            this.userTB.TabIndex = 3;
            this.userTB.Text = "TE";
            this.userTB.KeyDown += new System.Windows.Forms.KeyEventHandler(this.userTB_KeyDown);
            // 
            // okBtn
            // 
            this.okBtn.BackgroundImage = global::MT_ICT.Properties.Resources.arrow_right_72px;
            this.okBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.okBtn.Location = new System.Drawing.Point(326, 133);
            this.okBtn.Name = "okBtn";
            this.okBtn.Size = new System.Drawing.Size(46, 42);
            this.okBtn.TabIndex = 2;
            this.okBtn.UseVisualStyleBackColor = true;
            this.okBtn.Click += new System.EventHandler(this.okBtn_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(62, 141);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 28);
            this.label2.TabIndex = 1;
            this.label2.Text = "Key:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(52, 72);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 28);
            this.label1.TabIndex = 0;
            this.label1.Text = "User:";
            // 
            // loginPanel
            // 
            this.loginPanel.BackColor = System.Drawing.Color.Gainsboro;
            this.loginPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.loginPanel.Controls.Add(this.label4);
            this.loginPanel.Controls.Add(this.okBtn);
            this.loginPanel.Controls.Add(this.passwordTB);
            this.loginPanel.Controls.Add(this.label1);
            this.loginPanel.Controls.Add(this.userTB);
            this.loginPanel.Controls.Add(this.label2);
            this.loginPanel.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.loginPanel.Location = new System.Drawing.Point(341, 199);
            this.loginPanel.Name = "loginPanel";
            this.loginPanel.Size = new System.Drawing.Size(428, 210);
            this.loginPanel.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(3, 11);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(82, 31);
            this.label4.TabIndex = 6;
            this.label4.Text = "Login";
            // 
            // Welcome
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::MT_ICT.Properties.Resources.white1234;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1046, 640);
            this.Controls.Add(this.loginPanel);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.labStatus);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Welcome";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Welcome";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Welcome_FormClosing);
            this.Load += new System.EventHandler(this.Welcome_Load);
            this.Shown += new System.EventHandler(this.Welcome_Shown);
            this.loginPanel.ResumeLayout(false);
            this.loginPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labStatus;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.TextBox passwordTB;
        private System.Windows.Forms.TextBox userTB;
        private System.Windows.Forms.Button okBtn;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel loginPanel;
        private System.Windows.Forms.Label label4;
    }
}