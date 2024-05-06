using Clones.Infrastructure;
using UnityEngine;
using UnityEngine.UI;

namespace Clones.UI
{
    public class ChangeGameStateButton : MonoBehaviour
    {
        [SerializeField] private string _scene;
        [SerializeField] private Button _button;

        private IGameStateMachine _gameStateMachine;

        public void Init(IGameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
            _button.onClick.AddListener(Open);
        }

        private void Open()
        {
            _button.onClick.RemoveListener(Open);
            _gameStateMachine.Enter<LoadSceneState, (string, bool)>((_scene, true), _gameStateMachine.Enter<MainMenuLoopState>);
        }
    }
}