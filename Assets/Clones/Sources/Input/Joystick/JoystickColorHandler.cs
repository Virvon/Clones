using Clones.Input;
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

    private void OnEnable()
    {
        JoysticDirectionHandler.Deactivated += SetDeactivateColor;
        JoysticDirectionHandler.Activated += SetActivateColor;

        SetDeactivateColor();
    }

    private void OnDisable()
    {
        JoysticDirectionHandler.Activated -= SetActivateColor;
        JoysticDirectionHandler.Deactivated -= SetDeactivateColor;
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
