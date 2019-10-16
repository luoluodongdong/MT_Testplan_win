namespace MT_ICT
{
    partial class Password
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Password));
            this.tb_password = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // tb_password
            // 
            this.tb_password.Font = new System.Drawing.Font("宋体", 22.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tb_password.Location = new System.Drawing.Point(69, 99);
            this.tb_password.Name = "tb_password";
            this.tb_password.PasswordChar = '*';
            this.tb_password.Size = new System.Drawing.Size(361, 50);
            this.tb_password.TabIndex = 0;
            this.tb_password.Text = "123456";
            this.tb_password.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Password
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(515, 289);
            this.Controls.Add(this.tb_password);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(533, 336);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(533, 336);
            this.Name = "Password";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Password";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Password_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Password_FormClosed);
            this.Load += new System.EventHandler(this.Password_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tb_password;
    }
}