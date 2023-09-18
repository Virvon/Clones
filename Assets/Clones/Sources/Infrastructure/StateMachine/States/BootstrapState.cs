using Clones.Services;
using System;

namespace Clones.Infrastructure
{
    public class BootstrapState : IState
    {
        private const string InitScene = "InitInfrastructure";

        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly AllServices _services;
        private readonly ICoroutineRunner _coroutineRunner;

        public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoader, AllServices services, ICoroutineRunner coroutineRunner)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _services = services;
            _coroutineRunner = coroutineRunner;

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

            _services.RegisterSingle<IQuestsCreator>(new QuestsCreator(_services.Single<IPersistentProgressService>()));//
            _services.RegisterSingle<IDestroyDroppableReporter>(new DestroyDroppableReporter());//
            _services.RegisterSingle<IItemsCounter>(new ItemsCounter(_services.Single<IQuestsCreator>(), _services.Single<IPersistentProgressService>()));//

            _services.RegisterSingle<IGameFactory>(new GameFactory(_services.Single<IAssetProvider>(), _services.Single<IInputService>(), _services.Single<IStaticDataService>(), _services.Single<IQuestsCreator>(), _services.Single<IDestroyDroppableReporter>(), _services.Single<IItemsCounter>(), _services.Single<IPersistentProgressService>()));
            _services.RegisterSingle<IMainMenuFactory>(new MainMenuFactory(_services.Single<IAssetProvider>(), _services.Single<IGameStateMachine>()));
            _services.RegisterSingle<IEnemiesSpawner>(new EnemiesSpawner(_coroutineRunner)); //
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