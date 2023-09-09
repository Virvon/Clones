using Clones.Infrastructure;
using UnityEngine;

namespace Clones.StateMachine
{
    public abstract class Transition : MonoBehaviour
    {
        [SerializeField] private State _targetState;

        public State TargetState => _targetState;

        public bool NeedTransit { get; protected set; }

        protected IInputService DirectionHandler { get; private set; }

        public void Init(IInputService inputService)
        {
            DirectionHandler = inputService;
        }

        protected virtual void OnEnable()
        {
            NeedTransit = false;        
        }
    }
}
