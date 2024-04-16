using System;
using System.Collections;
using UnityEngine;

namespace Clones.UI
{
    public class CardsScrollRect : MonoBehaviour
    {
        [SerializeField] private MonoBehaviour _cardViewBehaviour;
        [SerializeField] private RectTransform _content;

        private ToggleWindows _toggleWindows;
        private ICardsView _cardView;

        public void Init(ToggleWindows toggleWindows)
        {
            _toggleWindows = toggleWindows;
        }

        private void OnValidate()
        {
            if (_cardViewBehaviour && _cardViewBehaviour is not ICardsView)
            {
                Debug.LogError(nameof(_cardViewBehaviour) + " needs to implement " + nameof(ICardsView));
                _cardViewBehaviour = null;
            }
        }

        private void Start()
        {
            _cardView = (ICardsView)_cardViewBehaviour;
            ScrollToCard();

            _toggleWindows.WindowToggled += ScrollToCard;
            _cardView.CardSelected += ScrollToCard;
        }

        private void OnDestroy()
        {
            _toggleWindows.WindowToggled -= ScrollToCard;

            if(_cardView != null)
            _cardView.CardSelected -= ScrollToCard;
        }

        public void ScrollToCard()
        {
            Debug.Log(_cardView != null);
            Debug.Log(_toggleWindows != null);
            Debug.Log(_content != null);
            StartCoroutine(SizeDeltaWaiter(callback: () =>
            {
                if (RectTransformUtility.ScreenPointToLocalPointInRectangle(_content, _cardView.CurrentCard.transform.position, null, out Vector2 localPoint))
                    _content.anchoredPosition = new Vector2(_content.anchoredPosition.x, -localPoint.y);
            }));
        }

        private IEnumerator SizeDeltaWaiter(Action callback)
        {
            while (_content.sizeDelta.y == 0)
                yield return null;

            callback.Invoke();
        }
    }
}