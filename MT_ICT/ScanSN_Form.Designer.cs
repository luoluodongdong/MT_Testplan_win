namespace MT_ICT
{
    partial class ScanSN_Form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ScanSN_Form));
            this.tb_scanSN = new System.Windows.Forms.TextBox();
            this.lb_showSNs = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // tb_scanSN
            // 
            this.tb_scanSN.Font = new System.Drawing.Font("微软雅黑", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tb_scanSN.Location = new System.Drawing.Point(89, 37);
            this.tb_scanSN.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tb_scanSN.Name = "tb_scanSN";
            this.tb_scanSN.Size = new System.Drawing.Size(321, 38);
            this.tb_scanSN.TabIndex = 0;
            this.tb_scanSN.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lb_showSNs
            // 
            this.lb_showSNs.BackColor = System.Drawing.SystemColors.Menu;
            this.lb_showSNs.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lb_showSNs.Font = new System.Drawing.Font("微软雅黑", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lb_showSNs.FormattingEnabled = true;
            this.lb_showSNs.ItemHeight = 35;
            this.lb_showSNs.Items.AddRange(new object[] {
            "1.1234567890123456789",
            "2.1234567890123456789"});
            this.lb_showSNs.Location = new System.Drawing.Point(41, 124);
            this.lb_showSNs.Name = "lb_showSNs";
            this.lb_showSNs.Size = new System.Drawing.Size(362, 350);
            this.lb_showSNs.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(23, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 36);
            this.label1.TabIndex = 2;
            this.label1.Text = "SN:";
            // 
            // ScanSN_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(446, 546);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lb_showSNs);
            this.Controls.Add(this.tb_scanSN);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ScanSN_Form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ScanSN_Form";
            this.Load += new System.EventHandler(this.ScanSN_Form_Load);
            this.Shown += new System.EventHandler(this.ScanSN_Form_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tb_scanSN;
        private System.Windows.Forms.ListBox lb_showSNs;
        private System.Windows.Forms.Label label1;
    }
}