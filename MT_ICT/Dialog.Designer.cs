namespace MT_ICT
{
    partial class Dialog
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
            this.msgLabel = new System.Windows.Forms.Label();
            this.okBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // msgLabel
            // 
            this.msgLabel.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.msgLabel.Location = new System.Drawing.Point(4, 27);
            this.msgLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.msgLabel.Name = "msgLabel";
            this.msgLabel.Size = new System.Drawing.Size(412, 103);
            this.msgLabel.TabIndex = 0;
            this.msgLabel.Text = "show message dialog";
            this.msgLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // okBtn
            // 
            this.okBtn.BackgroundImage = global::MT_ICT.Properties.Resources.ok_72p;
            this.okBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.okBtn.Location = new System.Drawing.Point(155, 180);
            this.okBtn.Name = "okBtn";
            this.okBtn.Size = new System.Drawing.Size(83, 37);
            this.okBtn.TabIndex = 1;
            this.okBtn.UseVisualStyleBackColor = true;
            this.okBtn.Click += new System.EventHandler(this.okBtn_Click);
            this.okBtn.MouseLeave += new System.EventHandler(this.okBtn_MouseLeave);
            this.okBtn.MouseHover += new System.EventHandler(this.okBtn_MouseHover);
            // 
            // Dialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 28F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.ClientSize = new System.Drawing.Size(421, 271);
            this.Controls.Add(this.okBtn);
            this.Controls.Add(this.msgLabel);
            this.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Dialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Dialog";
            this.Load += new System.EventHandler(this.Dialog_Load);
            this.Shown += new System.EventHandler(this.Dialog_Shown);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label msgLabel;
        private System.Windows.Forms.Button okBtn;
    }
}