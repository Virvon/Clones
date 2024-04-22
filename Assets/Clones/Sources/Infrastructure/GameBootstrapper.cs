using Clones.Services;
using Clones.UI;
using UnityEngine;
using UnityEngine.Audio;

namespace Clones.Infrastructure
{
    public class GameBootstrapper : MonoBehaviour, ICoroutineRunner
    {
        [SerializeField] private LoadingPanel _loadingPanelPrefab;
        [SerializeField] private AudioMixerGroup _audioMixer;

        private Game _game;

        private void Awake()
        {
            _game = new Game(Instantiate(_loadingPanelPrefab), _audioMixer, this);
            _game.StateMachine.Enter<BootstrapState>();

            DontDestroyOnLoad(this); 
        }
    }
}