using System;
using System.Collections;
using UnityEngine;

namespace Clones.UI
{
    public class CardsScrollRect : MonoBehaviour
    {
        [SerializeField] private RectTransform _content;

        public void ScrollToCard(Card card)
        {
            StartCoroutine(SizeDeltaWaiter(callback: () =>
            {
                if (RectTransformUtility.ScreenPointToLocalPointInRectangle(_content, card.transform.position, null, out Vector2 localPoint))
                    _content.anchoredPosition = new Vector2(_content.anchoredPosition.x, -localPoint.y);
            }));
        }

        private IEnumerator SizeDeltaWaiter(Action callback)
        {
            while(_content.sizeDelta.y == 0)
                yield return null;

            callback.Invoke();
        }
    }
}