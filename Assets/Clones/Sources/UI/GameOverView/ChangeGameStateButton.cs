using Clones.Infrastructure;
using UnityEngine;

namespace Clones.UI
{
    public class ChangeGameStateButton : MonoBehaviour
    {
        [SerializeField] private string _scene;

        private IGameStateMachine _gameStateMachine;

        public void Init(IGameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }

        public void Open()
        {
            _gameStateMachine.Enter<LoadSceneState, string>(_scene, _gameStateMachine.Enter<MainMenuLoopState>);
        }
    }
}
