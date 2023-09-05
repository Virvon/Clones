using System;
using System.Collections.Generic;

namespace Clones.Infrastructure
{
    public class GameStateMachine
    {
        private Dictionary<Type, IState> _states;
        private IState _currentState;

        public GameStateMachine()
        {
            _states = new Dictionary<Type, IState>()
            {
                [typeof(BootstrapState)] = new BootstrapState(this)
            };
        }

        public void Enter<TState>() where TState : IState
        {
            _currentState?.Exit();

            IState state = _states[typeof(TState)];

            _currentState = state;
            _currentState.Enter();
        }
    }
}