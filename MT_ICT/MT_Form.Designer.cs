namespace MT_ICT
{
    partial class MT_Form
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MT_Form));
            this.tc_mainPanel = new System.Windows.Forms.TabControl();
            this.TestPage = new System.Windows.Forms.TabPage();
            this.gb_loop = new System.Windows.Forms.GroupBox();
            this.btn_loopAbort = new System.Windows.Forms.Button();
            this.tb_setLoopCount = new System.Windows.Forms.TextBox();
            this.lb_showLoopCount = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.tb_scanSN = new System.Windows.Forms.TextBox();
            this.btn_Start = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lb_Timer = new System.Windows.Forms.Label();
            this.lb_Status = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.pb_refresh = new System.Windows.Forms.PictureBox();
            this.lb_fail = new System.Windows.Forms.Label();
            this.lb_yield = new System.Windows.Forms.Label();
            this.lb_pass = new System.Windows.Forms.Label();
            this.lb_input = new System.Windows.Forms.Label();
            this.SetPage = new System.Windows.Forms.TabPage();
            this.mtCfgBtn = new System.Windows.Forms.Button();
            this.rb_debugMode = new System.Windows.Forms.RadioButton();
            this.rb_testMode = new System.Windows.Forms.RadioButton();
            this.lb_swName = new System.Windows.Forms.Label();
            this.lb_swVersion = new System.Windows.Forms.Label();
            this.lb_debug = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.UserLabel = new System.Windows.Forms.Label();
            this.tc_mainPanel.SuspendLayout();
            this.TestPage.SuspendLayout();
            this.gb_loop.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pb_refresh)).BeginInit();
            this.SetPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // tc_mainPanel
            // 
            this.tc_mainPanel.Controls.Add(this.TestPage);
            this.tc_mainPanel.Controls.Add(this.SetPage);
            this.tc_mainPanel.Location = new System.Drawing.Point(12, 83);
            this.tc_mainPanel.Name = "tc_mainPanel";
            this.tc_mainPanel.SelectedIndex = 0;
            this.tc_mainPanel.Size = new System.Drawing.Size(1057, 566);
            this.tc_mainPanel.TabIndex = 0;
            this.tc_mainPanel.SelectedIndexChanged += new System.EventHandler(this.tc_mainPanel_SelectedIndexChanged);
            // 
            // TestPage
            // 
            this.TestPage.Controls.Add(this.gb_loop);
            this.TestPage.Controls.Add(this.groupBox3);
            this.TestPage.Controls.Add(this.groupBox2);
            this.TestPage.Controls.Add(this.groupBox1);
            this.TestPage.Location = new System.Drawing.Point(4, 29);
            this.TestPage.Name = "TestPage";
            this.TestPage.Padding = new System.Windows.Forms.Padding(3);
            this.TestPage.Size = new System.Drawing.Size(1049, 533);
            this.TestPage.TabIndex = 0;
            this.TestPage.Text = "Test";
            this.TestPage.UseVisualStyleBackColor = true;
            // 
            // gb_loop
            // 
            this.gb_loop.Controls.Add(this.btn_loopAbort);
            this.gb_loop.Controls.Add(this.tb_setLoopCount);
            this.gb_loop.Controls.Add(this.lb_showLoopCount);
            this.gb_loop.Location = new System.Drawing.Point(828, 455);
            this.gb_loop.Name = "gb_loop";
            this.gb_loop.Size = new System.Drawing.Size(200, 72);
            this.gb_loop.TabIndex = 2;
            this.gb_loop.TabStop = false;
            this.gb_loop.Text = "Loop";
            // 
            // btn_loopAbort
            // 
            this.btn_loopAbort.Font = new System.Drawing.Font("微软雅黑", 6.6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_loopAbort.Location = new System.Drawing.Point(106, 13);
            this.btn_loopAbort.Name = "btn_loopAbort";
            this.btn_loopAbort.Size = new System.Drawing.Size(75, 22);
            this.btn_loopAbort.TabIndex = 2;
            this.btn_loopAbort.Text = "Abort";
            this.btn_loopAbort.UseVisualStyleBackColor = true;
            this.btn_loopAbort.Click += new System.EventHandler(this.btn_loopAbort_Click);
            // 
            // tb_setLoopCount
            // 
            this.tb_setLoopCount.Location = new System.Drawing.Point(11, 35);
            this.tb_setLoopCount.Name = "tb_setLoopCount";
            this.tb_setLoopCount.Size = new System.Drawing.Size(89, 27);
            this.tb_setLoopCount.TabIndex = 1;
            this.tb_setLoopCount.Text = "1";
            // 
            // lb_showLoopCount
            // 
            this.lb_showLoopCount.AutoSize = true;
            this.lb_showLoopCount.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.lb_showLoopCount.Location = new System.Drawing.Point(106, 38);
            this.lb_showLoopCount.Name = "lb_showLoopCount";
            this.lb_showLoopCount.Size = new System.Drawing.Size(30, 20);
            this.lb_showLoopCount.TabIndex = 0;
            this.lb_showLoopCount.Text = "F:0";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.tb_scanSN);
            this.groupBox3.Controls.Add(this.btn_Start);
            this.groupBox3.Location = new System.Drawing.Point(828, 16);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(200, 131);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "ScanSN";
            // 
            // tb_scanSN
            // 
            this.tb_scanSN.Location = new System.Drawing.Point(20, 35);
            this.tb_scanSN.Name = "tb_scanSN";
            this.tb_scanSN.Size = new System.Drawing.Size(169, 27);
            this.tb_scanSN.TabIndex = 3;
            // 
            // btn_Start
            // 
            this.btn_Start.Location = new System.Drawing.Point(35, 79);
            this.btn_Start.Name = "btn_Start";
            this.btn_Start.Size = new System.Drawing.Size(132, 33);
            this.btn_Start.TabIndex = 2;
            this.btn_Start.Text = "Start(Ctrl+R)";
            this.btn_Start.UseVisualStyleBackColor = true;
            this.btn_Start.Click += new System.EventHandler(this.btn_Start_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lb_Timer);
            this.groupBox2.Controls.Add(this.lb_Status);
            this.groupBox2.Location = new System.Drawing.Point(828, 303);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(200, 146);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Status";
            // 
            // lb_Timer
            // 
            this.lb_Timer.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lb_Timer.Location = new System.Drawing.Point(6, 26);
            this.lb_Timer.Name = "lb_Timer";
            this.lb_Timer.Size = new System.Drawing.Size(188, 28);
            this.lb_Timer.TabIndex = 1;
            this.lb_Timer.Text = "0 s";
            this.lb_Timer.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lb_Status
            // 
            this.lb_Status.BackColor = System.Drawing.Color.Aqua;
            this.lb_Status.Font = new System.Drawing.Font("微软雅黑", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lb_Status.Location = new System.Drawing.Point(11, 68);
            this.lb_Status.Name = "lb_Status";
            this.lb_Status.Size = new System.Drawing.Size(178, 64);
            this.lb_Status.TabIndex = 0;
            this.lb_Status.Text = "Ready";
            this.lb_Status.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lb_Status.Click += new System.EventHandler(this.lb_Status_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.pb_refresh);
            this.groupBox1.Controls.Add(this.lb_fail);
            this.groupBox1.Controls.Add(this.lb_yield);
            this.groupBox1.Controls.Add(this.lb_pass);
            this.groupBox1.Controls.Add(this.lb_input);
            this.groupBox1.Location = new System.Drawing.Point(828, 162);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 135);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Yield";
            // 
            // pb_refresh
            // 
            this.pb_refresh.Image = global::MT_ICT.Properties.Resources.refresh1;
            this.pb_refresh.Location = new System.Drawing.Point(157, 90);
            this.pb_refresh.Name = "pb_refresh";
            this.pb_refresh.Size = new System.Drawing.Size(32, 34);
            this.pb_refresh.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pb_refresh.TabIndex = 4;
            this.pb_refresh.TabStop = false;
            this.pb_refresh.Click += new System.EventHandler(this.pb_refresh_Click);
            // 
            // lb_fail
            // 
            this.lb_fail.AutoSize = true;
            this.lb_fail.Location = new System.Drawing.Point(20, 80);
            this.lb_fail.Name = "lb_fail";
            this.lb_fail.Size = new System.Drawing.Size(46, 20);
            this.lb_fail.TabIndex = 3;
            this.lb_fail.Text = "Fail:0";
            // 
            // lb_yield
            // 
            this.lb_yield.AutoSize = true;
            this.lb_yield.Location = new System.Drawing.Point(20, 104);
            this.lb_yield.Name = "lb_yield";
            this.lb_yield.Size = new System.Drawing.Size(93, 20);
            this.lb_yield.TabIndex = 2;
            this.lb_yield.Text = "Yield:0.00%";
            // 
            // lb_pass
            // 
            this.lb_pass.AutoSize = true;
            this.lb_pass.Location = new System.Drawing.Point(20, 55);
            this.lb_pass.Name = "lb_pass";
            this.lb_pass.Size = new System.Drawing.Size(53, 20);
            this.lb_pass.TabIndex = 1;
            this.lb_pass.Text = "Pass:0";
            // 
            // lb_input
            // 
            this.lb_input.AutoSize = true;
            this.lb_input.Location = new System.Drawing.Point(20, 28);
            this.lb_input.Name = "lb_input";
            this.lb_input.Size = new System.Drawing.Size(60, 20);
            this.lb_input.TabIndex = 0;
            this.lb_input.Text = "Input:0";
            // 
            // SetPage
            // 
            this.SetPage.Controls.Add(this.mtCfgBtn);
            this.SetPage.Controls.Add(this.rb_debugMode);
            this.SetPage.Controls.Add(this.rb_testMode);
            this.SetPage.Location = new System.Drawing.Point(4, 29);
            this.SetPage.Name = "SetPage";
            this.SetPage.Size = new System.Drawing.Size(1049, 533);
            this.SetPage.TabIndex = 2;
            this.SetPage.Text = "Set";
            this.SetPage.UseVisualStyleBackColor = true;
            // 
            // mtCfgBtn
            // 
            this.mtCfgBtn.Location = new System.Drawing.Point(63, 192);
            this.mtCfgBtn.Name = "mtCfgBtn";
            this.mtCfgBtn.Size = new System.Drawing.Size(98, 34);
            this.mtCfgBtn.TabIndex = 2;
            this.mtCfgBtn.Text = "Config";
            this.mtCfgBtn.UseVisualStyleBackColor = true;
            this.mtCfgBtn.Click += new System.EventHandler(this.MtCfgBtn_Click);
            // 
            // rb_debugMode
            // 
            this.rb_debugMode.AutoSize = true;
            this.rb_debugMode.Location = new System.Drawing.Point(63, 114);
            this.rb_debugMode.Name = "rb_debugMode";
            this.rb_debugMode.Size = new System.Drawing.Size(80, 24);
            this.rb_debugMode.TabIndex = 1;
            this.rb_debugMode.Text = "DEBUG";
            this.rb_debugMode.UseVisualStyleBackColor = true;
            // 
            // rb_testMode
            // 
            this.rb_testMode.AutoSize = true;
            this.rb_testMode.Checked = true;
            this.rb_testMode.Location = new System.Drawing.Point(63, 60);
            this.rb_testMode.Name = "rb_testMode";
            this.rb_testMode.Size = new System.Drawing.Size(65, 24);
            this.rb_testMode.TabIndex = 0;
            this.rb_testMode.TabStop = true;
            this.rb_testMode.Text = "TEST";
            this.rb_testMode.UseVisualStyleBackColor = true;
            this.rb_testMode.CheckedChanged += new System.EventHandler(this.rb_testMode_CheckedChanged);
            // 
            // lb_swName
            // 
            this.lb_swName.Font = new System.Drawing.Font("微软雅黑", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lb_swName.Location = new System.Drawing.Point(12, 16);
            this.lb_swName.Name = "lb_swName";
            this.lb_swName.Size = new System.Drawing.Size(557, 46);
            this.lb_swName.TabIndex = 1;
            this.lb_swName.Text = "SW Name";
            this.lb_swName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lb_swVersion
            // 
            this.lb_swVersion.AutoSize = true;
            this.lb_swVersion.Location = new System.Drawing.Point(612, 60);
            this.lb_swVersion.Name = "lb_swVersion";
            this.lb_swVersion.Size = new System.Drawing.Size(62, 20);
            this.lb_swVersion.TabIndex = 2;
            this.lb_swVersion.Text = "version";
            // 
            // lb_debug
            // 
            this.lb_debug.AutoSize = true;
            this.lb_debug.Font = new System.Drawing.Font("微软雅黑", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lb_debug.ForeColor = System.Drawing.Color.Red;
            this.lb_debug.Location = new System.Drawing.Point(564, 9);
            this.lb_debug.Name = "lb_debug";
            this.lb_debug.Size = new System.Drawing.Size(167, 52);
            this.lb_debug.TabIndex = 4;
            this.lb_debug.Text = "DEBUG";
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // UserLabel
            // 
            this.UserLabel.AutoSize = true;
            this.UserLabel.Location = new System.Drawing.Point(868, 16);
            this.UserLabel.Name = "UserLabel";
            this.UserLabel.Size = new System.Drawing.Size(136, 20);
            this.UserLabel.TabIndex = 5;
            this.UserLabel.Text = "User:1234567890";
            // 
            // MT_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1082, 668);
            this.Controls.Add(this.UserLabel);
            this.Controls.Add(this.lb_debug);
            this.Controls.Add(this.lb_swVersion);
            this.Controls.Add(this.lb_swName);
            this.Controls.Add(this.tc_mainPanel);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "MT_Form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MT_ICT";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MT_Form_FormClosing);
            this.Load += new System.EventHandler(this.MT_Form_Load);
            this.Shown += new System.EventHandler(this.MT_Form_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MT_Form_KeyDown);
            this.tc_mainPanel.ResumeLayout(false);
            this.TestPage.ResumeLayout(false);
            this.gb_loop.ResumeLayout(false);
            this.gb_loop.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pb_refresh)).EndInit();
            this.SetPage.ResumeLayout(false);
            this.SetPage.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tc_mainPanel;
        private System.Windows.Forms.TabPage TestPage;
        private System.Windows.Forms.TabPage SetPage;
        private System.Windows.Forms.Button btn_Start;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lb_Timer;
        private System.Windows.Forms.Label lb_Status;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lb_swName;
        private System.Windows.Forms.Label lb_swVersion;
        private System.Windows.Forms.Label lb_yield;
        private System.Windows.Forms.Label lb_pass;
        private System.Windows.Forms.Label lb_input;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox tb_scanSN;
        private System.Windows.Forms.RadioButton rb_debugMode;
        private System.Windows.Forms.RadioButton rb_testMode;
        private System.Windows.Forms.Label lb_debug;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label lb_fail;
        private System.Windows.Forms.PictureBox pb_refresh;
        private System.Windows.Forms.GroupBox gb_loop;
        private System.Windows.Forms.TextBox tb_setLoopCount;
        private System.Windows.Forms.Label lb_showLoopCount;
        private System.Windows.Forms.Button btn_loopAbort;
        private System.Windows.Forms.Button mtCfgBtn;
        private System.Windows.Forms.Label UserLabel;
    }
}

