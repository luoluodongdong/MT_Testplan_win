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
    public delegate void SendSNs2MTEventHandler(List<string> SNs);
    public partial class ScanSN_Form : Form
    {
        public ScanSN_Form()
        {
            InitializeComponent();
        }
        
        public Dictionary<string, string> config;
        public string firstSN;
        public List<bool> arrSelected;
        private List<string> SNs;
        private int nextSelectedIndex;
        private string mode;
        public event SendSNs2MTEventHandler SendSNs2MTEvent;
        #region ScanSN Form
        private void ScanSN_Form_Load(object sender, EventArgs e)
        {
            SNs = new List<string>();
            mode = config["MODE"];
            //绑定输入SN后的回车事件
            tb_scanSN.KeyUp += new KeyEventHandler(tb_scanSN_KeyUp);
            Console.WriteLine("FirstSN:" + firstSN);
            lb_showSNs.Items.Clear();
            
        }
        private void ScanSN_Form_Shown(object sender, EventArgs e)
        {
            nextSelectedIndex = saveSnAndGetNextIndex(0, firstSN);
            Console.WriteLine("nextSelectedIndex:" + nextSelectedIndex);

            if (nextSelectedIndex == -1)
            {
                //MessageBox.Show("Finish Scan SN!");
                FinishScanSN();

            }
        }
        #endregion

        #region Scan SN Event
        //响应输入SN后的回车事件
        private void tb_scanSN_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                tb_scanSN.Enabled = false;
                if (tb_scanSN.Text.Length > 0)
                {
                    //lb_showSN.Text = tb_scanSN.Text;
                    //SendMsg2MTEvent(_id, "SN:" + tb_scanSN.Text);
                    
                    //Console.WriteLine("SN:" + tb_scanSN.Text);
                    nextSelectedIndex = saveSnAndGetNextIndex(nextSelectedIndex, tb_scanSN.Text);
                    Console.WriteLine("nextSelectedIndex:" + nextSelectedIndex);
                    //lb_showSNs.Items.Add(tb_scanSN.Text);
                    //MT_Form.getSNsFromScanSNForm(tb_scanSN.Text.ToString());
                    tb_scanSN.Text = "";

                    if (nextSelectedIndex == -1)
                    {
                        FinishScanSN();
                       
                    }

                }
                tb_scanSN.Enabled = true;
                tb_scanSN.Focus();
            }
        }
        private int saveSnAndGetNextIndex(int currentIndex, string sn)
        {
            int nextIndex = currentIndex + 1;
            for (int i = currentIndex; i < arrSelected.Count; i++)
            {
                if (arrSelected[i])
                {

                    if (!SNisOK(sn)) return nextIndex - 1;
                    SNs.Add(sn);
                    lb_showSNs.Items.Add(string.Format("{0}.{1}", (i + 1).ToString(), sn));
                    break;
                }
                else
                {
                    SNs.Add("");
                    lb_showSNs.Items.Add(string.Format("{0}.{1}", (i + 1).ToString(), "SKIP"));
                    nextIndex += 1;
                }
            }
            if (nextIndex == arrSelected.Count) return -1;

            for (int i = nextIndex; i < arrSelected.Count; i++)
            {
                if (arrSelected[i])
                {
                    break;
                }
                else
                {
                    SNs.Add("");
                    lb_showSNs.Items.Add(string.Format("{0}.{1}", (i + 1).ToString(), "SKIP"));
                    nextIndex += 1;
                }
            }

            if (nextIndex == arrSelected.Count) return -1;

            return nextIndex;
        }
        private void FinishScanSN()
        {
            //MessageBox.Show("Finish Scan SN!");
            Console.WriteLine("Finish Scan SN!");
            SendSNs2MTEvent(SNs);
            Close();
        }
        #endregion

        #region Check SN is OK?
        private bool SNisOK(string sn)
        {
            foreach (string item in SNs)
            {
                if (sn.Equals(item))
                {
                    MessageBox.Show("SN repeat error!");
                    return false;
                }
            }

            if (!mode.Equals("TEST")) return true;

            int sn_length = int.Parse(config["SN_LENGTH"]);
            if (sn.Length != sn_length)
            {
                MessageBox.Show("SN Length error!");
                return false;
            }

            return true;
        }
        #endregion

    }
}
