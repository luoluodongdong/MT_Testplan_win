using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Security.Cryptography;

namespace SignatureTool
{
    class Program
    {
        private static string myKey = "5DE1A8CE9A";
        static void Main(string[] args)
        {
            Console.WriteLine("Please choose security folder:");
            string folderPath = Console.ReadLine();
            
            if(!Directory.Exists(folderPath))
            {
                Console.WriteLine("[error]:security folder not exist");
                Console.WriteLine("Input any key to exit!");
                Console.ReadLine();
                return;
            }
            string securityFolder = folderPath.ToString();
            try
            {
                signFunc(securityFolder);
                Console.WriteLine("[successful]:sign finished!");
            }catch(Exception e)
            {
                Console.WriteLine("[error]:" + e.ToString());
                Console.WriteLine("[error]:sign failure!");
            }

            Console.WriteLine("Input any key to exit!");
            Console.ReadLine();
        }
        
        private static void signFunc(string securityFolder)
        {
            //string path = @"X:\XXX\XX";
            DirectoryInfo root = new DirectoryInfo(securityFolder);

            Dictionary<string,string> sourceFiles = new Dictionary<string, string>();
            Dictionary<string,string> encryptFiles = new Dictionary<string, string>();
            foreach (FileInfo f in root.GetFiles())
            {
                string name = f.Name;
                Console.WriteLine(name);
                if (name.Equals("R.txt"))
                {
                    continue;
                }
                string fullName = f.FullName;
                Console.WriteLine(fullName);
                string md5value = GetMD5(fullName);
                //文件名的md5值字典 文件名-它的MD5值
                sourceFiles.Add(name, md5value);
                string securityValue = RsaEncrypt(md5value);
                Console.WriteLine(securityValue);
                //加密过的md5值字典 文件名-它的加密过的md5值
                encryptFiles.Add(name, securityValue);

            }
            //FileMode.Append为不覆盖文件效果.create为覆盖
            string rFile = securityFolder + @"/R.txt";
            FileStream fs = new FileStream(rFile, FileMode.Create);
            string strData = "";
            foreach (string key in encryptFiles.Keys)
            {
                strData += key + "\r\n";
                strData += encryptFiles[key] + "\r\n";

            }
            //获得字节数组
            byte[] data = Encoding.Default.GetBytes(strData);
            //开始写入
            fs.Write(data, 0, data.Length);
            //清空缓冲区、关闭流
            fs.Flush();
            fs.Close();
        }

        static string GetMD5(string s)
        {
            try
            {
                FileStream file = new FileStream(s, FileMode.Open);
                MD5 md5 = new MD5CryptoServiceProvider();
                byte[] retval = md5.ComputeHash(file);
                file.Close();

                StringBuilder sc = new StringBuilder();
                for (int i = 0; i < retval.Length; i++)
                {
                    sc.Append(retval[i].ToString("x2"));
                }
                Console.WriteLine("文件MD5：{0}", sc);
                return sc.ToString();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return "";
        }
        /// <summary>

        /// 进行 RSA 加密

        /// </summary>

        /// <param name="sourceStr">源字符串</param>

        /// <returns>加密后字符串</returns>

        private static string RsaEncrypt(string sourceStr)

        {

            CspParameters param = new CspParameters();

            //密匙容器的名称，保持加密解密一致才能解密成功

            param.KeyContainerName = myKey;

            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(param))

            {

                //将要加密的字符串转换成字节数组

                byte[] plaindata = Encoding.Default.GetBytes(sourceStr);

                //通过字节数组进行加密

                byte[] encryptdata = rsa.Encrypt(plaindata, false);

                //将加密后的字节数组转换成字符串

                return Convert.ToBase64String(encryptdata);

            }

        }

        /// <summary>

        /// 通过RSA 加密方式进行解密

        /// </summary>

        /// <param name="codingStr">加密字符串</param>

        /// <returns>解密后字符串</returns>

        private static string RsaDesEncrypt(string codingStr)

        {

            CspParameters param = new CspParameters();

            param.KeyContainerName = myKey;

            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(param))

            {

                byte[] encryptdata = Convert.FromBase64String(codingStr);

                byte[] decryptdata = rsa.Decrypt(encryptdata, false);

                return Encoding.Default.GetString(decryptdata);

            }

        }
    }
}
