using Clones.Infrastructure;
using UnityEngine;
using UnityEngine.UI;

namespace Clones.UI
{
    public class PlayButton : MonoBehaviour
    {
        [SerializeField] private string _targetScene;
        [SerializeField] private Button _button;

        private IGameStateMachine _gameStateMachine;

        private void OnDisable() => 
            _button.onClick.RemoveListener(Play);

        public void Init(IGameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
            _button.onClick.AddListener(Play);
        }

        private void Play() =>
            _gameStateMachine.Enter<LoadSceneState, (string, bool)>((_targetScene, true), _gameStateMachine.Enter<GameLoopState>);
    }
}
