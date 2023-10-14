using System.Collections.Generic;
using UnityEngine;

public class CardsView : MonoBehaviour
{
    private Card _currentCard;
    private List<Card> _cards = new();

    private void OnDestroy()
    {
        foreach (var card in _cards)
            card.GetComponent<CardButton>().Clicked -= Select;
    }

    public void AddCard(Card card)
    {
        card.GetComponent<CardButton>().Clicked += Select;
        _cards.Add(card);
    }

    private void Select(Card card)
    {
        _currentCard?.Unselect();
        _currentCard = card;
        _currentCard.Select();
    }
}