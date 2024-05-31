using Clones.Input;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(JoystickDirectionHandler))]
public class JoystickPositionHandler : MonoBehaviour, IPointerDownHandler, IDragHandler
{
    [SerializeField] private RectTransform _slidingArea;
    [SerializeField] private RectTransform _handle;
    [SerializeField] private RectTransform _handleBackground;

    private Vector2 _handleBackgroundStartPosition;

    private void OnEnable()
    {
        _handleBackgroundStartPosition = _handleBackground.anchoredPosition;

        JoystickDirectionHandler.Deactivated += OnDeactivated;
    }

    private void OnDisable() =>
        JoystickDirectionHandler.Deactivated -= OnDeactivated;

    public void OnPointerDown(PointerEventData eventData)
    {
        Vector2 handleBackgroundPosition;

        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(_slidingArea, eventData.position, null, out handleBackgroundPosition))
            _handleBackground.anchoredPosition = handleBackgroundPosition;
    }

    public void OnDrag(PointerEventData eventData) => 
        _handle.anchoredPosition = JoystickDirectionHandler.Direction * (_handleBackground.sizeDelta / 2);

    private void OnDeactivated()
    {
        _handle.anchoredPosition = Vector2.zero;
        _handleBackground.anchoredPosition = _handleBackgroundStartPosition;
    }
}
