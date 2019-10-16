using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MT_ICT
{
    static class Program
    {
        public static MsgFromWelcomPageEvent msgFromWelcome;
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new MT_Form());
            //    显示Splash窗体
            Splash.Show();
            Console.WriteLine("splash show done.");
            bool continueFlag = true;
            //循环判断是否登陆OK
            while (!Splash.loginIsOk)
            {
                System.Threading.Thread.Sleep(100);
                //判断点击关闭事件
                if (Splash.clickClose)
                {
                    Splash.Close();
                    continueFlag = false;
                    break;
                }
            }

            if (continueFlag)
            {
                DoStartup();
                Console.WriteLine("do startup done.");
            }
            
        }
        static void DoStartup()
        {
            //做需要的事情
            //开启主窗体
            MT_Form mainform = new MT_Form();
            Application.Run(mainform);
        }
    }
}
