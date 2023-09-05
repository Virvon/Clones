using System.Collections.Generic;
using UnityEngine;

namespace Clones.StateMachine
{
    public abstract class State : MonoBehaviour
    {
        [SerializeField] private List<Transition> _transitions;

        protected DirectionHandler DirectionHandler { get; private set; }

        public void Enter(DirectionHandler directionHandler)
        {
            if(enabled == false)
            {
                DirectionHandler = directionHandler;
                enabled = true;

                foreach(var transition in _transitions)
                {
                    transition.Init(directionHandler);
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
