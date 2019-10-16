using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MT_ICT
{
    public partial class MT_LogView : Form
    {
        public MT_LogView()
        {
            InitializeComponent();
        }
        public BindingList<string> listData=new BindingList<string>();
        bool _show_view;
        private object objLock = new object();
        private void MT_LogView_Load(object sender, EventArgs e)
        {
            _show_view = true;
            //lb_logView.DataSource = listData;
        }
        public void initMTLogView()
        {
            if (_show_view) return; 
            listData.Clear();
            _show_view = false;
        }
        public void updateMTLog(string log)
        {
            lock (objLock)
            {
                listData.Add(log);
                //Console.WriteLine(log);
                if (_show_view && lb_logView.Items.Count > 1)
                {
                    lb_logView.TopIndex = lb_logView.Items.Count - 1;
                }

            }
            
        }
        private void MT_LogView_FormClosed(object sender, FormClosedEventArgs e)
        {
            _show_view = false;
            lb_logView.DataSource = null;
        }
        
        private void MT_LogView_Shown(object sender, EventArgs e)
        {
            lb_logView.DataSource = listData;
            if (lb_logView.Items.Count > 1)
            {
                lb_logView.TopIndex = lb_logView.Items.Count - 1;
            }
        }
    }
}
