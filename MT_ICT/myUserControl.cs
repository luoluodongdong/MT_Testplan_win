using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.IO.Ports;
using System.IO;
using System.Text.RegularExpressions;

namespace MT_ICT
{
   

    public delegate void SendMsg2MTEventHandler(int id, string msg);
    public delegate Dictionary<string, string> CallMTFunctionHandler(int id, string cmd);

    public partial class myUserControl : UserControl
    {
        public event SendMsg2MTEventHandler SendMsg2MTEvent;
        public event CallMTFunctionHandler CallMTFunction; 
        public Dictionary<string,string> cfg_dict;
        public string testplan_csvFile;
        public bool _slot_selected;
        public int _sn_length;

        public readonly int _id;
        
        public struct CFG_info
        {
            //public string comName;
            //public int baudRate;
            //public string controlBoardPort;
           // public int controlBoardBaud;
            //public string instrumentAddr;
            //public int instrBaudRate;
            public string stationID;
            public string mesPATH;
        }
        public struct TEST_info
        {
            public bool isTesting;
            public string snString;
            public bool result;
            public string startTime;
            public string endTime;
            public string errorCode;
            public string testerID;
            public string resultStr;
            public string csvData;
            //for async task
            public bool asyncKeyIsOK;
            public bool finishSyncTaskFlag;
            public string syncTaskReply;
            //for close self
            public bool abortTestingFlag;
        }
        private BackgroundWorker backgroundWorker1;
        private string _mode;

        DetialView detialView = new DetialView();
        myConfigView configView = new myConfigView();
        //myDeviceUC myDUT;
        //myDeviceUC myControlBoard;
        //myInstrumentUC myInstr2306;
        LoadTestPlan ltp = new LoadTestPlan();
      
        CFG_info cfg_info = new CFG_info();
        TEST_info test_info = new TEST_info();
        private CIni ini;
        //private object objLock;
        public myUserControl(int iIndex)
        {
            _id = iIndex + 1;
            InitializeComponent();
        }

        #region UC Load
        private void myUserControl_Load(object sender, EventArgs e)
        {
            configBtn.FlatStyle = FlatStyle.Flat;//样式
            configBtn.ForeColor = Color.Transparent;//前景
            configBtn.BackColor = Color.Transparent;//去背景
            configBtn.FlatAppearance.BorderSize = 0;//去边线
            configBtn.FlatAppearance.MouseOverBackColor = Color.Transparent;//鼠标经过
            configBtn.FlatAppearance.MouseDownBackColor = Color.Transparent;//鼠标按下

            // objLock = new object();
            test_info.testerID = _id.ToString();
            progressBar1.Visible = false;

            cfg_info.mesPATH = cfg_dict["MESPath"];
            cfg_info.stationID = cfg_dict["StationID"];

            BackColor = Color.FromArgb(64, 224, 208);
            cb_Slot_Selected.Text = "Slot-" + _id.ToString();
            _slot_selected = true;

            //config view setup
            configView.cfgName = "Slot_" + _id.ToString();
            configView.Text = "Slot_" + _id.ToString() + " Config View";
            configView.slot_id = _id;
            configView.msgFromConfigView += new myConfigView.MsgFromConfigView(configView_Msg);
            configView.loadDevices();
            
            backgroundWorker1 = new BackgroundWorker();                      //新建BackgroundWorker
            backgroundWorker1.WorkerReportsProgress = true;                  //允许报告进度
            backgroundWorker1.WorkerSupportsCancellation = false;             //允许取消线程
            backgroundWorker1.DoWork += backgroundWorker1_DoWork;                       //设置主要工作逻辑
            backgroundWorker1.ProgressChanged += backgroundWorker1_ProgressChanged;     //进度变化的相关处理
            backgroundWorker1.RunWorkerCompleted += backgroundWorker1_RunWorkerCompleted;  //线程完成时的处理

            //serialPort1.DataReceived += new SerialDataReceivedEventHandler(comm_DataReceived);

            //string xml_file= Application.StartupPath + @"\autoTestList.xml";
            ltp.csvFile = testplan_csvFile;
            if (!ltp.LoadCSVTestPlan()) myPrintf("Load testplan file error!");
            

            detialView._id = _id;
            detialView.testplanFile = testplan_csvFile;
            detialView.initDetialView();

            
            if (!configView.all_devices_is_ready)
            {
                lb_Status.Text = "ERROR";
                BackColor = Color.FromArgb(220,20,60);
            }
            

            if (Directory.Exists(cfg_info.mesPATH))
            {
                myPrintf(cfg_info.mesPATH + " 此文件夹已经存在，无需创建！");
            }
            else
            {
                Directory.CreateDirectory(cfg_info.mesPATH);
                myPrintf(cfg_info.mesPATH + " 创建成功!");
            }

            myPrintf("uc load finish.");
        }
        #endregion

