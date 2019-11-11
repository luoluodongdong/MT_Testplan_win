using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.IO;

namespace MT_ICT
{
    public partial class MT_Form : Form
    {
        public Dictionary<string, string> loginMsg;
        public MT_Form()
        {
            InitializeComponent();
        }
        private static List<myUserControl> arrUserControl;
        private Dictionary<string, string> scanSN_CFG;
        private List<bool> arrSolt_selected;
        private static List<string> arrSNs;
        private int _selected_count;
        private int _UNIT_COUNT;
        private object objLock;
        private CIni ini;
        Password pw_form;
        myConfigView mtCfgView = new myConfigView();

        private Dictionary<string, string> cfg_dict;
        public struct CFG_info
        {
            public string swName;
            public string swVersion;
            public string mode;
            public string password;
            public long inputCount;
            public long passCount;
            public string csvPath;
            public string stationID;
            public string mesPATH;
            public string testplanPATH;
            public int unitCount;
            public string cfgFilePath;
            public string cfgJsonFile;
            public string testplanFile;
        }
        public struct CFG_test
        {
            public DateTime _startT;
            public bool _scan_sn_ready;
            public bool _testing_flag;
            public int _finished_count;
            public bool _test_result;
            public string _csv_data;
            public long _loop_count;
            public bool _loop_abort;
            //for async task
            public bool asyncKey;
            //for sync task
            public int _sync_request_count;
            public int _check_sync_sum;
            //slot close self count
            public int _slot_close_self_count;
            //check security is ok?
            public bool _check_security_isOK;
        }
        private CFG_info cfg_info;
        private static CFG_test cfg_test;
        MT_LogView mt_lv;
        LoadTestPlan ltp;
        //for uc async key

        //json config
        JsonHelper json_helper;
        JsonHelper.myModel json_model;

