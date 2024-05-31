using Clones.Services;
using System.Collections.Generic;
using UnityEngine;

namespace Clones.StateMachine
{
    public abstract class State : MonoBehaviour
    {
        [SerializeField] private List<Transition> _transitions;

        protected IInputService InputServiece { get; private set; }

        public void Enter(IInputService inputService)
        {
            if (enabled == false)
            {
                InputServiece = inputService;
                enabled = true;

                foreach (var transition in _transitions)
                {
                    transition.Init(inputService);
                    transition.enabled = true;
                }
            }
        }

        public void Exit()
        {
            foreach (var transition in _transitions)
                transition.enabled = false;

            enabled = false;
        }

        public State GetNextState()
        {
            foreach(var transition in _transitions)
            {
                if (transition.NeedTransit)
                    return transition.TargetState;
            }

            return null;
        }
    }
}
