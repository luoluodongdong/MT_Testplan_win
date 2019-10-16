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

namespace MT_ICT
{
    public partial class myDeviceUC : UserControl
    {
        public myDeviceUC()
        {
            InitializeComponent();
        }
        public delegate void SaveConfigEventHandler(int id,Dictionary<string,string> cfg);
        public event SaveConfigEventHandler saveConfigEvent;
        public string groupName;
        public string serialPortName;
        public int serialPortBaud;
        public string slot_name;
        public int dev_id;

        #region Device UC load
        private void myDeviceUC_Load(object sender, EventArgs e)
        {
            DeviceGB.Text = groupName;
            PortsCB.Items.Clear();
            PortsCB.Items.AddRange(SerialPort.GetPortNames());

            if (PortsCB.Items.Count != 0)
            {
                PortsCB.SelectedIndex = 0;
            }
            
        }
        
        public void initDeviceUC()
        {
            PortsCB.Items.Clear();
            PortsCB.Items.AddRange(SerialPort.GetPortNames());
        }
        public void showDeviceUC()
        {
            if (serialPort1.IsOpen)
            {
                PortsCB.SelectedItem = serialPortName;
                PortsCB.Enabled = false;
                OpenBtn.Text = "Close";
                ScanBtn.Enabled = false;
            }
            else
            {
                Console.WriteLine("serial port not opened!");
            }
        }
        #endregion

        #region delegate event
        private void OnSaveConfigEvent(int id)
        {
            Dictionary<string, string> cfg = new Dictionary<string, string>();
            cfg.Add("Port",serialPort1.PortName);
            saveConfigEvent(id,cfg);
        }
        #endregion

        #region Button Open/Scan Click event
        private void OpenBtn_Click(object sender, EventArgs e)
        {
            if (PortsCB.SelectedItem == null) return;
            if (OpenBtn.Text.Equals("Open"))
            {
                serialPort1.BaudRate = serialPortBaud;
                serialPort1.PortName = PortsCB.SelectedItem.ToString();
                bool error_flag = false;
                try
                {
                    serialPort1.Open();
                    OpenBtn.Text = "Close";
                    ScanBtn.Enabled = false;
                    PortsCB.Enabled = false;
                }
                catch (Exception ex)
                {
                    showMessageBox("Error:" + ex.ToString());
                    error_flag = true;
                }

                if (!error_flag)
                {
                    serialPortName = serialPort1.PortName;
                    OnSaveConfigEvent(dev_id);
                }

            }
            else
            {
                try
                {
                    serialPort1.Close();

                }
                catch (Exception ex)
                {
                    showMessageBox("Error:" + ex.ToString());
                }
                finally
                {
                    OpenBtn.Text = "Open";
                    ScanBtn.Enabled = true;
                    PortsCB.Enabled = true;
                }
            }
        }

