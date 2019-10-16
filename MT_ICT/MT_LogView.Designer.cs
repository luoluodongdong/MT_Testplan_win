namespace MT_ICT
{
    partial class MT_LogView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MT_LogView));
            this.lb_logView = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // lb_logView
            // 
            this.lb_logView.BackColor = System.Drawing.Color.Black;
            this.lb_logView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lb_logView.ForeColor = System.Drawing.Color.Lime;
            this.lb_logView.FormattingEnabled = true;
            this.lb_logView.ItemHeight = 20;
            this.lb_logView.Location = new System.Drawing.Point(0, 0);
            this.lb_logView.Name = "lb_logView";
            this.lb_logView.Size = new System.Drawing.Size(783, 744);
            this.lb_logView.TabIndex = 0;
            // 
            // MT_LogView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(783, 744);
            this.Controls.Add(this.lb_logView);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "MT_LogView";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "MT_LogView";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MT_LogView_FormClosed);
            this.Load += new System.EventHandler(this.MT_LogView_Load);
            this.Shown += new System.EventHandler(this.MT_LogView_Shown);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lb_logView;
    }
}