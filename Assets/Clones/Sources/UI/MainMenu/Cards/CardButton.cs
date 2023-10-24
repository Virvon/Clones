using System;
using UnityEngine;
using UnityEngine.UI;

namespace Clones.UI
{
    [RequireComponent(typeof(Card), typeof(Button))]
    public class CardButton : MonoBehaviour
    {
        private Card _card;
        private Button _button;

        public event Action<Card> Clicked;

        private void OnEnable()
        {
            _card = GetComponent<Card>();
            _button = GetComponent<Button>();

            _button.onClick.AddListener(OnClicked);
        }

        private void OnDisable() =>
            _button.onClick.RemoveListener(OnClicked);

        private void OnClicked() =>
            Clicked?.Invoke(_card);
    }
}
