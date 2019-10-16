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
    public partial class DetialView : Form
    {
        public DetialView()
        {
            InitializeComponent();
        }
        private BindingList<string> listData = new BindingList<string>();
        
        public string testplanFile;
        public int _id;

        bool _show_view;
        //XmlOperat xo = new XmlOperat();
        LoadTestPlan ltp = new LoadTestPlan();

        private object objLock = new object();

        #region DetialView Event
        private void DetialView_Load(object sender, EventArgs e)
        {
            Text = string.Format("Slot-{0}", _id.ToString());
            _show_view = true;
        }
        private void DetialView_Shown(object sender, EventArgs e)
        {
            lb_log.DataSource = listData;
            if (lb_log.Items.Count > 5)
            {
                lb_log.TopIndex = lb_log.Items.Count - 1;
            }
            lb_log.Focus();
        }
        private void DetialView_FormClosed(object sender, FormClosedEventArgs e)
        {
            Console.WriteLine("detialView closed");
            _show_view = false;
            lb_log.DataSource = null;
        }
        #endregion

        #region Init DetialView
        public void initDetialView()
        {
            //_show_view = false;
            ltp.csvFile = testplanFile;
            if (ltp.LoadCSVTestPlan())
            {
                loadTestItems();
            }
            else
            {
                MessageBox.Show("Csv file load error!");
            }
            if (_show_view)
            {
                if (listData.Count > 100000) listData.Clear();
            }
            else
            {
                listData.Clear();
            }

            //lb_log.Items.Clear();
            //lb_log.DataSource = listData;
        }
        #endregion

        #region Update LogList
        public void updateLogList(string log)
        {
            lock (objLock)
            {
                listData.Add(log);
                //Console.WriteLine(log);
                if (_show_view && lb_log.Items.Count > 1)
                {
                    lb_log.TopIndex = lb_log.Items.Count - 1;
                }
            }
            
        }
        #endregion

        #region Print Testing STATUS
        public void printTesting(int row)
        {
            printStr2ListView(row, 2, "TESTING");
        }
        #endregion

        #region Load Test Items
        private void loadTestItems()
        {
            this.lv_detial.Items.Clear();
            Console.WriteLine("Info:begin load items...");
            this.lv_detial.BeginUpdate();
            for (int i = 0; i < ltp.itemNameList.Count; i++)
            {
                //column #1: number
                ListViewItem i_item = lv_detial.Items.Add((lv_detial.Items.Count + 1) + "");
                //column #2:items
                i_item.SubItems.Add(ltp.itemNameList[i]);
                //column #3:status
                i_item.SubItems.Add("");
                //column #4:value
                i_item.SubItems.Add("");
                //column #5:low
                i_item.SubItems.Add(ltp.lowerLimitList[i]);
                //column #6 typ
                i_item.SubItems.Add(ltp.typValueList[i]);
                //column #7:up
                i_item.SubItems.Add(ltp.upperLimitList[i]);
                //column #8:unit
                i_item.SubItems.Add(ltp.measUnitList[i]);
                //column #9:duration
                i_item.SubItems.Add("");
            }

            //设置行背景颜色
            //  listView1.Items[2].BackColor = Color.LightGreen;

            this.lv_detial.EndUpdate();
            //滚动至第一行
            this.lv_detial.EnsureVisible(0);
            Console.WriteLine("Info:load successful");
        }
        #endregion

        #region ListView
        //listView 第row行 第columns列 显示字符串str
        public void printStr2ListView(int row, int columns, string str)
        {
            //row = row - 1;
            //columns = columns - 1;
            //Invoke((EventHandler)(delegate {
                lv_detial.Items[row].SubItems[columns].Text = str;
                //this.listView1.Items[row].UseItemStyleForSubItems = false;
                if (lv_detial.Items[row].SubItems[2].Text == "PASS")
                {
                    lv_detial.Items[row].BackColor = Color.FromArgb(0, 210, 0);
                }
                else if (lv_detial.Items[row].SubItems[2].Text == "FAIL")
                {
                    lv_detial.Items[row].BackColor = Color.Red;
                }
                else if (lv_detial.Items[row].SubItems[2].Text == "TESTING")
                {
                    lv_detial.Items[row].BackColor = Color.Yellow;
                }
                else if (lv_detial.Items[row].SubItems[2].Text == "SKIPED")
                {
                    lv_detial.Items[row].BackColor = Color.Gray;
                }
                else
                {
                    lv_detial.Items[row].BackColor = Color.LightGray;
                }
                //滚动至row行
                this.lv_detial.EnsureVisible(row);
              //  Application.DoEvents();
            //}));

        }
        //listView 第row行 显示字符串2-status 3-value 8-duration
        public void printMsg2ListView(int row, string status,string value,string duration)
        {
            //row = row - 1;
            //columns = columns - 1;
           // Invoke((EventHandler)(delegate {
                lv_detial.Items[row].SubItems[2].Text = status;
                lv_detial.Items[row].SubItems[3].Text = value;
                lv_detial.Items[row].SubItems[8].Text = duration;
                //this.listView1.Items[row].UseItemStyleForSubItems = false;
                if (lv_detial.Items[row].SubItems[2].Text == "PASS")
                {
                    lv_detial.Items[row].BackColor = Color.FromArgb(0, 210, 0);
                }
                else if (lv_detial.Items[row].SubItems[2].Text == "FAIL")
                {
                    lv_detial.Items[row].BackColor = Color.Red;
                }
                else if (lv_detial.Items[row].SubItems[2].Text == "TESTING")
                {
                    lv_detial.Items[row].BackColor = Color.Yellow;
                }
                else if (lv_detial.Items[row].SubItems[2].Text == "SKIPED")
                {
                    lv_detial.Items[row].BackColor = Color.Gray;
                }
                else
                {
                    lv_detial.Items[row].BackColor = Color.LightGray;
                }
                //滚动至row行
                this.lv_detial.EnsureVisible(row);
               // Application.DoEvents();
           // }));

        }
        #endregion

        
    }
}
