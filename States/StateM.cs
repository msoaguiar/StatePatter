using System;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace StatePattern.States
{
    public class StateM : BaseState
    {
        override
        public event EventHandler<int> NextState;

        private AutoResetEvent keyListener = new AutoResetEvent(false);

        private int pressedKey;

        public StateM(MainView screen, State parameters)
            : base(screen, parameters)
        {
        }



        override
        public void Run()
        {
            var data = _param.Parameters;

            this._mainForm.Draw(data["Screen"]);

            var wasKeyPressed = keyListener.WaitOne(int.Parse(data["Timeout"]));

            if ( !wasKeyPressed )
            {                
                NextState?.Invoke(this, int.Parse(data["TimeoutState"]) ); 

                return;
            }



            var enabledKeys = _param.Parameters["EnabledKeys"].Split(',');
            var goodKey = enabledKeys.Any(k => int.Parse(k) == pressedKey);

            int nextState = 0;
            if (goodKey)
            {
                nextState = int.Parse(data["NextStateGood"]);
                MessageBox.Show("Go to Next State Good");
            }
            else
            {
                nextState = int.Parse(data["NextStateBad"]);
                MessageBox.Show("Go to Next State Error");
            }

            this.NextState?.Invoke(this, nextState);
        }



        override
        public void OnKeyPress(object sender, KeyPressEventArgs e)
        {
            pressedKey = e.KeyChar;
            keyListener.Set();
        }
    }
}
