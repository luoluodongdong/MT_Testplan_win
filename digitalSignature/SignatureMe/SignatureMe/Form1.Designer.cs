namespace SignatureMe
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.SignBtn = new System.Windows.Forms.Button();
            this.VerifyBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // SignBtn
            // 
            this.SignBtn.Location = new System.Drawing.Point(146, 107);
            this.SignBtn.Name = "SignBtn";
            this.SignBtn.Size = new System.Drawing.Size(128, 40);
            this.SignBtn.TabIndex = 0;
            this.SignBtn.Text = "Sign";
            this.SignBtn.UseVisualStyleBackColor = true;
            this.SignBtn.Click += new System.EventHandler(this.SignBtn_Click);
            // 
            // VerifyBtn
            // 
            this.VerifyBtn.Location = new System.Drawing.Point(425, 107);
            this.VerifyBtn.Name = "VerifyBtn";
            this.VerifyBtn.Size = new System.Drawing.Size(116, 40);
            this.VerifyBtn.TabIndex = 1;
            this.VerifyBtn.Text = "Verify";
            this.VerifyBtn.UseVisualStyleBackColor = true;
            this.VerifyBtn.Click += new System.EventHandler(this.VerifyBtn_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.VerifyBtn);
            this.Controls.Add(this.SignBtn);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button SignBtn;
        private System.Windows.Forms.Button VerifyBtn;
    }
}

