using System;
using System.Windows.Forms;

namespace StatePattern.States
{


    public abstract class BaseState : IState
    {
        public virtual event EventHandler<int> NextState;


        public int Id { get; set; }


        protected readonly MainView _mainForm;

        protected readonly State _param;

        protected BaseState(MainView screen, State parameters)
        {
            _mainForm = screen;
            _param = parameters;
        }




        public virtual void Run() {
            throw new NotImplementedException($"State Id = {Id} does not implement ::Run");
        }




        public virtual void OnKeyPress(object sender, KeyPressEventArgs e)
        { 
        }

    }

    
}