        #region MT Form
        private void MT_Form_Load(object sender, EventArgs e)
        {

            lb_debug.Visible = false;
            gb_loop.Visible = false;
            cfg_info = new CFG_info();

            objLock = new object();

            scanSN_CFG = new Dictionary<string, string>();

            
            arrSolt_selected = new List<bool>();
            arrSNs = new List<string>();

            Splash.Status = "状态:载入初始化模块...OK";
            Splash.ProgressValue = 10;

            KeyPreview = true; //enable key preview to main form
            //---------------------------------------------------
            /*         check security start                   */
            cfg_test._check_security_isOK = false;
            CheckSecurity cs = new CheckSecurity();
            string securityFolder = Application.StartupPath + @"\security";
            if (cs.verifySecurity(securityFolder)){
                Splash.Status = "状态:安全验证数字签名...OK";
                Splash.ProgressValue = 15;
                cfg_test._check_security_isOK = true;
            }
            else
            {
                showMessageBox("[Error]:Fatal error,check security failure!");
                Splash.Close();
                cfg_test._check_security_isOK = false;
                Application.Exit();
                return;
            }

            /*         check security end                    */
            //---------------------------------------------------
            cfg_info.cfgJsonFile = Application.StartupPath + @"\cfg.json";
            json_helper = new JsonHelper();
            json_helper.jsonFile = cfg_info.cfgJsonFile;
            json_helper.loadJson();
            json_model = json_helper.cfgModel;

            string mesPath = json_model.MESPath;
            Console.WriteLine(mesPath);
            Console.WriteLine(json_model.MT_Config.Dev1["Name"]);

            mesPath = json_helper.cfgDict["MESPath"];
            Console.WriteLine(mesPath);
            Console.WriteLine(json_helper.devDict["MT_Config"].Dev1["Name"]);

            //jsobj.MT_Config.Dev1["Name"] = "123";
            //jh.saveJson(jsobj, jh.jsonFile);

            cfg_info.cfgFilePath = Application.StartupPath + @"\yield.CFG";
            if (!readCFGInfo(cfg_info.cfgFilePath))
            {
                Activate();
                MessageBox.Show(string.Format("FATAL ERROR:{0} not exist!", cfg_info.cfgFilePath));
                Application.Exit();
                return;
            }
            Splash.Status = "状态:载入配置文件...OK";
            Splash.ProgressValue = 20;
            _UNIT_COUNT = cfg_info.unitCount;
            _selected_count = _UNIT_COUNT;
            cfg_info.testplanFile = Application.StartupPath + cfg_info.testplanPATH;//@"\TestPlan.csv";
            if (!File.Exists(cfg_info.testplanFile))
            {
                Activate();
                MessageBox.Show(string.Format("{0} not exist!", cfg_info.testplanFile));
                Application.Exit();
                return;
            }
            ltp = new LoadTestPlan();
            ltp.csvFile = cfg_info.testplanFile;
            if (!ltp.LoadCSVTestPlan())
            {
                Activate();
                MessageBox.Show("testplan file load fail!");
                Application.Exit();
                return;
            }
            Splash.Status = "状态:载入测试序列CSV...OK";
            Splash.ProgressValue = 30;

            mtCfgView.cfgName = "MT_Config";
            mtCfgView.Text = "MT Config View";
            mtCfgView.slot_id = 0;
            mtCfgView.msgFromConfigView += new myConfigView.MsgFromConfigView(mtCfgView_Msg);
            mtCfgView.loadDevices();

            arrUserControl = new List<myUserControl>();
            for (int i = 0; i < _UNIT_COUNT; i++)
            {
                Splash.Status = string.Format("状态:载入单元{0}测试模块...", i + 1);
                arrSolt_selected.Add(true);
                arrSNs.Add("");
                myUserControl uc = new myUserControl(i);
                if (i < 2)
                {
                    uc.Location = new Point(5 + i * 360, 10);
                }
                else
                {
                    uc.Location = new Point(5 + (i - 2) * 360, 10 + 260);
                }

                uc.SendMsg2MTEvent += new SendMsg2MTEventHandler(uc_SendMsg2MTEvent);
                uc.CallMTFunction += new CallMTFunctionHandler(uc_CallMTFunction);
                uc.cfg_dict = cfg_dict;
                uc.testplan_csvFile = cfg_info.testplanFile;

                tc_mainPanel.TabPages[0].Controls.Add(uc);
                arrUserControl.Add(uc);

                Splash.Status = string.Format("状态:载入单元{0}测试模块...OK",i+1);
                Splash.ProgressValue += 10;
            }

            for (int i = 0; i < _UNIT_COUNT; i++)
            {
                arrUserControl[i].ReceiveMsg("MODE:TEST");
            }

            scanSN_CFG.Add("MODE", "TEST");
            //test cfg
            cfg_test._scan_sn_ready = false;
            cfg_test._testing_flag = false;
            cfg_test._loop_abort = false;
            cfg_test._slot_close_self_count = 0;

            //绑定输入SN后的回车事件
            tb_scanSN.KeyUp += new KeyEventHandler(tb_scanSN_KeyUp);

            pw_form = new Password();
            pw_form.sendResult2MT += new SendPassWord2MTEventHandler(pw_sendResult2MT);

            mt_lv = new MT_LogView();
            mt_lv.initMTLogView();
            updatePannel();
            Splash.Status = "状态:注册触发事件...OK";
            Splash.ProgressValue = 100;
            Thread.Sleep(1000);
            Splash.Close();

            //显示登陆界面录入的信息
            string userName = loginMsg["user"];
            UserLabel.Text = "User:"+userName;
        }

