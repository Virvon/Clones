using Clones.Services;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Clones.UI
{
    public abstract class CardsView<TType> : MonoBehaviour where TType : Enum
    {
        private Card _currentCard;

        private Dictionary<Card, TType> _types = new();
        private Dictionary<TType, Card> _cards = new();

        protected IPersistentProgressService PersistentProgress { get; private set; }
        protected IMainMenuStaticDataService MainMenuStaticDataService { get; private set; }

        private void OnDestroy()
        {
            foreach (var card in _cards.Values)
            {
                card.GetComponent<CardButton>().Clicked -= Select;
                card.GetComponent<BuyCardView>().BuyCardTried -= OnBuyTried;
            }
        }

        public void Init(IPersistentProgressService persistentProgress, IMainMenuStaticDataService mainMenuStaticDataService)
        {
            PersistentProgress = persistentProgress;
            MainMenuStaticDataService = mainMenuStaticDataService;
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
            _currentCard?.Unselect();
            _currentCard = card;
            _currentCard.Select();

            SaveCurrentCard(_currentCard);
        }

        protected TType GetType(Card card) =>
            _types.GetValueOrDefault(card);

        protected Card GetCard(TType type) =>
            _cards.GetValueOrDefault(type);

        protected abstract void SelectDefault();

        protected abstract void OnBuyTried(Card card);

        protected abstract void SaveCurrentCard(Card card);
    }
}