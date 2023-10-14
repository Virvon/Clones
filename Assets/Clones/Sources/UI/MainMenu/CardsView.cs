using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CardsView : MonoBehaviour
{
    private Card _currentCard;
    private List<Card> _cards;

    public void Select(Card card)
    {
        _currentCard?.Unselect();
        _currentCard = card;
        _currentCard.Select();
    }

    public void AddCard(Card card) =>
        _cards.Add(card);

    public Card TryGetAccessibleCard() =>
        _cards.Where(card => card.CanSelected).FirstOrDefault();
}