        #region Slot Selected
        private void cb_Slot_Selected_CheckedChanged(object sender, EventArgs e)
        {
            //SendMsg2MTEvent(_id, "Selected");
            if(cb_Slot_Selected.Checked)
            {
                _slot_selected = true;
                SendMsg2MTEvent(_id, "Selected:1");
                //tb_scanSN.Text = "";
                lb_showSN.Text = "NA";
                lb_Status.Visible = true;
                lb_Status.Text = "Ready";
                //tb_scanSN.Visible=true;
                BackColor = Color.FromArgb(64,224,208);
            }
            else
            {
                _slot_selected = false;
                SendMsg2MTEvent(_id, "Selected:0");
                //tb_scanSN.Text = "";
                lb_showSN.Text = "";
                lb_Status.Text = "";
                lb_Status.Visible = false;
                //tb_scanSN.Visible=false;
                progressBar1.Visible = false;
                BackColor = Color.FromArgb(169,169,169);
            }

        }
        #endregion

        #region Device UserControl event
        /*
        private void devUC_saveCfgEvent(int id)
        {
            if(id == 100)
            {
                Console.WriteLine("DUT port:" + myDUT.serialPortName);
                string SERIAL_key = string.Format("SERIAL{0}", _id);
                ini.setKeyValue(SERIAL_key, "COM", myDUT.serialPortName);
                lb_Status.Text = "Ready";
                BackColor = Color.FromArgb(64, 224, 208);
            }
            

        }
        */
        #endregion
        
        #region Message From MT
        public void ReceiveMsg(string msg)
        {
            myPrintf(msg);
            //SN:SN001
            if (msg.StartsWith("SN:"))
            {
                string snStr = msg.Split(':')[1];
                test_info.snString = snStr.ToUpper();
                Console.WriteLine("snStr:" + snStr);
                lb_showSN.Text = snStr;
                lb_Status.Text = "Ready";
                BackColor = Color.FromArgb(64, 224, 208);
                //tb_scanSN.Text = "";
                //tb_scanSN.Focus();
            }
            //Scan SN
            else if (msg.Equals("SCANSN"))
            {
                lb_showSN.Text = "";
                lb_Status.Text = "IDLE";
                BackColor = Color.FromArgb(186, 85, 211);
                detialView.initDetialView();
                progressBar1.Visible = false;
            }
            //receive mode string "MODE:TEST"/"MODE:DEBUG"
            else if (msg.StartsWith("MODE:"))
            {
                _mode = msg.Split(':')[1];
                if (_mode.Equals("TEST"))
                {
                    configBtn.Visible = false;
                }
                else
                {
                    configBtn.Visible = true;
                }
            }
            //START single trigger start testing
            else if (msg.Equals("START"))
            {
                configBtn.Enabled = false;
                cb_Slot_Selected.Enabled = false;
                lb_Status.Text = "TEST...";
                BackColor = Color.Yellow;
                test_info.result = true;
                test_info.asyncKeyIsOK = false;
                test_info.finishSyncTaskFlag = false;
                test_info.abortTestingFlag = false;
                test_info.isTesting = true;
                progressBar1.Value = 0;
                progressBar1.Visible = true;
                backgroundWorker1.RunWorkerAsync();
            }
            //Check async KEY is OK?
            else if (msg.Equals("KEYisOK"))
            {
                test_info.asyncKeyIsOK = true;
            }
            //Check finish sync task
            else if (msg.StartsWith("SyncTaskFinish"))
            {
                test_info.syncTaskReply = msg.Split('#')[1];
                test_info.finishSyncTaskFlag = true;
            }
            else if (msg.Equals("EXIT"))
            {
                Thread closeThread = new Thread(new ThreadStart(closeSelf));
                closeThread.IsBackground = true;
                closeThread.Start();
               
            }
        }
        private void closeSelf()
        {
            //#0,abort testing
            if (test_info.isTesting)
            {
                test_info.abortTestingFlag = true;
                while (test_info.isTesting)
                {
                    Thread.Sleep(200);
                    //waiting for abort testing
                }
            }
            //#1,close opend devices
            configView.closeDevices();
            /*
            if (myDUT.serialPort1.IsOpen)
            {
                myDUT.serialPort1.Close();
                Console.WriteLine("Closing serial port...");
            }
            */
            //#2,send closeIsOK msg to MT
            SendMsg2MTEvent(_id, "CloseSelfOK");
        }
        #endregion

