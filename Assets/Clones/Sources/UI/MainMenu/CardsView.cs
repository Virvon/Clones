using Clones.Services;
using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class CardsView<TType> : MonoBehaviour where TType : Enum
{
    private Card _currentCard;

    protected Dictionary<Card, TType> Cards = new();

    protected IPersistentProgressService PersistentProgress;
    protected IMainMenuStaticDataService MainMenuStaticDataService;

    private void OnDestroy()
    {
        foreach (var card in Cards.Keys)
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
        Cards.Add(card, type);
    }

    private void Select(Card card)
    {
        _currentCard?.Unselect();
        _currentCard = card;
        _currentCard.Select();
    }

    protected abstract void OnBuyTried(Card card);
}