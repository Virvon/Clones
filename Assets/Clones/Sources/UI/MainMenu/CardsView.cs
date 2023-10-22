using Clones.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class CardsView<TType> : MonoBehaviour where TType : Enum
{
    private Card _currentCard;

    protected Dictionary<Card, TType> Types = new();
    protected Dictionary<TType, Card> Cards = new();

    protected IPersistentProgressService PersistentProgress { get; private set; }
    protected IMainMenuStaticDataService MainMenuStaticDataService { get; private set; }

    private void OnDestroy()
    {
        foreach (var card in Types.Keys)
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

        Types.Add(card, type);
        Cards.Add(type, card);
    }

    protected void Select(Card card)
    {
        _currentCard?.Unselect();
        _currentCard = card;
        _currentCard.Select();

        SaveCurrentCard(_currentCard);
    }

    protected abstract void SelectDefault();

    public abstract void SelectCurrentOrDefault();

    protected TType GetType(Card card) => 
        Types.GetValueOrDefault(card);

    protected Card GetCard(TType type) => 
        Cards.GetValueOrDefault(type);

    protected abstract void OnBuyTried(Card card);

    protected abstract void SaveCurrentCard(Card card);
}