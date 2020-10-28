using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace StatePattern
{
    public static class HelperCtrl
    {
        public static void SafeInvoke(this Control control, Action handler)
        {
            if (control.InvokeRequired)
            {
                control.Invoke(handler);
            }
            else
            {
                handler();
            }
        }

        public static T SafeInvoke<T>(this Control control, Func<T> handler)
        {
            if (control.InvokeRequired)
            {
                return (T) control.Invoke(handler);
            }
            else
            {
                return (T) handler();
            }
        }
    }
}
