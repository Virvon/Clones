using Clones.Services;
using UnityEngine;

namespace Clones.StateMachine
{
    public class PlayerStateMashine : MonoBehaviour
    {
        [SerializeField] private State _firstState;

        private IInputService _inputService;

        public State CurrentState { get; private set; }

        private void Awake()
        {
            _inputService = AllServices.Instance.Single<IInputService>();
            Reset();
        }


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
                CurrentState.Enter(_inputService);
        }

        private void Reset()
        {
            CurrentState = _firstState;

            if (CurrentState != null)
                CurrentState.Enter(_inputService);
        }
    }
}
