using Agava.YandexGames;
using Clones.Services;
using Clones.UI;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

namespace Clones.Infrastructure
{
    public class BootstrapState : IState
    {
        private const string InitScene = "Init";

        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly AllServices _services;
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly LoadingPanel _loadingPanel;

        public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoader, AllServices services, ICoroutineRunner coroutineRunner, LoadingPanel loadingPanel)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _services = services;
            _coroutineRunner = coroutineRunner;
            _loadingPanel = loadingPanel;

            RegisterServices();
        }

        public void Enter()
        {
            _loadingPanel.Open();
            _coroutineRunner.StartCoroutine(InitializeYandexSdk());
        }

        public void Exit() =>
            _loadingPanel.Close();

        private IEnumerator InitializeYandexSdk()
        {
#if !UNITY_WEBGL || UNITY_EDITOR
            _sceneLoader.Load(InitScene, callback: EnterLoadProgress);
            yield break;
#endif

            yield return YandexGamesSdk.Initialize();

            if (YandexGamesSdk.IsInitialized == false)
                throw new ArgumentNullException(nameof(YandexGamesSdk), "Yandex SDK didn't initialized correctly");

            YandexGamesSdk.CallbackLogging = true;
            _sceneLoader.Load(InitScene, callback: EnterLoadProgress);
        }

        private void RegisterServices()
        {
            RegisterMainMenuStaticData();
            RegisterGameStaticData();
            RegisterInputService();

            _services.RegisterSingle<IGameStateMachine>(_stateMachine);
            _services.RegisterSingle<IAssetProvider>(new AssetProvider());
            _services.RegisterSingle<IPersistentProgressService>(new PersistentProgressService());
            _services.RegisterSingle<ISaveLoadService>(new SaveLoadService(_services.Single<IPersistentProgressService>()));
            _services.RegisterSingle<ITimeScale>(new TimeScale());
            _services.RegisterSingle<IAdvertisingDisplay>(new AdvertisingDisplay(GetAudioMixerGroup(), _services.Single<ITimeScale>()));

            _services.RegisterSingle<IGameFacotry>(new GameFactory(_services.Single<IAssetProvider>(), _services.Single<IInputService>(), _services.Single<IGameStaticDataService>(), _services.Single<ITimeScale>(), _services.Single<IPersistentProgressService>(), _services.Single<IMainMenuStaticDataService>()));
            _services.RegisterSingle<IUiFactory>(new UiFactory(_services.Single<IAssetProvider>(), _services.Single<IPersistentProgressService>(), _stateMachine, _services.Single<IInputService>()));
            _services.RegisterSingle<IPartsFactory>(new PartsFactory(_services.Single<IGameStaticDataService>()));
            _services.RegisterSingle<IMainMenuFactory>(new MainMenuFactory(_services.Single<IAssetProvider>(), _services.Single<IGameStateMachine>(), _services.Single<IPersistentProgressService>(), _services.Single<IMainMenuStaticDataService>()));
            _services.RegisterSingle<IEducationFactory>(new EducationFactory(_services.Single<IPartsFactory>(), _services.Single<IGameStaticDataService>()));
        }

        private void RegisterInputService()
        {
#if !UNITY_EDITOR && UNITY_WEBGL
            if (UnityEngine.Application.isMobilePlatform)
                _services.RegisterSingle<IInputService>(new MobileInputService());
            else
                _services.RegisterSingle<IInputService>(new DescktopInputService());
#else
            _services.RegisterSingle<IInputService>(new MobileInputService());
#endif
        }

        private void RegisterMainMenuStaticData()
        {
            IMainMenuStaticDataService staticData = new MainMenuStaticDataService();
            staticData.Load();
            _services.RegisterSingle(staticData);
        }

        private void RegisterGameStaticData()
        {
            IGameStaticDataService staticData = new GameStaticDataService();
            staticData.Load();
            _services.RegisterSingle(staticData);
        }

        private void EnterLoadProgress() => 
            _stateMachine.Enter<LoadProgressState>();

        private AudioMixerGroup GetAudioMixerGroup() => 
            Resources.Load<AudioMixerGroup>(AssetPath.AudioMixerGroup);
    }
}