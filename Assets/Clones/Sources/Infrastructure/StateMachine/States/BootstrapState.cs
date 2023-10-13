using Clones.Services;
using System;

namespace Clones.Infrastructure
{
    public class BootstrapState : IState
    {
        private const string InitScene = "Init";

        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly AllServices _services;

        public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoader, AllServices services)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _services = services;

            RegisterServices();
        }

        public void Enter() => 
            _sceneLoader.Load(InitScene, callback: EnterLoadProgress);

        public void Exit()
        {
            
        }

        private void RegisterServices()
        {
            RegisterStaticData();

            _services.RegisterSingle<IGameStateMachine>(_stateMachine);
            _services.RegisterSingle<IInputService>(new MobileInputService());
            _services.RegisterSingle<IAssetProvider>(new AssetProvider());
            _services.RegisterSingle<IPersistentProgressService>(new PersistentProgressService());
            _services.RegisterSingle<ISaveLoadService>(new SaveLoadService(_services.Single<IPersistentProgressService>()));
            _services.RegisterSingle<ITimeScale>(new TimeScale());
            _services.RegisterSingle<IPlayerStats>(new PlayerStats());

            _services.RegisterSingle<IGameFacotry>(new GameFactory(_services.Single<IAssetProvider>(), _services.Single<IInputService>(), _services.Single<IStaticDataService>(), _services.Single<ITimeScale>()));
            _services.RegisterSingle<IUiFactory>(new UiFactory(_services.Single<IAssetProvider>(), _services.Single<IPersistentProgressService>(), _stateMachine));
            _services.RegisterSingle<IPartsFactory>(new PartsFactory(_services.Single<IStaticDataService>()));
            _services.RegisterSingle<IMainMenuFactory>(new MainMenuFactory(_services.Single<IAssetProvider>(), _services.Single<IGameStateMachine>(), _services.Single<IPersistentProgressService>()));
        }

        private void RegisterStaticData()
        {
            IStaticDataService staticData = new StaticDataService();
            staticData.Load();
            _services.RegisterSingle(staticData);
        }

        private void EnterLoadProgress() => 
            _stateMachine.Enter<LoadProgressState>();
    }
}