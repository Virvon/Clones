﻿using Clones.Services;
using Clones.UI;

namespace Clones.Infrastructure
{
    public class Game
    {
        public GameStateMachine _stateMachine { get; private set; }

        public Game(LoadingPanel loadingPanel, ICoroutineRunner coroutineRunner)
        {
            _stateMachine = new GameStateMachine(new SceneLoader(), loadingPanel, AllServices.Instance, coroutineRunner);
        }
    }
}