using Microsoft.Extensions.Options;
using StatePattern.States;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StatePattern
{
    public class StateManager
    {
        private static StateManager _this = null;

        public static StateManager Instance { get { return _this ??= new StateManager(); } }


        public event EventHandler<int> NextState;


        private MainView _parent { get; set; }

        public List<State> _stateData { get; set; }


        private IState CurrentState;


        private StateManager()
        {
        }




        public void Initilize(MainView parent, IOptions<List<State>> stateData)
        {
            _parent = parent;
            _stateData = stateData.Value;


            // register events to pass those states that require them
            //_parent.KeyPress += this.OnKeyPress;
            //_parent.MouseClick += this.OnMouserClick;
        }




        public void RunState(int idState)
        {
            CurrentState = CreateState(idState);
            CurrentState.NextState += OnNextState;

            Task.Factory.StartNew(() => CurrentState.Run());
        }




        /// <summary>
        /// It create an instante of state identified by 'idState', the state class 
        /// implementation and its parameters should be provided on 'StateData'
        /// </summary>
        /// <param name="idSate"></param>
        /// <returns>an instace of implementation IState</returns>
        private IState CreateState(int idSate)
        {
            var stateParam = _stateData?.FirstOrDefault(p => p.Id == idSate);

            if (stateParam == default(IState))
                throw new StateException($"State ID = {idSate} not found");


            if (string.IsNullOrEmpty(stateParam.Type))
                throw new StateException($"State ID = {idSate} implementation class not found");

            var classType = Type.GetType(stateParam.Type);
            if (classType == null)
                throw new StateException($"State State ID = {idSate} with type '{stateParam.Type}' not loaded");

            var state = Activator.CreateInstance(classType, _parent, stateParam) as IState;

            if (state == null)
                throw new StateException($"State State ID = {idSate} with type '{stateParam.Type}' creation fails");

            return state;
        }



        /// <summary>
        /// Fire to 'parent' object an event to execute next state identified by 'nextStateId'
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="nextStateId"></param>
        public void OnNextState(object sender, int nextStateId)
        {
            if (!Object.ReferenceEquals(sender, CurrentState))
                return;

            NextState?.Invoke(this, nextStateId);
        }



        public void OnKeyPress(object sender, KeyPressEventArgs e)
        {
            CurrentState?.OnKeyPress(sender, e);
        }
    }
}
