namespace MT_ICT
{
    partial class myInstrumentUC
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
            this.components = new System.ComponentModel.Container();
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.DeviceGB = new System.Windows.Forms.GroupBox();
            this.PortsCB = new System.Windows.Forms.ComboBox();
            this.OpenBtn = new System.Windows.Forms.Button();
            this.ScanBtn = new System.Windows.Forms.Button();
            this.DeviceGB.SuspendLayout();
            this.SuspendLayout();
            // 
            // DeviceGB
            // 
            this.DeviceGB.Controls.Add(this.PortsCB);
            this.DeviceGB.Controls.Add(this.OpenBtn);
            this.DeviceGB.Controls.Add(this.ScanBtn);
            this.DeviceGB.Location = new System.Drawing.Point(3, 7);
            this.DeviceGB.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.DeviceGB.Name = "DeviceGB";
            this.DeviceGB.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.DeviceGB.Size = new System.Drawing.Size(307, 58);
            this.DeviceGB.TabIndex = 0;
            this.DeviceGB.TabStop = false;
            this.DeviceGB.Text = "DeviceName";
            // 
            // PortsCB
            // 
            this.PortsCB.FormattingEnabled = true;
            this.PortsCB.Location = new System.Drawing.Point(16, 24);
            this.PortsCB.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.PortsCB.Name = "PortsCB";
            this.PortsCB.Size = new System.Drawing.Size(91, 25);
            this.PortsCB.TabIndex = 2;
            // 
            // OpenBtn
            // 
            this.OpenBtn.Location = new System.Drawing.Point(244, 20);
            this.OpenBtn.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.OpenBtn.Name = "OpenBtn";
            this.OpenBtn.Size = new System.Drawing.Size(57, 31);
            this.OpenBtn.TabIndex = 1;
            this.OpenBtn.Text = "Open";
            this.OpenBtn.UseVisualStyleBackColor = true;
            this.OpenBtn.Click += new System.EventHandler(this.OpenBtn_Click);
            // 
            // ScanBtn
            // 
            this.ScanBtn.Location = new System.Drawing.Point(148, 20);
            this.ScanBtn.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ScanBtn.Name = "ScanBtn";
            this.ScanBtn.Size = new System.Drawing.Size(54, 31);
            this.ScanBtn.TabIndex = 0;
            this.ScanBtn.Text = "Scan";
            this.ScanBtn.UseVisualStyleBackColor = true;
            this.ScanBtn.Click += new System.EventHandler(this.ScanBtn_Click);
            // 
            // myInstrumentUC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.DeviceGB);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "myInstrumentUC";
            this.Size = new System.Drawing.Size(319, 72);
            this.Load += new System.EventHandler(this.myInstrumentUC_Load);
            this.DeviceGB.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox DeviceGB;
        private System.Windows.Forms.ComboBox PortsCB;
        private System.Windows.Forms.Button OpenBtn;
        private System.Windows.Forms.Button ScanBtn;
        public System.IO.Ports.SerialPort serialPort1;
    }
}
