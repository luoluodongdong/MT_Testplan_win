namespace MT_ICT
{
    partial class DetialView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DetialView));
            this.lb_log = new System.Windows.Forms.ListBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.lv_detial = new MT_ICT.ListViewNF();
            this.No = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Items = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Status = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Value = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Low = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.TypHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.UpHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.UnitHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.DurationHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lb_log
            // 
            this.lb_log.BackColor = System.Drawing.Color.Black;
            this.lb_log.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lb_log.ForeColor = System.Drawing.Color.Lime;
            this.lb_log.FormattingEnabled = true;
            this.lb_log.ItemHeight = 20;
            this.lb_log.Location = new System.Drawing.Point(0, 0);
            this.lb_log.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lb_log.Name = "lb_log";
            this.lb_log.Size = new System.Drawing.Size(982, 260);
            this.lb_log.TabIndex = 0;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.lv_detial);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.lb_log);
            this.splitContainer1.Size = new System.Drawing.Size(982, 653);
            this.splitContainer1.SplitterDistance = 388;
            this.splitContainer1.SplitterWidth = 5;
            this.splitContainer1.TabIndex = 2;
            // 
            // lv_detial
            // 
            this.lv_detial.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.No,
            this.Items,
            this.Status,
            this.Value,
            this.Low,
            this.TypHeader,
            this.UpHeader1,
            this.UnitHeader1,
            this.DurationHeader1});
            this.lv_detial.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lv_detial.GridLines = true;
            this.lv_detial.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lv_detial.Location = new System.Drawing.Point(0, 0);
            this.lv_detial.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lv_detial.MultiSelect = false;
            this.lv_detial.Name = "lv_detial";
            this.lv_detial.Size = new System.Drawing.Size(982, 388);
            this.lv_detial.TabIndex = 2;
            this.lv_detial.UseCompatibleStateImageBehavior = false;
            this.lv_detial.View = System.Windows.Forms.View.Details;
            // 
            // No
            // 
            this.No.Text = "No";
            // 
            // Items
            // 
            this.Items.Text = "Items";
            this.Items.Width = 191;
            // 
            // Status
            // 
            this.Status.Text = "Status";
            this.Status.Width = 82;
            // 
            // Value
            // 
            this.Value.Text = "Value";
            this.Value.Width = 264;
            // 
            // Low
            // 
            this.Low.Text = "Low";
            this.Low.Width = 63;
            // 
            // TypHeader
            // 
            this.TypHeader.Text = "Typ";
            // 
            // UpHeader1
            // 
            this.UpHeader1.Text = "Up";
            // 
            // UnitHeader1
            // 
            this.UnitHeader1.Text = "Unit";
            // 
            // DurationHeader1
            // 
            this.DurationHeader1.Text = "Duration";
            this.DurationHeader1.Width = 83;
            // 
            // DetialView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(982, 653);
            this.Controls.Add(this.splitContainer1);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "DetialView";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "DetialView";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.DetialView_FormClosed);
            this.Load += new System.EventHandler(this.DetialView_Load);
            this.Shown += new System.EventHandler(this.DetialView_Shown);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private ListViewNF lv_detial;
        private System.Windows.Forms.ListBox lb_log;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ColumnHeader No;
        private System.Windows.Forms.ColumnHeader Items;
        private System.Windows.Forms.ColumnHeader Status;
        private System.Windows.Forms.ColumnHeader Value;
        private System.Windows.Forms.ColumnHeader Low;
        private System.Windows.Forms.ColumnHeader TypHeader;
        private System.Windows.Forms.ColumnHeader UpHeader1;
        private System.Windows.Forms.ColumnHeader UnitHeader1;
        private System.Windows.Forms.ColumnHeader DurationHeader1;
    }
}