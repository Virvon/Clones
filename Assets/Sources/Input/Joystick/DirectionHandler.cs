using System;
using UnityEngine;
using UnityEngine.EventSystems;
using Clones.Input;

public class DirectionHandler : MonoBehaviour, IDragHandler
{
    [SerializeField] private RectTransform _handleBackground;

    public Vector2 Direction { get; private set; }

    private PlayerInput _input;

    public event Action Activated;
    public event Action Deactivated;

    private void OnEnable()
    {
        _input = new PlayerInput();
        _input.Enable();

        _input.Player.Touch.performed += ctx => OnUpTouch();
    }

    private void OnDisable()
    {
        _input.Player.Touch.performed -= ctx => OnUpTouch();

        _input.Disable();
    }

    private void Update()
    {
        //Debug.Log(Direction);

        if (_input.Player.Touch.phase == UnityEngine.InputSystem.InputActionPhase.Started)
            OnDownTouch();
    }
    public void OnDrag(PointerEventData eventData)
    {
        Vector2 handlePosition;

        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(_handleBackground, eventData.position, null, out handlePosition))
        {
            handlePosition = handlePosition * 2 / _handleBackground.sizeDelta;

            if (handlePosition.magnitude > 1)
                handlePosition.Normalize();

            Direction = handlePosition;
        }
    }

    private void OnDownTouch()
    {
        if (Direction != Vector2.zero)
            Activated?.Invoke();
    }

    private void OnUpTouch()
    {
        Direction = Vector2.zero;
        Deactivated?.Invoke();
    }
}
