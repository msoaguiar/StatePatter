using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace StatePattern.States
{
    //Last state of a flow, should always return to initial state A
    public class StateZ : BaseState
    {
        override
        public event EventHandler<int> NextState;



        public StateZ(MainView screen, State parameters)
           : base(screen, parameters)
        {

        }



        override
        public void Run() {

            var data = _param.Parameters;

            this._mainForm.Draw(data["Screen"]);

            Thread.Sleep(int.Parse(data["Timeout"]));

            NextState?.Invoke(this, 0);
        }
    }
}
