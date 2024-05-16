using Clones.Services;
using Clones.UI;
using System;
using System.Collections.Generic;
using UnityEngine.Audio;

namespace Clones.Infrastructure
{
    public class GameStateMachine : IGameStateMachine
    {
        private readonly Dictionary<Type, IExitableState> _states;
        private IExitableState _currentState;

        public GameStateMachine(SceneLoader sceneLoader, LoadingPanel loadingPanel, AudioMixerGroup audioMixer, AllServices services, ICoroutineRunner coroutineRunner)
        {
            _states = new Dictionary<Type, IExitableState>()
            {
                [typeof(BootstrapState)] = new BootstrapState(this, sceneLoader, services, coroutineRunner, loadingPanel, audioMixer),
                [typeof(LoadProgressState)] = new LoadProgressState(this, services.Single<IPersistentProgressService>(), services.Single<ISaveLoadService>(), services.Single<IMainMenuStaticDataService>()),
                [typeof(EducationState)] = new EducationState(services.Single<IGameFacotry>(), services.Single<IPartsFactory>(), services.Single<IGameStaticDataService>(), services.Single<IPersistentProgressService>(), services.Single<IUiFactory>(), services.Single<IInputService>(), services.Single<IEducationFactory>(), services.Single<ITimeScaler>(), coroutineRunner, services.Single<ILocalization>(), services.Single<ICharacterFactory>(), services.Single<ISaveLoadService>()),
                [typeof(GameLoopState)] = new GameLoopState(services.Single<IGameFacotry>(), services.Single<IUiFactory>(), services.Single<IPartsFactory>(), services.Single<IPersistentProgressService>(), services.Single<ITimeScaler>(), services.Single<IMainMenuStaticDataService>(), services.Single<ISaveLoadService>(), services.Single<IGameStaticDataService>(), coroutineRunner, services.Single<IAdvertisingDisplay>(), services.Single<ILocalization>(), services.Single<ICharacterFactory>(), services.Single<ILeaderboard>()),
                [typeof(MainMenuLoopState)] = new MainMenuLoopState(services.Single<IMainMenuFactory>(), services.Single<IMainMenuStaticDataService>(), services.Single<ICharacterFactory>(), services.Single<IProgressReadersReporter>()),
                [typeof(LoadSceneState)] = new LoadSceneState(sceneLoader, loadingPanel, services.Single<IAdvertisingDisplay>())
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