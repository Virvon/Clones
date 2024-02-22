using UnityEngine;

namespace Clones.UI
{
    public class EducationRevivalView : MonoBehaviour, IOpenableView
    {
        private GameOverView _gameOverView;

        public void Init(GameOverView gameOverView) => 
            _gameOverView = gameOverView;

        public void Open() => 
            _gameOverView.Open();
    }
}