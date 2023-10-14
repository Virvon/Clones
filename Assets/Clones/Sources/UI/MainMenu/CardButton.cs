using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Card), typeof(Button))]
public class CardButton : MonoBehaviour
{
    private Card _card;
    private Button _button;

    public event Action<Card> Clicked;

    private void Start()
    {
        _card = GetComponent<Card>();
        _button = GetComponent<Button>();

        _button.onClick.AddListener(OnClicked);
    }

    private void OnDestroy() => 
        _button.onClick.RemoveListener(OnClicked);

    private void OnClicked() => 
        Clicked?.Invoke(_card);
}
