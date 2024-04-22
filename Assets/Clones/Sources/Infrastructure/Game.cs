using Clones.Services;
using Clones.UI;
using UnityEngine.Audio;

namespace Clones.Infrastructure
{
    public class Game
    {
        public GameStateMachine StateMachine { get; private set; }

        public Game(LoadingPanel loadingPanel, AudioMixerGroup audioMixer, ICoroutineRunner coroutineRunner)
        {
            StateMachine = new GameStateMachine(new SceneLoader(), loadingPanel, audioMixer, AllServices.Instance, coroutineRunner);
        }
    }
}