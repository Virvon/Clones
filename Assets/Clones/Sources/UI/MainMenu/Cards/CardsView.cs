using Clones.Services;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Clones.UI
{
    public abstract class CardsView<TType> : MonoBehaviour, ICardsView where TType : Enum
    {
        private Dictionary<Card, TType> _types = new();
        private Dictionary<TType, Card> _cards = new();

        public Card CurrentCard { get; private set; }

        protected IPersistentProgressService PersistentProgress { get; private set; }
        protected IMainMenuStaticDataService MainMenuStaticDataService { get; private set; }
        protected ISaveLoadService SaveLoadService { get; private set; }

        public event Action CardSelected;

        public void Init(IPersistentProgressService persistentProgress, IMainMenuStaticDataService mainMenuStaticDataService, ISaveLoadService saveLoadService)
        {
            PersistentProgress = persistentProgress;
            MainMenuStaticDataService = mainMenuStaticDataService;
            SaveLoadService = saveLoadService;
        }

        private void OnDestroy()
        {
            foreach (var card in _cards.Values)
            {
                card.GetComponent<CardButton>().Clicked -= Select;
                card.GetComponent<BuyCardView>().BuyCardTried -= OnBuyTried;
            }
        }

        public void AddCard(Card card, TType type)
        {
            card.GetComponent<CardButton>().Clicked += Select;
            card.GetComponent<BuyCardView>().BuyCardTried += OnBuyTried;

            _types.Add(card, type);
            _cards.Add(type, card);
        }

        public abstract void SelectCurrentOrDefault();

        protected void Select(Card card)
        {
            if (CurrentCard == card)
                return;

            CurrentCard?.Unselect();
            CurrentCard = card;
            CurrentCard.Select();

            UpdateCurrentProgress(CurrentCard);
            SaveLoadService.SaveProgress();

            CardSelected?.Invoke();
        }

        protected TType GetType(Card card) =>
            _types.GetValueOrDefault(card);

        protected Card GetCard(TType type) =>
            _cards.GetValueOrDefault(type);

        protected abstract void SelectDefault();

        protected abstract void OnBuyTried(Card card);

        protected abstract void UpdateCurrentProgress(Card card);
    }
}