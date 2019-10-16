using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using System.Threading;
using NONATEST;

namespace MT_ICT
{
    public partial class myInstrumentUC : UserControl
    {
        public myInstrumentUC()
        {
            InitializeComponent();
        }
        public delegate void SaveConfigEventHandler(int id, Dictionary<string, string> cfg);
        public event SaveConfigEventHandler saveConfigEvent;
        public string groupName;
        public string instrAddr;
        public int instrBaudRate;
        public string slot_name;
        public int instr_id;

        private Interface_3497xx _instrument;
        private bool _instr_isOpened;
        #region Insturment UC load
        private void myInstrumentUC_Load(object sender, EventArgs e)
        {
            PortsCB.Items.Clear();
            List<string> devices = _instrument.ScanDevices();

            foreach (string item in devices)
            {
                PortsCB.Items.Add(item);
                Console.WriteLine("device:" + item);
            }
        }
        #endregion
        public void initInstrumentUC()
        {
            _instrument = new _34970();
            _instr_isOpened = false;

            DeviceGB.Text = groupName;
            PortsCB.Items.Clear();
            List<string> devices = _instrument.ScanDevices();

            foreach (string item in devices)
            {
                PortsCB.Items.Add(item);
                Console.WriteLine("device:" + item);
            }
            // cb.Items.AddRange(SerialPort.GetPortNames());
            if (PortsCB.Items.Count != 0)
            {
                PortsCB.SelectedIndex = 0;
            }
        }
        public void showInstrumentUC()
        {
            if (_instr_isOpened)
            {
                PortsCB.SelectedItem = instrAddr;
                OpenBtn.Text = "Close";
                ScanBtn.Enabled = false;
                PortsCB.Enabled = false;
                OpenBtn.Enabled = true;
            }
            else
            {
                if (PortsCB.Items.Count != 0)
                {
                    PortsCB.SelectedIndex = 0;
                }
                OpenBtn.Text = "Open";
                ScanBtn.Enabled = true;
                PortsCB.Enabled = true;
                OpenBtn.Enabled = true;
            }
        }
        public void closeInstr()
        {
            if (_instr_isOpened)
            {
                _instrument.Close();
            }
        }
        #region delegate event
        private void OnSaveConfigEvent(int id, Dictionary<string, string> cfg)
        {
            //Dictionary<string,string> cfg =new Dictionary<string,string>()
            saveConfigEvent(id,cfg);
        }
        #endregion

        #region Button Open/Scan Click event
        private void OpenBtn_Click(object sender, EventArgs e)
        {
            ManualOpenInstrument(ref instrAddr, ref _instrument, ref OpenBtn, ref ScanBtn, ref PortsCB);
            if (_instr_isOpened)
            {
                Dictionary<string, string> cfg = new Dictionary<string, string>();
                cfg.Add("Port", instrAddr);
                OnSaveConfigEvent(instr_id, cfg);
            }
        }

        private void ScanBtn_Click(object sender, EventArgs e)
        {
            RefreshInstruments(_instrument, OpenBtn, PortsCB);
        }
        #endregion

