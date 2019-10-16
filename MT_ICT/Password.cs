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
    public delegate void SendPassWord2MTEventHandler(bool result);

    public partial class Password : Form
    {
        public Password()
        {
            InitializeComponent();
        }
        public string str_password;
        public event SendPassWord2MTEventHandler sendResult2MT;
        private bool _have_send;
        private void Password_Load(object sender, EventArgs e)
        {
            _have_send = false;
            tb_password.BackColor = Color.White;
            tb_password.Text = "";
            //绑定输入SN后的回车事件
            tb_password.KeyUp += new KeyEventHandler(tb_password_KeyUp);
        }
        //响应输入SN后的回车事件
        private void tb_password_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if(tb_password.Text == str_password)
                {
                    sendResult2MT(true);
                    _have_send = true;
                    Close();
                }
                else
                {
                    tb_password.BackColor = Color.Red;
                    tb_password.Text = "";
                }
            }
        }

        private void Password_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }

        private void Password_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (!_have_send)
            {
                sendResult2MT(false);
            }
            
        }
    }
}
