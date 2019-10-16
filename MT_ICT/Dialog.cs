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
    public partial class Dialog : Form
    {
        public Dialog()
        {
            InitializeComponent();
        }
        public string message;
        public Color backcolor;
        private void Dialog_Load(object sender, EventArgs e)
        {
            if (message == null)
            {
                message = "No Message To Show!";
            }
            if(backcolor != null)
            {
                BackColor = backcolor;
            }
            okBtn.FlatStyle = FlatStyle.Flat;//样式
            okBtn.ForeColor = Color.Transparent;//前景
            okBtn.BackColor = Color.Transparent;//去背景
            okBtn.FlatAppearance.BorderSize = 0;//去边线
            okBtn.FlatAppearance.MouseOverBackColor = Color.Transparent;//鼠标经过
            okBtn.FlatAppearance.MouseDownBackColor = Color.Transparent;//鼠标按下
            
            msgLabel.Text = message;
            Activate();
        }

        private void okBtn_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Dialog_Shown(object sender, EventArgs e)
        {
            okBtn.Focus();
        }

        private void okBtn_MouseHover(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            btn.FlatAppearance.BorderSize = 1;
        }

        private void okBtn_MouseLeave(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            btn.FlatAppearance.BorderSize = 0;
        }
    }
}
