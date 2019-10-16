using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using System.IO;

namespace MT_ICT
{
    class LoadTestPlan
    {
        //list
        public List<string> itemNameList { get; set; }
        public List<string> groupList { get; set; }
        public List<string> functionList { get; set; }
        public List<string> commandList { get; set; }
        public List<string> recSuffixList { get; set; }
        public List<string> valueStyleList { get; set; }
        public List<string> valueSaveList { get; set; }
        public List<string> typValueList { get; set; }
        public List<string> upperLimitList { get; set; }
        public List<string> lowerLimitList { get; set; }
        public List<string> measUnitList { get; set; }
        public List<string> timeOutList { get; set; }
        public List<string> delayList { get; set; }
        public List<string> exitEnableList { get; set; }
        public List<string> skipList { get; set; }

        public string csvFile = "";
        public bool LoadCSVTestPlan()
        {
            if (csvFile == "") return false;
            if (!File.Exists(csvFile)) return false;
            itemNameList = new List<string>();
            groupList = new List<string>();
            functionList = new List<string>();
            commandList = new List<string>();
            recSuffixList = new List<string>();
            valueStyleList = new List<string>();
            valueSaveList = new List<string>();
            typValueList = new List<string>();
            upperLimitList = new List<string>();
            lowerLimitList = new List<string>();
            measUnitList = new List<string>();
            timeOutList = new List<string>();
            delayList = new List<string>();
            exitEnableList = new List<string>();
            skipList = new List<string>();
            FileStream fs = null;
            StreamReader sr=null;
            bool bResult = true;
            try
            {
                fs = new FileStream(csvFile, FileMode.Open, FileAccess.Read);
                sr = new StreamReader(fs, Encoding.Default);
                string lineData = sr.ReadLine(); //line 0
                lineData = sr.ReadLine(); //line 1
                while (lineData != null)
                {
                    Console.WriteLine(lineData);
                    string[] data = lineData.Split(',');
                    itemNameList.Add(data[0]);
                    groupList.Add(data[1]);
                    functionList.Add(data[2]);
                    commandList.Add(data[3]);
                    recSuffixList.Add(data[4]);
                    valueStyleList.Add(data[5]);
                    valueSaveList.Add(data[6]);
                    typValueList.Add(data[7]);
                    lowerLimitList.Add(data[8]);
                    upperLimitList.Add(data[9]);
                    measUnitList.Add(data[10]);
                    timeOutList.Add(data[11]);
                    delayList.Add(data[12]);
                    exitEnableList.Add(data[13]);
                    skipList.Add(data[14]);

                    lineData = sr.ReadLine();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("load csv testplan err:" + ex.ToString());
                bResult = false;
            }
            finally
            {
                if(sr != null) sr.Close();
                if(fs != null) fs.Close();

            }
            


            
            return bResult;
        }
    }
}
