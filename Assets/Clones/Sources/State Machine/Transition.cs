using UnityEngine;

namespace Clones.StateMachine
{
    public abstract class Transition : MonoBehaviour
    {
        [SerializeField] private State _targetState;

        public State TargetState => _targetState;

        public bool NeedTransit { get; protected set; }

        protected DirectionHandler DirectionHandler { get; private set; }

        public void Init(DirectionHandler directionHandler)
        {
            DirectionHandler = directionHandler;
        }

        protected virtual void OnEnable()
        {
            NeedTransit = false;        
        }
    }
}
