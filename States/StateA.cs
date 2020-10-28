using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace StatePattern.States
{

    // Estado inicial de fluxo de estados  o fim dos fluxos de estado termina com o estado Z
    // e
    public class StateA : BaseState
    {

        private AutoResetEvent goodTermination = new AutoResetEvent(false);
        private AutoResetEvent badTermination = new AutoResetEvent(false);


        override
        public event EventHandler<int> NextState;



        public StateA(MainView screen, State parameters)
            : base(screen, parameters)
        { 
        }
        


        override
        public void Run()
        {
            this._mainForm.Draw(_param.Parameters["Screen"]);

            var eventIdx = WaitHandle.WaitAny(new [] { goodTermination, badTermination });

            int nextState = 0;
            if (eventIdx == 0) 
            {
                nextState = int.Parse(_param.Parameters["NextStateGood"]);
                MessageBox.Show("Good Key or Touch Area activated");
            }
            else
            { 
                nextState = int.Parse((_param.Parameters["NextStateBad"]));
                MessageBox.Show("Bad Key or Touch Area activated");
            }
            NextState?.Invoke(this, nextState);
        }




        override
        public void OnKeyPress(object sender, KeyPressEventArgs e)
        {
            var goodKey = int.Parse(_param.Parameters["KeyGood"]);
            var badKey = int.Parse(_param.Parameters["KeyBad"]);

            if (e.KeyChar == 'X')
                Process.GetCurrentProcess().Kill();

            if (e.KeyChar == (char)goodKey)
            {
                goodTermination.Set();
            }
            else
            {
                if (e.KeyChar == (char)badKey)
                    badTermination.Set();
            }
        }


    }
}
