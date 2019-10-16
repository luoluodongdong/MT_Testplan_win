using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MT_ICT
{
    //Form1.Designer.cs-->>modify-->>
    //this.listView1 = new ListViewTestSingle.ListViewNF();
    //private ListViewNF listView1;
    class ListViewNF : System.Windows.Forms.ListView
    {
        public ListViewNF()
        {
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.EnableNotifyMessage, true);
        }
        protected override void OnNotifyMessage(Message m)
        {
            if (m.Msg != 0x14)
            {
                base.OnNotifyMessage(m);
            }

        }
    }
}