        #region Test DetialView(just for debug)
        private void testDetialView()
        {
            // detialView.listData.Clear();
           // detialView.initDetialView();
           // Console.WriteLine("initDetialView...");
           for(int i = 0; i < ltp.itemNameList.Count; i++)
            {
                Thread.Sleep(50);
                Invoke((EventHandler)(delegate
                {
                    detialView.printTesting(i);
                }));
                //
                Thread.Sleep(100);
                Invoke((EventHandler)(delegate
                {
                    detialView.printMsg2ListView(i, "PASS", "123456", "0.123456s");
                }));
                //
                for (int k = 0; k < 5; k++)
                {
                    Thread.Sleep(10);
                    Invoke((EventHandler)(delegate
                    {

                        detialView.updateLogList("item" + i.ToString());

                    }));
                    
                }

                //Console.WriteLine("testing...");
            }
        }
        #endregion

        #region BackgroundWorker1
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            test_info.startTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:fff");
            test_info.errorCode = "";
            test_info.csvData = "";
            //testDetialView();
            if (!configView.all_devices_is_ready)
            {
                myPrintf("Devices not ready!");
                test_info.errorCode = "ERROR";
                test_info.result = false;
                return;
            }
            string FeedBackStr = "";
            int itemCount = ltp.itemNameList.Count;
            int eachNum = 100 / itemCount;
            for(int i = 0; i < itemCount; i++)
            {
                DateTime startT = DateTime.Now;
                string thisDescription = ltp.itemNameList[i];
                string thisGroup = ltp.groupList[i];
                string thisFunc = ltp.functionList[i];
                string thisSkipTag = ltp.skipList[i];
                string thisCommand = ltp.commandList[i];
                double thisTimeOut = double.Parse(ltp.timeOutList[i]);
                string thisJudgeStyle = ltp.valueStyleList[i];
                string thisTypValue = ltp.typValueList[i];
                string thisValueSave = ltp.valueSaveList[i];
                string thisLow = ltp.lowerLimitList[i];
                string thisUp = ltp.upperLimitList[i];
                string thisUnit = ltp.measUnitList[i];
                int thisDelay = (int)(1000 * double.Parse(ltp.delayList[i]));
                string thisExitEnable = ltp.exitEnableList[i];

                //receive data time out ?
                bool recTimeOut = false;
                string thisValue = "";
                string thisStatus = "PASS";
                string thisDuration = "";

                myPrintf("==========================================");
                myPrintf(string.Format("item:{0}", thisDescription));
                //refresh detial view =>"TESTING"
                Invoke((EventHandler)(delegate
                {
                    detialView.printTesting(i);
                }));

                //check skip item
                if (thisSkipTag.Equals("1"))
                {
                    thisValue = "skip";
                    thisStatus = "SKIPED";

                }

                else if (thisFunc.Equals("Serial"))
                {
                    if (!configView.serialDict["DUT"].SendCmdWithTimeOut(thisCommand, thisTimeOut, out thisValue, out recTimeOut))
                    {
                        thisValue = "ERROR";
                        thisStatus = "FAIL";
                        break;
                    }
                    if (recTimeOut)
                    {
                        thisValue = "TimeOut";
                        thisStatus = "FAIL";
                    }
                    else
                    {
                        if (thisJudgeStyle == "value") thisValue = RegexValue(thisValue);
                        if (!JudgeValueWithLimit(thisJudgeStyle, thisValue, thisTypValue, thisLow, thisUp))
                        {
                            thisStatus = "FAIL";
                        }
                    }
                    if (thisValueSave == "1") FeedBackStr = thisValue;
                }
                else if (thisFunc.Equals("OpenShort"))
                {
                    if (!configView.serialDict["DUT"].SendCmdWithTimeOut(thisCommand, thisTimeOut, out thisValue, out recTimeOut))
                    {
                        thisValue = "ERROR";
                        thisStatus = "FAIL";
                        break;
                    }
                    if (recTimeOut)
                    {
                        thisValue = "TimeOut";
                        thisStatus = "FAIL";
                    }
                    else
                    {
                        thisStatus = "PASS";
                    }
                    if (thisValueSave == "1") FeedBackStr = thisValue;
                }
                else if (thisFunc.Equals("CheckFB"))
                {
                    string[] tempArr = FeedBackStr.Split(',');
                    int index = int.Parse(thisCommand);
                    if(index < tempArr.Count())
                    {
                        thisValue = tempArr[index];
                        if (!JudgeValueWithLimit(thisJudgeStyle, thisValue, thisTypValue, thisLow, thisUp))
                        {
                            thisStatus = "FAIL";
                        }
                    }
                    else
                    {
                        thisValue = "data err";
                        thisStatus = "FAIL";
                    }
                }
                else if (thisFunc.Equals("Delay"))
                {
                    int delayT = int.Parse(thisCommand);
                    Thread.Sleep(delayT);
                    thisValue = "OK";
                    thisStatus = "PASS";
                }
                else if (thisFunc.Equals("Dialog"))
                {
                    MessageBox.Show(thisCommand);
                    thisValue = "OK";
                    thisStatus = "PASS";
                }
                else if (thisFunc.Equals("Async"))
                {
                    SendMsg2MTEvent(_id, "AsyncKEY");
                    Thread.Sleep(100);
                    bool getKeyIsSuccessful = false;
                    for(int k=0;k<100; k++)
                    {
                        if (test_info.asyncKeyIsOK)
                        {
                            getKeyIsSuccessful = true;
                            break;
                        }
                        Thread.Sleep(500);
                        SendMsg2MTEvent(_id, "AsyncKEY");
                    }
                    if (getKeyIsSuccessful)
                    {
                        //execute async task
                        Dictionary<string, string> taskResult = CallMTFunction(_id, thisCommand);
                        myPrintf(string.Format("AsyncTask-CallMTFunction-Response:{0}", taskResult["response"]));
                        //MessageBox.Show(string.Format("[Slot-{0}]\nIs this a async dialog?",_id));
                        //return KEY to MT
                        SendMsg2MTEvent(_id, "ReturnKEY");
                        test_info.asyncKeyIsOK = false;
                        thisValue = "OK";
                        thisStatus = "PASS";
                    }
                    else
                    {
                        thisValue = "NG";
                        thisStatus = "PASS";
                    }
                    
                }
                //execute sync task
                else if (thisFunc.Equals("Sync"))
                {
                    SendMsg2MTEvent(_id, "SyncRequest#test cmd");
                    bool executeSuccessful = false;
                    for(int k = 0; k < 100; k++)
                    {
                        if (test_info.finishSyncTaskFlag)
                        {
                            executeSuccessful = true;
                            break;
                        }
                        Thread.Sleep(1000);
                    }
                    if (executeSuccessful)
                    {
                        test_info.finishSyncTaskFlag = false;
                        //get result from MT function
                        string taskReply = test_info.syncTaskReply;
                        myPrintf("SyncRequestReply:" + taskReply);
                        thisValue = "OK";
                        thisStatus = "PASS";
                    }
                    else
                    {
                        thisValue = "NG";
                        thisStatus = "FAIL";
                    }
                }
                //other invalid item/testtag
                else
                {
                    thisValue = "TestTag error";
                    thisStatus = "FAIL";
                }


                thisValue = thisValue.Replace("\r", "");
                thisValue = thisValue.Replace("\n", "");
                thisValue = thisValue.Replace(",", "@");
                thisValue = thisValue.Replace("\r\n", "@");
                thisValue = thisValue.Replace("#", "@");
                myPrintf(string.Format("val:{0}", thisValue));
                //test item duration time
                TimeSpan span = DateTime.Now - startT;
                thisDuration = string.Format("{0}s",span.TotalSeconds.ToString());
                myPrintf(string.Format("value:{0} low:{1} up:{2} unit:{3}", thisValue, thisLow, thisUp, thisUnit));
                myPrintf(string.Format("status:{0} duration:{1}", thisStatus,thisDuration));
                //refresh detial view =>status value duration
                Invoke((EventHandler)(delegate
                {
                    detialView.printMsg2ListView(i, thisStatus, thisValue, thisDuration);
                }));

                if(thisStatus == "FAIL" && test_info.errorCode=="")
                {
                    test_info.result = false;
                    test_info.errorCode = string.Format("EC_{0}", (i + 1).ToString());
                }

                test_info.csvData += thisValue + ",";
                if (thisStatus == "FAIL" && thisExitEnable == "1")
                {
                    break;
                }
                int progressNum = (i + 1) * eachNum;
                Console.WriteLine("progress:" + progressNum.ToString());
                backgroundWorker1.ReportProgress(progressNum);
                Thread.Sleep(thisDelay);

                if (test_info.abortTestingFlag)
                {
                    thisStatus = "FAIL";
                    break;
                }
            }
            
        }
        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if(e.ProgressPercentage > 100)
            {
                progressBar1.Value = 100;
            }
            else
            {
                progressBar1.Value = e.ProgressPercentage;
            }
            

        }
        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            progressBar1.Value = 100;
            test_info.endTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:fff");

            string str_result = "FAIL";
            if (test_info.result)
            {
                str_result = "PASS";
                lb_Status.Text = "PASS";
                BackColor = Color.FromArgb(124, 252, 0);
            }
            else
            {
                lb_Status.Text = test_info.errorCode;//"FAIL";
                BackColor = Color.FromArgb(220, 20, 60);
            }

            test_info.resultStr = str_result;
            test_info.csvData = test_info.snString + "," +
                                test_info.resultStr + "," +
                                test_info.errorCode + "," +
                                test_info.testerID + "," +
                                test_info.startTime + "," +
                                test_info.endTime + "," +
                                test_info.csvData + "\r\n";

            SendMsg2MTEvent(_id, string.Format("CSVDATA#{0}", test_info.csvData));
            myPrintf(test_info.csvData);
            Thread.Sleep(1);
            SendMsg2MTEvent(_id, string.Format("Finish:{0}", str_result));
            if (_mode == "TEST") Save2MESCSV();
            myPrintf("Finish work...");


            cb_Slot_Selected.Enabled = true;
            configBtn.Enabled = true;
            test_info.isTesting = false;
        }
        #endregion

        #region Judge Response
        private string RegexValue(string value)
        {
            //string str = "优惠6.0万"; 
            /**  \\d+\\.?\\d*
            * \d 表示数字
            * + 表示前面的数字有一个或多个（至少出现一次）
            * \. 此处需要注意，. 表示任何原子，此处进行转义，表示单纯的 小数点
            * ? 表示0个或1个
            * * 表示0次或者多次
            */
            Regex r = new Regex("\\d+\\.?\\d*");
            bool ismatch = r.IsMatch(value);
            if (!ismatch) return "NA";
            MatchCollection mc = r.Matches(value);
            string result = string.Empty;
            for (int i = 0; i < mc.Count; i++)
            {
                result += mc[i];
                //匹配结果是完整的数字，此处可以不做拼接的 
            }
            myPrintf("regex result:" + result);
            return result;
        }
        private bool JudgeValueWithLimit(string judgeStyle, string value, string typValue, string low, string up)
        {
            //strCheck:OK
            if (judgeStyle.Equals("string"))
            {
                //string matchStr = judgeStyle.Split(':')[1];
                return value.Contains(typValue);
            }
            //judgeStyle:"valueCheck:C" value:34.67C
            else if (judgeStyle.Equals("value"))
            {
                
                return judgeValue(value, up, low);
            }
            else
            {
                MessageBox.Show("Error judgeStyle:" + judgeStyle);
                return false;
            }

        }
        //匹配数值范围
        private bool judgeValue(string strValue, string upperLimit, string lowerLimit)
        {
            if (strValue == "") return false;

            //double receiveValue = System.Convert.ToDouble(strValue);
            double receiveValue = 0.00;
            bool convert_flag = double.TryParse(strValue, out receiveValue);
            if (!convert_flag)
            {
                myPrintf(string.Format("{0} convert error!", strValue));
                return false;
            }

            if (upperLimit == "" && lowerLimit != "")
            {
                double lower = System.Convert.ToDouble(lowerLimit);

                if (receiveValue >= lower) return true;
                else return false;
            }
            else if (upperLimit != "" && lowerLimit == "")
            {
                double upper = System.Convert.ToDouble(upperLimit);
                if (receiveValue <= upper) return true;
                else return false;
            }
            else if (upperLimit != "" && lowerLimit != "")
            {
                double upper = System.Convert.ToDouble(upperLimit);
                double lower = System.Convert.ToDouble(lowerLimit);

                if (receiveValue >= lower && receiveValue <= upper) return true;
                else return false;
            }
            else
            {
                return true;
            }


        }
        #endregion

        #region Others
        private void myPrintf(string msg)
        {
            string log = string.Format("[{0}]:{1}", DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:fff"), msg);
            
            Invoke((EventHandler)(delegate
            {

                detialView.updateLogList(log);

            }));

            Console.WriteLine(string.Format("[Slot-{0}]:{1}", _id, msg));
        }
        private void showMessageBox(string msg)
        {
            string str = string.Format("[Slot-{0}]:{1}", _id, msg);
            MessageBox.Show(str);
            myPrintf(msg);
        }
        private void lb_Status_Click(object sender, EventArgs e)
        {
            //myPrintf("present detial view...");
            detialView.ShowDialog();
        }
        #endregion

        #region Save CSV

        //private string getResultMsg()
        //{
        //    string bResult = "";
        //    int count = dataList.Count;
        //    for (int i = 0; i < count; i++)
        //    {
        //        bResult += xo.descriptionsList[i] + "=" + dataList[i] + ";";
        //    }
        //    Console.WriteLine("Message:" + bResult);
        //    return bResult;
        //}
        private void Save2MESCSV()
        {
            //1.获取message
            string msgString = "";//getResultMsg();
            //2.创建临时文件夹
            string strPreDirectory = "D:\\csvTemp";
            if (Directory.Exists(strPreDirectory))
            {
                myPrintf(strPreDirectory + " 此文件夹已经存在，无需创建！");
            }
            else
            {
                Directory.CreateDirectory(strPreDirectory);
                myPrintf(strPreDirectory + " 创建成功!");
            }

            string targetPath = cfg_info.mesPATH;//"D:\\MESlog\\测试文件路径";//"D:\\UpdateMEScsv";

            //3.生成CSV文件
            string csvName = strPreDirectory + "\\" +test_info.snString+ DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss_fff") + ".csv";


            try
            {
                string stationID = cfg_info.stationID;
                using (StreamWriter sw = File.CreateText(csvName))
                {
                    //ITKS_A02-MICFPCTEST01 666666 PASS 00 NA
                    sw.WriteLine("StationID,sn,Result,ErrorCode,Message");
                    sw.WriteLine(stationID + "," + 
                        test_info.snString + "," + 
                        test_info.resultStr + "," + 
                        test_info.errorCode + "," 
                        + msgString);
                    sw.Close();
                }
                //4.移动CSV文件至MES LOG文件夹
                FileMove(csvName, targetPath);
            }
            catch (Exception ex)
            {
                myPrintf("Save MES CSV ERROR:" + ex.ToString());
                MessageBox.Show("Save MES CSV ERROR:" + ex.ToString());
            }
            //Delay(200);
        }
        private void FileMove(string sourceFile, string targetPath)
        {
            if (!Directory.Exists(targetPath))
            {
                Directory.CreateDirectory(targetPath);
            }
            FileInfo file = new FileInfo(sourceFile);
            file.MoveTo(targetPath + @"\" + file.Name);
        }
        #endregion

        #region Config View
        private void configView_Msg(string msg)
        {
            myPrintf("msg from configView:" + msg);
            if ("OK" == msg)
            {
                lb_Status.Text = "Ready";
                BackColor = Color.FromArgb(64, 224, 208);
            }
            else
            {
                lb_Status.Text = "ERROR";
                BackColor = Color.FromArgb(220, 20, 60);
            }
        }
        private void ConfigBtn_Click(object sender, EventArgs e)
        {
            configView.cfgName = "Slot_" + _id.ToString();
            configView.Text = "Slot_" + _id.ToString() + " Config View";
            configView.slot_id = _id;
            configView.ShowDialog();
        }
        #endregion
    }
}
