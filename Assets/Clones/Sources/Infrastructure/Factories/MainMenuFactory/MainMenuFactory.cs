﻿using Clones.Data;
using Clones.Services;
using Clones.StaticData;
using Clones.UI;
using System.Linq;
using UnityEngine;

namespace Clones.Infrastructure
{
    public class MainMenuFactory : IMainMenuFactory
    {
        private readonly IAssetProvider _assets;
        private readonly IGameStateMachine _gameStateMachine;
        private readonly IPersistentProgressService _persistentProgress;
        private readonly IMainMenuStaticDataService _staticDataService;

        private MainMenuContainers _containers;
        private ClonesCardsView _clonesCardsView;

        public MainMenuFactory(IAssetProvider assets, IGameStateMachine gameStateMachine, IPersistentProgressService persistentProgress, IMainMenuStaticDataService staticDataService)
        {
            _assets = assets;
            _gameStateMachine = gameStateMachine;
            _persistentProgress = persistentProgress;
            _staticDataService = staticDataService;
        }

        public GameObject CreateMainMenu()
        {
            MainMenuStaticData menuData = _staticDataService.GetMainMenu();

            var menu = Object.Instantiate(menuData.Prefab);

            menu.GetComponentInChildren<PlayButton>()
                .Init(_gameStateMachine);

            menu.GetComponentInChildren<MoneyView>()
                .Init(_persistentProgress.Progress.Wallet);

            menu.GetComponentInChildren<DnaView>()
                .Init(_persistentProgress.Progress.Wallet);

            _containers = menu.GetComponentInChildren<MainMenuContainers>();

            return menu;
        }

        public void CreateCloneCard(CloneType type)
        {
            CloneStaticData cloneStaticData = _staticDataService.GetClone(type);

            var cardObject = Object.Instantiate(cloneStaticData.Card, _clonesCardsView.transform);
            CloneCard card = cardObject.GetComponent<CloneCard>();

            _clonesCardsView.AddCard(card, type);

            bool isBuyed = _persistentProgress.Progress.AvailableClones.Clones.Any(clone => clone.Type == type);
            card.Init(isBuyed);

            if(isBuyed == false)
                cardObject.GetComponent<BuyCardView>().Init(cloneStaticData.BuyPrice, _persistentProgress.Progress.Wallet);
        }

        public void CreateWandCard(WandType type)
        {
            WandStaticData wandData = _staticDataService.GetWand(type);

            //CardsView<WandType> wandsCardsView = _containers.WandsCardsView;

            //var cardObject = Object.Instantiate(wandData.Card, wandsCardsView.transform);
            //CardWand card = cardObject.GetComponent<CardWand>();

            //wandsCardsView.AddCard(card, type);

            //card.Init(wandData.Damage, wandData.Cooldown);
        }

        public void CreateClonesCardsView()
        {
            GameObject viewObject = _assets.Instantiate(AssetPath.ClonesCardsView, _containers.ClonesCards);

            _clonesCardsView = viewObject.GetComponentInChildren<ClonesCardsView>();
            _clonesCardsView.Init(_persistentProgress, _staticDataService);
        }
    }
}