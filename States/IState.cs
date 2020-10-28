using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace StatePattern.States
{
    public interface IState
    {
        event EventHandler<int> NextState;

        void Run();

        void OnKeyPress(object sender, KeyPressEventArgs e);
    }
}