        private void MT_Form_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.R && e.Control) //Ctrl+R -> start button
            {
                e.Handled = true;       //将Handled设置为true，指示已经处理过KeyDown事件   
                if (tc_mainPanel.SelectedIndex != 0) tc_mainPanel.SelectedIndex = 0;
                btn_Start.PerformClick(); //执行单击button1的动作   
            }
        }
        private void MT_Form_Shown(object sender, EventArgs e)
        {
            Activate();
            tb_scanSN.Focus();
        }
        private void MT_Form_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(false == cfg_test._check_security_isOK)
            {
                return;
            }
            e.Cancel = true;
            if (MessageBox.Show("Exit application？", "Exit", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                //Application.Exit();
            }
            else

            {
                //e.Cancel = true;
                return;
            }
            if (arrUserControl != null)
            {
                for (int i = 0; i < _UNIT_COUNT; i++)
                {
                    arrUserControl[i].ReceiveMsg("EXIT");
                }
            }        
            //Thread.Sleep(200);
            Thread closeThread = new Thread(new ThreadStart(closeSelf));
            closeThread.IsBackground = true;
            closeThread.Start();
        }
        //close MT self
        private void closeSelf()
        {
            //waiting all slots close self
            while (true)
            {
                if (cfg_test._slot_close_self_count == _UNIT_COUNT)
                {
                    break;
                }
                Thread.Sleep(100);
            }
            //close MT opened devices
            //...
            //exit this application
            Invoke((EventHandler)(delegate
            {
                Dispose();
            }));
           
            Application.Exit();
        }
        #endregion

        #region Read CFG file
        private bool readCFGInfo(string profile)
        {
            if (!File.Exists(profile))
                return false;

            ini = new CIni(profile);
            cfg_dict = new Dictionary<string, string>();
            //cfg_info.mesPATH = ini.getKeyValue("STATION", "MESPath");
            cfg_info.mesPATH = json_model.MESPath;
            cfg_dict.Add("MESPath", cfg_info.mesPATH);
            cfg_info.inputCount = long.Parse(ini.getKeyValue("STATION", "Input"));
            cfg_info.passCount = long.Parse(ini.getKeyValue("STATION", "Pass"));

            string stationCfg = Application.StartupPath + @"\security\station.CFG";
            CIni iniSecurity = new CIni(stationCfg);
            cfg_info.swName = iniSecurity.getKeyValue("STATION", "SWName");
            cfg_dict.Add("SWName", cfg_info.swName);
            cfg_info.swVersion = iniSecurity.getKeyValue("STATION", "SWVersion");
            cfg_dict.Add("SWVersion", cfg_info.swVersion);
            cfg_info.password = iniSecurity.getKeyValue("STATION", "PassWord");
            cfg_info.csvPath = iniSecurity.getKeyValue("STATION", "CSVPath");
            cfg_dict.Add("CSVPath", cfg_info.csvPath);
            cfg_info.stationID = iniSecurity.getKeyValue("STATION", "StationID");
            cfg_dict.Add("StationID", cfg_info.stationID);
            cfg_info.testplanPATH= iniSecurity.getKeyValue("STATION", "TestPlan");
            cfg_dict.Add("TestPlan", cfg_info.testplanFile);
            cfg_info.unitCount = iniSecurity.getKeyIntValue("STATION", "UnitCount");
            string sn_length = iniSecurity.getKeyValue("STATION", "SNLength");

            lb_swName.Text = cfg_info.swName;
            lb_swVersion.Text = cfg_info.swVersion;
            scanSN_CFG.Add("SN_LENGTH", sn_length);
            return true;
        }
        #endregion

        #region Message From Password
        //receive Message from password form
        private void pw_sendResult2MT(bool result)
        {
            if (result)
            {
                tc_mainPanel.SelectedIndex = 1;
            }
            else
            {
                tc_mainPanel.SelectedIndex = 0;
            }
        }
        #endregion

        #region Message From ScanSN
        //send SNs to MT Form
        private void getSNsFromScanSNForm(List<string> SNs)
        {
            for (int i = 0; i < SNs.Count; i++)
            {
                string msg = "SN:" + SNs[i];
                Console.WriteLine(msg);

                if (arrUserControl[i]._slot_selected)
                {
                    arrSNs[i] = SNs[i];
                    arrUserControl[i].ReceiveMsg(msg);
                }
                else
                {
                    arrSNs[i] = "";
                }
            }
            cfg_test._scan_sn_ready = true;
            //btn_Start.PerformClick();
            Thread t = new Thread(ThreadStartTest);
            t.Start();
        }
        private void ThreadStartTest()
        {
            Thread.Sleep(100);
            Invoke((EventHandler)(delegate
            {
                btn_Start.PerformClick();
            }));
        }
        #endregion

        #region Message From UC
        //receive Message from single usercontrol
        private void uc_SendMsg2MTEvent(int id,string msg)
        {
            lock (objLock)
            {
                myPrintf(string.Format("Hello,I am form id_{0}:{1}", id,msg));
                //System.Threading.Thread.Sleep(1000);
                //Finish:PASS   //FAIL
                if (msg.StartsWith("Finish"))
                {
                    cfg_test._finished_count += 1;
                    
                    cfg_info.inputCount += 1;

                    cfg_test._check_sync_sum -= 1;



                    string result = msg.Split(':')[1];
                    if (result == "PASS")
                    {
                        cfg_info.passCount += 1;
                    }
                    else
                    {
                        cfg_test._test_result = false;
                    }
                    
                    //check all unit finish test
                    if(cfg_test._finished_count == _selected_count)
                    {
                        timer1.Enabled = false;
                        myPrintf(cfg_test._csv_data);
                        myPrintf("All unit finish test.");
                        cfg_test._finished_count = 0;

                        //loop count
                        cfg_test._loop_count += 1;
                        lb_showLoopCount.Text = string.Format("F:{0}", cfg_test._loop_count);
                        if (cfg_test._loop_count == long.Parse(tb_setLoopCount.Text))
                        {
                            cfg_test._scan_sn_ready = false;
                        }
                        updatePannel();
                        if (cfg_test._test_result)
                        {
                            lb_Status.Text = "PASS";
                            lb_Status.BackColor = Color.FromArgb(124, 252, 0);
                        }
                        else
                        {
                            lb_Status.Text = "FAIL";
                            lb_Status.BackColor = Color.FromArgb(220,20,60);
                        }

                        saveCSV();

                        tb_scanSN.Enabled = true;
                        btn_Start.Enabled = true;
                        pb_refresh.Enabled = true;
                        tb_scanSN.Focus();
                        cfg_test._testing_flag = false;

                        //check finish loop test?
                        if(!cfg_test._loop_abort && cfg_test._loop_count < long.Parse(tb_setLoopCount.Text))
                        {
                            Thread t = new Thread(new ThreadStart(program_loop));

                            t.IsBackground = true;
                            t.Start();

                            
                        }
                        else
                        {
                            tb_setLoopCount.Enabled = true;
                            btn_loopAbort.Visible = true;
                            cfg_test._loop_abort = false;
                            myPrintf("-==FINISH ALL LOOP TEST==-");
                        }

                    }
                    
                }
                //Selected:0  //1
                else if (msg.StartsWith("Selected"))
                {
                    string isSelected = msg.Split(':')[1];
                    myPrintf("isSelected:" + isSelected);
                    if (isSelected.Equals("0"))
                    {
                        arrSolt_selected[id - 1] = false;
                        _selected_count -= 1;
                    }
                    else
                    {
                        arrSolt_selected[id - 1] = true;
                        _selected_count += 1;
                    }
                    for(int i = 0; i < _UNIT_COUNT; i++)
                    {
                        myPrintf(arrSolt_selected[i].ToString());
                    }
                }
                //request async KEY
                else if (msg.StartsWith("AsyncKEY"))
                {
                    string reply = "KEYisNG";
                    if (cfg_test.asyncKey)
                    {
                        cfg_test.asyncKey = false;
                        reply = "KEYisOK";
                    }
                    arrUserControl[id-1].ReceiveMsg(reply);

                }
                //return async KEY
                else if (msg.StartsWith("ReturnKEY"))
                {
                    cfg_test.asyncKey = true;
                }
                //sync task request
                else if (msg.StartsWith("SyncRequest"))
                {
                    string cmd = msg.Split('#')[1];
                    cfg_test._sync_request_count += 1;
                    if(cfg_test._sync_request_count == cfg_test._check_sync_sum)
                    {
                        Thread taskThread = new Thread(new ParameterizedThreadStart(ExecuteSyncTask));
                        taskThread.IsBackground = true; 
                        taskThread.Start(cmd);
                    } 
                }
                //close self count 
                else if (msg.StartsWith("CloseSelfOK"))
                {
                    cfg_test._slot_close_self_count += 1;
                    
                }
                //CSVDATA#SN001,PASS,...
                else if (msg.StartsWith("CSVDATA"))
                {
                    cfg_test._csv_data += msg.Split('#')[1];
                }
            }

        }
        #endregion

        #region UC Call MT Function (*Sub Thread)
        private Dictionary<string,string> uc_CallMTFunction(int slot_id,string cmd)
        {
            myPrintf(string.Format("CallMTFunction:[Slot-{0}]:{1}", slot_id, cmd));
            Dictionary<string,string> result =new Dictionary<string, string>();
            result.Add("response", "test");

            return result;
        }
        #endregion

        #region Pannel
        private void updatePannel()
        {
            
            ini.setKeyValue("STATION", "Input", cfg_info.inputCount.ToString());
            ini.setKeyValue("STATION", "Pass", cfg_info.passCount.ToString());


            long failL = cfg_info.inputCount - cfg_info.passCount;

            float fPassRate = 0;

            if (cfg_info.inputCount == 0)
            {
                fPassRate = 0;
            }
            else
            {
                fPassRate = (float)cfg_info.passCount / cfg_info.inputCount * 100f;
                myPrintf("fPassRate:" + fPassRate.ToString());
            }
            Invoke((EventHandler)(delegate
            {
                lb_input.Text = "Input:"+cfg_info.inputCount.ToString();
                lb_pass.Text = "Pass:"+cfg_info.passCount.ToString();
                lb_fail.Text = "Fail:"+failL.ToString();
                lb_yield.Text = "Yield:"+fPassRate.ToString("0.00") + "%";
            }));

        }
        #endregion

        #region Test Timer
        private void timer1_Tick(object sender, EventArgs e)
        {
            DateTime currentT = DateTime.Now;
            TimeSpan span = currentT - cfg_test._startT;
            int timerMi = span.Minutes;
            int timerS = span.Seconds;
            double timerM = span.Milliseconds; // / 1000.0;
            string timerM_str = timerM.ToString();
            int timerSec = timerMi * 60 + timerS;

            lb_Timer.Text = timerSec.ToString() + "." + timerM_str[0] + " s";
        }
        #endregion

        #region my Printf
        private void myPrintf(string log)
        {
            string msg = string.Format("[{0}]{1}", DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:fff"), log);
            Invoke((EventHandler)(delegate
            {
                mt_lv.updateMTLog(msg);
            }));
            
            Console.WriteLine(string.Format("[MT_Form]:{0}", log));
        }
        private void showMessageBox(string msg)
        {
            string str = string.Format("[MT]:{0}", msg);
            Dialog dlg = new Dialog();
            dlg.message = str;
            dlg.backcolor = Color.IndianRed;
            dlg.ShowDialog();
            //MessageBox.Show(str);
            //myPrintf(msg);
        }
        #endregion

        #region Click Event
        //响应输入SN后的回车事件
        private void tb_scanSN_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cfg_test._scan_sn_ready = false;
                //loop count
                cfg_test._loop_count = 0;
                lb_showLoopCount.Text = "F:0";

                for (int i = 0; i < _UNIT_COUNT; i++)
                {
                    arrSNs[i] = "";
                    if (arrUserControl[i]._slot_selected)
                    {
                        arrUserControl[i].ReceiveMsg("SCANSN");
                    }
                }

                bool find_selected = false;
                foreach (bool item in arrSolt_selected)
                {
                    if (item) find_selected = true;

                }
                if (!find_selected)
                {
                    MessageBox.Show("Not any slot selected!");
                    return;
                }
                if (tb_scanSN.Text.Length > 0)
                {
                    //lb_showSN.Text = tb_scanSN.Text;
                    //SendMsg2MTEvent(_id, "SN:" + tb_scanSN.Text);

                    ScanSN_Form snForm = new ScanSN_Form
                    {
                        config = scanSN_CFG,
                        firstSN = tb_scanSN.Text.ToString(),
                        arrSelected = arrSolt_selected,
                    };
                    snForm.SendSNs2MTEvent += new SendSNs2MTEventHandler(getSNsFromScanSNForm);
                    tb_scanSN.Text = "";
                    snForm.ShowDialog();
                }

            }
        }
        //start button event
        private void btn_Start_Click(object sender, EventArgs e)
        {
            if (!cfg_test._scan_sn_ready)
            {
                MessageBox.Show("Scan SN First");
                return;
            }
            cfg_test._startT = DateTime.Now;
            cfg_test._test_result = true;
            timer1.Enabled = true;
            cfg_test._finished_count = 0;
            cfg_test._csv_data = "";
            cfg_test._check_sync_sum = _selected_count;
            cfg_test.asyncKey = true;
            mt_lv.initMTLogView();
            //Thread.Sleep(100);
            myPrintf("Press Start Button");

            if (cfg_test._loop_count == 0)
            {
                Dialog dlg = new Dialog();
                dlg.message = "Plug in DUT,OK?";
                dlg.backcolor = Color.Yellow;
                dlg.ShowDialog();
                //MessageBox.Show("Connect DUT,OK?");
            }

            for (int i = 0; i < _UNIT_COUNT; i++)
            {
                if (arrSolt_selected[i])
                {
                    arrUserControl[i].ReceiveMsg("START");
                    Thread.Sleep(10);
                }

            }
            lb_Status.Text = "TEST...";
            lb_Status.BackColor = Color.Yellow;
            tb_scanSN.Text = "";
            tb_scanSN.Enabled = false;
            btn_Start.Enabled = false;
            pb_refresh.Enabled = false;
            tb_setLoopCount.Enabled = false;
            cfg_test._testing_flag = true;
        }
        //change tabcontrol index
        private void tc_mainPanel_SelectedIndexChanged(object sender, EventArgs e)
        {
            //check testing flag
            if (cfg_test._testing_flag)
            {
                tc_mainPanel.SelectedIndex = 0;
                return;
            }
            //check selected test page
            if (tc_mainPanel.SelectedIndex == 0)
            {
                tb_scanSN.Focus();
            }
            //check selected set page
            else if (tc_mainPanel.SelectedIndex == 1 && rb_testMode.Checked)
            {
                pw_form.str_password = cfg_info.password;
                //pw_form.Visible = false;
                pw_form.ShowDialog();

            }
        }
        //change test MODE
        private void rb_testMode_CheckedChanged(object sender, EventArgs e)
        {
            string modeStr = "MODE:DEBUG";
            if (rb_testMode.Checked)
            {
                scanSN_CFG["MODE"] = "TEST";
                modeStr = "MODE:TEST";
                lb_debug.Visible = false;
                BackColor = Color.WhiteSmoke;

                gb_loop.Visible = false;
                tb_setLoopCount.Text = "1";

            }
            else
            {
                scanSN_CFG["MODE"] = "DEBUG";
                lb_debug.Visible = true;
                BackColor = Color.Orange;

                gb_loop.Visible = true;
                tb_setLoopCount.Text = "1";
            }

            for (int i = 0; i < _UNIT_COUNT; i++)
            {
                arrUserControl[i].ReceiveMsg(modeStr);
            }
            myPrintf(modeStr);
        }
        //open log view
        private void lb_Status_Click(object sender, EventArgs e)
        {
            //myPrintf("present MT LogView...");
            mt_lv.ShowDialog();
        }
        //refresh pannel
        private void pb_refresh_Click(object sender, EventArgs e)
        {
            cfg_info.inputCount = 0;
            cfg_info.passCount = 0;
            updatePannel();
            MessageBox.Show("Refresh panel successful!");
        }
        #endregion

        #region Save CSV
        private void saveCSV()
        {
            //save location csv data
            SaveAllOneCSV(cfg_info.csvPath, cfg_info.swName, cfg_test._csv_data);
        }
        private void SaveAllOneCSV(string Path, string SWName, string data)
        {
            //1.创建文件夹
            string dateTime = DateTime.Now.ToString("yyyy-MM");
            Path = Path + "\\" + SWName + "\\" + dateTime;
            if (Directory.Exists(Path))
            {
                myPrintf(Path + " 此文件夹已经存在，无需创建！");
            }
            else
            {
                Directory.CreateDirectory(Path);
                myPrintf(Path + " 创建成功!");
            }
            //2.创建CSV文件
            string modeStr = "TEST";
            if (rb_debugMode.Checked) modeStr = "DEBUG";
            dateTime = DateTime.Now.ToString("yyyy-MM-dd");
            string fileName = Path + "\\" + dateTime +modeStr+ ".csv";

            if (!File.Exists(fileName))
            {
                string firstLine = "SerialNumber,Result,ErrorCode,TesterID,Test Start Time,Test End Time,";
                string secondLine = "Upper Limits---->,,,,,,";
                string thirdLine = "Lower Limits---->,,,,,,";
                string fourthLine = "MeasurementUnit---->,,,,,,";
                for (int i = 0; i < ltp.itemNameList.Count; i++)
                {
                    firstLine += ltp.itemNameList[i] + ",";
                    secondLine += ltp.upperLimitList[i] + ",";
                    thirdLine += ltp.lowerLimitList[i] + ",";
                    fourthLine += ltp.measUnitList[i] + ",";
                }
                try
                {
                    FileStream fsTemp = new FileStream(fileName, FileMode.Append);
                    StreamWriter swTemp = new StreamWriter(fsTemp);
                    //swTemp.Write()
                    swTemp.WriteLine(firstLine);
                    swTemp.WriteLine(secondLine);
                    swTemp.WriteLine(thirdLine);
                    swTemp.WriteLine(fourthLine);
                    swTemp.Close();
                    fsTemp.Close();
                }
                catch (Exception ex)
                {
                    myPrintf("Save MES CSV ERROR:" + ex.ToString());
                    MessageBox.Show("Save MES CSV ERROR:" + ex.ToString());
                }
            }
            //3.写入测试数据           
            FileStream fs = new FileStream(fileName, FileMode.Append);
            StreamWriter sw = new StreamWriter(fs);
            sw.Write(data);
            sw.Close();
            fs.Close();
        }

        #endregion

        #region Loop Test
        private void btn_loopAbort_Click(object sender, EventArgs e)
        {
            cfg_test._loop_abort = true;
            btn_loopAbort.Visible = false;
        }
        private void program_loop()
        {
            Thread.Sleep(500);
            Invoke((EventHandler)(delegate
            {

                for (int i = 0; i < _UNIT_COUNT; i++)
                {

                    if (arrUserControl[i]._slot_selected)
                    {
                        arrUserControl[i].ReceiveMsg("SCANSN");
                    }
                }


            }));

            Thread.Sleep(100);
            Invoke((EventHandler)(delegate
            {

                for (int i = 0; i < _UNIT_COUNT; i++)
                {
                    string msg = "SN:" + arrSNs[i];
                    if (arrUserControl[i]._slot_selected)
                    {
                        arrUserControl[i].ReceiveMsg(msg);
                    }
                }


            }));

            Thread.Sleep(100);
            Invoke((EventHandler)(delegate
            {

                btn_Start.PerformClick();

            }));

        }
        #endregion

        #region Execute Sync Task (*Sub Thread)

        private void ExecuteSyncTask(object cmd)
        {
            string response = "test response";
            MessageBox.Show("[All-Slots]\nIs this a sync dialog?");
            cfg_test._sync_request_count = 0;
            for (int i = 0; i < _UNIT_COUNT; i++)
            {
                if (arrSolt_selected[i])
                {
                    arrUserControl[i].ReceiveMsg("SyncTaskFinish#"+response);
                    Thread.Sleep(10);
                }

            }
        }

        #endregion

        #region MT Config View
        private void MtCfgBtn_Click(object sender, EventArgs e)
        {
            mtCfgView.ShowDialog();
        }
        private void mtCfgView_Msg(string msg)
        {
            myPrintf("msg from config view:" + msg);
        }
        #endregion
    }
}
