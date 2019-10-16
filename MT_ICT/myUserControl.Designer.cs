namespace MT_ICT
{
    partial class myUserControl
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.cb_Slot_Selected = new System.Windows.Forms.CheckBox();
            this.lb_Status = new System.Windows.Forms.Label();
            this.lb_showSN = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.configBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cb_Slot_Selected
            // 
            this.cb_Slot_Selected.AutoSize = true;
            this.cb_Slot_Selected.Checked = true;
            this.cb_Slot_Selected.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb_Slot_Selected.Font = new System.Drawing.Font("微软雅黑", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cb_Slot_Selected.Location = new System.Drawing.Point(9, 4);
            this.cb_Slot_Selected.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cb_Slot_Selected.Name = "cb_Slot_Selected";
            this.cb_Slot_Selected.Size = new System.Drawing.Size(107, 43);
            this.cb_Slot_Selected.TabIndex = 0;
            this.cb_Slot_Selected.Text = "Slot-";
            this.cb_Slot_Selected.UseVisualStyleBackColor = true;
            this.cb_Slot_Selected.CheckedChanged += new System.EventHandler(this.cb_Slot_Selected_CheckedChanged);
            // 
            // lb_Status
            // 
            this.lb_Status.Font = new System.Drawing.Font("微软雅黑", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lb_Status.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lb_Status.Location = new System.Drawing.Point(37, 87);
            this.lb_Status.Name = "lb_Status";
            this.lb_Status.Size = new System.Drawing.Size(265, 121);
            this.lb_Status.TabIndex = 3;
            this.lb_Status.Text = "Ready";
            this.lb_Status.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lb_Status.Click += new System.EventHandler(this.lb_Status_Click);
            // 
            // lb_showSN
            // 
            this.lb_showSN.Font = new System.Drawing.Font("微软雅黑", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lb_showSN.Location = new System.Drawing.Point(3, 49);
            this.lb_showSN.Name = "lb_showSN";
            this.lb_showSN.Size = new System.Drawing.Size(344, 42);
            this.lb_showSN.TabIndex = 7;
            this.lb_showSN.Text = "1234567890123456789";
            this.lb_showSN.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // progressBar1
            // 
            this.progressBar1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.progressBar1.Location = new System.Drawing.Point(6, 211);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(341, 10);
            this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBar1.TabIndex = 8;
            // 
            // configBtn
            // 
            this.configBtn.BackgroundImage = global::MT_ICT.Properties.Resources.settings_128px_1234895_easyicon_net;
            this.configBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.configBtn.Location = new System.Drawing.Point(296, 4);
            this.configBtn.Name = "configBtn";
            this.configBtn.Size = new System.Drawing.Size(42, 40);
            this.configBtn.TabIndex = 9;
            this.configBtn.UseVisualStyleBackColor = true;
            this.configBtn.Click += new System.EventHandler(this.ConfigBtn_Click);
            // 
            // myUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.configBtn);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.lb_showSN);
            this.Controls.Add(this.lb_Status);
            this.Controls.Add(this.cb_Slot_Selected);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "myUserControl";
            this.Size = new System.Drawing.Size(350, 253);
            this.Load += new System.EventHandler(this.myUserControl_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox cb_Slot_Selected;
        private System.Windows.Forms.Label lb_Status;
        private System.Windows.Forms.Label lb_showSN;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Button configBtn;
    }
}
