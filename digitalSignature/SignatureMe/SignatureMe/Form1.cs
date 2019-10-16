﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Security.Cryptography;

namespace SignatureMe
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private string securityFloder;

        private static string myKey = "5DE1A8CE9A";
        private Dictionary<string,string> sourceFiles;
        private Dictionary<string, string> encryptFiles;
        private void Form1_Load(object sender, EventArgs e)
        {
            securityFloder = Application.StartupPath + @"/Security";

            
        }

        private void SignBtn_Click(object sender, EventArgs e)
        {
            //string path = @"X:\XXX\XX";
            DirectoryInfo root = new DirectoryInfo(securityFloder);

            sourceFiles = new Dictionary<string, string>();
            encryptFiles = new Dictionary<string, string>();
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
            string rFile = securityFloder + @"/R.txt";
            FileStream fs = new FileStream(rFile, FileMode.Create);
            string strData = "";
            foreach(string key in encryptFiles.Keys)
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

        private void VerifyBtn_Click(object sender, EventArgs e)
        {
            bool isOk = verifySecurity(securityFloder);
            Console.WriteLine("verify result:" + isOk.ToString());

        }
        private bool verifySecurity(string floder)
        {
            DirectoryInfo root = new DirectoryInfo(floder);

            string rFile = floder + @"/R.txt";
            if (!File.Exists(rFile))
            {
                Console.WriteLine("R.text file not exist!");
            }

            StreamReader sr = new StreamReader(rFile, Encoding.Default);

            Dictionary<string, string> getEncryptValues = new Dictionary<string, string>();

            String line;
            int count = 0;
            string key = "";
            string value = "";

            while ((line = sr.ReadLine()) != null)
            {
                Console.WriteLine(line.ToString());

                if (count % 2 == 0)
                {
                    key = line;
                }
                else
                {
                    value = line;
                    getEncryptValues.Add(key, value);
                }

                count += 1;
            }
            sr.Close();

            bool isOK = true;

            foreach (FileInfo f in root.GetFiles())
            {
                
                try
                {
                    string name = f.Name;
                    Console.WriteLine(name);
                    if (name.Equals("R.txt"))
                    {
                        continue;
                    }
                    string fullName = f.FullName;
                    Console.WriteLine(fullName);
                    string NowMD5Value = GetMD5(fullName);

                    string encryptMD5Value = getEncryptValues[name];
                    string originMD5Value = RsaDesEncrypt(encryptMD5Value);

                    if (NowMD5Value.Equals(originMD5Value))
                    {
                        Console.WriteLine("verify md5 value OK");
                    }
                    else
                    {
                        Console.WriteLine("verify md5 value NG");
                        isOK = false;
                        break;
                    }
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    isOK= false;
                    break;
                }
            }
            return isOK;
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
