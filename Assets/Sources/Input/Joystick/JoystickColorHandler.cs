using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class JoystickColorHandler : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private Image _handle;
    [SerializeField] private Image _handleBackground;

    [SerializeField] private Color _activateHandleColor;
    [SerializeField] private Color _deactivateHandleColor;

    [SerializeField] private Color _activateHandleBackgroundColor;
    [SerializeField] private Color _deactivateHandleBackgroundColor;

    private DirectionHandler _directionHandler;

    private void OnEnable()
    {
        _directionHandler = GetComponent<DirectionHandler>();

        _directionHandler.Deactivated += SetDeactivateColor;

        SetDeactivateColor();
    }

    private void OnDisable()
    {
        _directionHandler.Activated -= SetActivateColor;
        _directionHandler.Deactivated -= SetDeactivateColor;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        SetActivateColor();
    }

    private void SetActivateColor()
    {
        _handle.color = _activateHandleColor;
        _handleBackground.color = _activateHandleBackgroundColor;
    }

    private void SetDeactivateColor()
    {
        _handle.color = _deactivateHandleColor;
        _handleBackground.color = _deactivateHandleBackgroundColor;
    }

}
