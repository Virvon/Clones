using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(DirectionHandler))]
public class JoystickPositionHandler : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private RectTransform _slidingArea;

    [SerializeField] private RectTransform _handle;
    [SerializeField] private RectTransform _handleBackground;

    private DirectionHandler _directionHandler;
    private Vector2 _handleBackgroundStartPosition;

    private void OnEnable()
    {
        _handleBackgroundStartPosition = _handleBackground.anchoredPosition;
        _directionHandler = GetComponent<DirectionHandler>();

        _directionHandler.Activated += OnActivated;
        _directionHandler.Deactivated += OnDeactivated;
    }

    private void OnDisable()
    {
        _directionHandler.Activated -= OnActivated;
        _directionHandler.Deactivated -= OnDeactivated;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Vector2 handleBackgroundPosition;

        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(_slidingArea, eventData.position, null, out handleBackgroundPosition))
            _handleBackground.anchoredPosition = handleBackgroundPosition;
    }

    private void OnActivated()
    {
        _handle.anchoredPosition = _directionHandler.Direction * (_handleBackground.sizeDelta / 2);
    }

    private void OnDeactivated()
    {
        _handle.anchoredPosition = Vector2.zero;
        _handleBackground.anchoredPosition = _handleBackgroundStartPosition;
    }
}
