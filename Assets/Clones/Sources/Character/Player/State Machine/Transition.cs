using Clones.Services;
using UnityEngine;

namespace Clones.StateMachine
{
    public abstract class Transition : MonoBehaviour
    {
        [SerializeField] private State _targetState;

        public State TargetState => _targetState;

        public bool NeedTransit { get; protected set; }

        protected IInputService InputService { get; private set; }

        public void Init(IInputService inputService)
        {
            InputService = inputService;
            Init();
        }

        protected virtual void Init() { }

        private void OnEnable()
        {
            NeedTransit = false;        
        }
    }
}
