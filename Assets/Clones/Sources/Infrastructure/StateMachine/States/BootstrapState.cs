using Agava.YandexGames;
using Clones.Services;
using Clones.UI;
using Lean.Localization;
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
            _coroutineRunner.StartCoroutine(InitializeYandexSdk(callback: () =>
            {
                _sceneLoader.Load(InitScene, false, callback: EnterLoadProgress);
                _coroutineRunner.StartCoroutine(SetLanguage(_services.Single<IPersistentProgressService>(), callback: _sceneLoader.AllowSceneActivation));
            }));
        }


        private enum Language
        {
            Russian,
            English
        }

        private IEnumerator SetLanguage(IPersistentProgressService persistentProgress, Action callback = null)
        {
            while (LeanLocalization.CurrentLanguages.Count == 0)
                yield return null;

            string isoLanguage;
            string leanLanguage;

#if !UNITY_WEBGL || UNITY_EDITOR
            isoLanguage = "ru";
#else
            isoLanguage = YandexGamesSdk.Environment.i18n.lang;
#endif

            leanLanguage = persistentProgress.Progress.Language.TryGetCurrentLeanLanguage(out string language) ? language : persistentProgress.Progress.Language.TranslateToLeanLanguage(isoLanguage);
            persistentProgress.Progress.Language.CurrentIsoLanguage = isoLanguage;
            LeanLocalization.SetCurrentLanguageAll(leanLanguage);

            Debug.Log("boot iso language local " + persistentProgress.Progress.Language.CurrentIsoLanguage);
            Debug.Log("boot iso language service " + _services.Single<IPersistentProgressService>().Progress.Language.CurrentIsoLanguage);

            callback?.Invoke();
        }
        

        public void Exit() =>
            _loadingPanel.Close();

        private IEnumerator InitializeYandexSdk(Action callback = null)
        {
#if !UNITY_WEBGL || UNITY_EDITOR
            callback?.Invoke();
            yield break;
#else

            yield return YandexGamesSdk.Initialize();

            if (YandexGamesSdk.IsInitialized == false)
                throw new ArgumentNullException(nameof(YandexGamesSdk), "Yandex SDK didn't initialized correctly");

            YandexGamesSdk.CallbackLogging = true;
            callback?.Invoke();
#endif
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
            _services.RegisterSingle<IAdvertisingDisplay>(new AdvertisingDisplay(GetAudioMixerGroup(), _services.Single<ITimeScale>(), _coroutineRunner));

            _services.RegisterSingle<IGameFacotry>(new GameFactory(_services.Single<IAssetProvider>(), _services.Single<IInputService>(), _services.Single<IGameStaticDataService>(), _services.Single<ITimeScale>(), _services.Single<IPersistentProgressService>(), _services.Single<IMainMenuStaticDataService>()));
            _services.RegisterSingle<IUiFactory>(new UiFactory(_services.Single<IAssetProvider>(), _services.Single<IPersistentProgressService>(), _stateMachine, _services.Single<IInputService>()));
            _services.RegisterSingle<IPartsFactory>(new PartsFactory(_services.Single<IGameStaticDataService>()));
            _services.RegisterSingle<IMainMenuFactory>(new MainMenuFactory(_services.Single<IAssetProvider>(), _services.Single<IGameStateMachine>(), _services.Single<IPersistentProgressService>(), _services.Single<IMainMenuStaticDataService>(), _services.Single<ISaveLoadService>()));
            _services.RegisterSingle<IEducationFactory>(new EducationFactory(_services.Single<IPartsFactory>(), _services.Single<IGameStaticDataService>(), _services.Single<IAssetProvider>()));
        }

        private void RegisterInputService()
        {
#if !UNITY_EDITOR && UNITY_WEBGL
            if (UnityEngine.Application.isMobilePlatform)
                _services.RegisterSingle<IInputService>(new MobileInputService());
            else
                _services.RegisterSingle<IInputService>(new DescktopInputService());
#else
            _services.RegisterSingle<IInputService>(new DescktopInputService());
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