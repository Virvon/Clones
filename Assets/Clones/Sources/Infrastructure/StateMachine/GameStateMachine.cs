﻿using Clones.Services;
using Clones.UI;
using System;
using System.Collections.Generic;

namespace Clones.Infrastructure
{
    public class GameStateMachine : IGameStateMachine
    {
        private readonly Dictionary<Type, IExitableState> _states;
        private IExitableState _currentState;

        public GameStateMachine(SceneLoader sceneLoader, LoadingPanel loadingPanel, AllServices services, ICoroutineRunner coroutineRunner)
        {
            _states = new Dictionary<Type, IExitableState>()
            {
                [typeof(BootstrapState)] = new BootstrapState(this, sceneLoader, services, coroutineRunner),
                [typeof(LoadProgressState)] = new LoadProgressState(this, services.Single<IPersistentProgressService>(), services.Single<ISaveLoadService>(), services.Single<IMainMenuStaticDataService>()),
                [typeof(GameLoopState)] = new GameLoopState(this, services.Single<IGameFacotry>(), services.Single<IUiFactory>(), services.Single<IPartsFactory>(), services.Single<IPersistentProgressService>(), services.Single<ITimeScale>(), services.Single<IMainMenuStaticDataService>(), services.Single<ISaveLoadService>(), services.Single<IGameStaticDataService>()),
                [typeof(MainMenuLoopState)] = new MainMenuLoopState(this, services.Single<IMainMenuFactory>(), services.Single<IMainMenuStaticDataService>(), services.Single<ISaveLoadService>()),
                [typeof(LoadSceneState)] = new LoadSceneState(this, sceneLoader, loadingPanel)
            };
        }

        public void Enter<TState>() where TState : class, IState
        {
            TState state = ChangeState<TState>();
            state.Enter();
        }

        public void Enter<TState, TPayload>(TPayload payload, Action callback) where TState : class, IPayloadState<TPayload>
        {
            TState state = ChangeState<TState>();
            state.Enter(payload, callback);
        }

        private TState GetState<TState>() where TState : class, IExitableState =>
            _states[typeof(TState)] as TState;

        private TState ChangeState<TState>() where TState : class, IExitableState
        {
            _currentState?.Exit();

            TState state = GetState<TState>();

            _currentState = state;

            return state;
        }
    }
}