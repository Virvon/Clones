using UnityEngine;

namespace Clones.StateMachine
{
    public class CharacterStateMashine : MonoBehaviour
    {
        [SerializeField] private State _firstState;
        [SerializeField] private DirectionHandler _directionHandler;

        public State CurrentState { get; private set; }

        private void Start() => Reset();

        private void Update()
        {
            if (CurrentState == null)
                return;

            State nextState = CurrentState.GetNextState();

            if (nextState != null)
                Transit(nextState);
        }

        private void Transit(State state)
        {
            if (CurrentState != null)
                CurrentState.Exit();

            CurrentState = state;

            if (CurrentState != null)
                CurrentState.Enter(_directionHandler);
        }

        private void Reset()
        {
            CurrentState = _firstState;

            if (CurrentState != null)
                CurrentState.Enter(_directionHandler);
        }
    }
}
