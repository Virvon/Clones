using Clones.GameLogic;
using Clones.Input;
using Clones.Services;
using UnityEngine;
using UnityEngine.UI;

namespace Clones.UI
{
    public class ExitToMenuButton : MonoBehaviour
    {
        [SerializeField] private Button _button;

        private GameObject _controlObject;
        private IDestoryableEnemies _destoryableEnemies;
        private PlayerHealth _playerHealth;
        private GameOverView _gameOverView;
        private ITimeScaler _timeScaler;
        private GameRevivalView _gameRevivalView;

        public void Init(GameObject controlObject, IDestoryableEnemies destoryableEnemies, PlayerHealth playerHealth, GameOverView gameOverView, ITimeScaler timeScaler, GameRevivalView gameRevivalView)
        {
            _controlObject = controlObject;
            _destoryableEnemies = destoryableEnemies;
            _playerHealth = playerHealth;   
            _gameOverView = gameOverView;
            _timeScaler = timeScaler;
            _gameRevivalView = gameRevivalView;

            _button.onClick.AddListener(OnButtonClick);
            _gameRevivalView.Opened += DisableInteractable;
        }

        public void OnDestroy()
        {
            _button.onClick.RemoveListener(OnButtonClick);
            _gameRevivalView.Opened -= DisableInteractable;
        }

        private void OnButtonClick()
        {
            _playerHealth.SetInvulnerability();
            _timeScaler.Scaled(0);
            _controlObject.GetComponent<IStopable>().Stop();
            _controlObject.SetActive(false);
            _destoryableEnemies.DestroyExistingEnemies();
            DisableInteractable();
            _gameOverView.Open();
        }

        private void DisableInteractable() => 
            _button.interactable = false;
    }
}