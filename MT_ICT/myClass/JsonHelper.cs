using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;

namespace MT_ICT
{
    class JsonHelper
    {
        public string jsonFile = "";

        public class my_dev
        {
            public Dictionary<string, string> Dev1 { get; set; }
            public Dictionary<string, string> Dev2 { get; set; }
        }
        public class myModel{
            public string MESPath { get; set; } 
            public my_dev MT_Config { get; set; }
            public my_dev Slot_1 { get; set; }
            public my_dev Slot_2 { get; set; }
            public my_dev Slot_3 { get; set; }
            public my_dev Slot_4 { get; set; }
            public my_dev Slot_5 { get; set; }
            public my_dev Slot_6 { get; set; }
            public my_dev Slot_7 { get; set; }
            public my_dev Slot_8 { get; set; }

        }
        public myModel cfgModel;
        public Dictionary<string, string> cfgDict;
        public Dictionary<string, my_dev> devDict;
        public bool loadJson()
        {
            myModel jsonObj = null;
            try
            {
                StreamReader streamReader = new StreamReader(File.OpenRead(jsonFile));
                string str = "";
                string jsonstr;
                while ((jsonstr = streamReader.ReadLine()) != null)
                {
                    str += jsonstr;
                }
                streamReader.Close();
                Console.WriteLine(str);

                jsonObj = JsonConvert.DeserializeObject<myModel>(str);


            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
            cfgDict = new Dictionary<string, string>();
            cfgDict.Add("MESPath", jsonObj.MESPath);

            devDict = new Dictionary<string, my_dev>();
            devDict.Add("MT_Config", jsonObj.MT_Config);
            devDict.Add("Slot_1", jsonObj.Slot_1);
            devDict.Add("Slot_2", jsonObj.Slot_2);
            devDict.Add("Slot_3", jsonObj.Slot_3);
            devDict.Add("Slot_4", jsonObj.Slot_4);

            cfgModel = jsonObj;
            return true;
        }
        public bool saveJson()
        {
            try
            {
                string output = JsonConvert.SerializeObject(cfgModel, Formatting.Indented);
                File.WriteAllText(jsonFile, output);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
            
            return true;
        }
    }
}
