using Clones.Infrastructure;
using UnityEngine;

namespace Clones.UI
{
    public class PlayButton : MonoBehaviour
    {
        [SerializeField] private string _targetScene;

        private IGameStateMachine _gameStateMachine;

        public void Init(IGameStateMachine gameStateMachine) =>
            _gameStateMachine = gameStateMachine;

        public void Play() =>
            _gameStateMachine.Enter<LoadSceneState, string>(_targetScene, _gameStateMachine.Enter<GameLoopState>);
    }
}
