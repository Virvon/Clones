using System;
using UnityEngine;
using UnityEngine.EventSystems;
using Clones.Input;

public class DirectionHandler : MonoBehaviour, IDragHandler
{
    [SerializeField] private RectTransform _handleBackground;

    public static Vector2 Direction { get; private set; }
    public static DirectionHandler Instance => _instance ?? (_instance = new DirectionHandler());

    private static DirectionHandler _instance;
    private PlayerInput _input;

    public static event Action Activated;
    public static event Action Deactivated;

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
        {
            Activated?.Invoke();
            Debug.Log("touch");
        }
    }

    private void OnUpTouch()
    {
        Direction = Vector2.zero;
        Deactivated?.Invoke();
    }

}