        private void ScanBtn_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                serialPort1.Close();
                OpenBtn.Text = "Open";
            }
            PortsCB.Items.Clear();
            PortsCB.Items.AddRange(SerialPort.GetPortNames());
            if (PortsCB.Items.Count != 0)
            {
                PortsCB.SelectedIndex = 0;
            }
        }
        #endregion

        #region Auto Open Port Function
        public bool AutoOpenSerialPort()
        {
            int count = PortsCB.Items.Count;
            if (count == 0)
            {
                showMessageBox("No any port exist!");
                return false;
            }
            bool find_flag = false;
            for (int i = 0; i < count; i++)
            {
                if (serialPortName == PortsCB.Items[i].ToString())
                {
                    PortsCB.SelectedIndex = i;
                    find_flag = true;
                    break;
                }
            }
            if (find_flag)
            {

                try
                {
                    serialPort1.BaudRate = serialPortBaud;
                    serialPort1.PortName = serialPortName;
                    serialPort1.Open();
                }
                catch (Exception ex)
                {
                    showMessageBox("Error:" + ex.ToString());
                }
            }
            else
            {
                showMessageBox("Port not exist!");
            }
            if (!serialPort1.IsOpen)
            {
                return false;
            }
            else
            {
                Thread.Sleep(1000);
                serialPort1.ReadExisting();
            }
            return true;
        }
        #endregion

        #region Show Message Box with ID
        private void showMessageBox(string msg)
        {
            string str = string.Format("[{0}-{1}]:{2}", slot_name,dev_id, msg);
            Dialog dlg = new Dialog();
            dlg.message = str;
            dlg.backcolor = Color.IndianRed;
            dlg.ShowDialog();
            //MessageBox.Show(str);
            //myPrintf(msg);
        }
        #endregion

        #region Send & Receive Command(Serial Port)
        public bool SendCmd(string cmd)
        {
            Console.WriteLine(string.Format("send:" + cmd));
            try
            {
                cmd += Environment.NewLine;
                serialPort1.WriteLine(cmd);

            }
            catch (Exception ex)
            {
                showMessageBox("Write Error:" + ex.ToString());
                return false;
            }

            return true;
        }
        public bool ReadData(ref string recData, double timeout, string matchStr)
        {
            bool isTimeOut = true;
            recData = "";
            Thread.Sleep(100);
            DateTime startT = DateTime.Now;
            double duration = 0.00;
            //string receiveStr = "";
            while (duration <= timeout)
            {
                Thread.Sleep(20);
                recData += serialPort1.ReadExisting();
                if (recData.Contains(matchStr))
                {
                    Thread.Sleep(100);
                    recData += serialPort1.ReadExisting();
                    isTimeOut = false;
                    break;
                }
                TimeSpan span = DateTime.Now - startT;
                duration = span.TotalSeconds;
            }
            recData = recData.Trim();
            Console.WriteLine(recData);
            return isTimeOut;
        }
        public bool SendCmdWithTimeOut(string cmd, double timeout, out string receiveStr, out bool isTimeOut)
        {
            receiveStr = "";
            isTimeOut = true;
            Console.WriteLine(string.Format("send:" + cmd));
            try
            {
                cmd += Environment.NewLine;
                serialPort1.WriteLine(cmd);

            }
            catch (Exception ex)
            {
                showMessageBox("Write Error:" + ex.ToString());
                return false;
            }

            DateTime startT = DateTime.Now;
            double duration = 0.00;
            //string receiveStr = "";
            while (duration <= timeout)
            {
                Thread.Sleep(20);
                receiveStr += serialPort1.ReadExisting();
                if (receiveStr.EndsWith("\r\n"))
                {
                    isTimeOut = false;
                    break;
                }

                TimeSpan span = DateTime.Now - startT;
                duration = span.TotalSeconds;
            }
            return true;
        }
        public bool SendCmdAndMatchStrWithTimeOut(string cmd, double timeout, out string receiveStr, out bool isTimeOut, string matchStr)
        {
            receiveStr = "";
            isTimeOut = true;
            Console.WriteLine(string.Format("control board send:" + cmd));
            if (!serialPort1.IsOpen) return false;
            try
            {
                cmd += Environment.NewLine;
                serialPort1.WriteLine(cmd);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Write Error:" + ex.ToString());
                return false;
            }
            Thread.Sleep(100);
            DateTime startT = DateTime.Now;
            double duration = 0.00;
            //string receiveStr = "";
            while (duration <= timeout)
            {
                Thread.Sleep(20);
                receiveStr += serialPort1.ReadExisting();
                Console.WriteLine("rec:" + receiveStr);
                if (receiveStr.Contains(matchStr) && receiveStr.EndsWith("\r\n"))
                {
                    Thread.Sleep(20);
                    receiveStr += serialPort1.ReadExisting();
                    isTimeOut = false;
                    break;
                }
                TimeSpan span = DateTime.Now - startT;
                duration = span.TotalSeconds;
            }
            
            return true;
        }
        public void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {

        }
        #endregion

    }
}
