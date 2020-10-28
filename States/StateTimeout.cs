using System;
using System.Media;
using System.Threading;
using System.Windows.Forms;

namespace StatePattern.States
{
    public class StateTimeout : BaseState
    {
        override
       public event EventHandler<int> NextState;

        private AutoResetEvent keyListener = new AutoResetEvent(false);

        private int pressedKey;


        public StateTimeout(MainView screen, State parameters)
            : base(screen, parameters)
        {
        }



        override
        public void Run()
        {
            var data = _param.Parameters;

            this._mainForm.Draw(data["Screen"]);

            var maxTime = int.Parse(data["Timeout"]);
            bool wasKeyPressed;
            do
            {
                //SystemSounds.Beep.Play();
                Console.Beep(2500, 250);
                wasKeyPressed = keyListener.WaitOne(250);
                maxTime -= 250;

            } while (maxTime > 0 && !wasKeyPressed);


            int nextState = 0;
            if (wasKeyPressed && pressedKey==(int)'Y')
            {
                nextState = int.Parse(data["NextStateGood"]);
                MessageBox.Show("Key pressed - Next State good");
            }
            else
            {
                nextState = int.Parse(data["NextStateBad"]);
                MessageBox.Show("Timeout Waiting Key - Next State Bad");
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
