using System;
using UnityEngine;

namespace Clones.Infrastructure
{
    public class BootstrapState : IState
    {
        private const string InitScene = "Init2";
        private const string GameScene = "ExampleScene2";

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
            _sceneLoader.Load(InitScene, callback: EnterLoadLevel);

        public void Exit()
        {
            
        }

        private void RegisterServices()
        {
            _services.RegisterSingle<IInputService>(new MobileInputService());
            _services.RegisterSingle<IAssetProvider>(new AssetProvider());
            _services.RegisterSingle<IGameFactory>(new GameFactory(AllServices.Instance.Single<IAssetProvider>()));
        }

        private void EnterLoadLevel() => 
            _stateMachine.Enter<LoadSceneState, string>(GameScene);
    }
}