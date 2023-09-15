using System;

namespace Clones.Infrastructure
{
    public class BootstrapState : IState
    {
        private const string InitScene = "InitInfrastructure";
        private const string MainMenuScene = "MainInfrasructureScene";

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
            _sceneLoader.Load(InitScene, callback: EnterMainMenu);

        public void Exit()
        {
            
        }

        private void RegisterServices()
        {
            RegisterStaticData();

            _services.RegisterSingle<IGameStateMachine>(_stateMachine);
            _services.RegisterSingle<IInputService>(new MobileInputService());
            _services.RegisterSingle<IAssetProvider>(new AssetProvider());
            _services.RegisterSingle<IGameFactory>(new GameFactory(_services.Single<IAssetProvider>(), _services.Single<IInputService>(), _services.Single<IStaticDataService>()));
            _services.RegisterSingle<IPersistentProgressService>(new PersistentProgressService());
            _services.RegisterSingle<IMainMenuFactory>(new MainMenuFactory(_services.Single<IAssetProvider>(), _services.Single<IGameStateMachine>()));

            _services.RegisterSingle<IDestroyDroppableReporter>(new DestroyDroppableReporter(_services.Single<IGameFactory>()));
            _services.RegisterSingle<IItemsDropper>(new ItemsDropper(_services.Single<IGameFactory>(), _services.Single<IDestroyDroppableReporter>()));
        }

        private void RegisterStaticData()
        {
            IStaticDataService staticData = new StaticDataService();
            staticData.Load();
            _services.RegisterSingle<IStaticDataService>(staticData);
        }

        private void EnterMainMenu() => 
            _stateMachine.Enter<LoadSceneState, string>(MainMenuScene, _stateMachine.Enter<MainMenuLoopState>);
    }
}