using Clones.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class CardsView<TType> : MonoBehaviour where TType : Enum
{
    private Card _currentCard;

    private Dictionary<Card, TType> _types = new();
    private Dictionary<TType, Card> _cards = new();

    protected IPersistentProgressService PersistentProgress { get; private set; }
    protected IMainMenuStaticDataService MainMenuStaticDataService { get; private set; }

    private void OnDestroy()
    {
        foreach (var card in _types.Keys)
        {
            card.GetComponent<CardButton>().Clicked -= Select;
            card.GetComponent<BuyCardView>().BuyTried -= OnBuyTried;
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
        card.GetComponent<BuyCardView>().BuyTried += OnBuyTried;

        _types.Add(card, type);
        _cards.Add(type, card);
    }

    protected void Select(Card card)
    {
        _currentCard?.Unselect();
        _currentCard = card;
        _currentCard.Select();

        SaveCurrentCard(_currentCard);
    }

    protected void SelectDefault() => 
        Select(_cards.Values.First());

    public abstract void SelectCurrentOrDefault();

    protected TType GetType(Card card) => 
        _types.GetValueOrDefault(card);

    protected Card GetCard(TType type) => 
        _cards.GetValueOrDefault(type);

    protected abstract void OnBuyTried(Card card);

    protected abstract void SaveCurrentCard(Card card);
}