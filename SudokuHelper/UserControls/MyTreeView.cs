using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SudokuHelper
{
    class MyTreeView : TreeView
    {
        //handle double click problem on checkbox in tree view
        //solution
        //https://stackoverflow.com/questions/14647216/treeview-ignore-double-click-only-at-checkbox
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x0203 && this.CheckBoxes)
            {
                var localPos = this.PointToClient(Cursor.Position);
                var hitTestInfo = this.HitTest(localPos);
                if (hitTestInfo.Location == TreeViewHitTestLocations.StateImage)
                {
                    m.Msg = 0x0201;
                }
            }
            base.WndProc(ref m);
        }
    }
}
