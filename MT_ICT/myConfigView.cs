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
    public partial class myConfigView : Form
    {
        public myConfigView()
        {
            InitializeComponent();
        }
        public delegate void MsgFromConfigView(string msg);
        public event MsgFromConfigView msgFromConfigView;
        //public JsonHelper.myModel.my_dev device;
        public string cfgName="MT_Config";
        public int slot_id = 0;
        public bool all_devices_is_ready;
        private List<bool> deviceStatusList;
        /*------------------------------*/
        //serial port dictionary and name list
        myDeviceUC serialDev;
        public Dictionary<string, myDeviceUC> serialDict;
        List<string> serialNameList;
        /*-------------------------------*/
        /*------------------------------*/
        //NIVISA instrument port dictionary and name list
        myInstrumentUC instrumentDev;
        public Dictionary<string, myInstrumentUC> instrDict;
        List<string> instrNameList;
        /*-------------------------------*/

        JsonHelper json_helper;
        private void MyConfigView_Load(object sender, EventArgs e)
        {
            myPrint("load...");
            //loadDevices();
            for (int i = 0; i < serialNameList.Count; i++)
            {
                serialDict[serialNameList[i]].showDeviceUC();
            }
            for (int i = 0; i < instrNameList.Count; i++)
            {
                instrDict[instrNameList[i]].showInstrumentUC();
            }
        }
        public void loadDevices()
        {
            all_devices_is_ready = false;
            deviceStatusList = new List<bool>();
            serialDict = new Dictionary<string, myDeviceUC>();
            serialNameList = new List<string>();
            instrDict = new Dictionary<string, myInstrumentUC>();
            instrNameList = new List<string>();

            List<Dictionary<string, string>>  devList = new List<Dictionary<string, string>>();
            string cfgJsonFile = Application.StartupPath + @"\cfg.json";
            json_helper = new JsonHelper();
            json_helper.jsonFile = cfgJsonFile;
            json_helper.loadJson();

            Dictionary<string,string> dev = json_helper.devDict[cfgName].Dev1;
            if (null != dev)
            {
                devList.Add(dev);
            }
            dev = json_helper.devDict[cfgName].Dev2;
            if (null != dev)
            {
                devList.Add(dev);
            }

            int index = 0;
            for(int i = 0; i < devList.Count; i++)
            {
                //JsonHelper.myModel json_model = json_helper.cfgModel;
                string dev_name = devList[i]["Name"];
                string dev_desc = devList[i]["Description"];
                int dev_id = int.Parse(devList[i]["ID"]);
                string dev_load = devList[i]["Load"];
                string dev_type = devList[i]["Type"];
                string dev_port = devList[i]["Port"];
                int dev_baud = int.Parse(devList[i]["BaudRate"]);
                myPrint(dev_name);

                if("NO" == dev_load)
                {
                    continue;
                }
                if("SERIAL" == dev_type)
                {
                    serialDev = new myDeviceUC();
                    serialDev.slot_name = cfgName;
                    serialDev.dev_id = dev_id;
                    serialDev.groupName = dev_desc;
                    serialDev.serialPortName = dev_port;
                    serialDev.serialPortBaud = dev_baud;
                    serialDev.saveConfigEvent += new myDeviceUC.SaveConfigEventHandler(devUC_saveCfgEvent);
                    serialDev.Location = new Point(10, 20 + 80 * index);
                    Controls.Add(serialDev);
                    serialDev.initDeviceUC();
                    if (serialDev.AutoOpenSerialPort())
                    {
                        myPrint("serial port:" + dev_name + " Opened OK");
                        deviceStatusList.Add(true);
                    }
                    else
                    {
                        myPrint("serial port:" + dev_name + " Opened NG");
                        deviceStatusList.Add(false);
                    }

                    serialDict.Add(dev_name, serialDev);
                    serialNameList.Add(dev_name);

                    //myPrint(serialDev.GetType().ToString());
                }
                else if("INSTR" == dev_type)
                {
                    instrumentDev = new myInstrumentUC();
                    instrumentDev.slot_name = cfgName;
                    instrumentDev.instr_id = dev_id;
                    instrumentDev.groupName = dev_desc;
                    instrumentDev.instrAddr = dev_port;
                    instrumentDev.instrBaudRate = dev_baud;
                    instrumentDev.saveConfigEvent += new myInstrumentUC.SaveConfigEventHandler(devUC_saveCfgEvent);
                    instrumentDev.Location = new Point(10, 20 + 80 * index);
                    Controls.Add(instrumentDev);
                    instrumentDev.initInstrumentUC();
                    if (instrumentDev.AutoOpenInstrument())
                    {
                        myPrint("instrument port:" + dev_name + " Opened OK");
                        deviceStatusList.Add(true);
                    }
                    else
                    {
                        myPrint("instrument port:" + dev_name + " Opened NG");
                        deviceStatusList.Add(false);
                    }
                    instrDict.Add(dev_name, instrumentDev);
                    instrNameList.Add(dev_name);
                }
                else
                {
                    MessageBox.Show("Unknown device type:" + dev_type);
                }
                index += 1;
            }
            updateDevStatus();
        }
        public void closeDevices()
        {
            for(int i = 0; i < serialNameList.Count; i++)
            {
                myDeviceUC dev = serialDict[serialNameList[i]];
                if (dev.serialPort1.IsOpen)
                {
                    dev.serialPort1.Close();
                    myPrint("serial port:"+serialNameList[i] +" closed");
                }
                
            }
            for (int i = 0; i < instrNameList.Count; i++)
            {
                myInstrumentUC dev = instrDict[instrNameList[i]];
                dev.closeInstr();
                myPrint("instrument port:" + instrNameList[i] + " closed");
            }
        }
        private bool updateDevStatus()
        {
            all_devices_is_ready = true;
            foreach (bool dev_status in deviceStatusList)
            {
                if (!dev_status)
                {
                    all_devices_is_ready = false;
                    break;
                }
            }
            return all_devices_is_ready;   
        }
        #region Device UserControl event
        private void devUC_saveCfgEvent(int id,Dictionary<string,string> cfg)
        {
            myPrint("devUC id:" + id.ToString());
            if (id == 1000)
            {
                json_helper.devDict[cfgName].Dev1["Port"] = cfg["Port"];
                deviceStatusList[0] = true;
            }
            else if(id == 2000)
            {
                json_helper.devDict[cfgName].Dev2["Port"] = cfg["Port"];
                deviceStatusList[1] = true;
            }
            if (updateDevStatus())
            {
                msgFromConfigView("OK");
            }
            else
            {
                msgFromConfigView("NG");
            }
            bool status = json_helper.saveJson();

            MessageBox.Show("["+cfgName+"]:save port: "+cfg["Port"]+" "+status.ToString()+" !");
        }
        #endregion
        private void myPrint(string msg)
        {
            Console.WriteLine("[CFG-View-" + cfgName + "]:" + msg);
        }
    }
}