        #region  Operate Instrument Function
        private void RefreshInstruments(Interface_3497xx instr, Button open, ComboBox cb)
        {
            if (_instr_isOpened)
            {
                instr.Close();
                open.Text = "Open";
            }
            cb.Items.Clear();
            List<string> devices = _instrument.ScanDevices();

            foreach (string item in devices)
            {
                cb.Items.Add(item);
                Console.WriteLine("device:" + item);
            }
            // cb.Items.AddRange(SerialPort.GetPortNames());
            if (cb.Items.Count != 0)
            {
                cb.SelectedIndex = 0;
            }

        }
        private void ManualOpenInstrument(ref string addr, ref Interface_3497xx instr, ref Button open, ref Button scan, ref ComboBox cb)
        {
            if (open.Text.Equals("Open"))
            {
                if (cb.SelectedItem == null) return;
                Console.WriteLine("instru.baud:" + instrBaudRate.ToString());
                string addrSelected = cb.SelectedItem.ToString();

                bool error_flag = false;
                try
                {
                    //sp.RtsEnable = true;
                    //sp.DtrEnable = true;
                    if (instr.Open(addrSelected, 2000, instrBaudRate))
                    {
                        open.Text = "Close";
                        scan.Enabled = false;
                        cb.Enabled = false;
                        _instr_isOpened = true;
                    }
                    else
                    {
                        _instr_isOpened = false;
                        MessageBox.Show("Open fail,please check setting!");
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error:" + ex.ToString());
                    error_flag = true;
                    _instr_isOpened = false;
                }

                if (!error_flag)
                {
                    //MessageBox.Show("Open device successful!");
                    //string SERIAL_key = string.Format("SERIAL{0}", _id);
                    addr = addrSelected; 
                }

            }
            else
            {
                try
                {
                    instr.Close();

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error:" + ex.ToString());
                }
                finally
                {
                    open.Text = "Open";
                    scan.Enabled = true;
                    cb.Enabled = true;
                }
            }
        }
        public bool AutoOpenInstrument()
        {
            int count = PortsCB.Items.Count;
            if (count == 0)
            {
                showMessageBox("Instrument:No any port exist!");
                return false;
            }
            bool find_flag = false;
            for (int i = 0; i < count; i++)
            {
                if (instrAddr == PortsCB.Items[i].ToString())
                {
                    PortsCB.SelectedIndex = i;
                    find_flag = true;
                    break;
                }
            }
            if (find_flag)
            {

                ManualOpenInstrument(ref instrAddr, ref _instrument, ref OpenBtn, ref ScanBtn, ref PortsCB);
            }
            else
            {
                showMessageBox("Port not exist!");
            }
            if (!_instr_isOpened) return false;
            return true;
        }
        #endregion

        #region Show Message Box with ID
        private void showMessageBox(string msg)
        {
            string str = string.Format("[{0}-{1}]:{2}", slot_name,instr_id, msg);
            Dialog dlg = new Dialog();
            dlg.message = str;
            dlg.backcolor = Color.IndianRed;
            dlg.ShowDialog();
            //MessageBox.Show(str);
            //myPrintf(msg);
        }
        #endregion

        #region Instrument Send & Receive Command(NI VISA)
        private bool _instrSendCmd(string cmd)
        {
            if (!_instr_isOpened) return false;
            Thread.Sleep(100);
            Console.WriteLine(string.Format("[TX]:" + cmd));
            return _instrument.WriteLine(cmd); ;
        }
        public bool SendCmdWithTimeOut(string cmd,double timeout,out string response,out bool isTimeOut)
        {
            response = "";
            isTimeOut = true;
            if (!_instr_isOpened) return false;
            Console.WriteLine(string.Format("[INSTR_TX]:" + cmd));

            if (_instrument.WriteLine(cmd))
            {
                Thread.Sleep(100);
                response = _instrument.ReadLine();
                Console.WriteLine(string.Format("[INSTR_RX]:" + response));
            }
            else
            {
                MessageBox.Show("Instrument Write Error:" + cmd);
                return false;
            }
            if("" != response)
            {
                isTimeOut = false;
            }
            return true;
        }
        public bool _instrSendCmdAndRec(string cmd, ref string recStr)
        {
            if (!_instr_isOpened) return false;
            Console.WriteLine(string.Format("[INSTR_TX]:" + cmd));
            recStr = "";

            if (_instrument.WriteLine(cmd))
            {
                Thread.Sleep(100);
                recStr = _instrument.ReadLine();
                Console.WriteLine(string.Format("[INSTR_RX]:" + recStr));
            }
            else
            {
                MessageBox.Show("Write Error:" + cmd);
                return false;
            }
            return true;
        }
        public bool _instrSendCmds(string cmd)
        {
            if (!_instr_isOpened) return false;
            if (!cmd.Contains(";")) return _instrSendCmd(cmd);
            string[] cmds = cmd.Split(';');
            bool send_flag = true;
            foreach (string item in cmds)
            {
                if (item.Equals("")) continue;
                send_flag = _instrSendCmd(item);
                if (!send_flag) break;
            }

            return send_flag;
        }
        #endregion
        
    }
}
