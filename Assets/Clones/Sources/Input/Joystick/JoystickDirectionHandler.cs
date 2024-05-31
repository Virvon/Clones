using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Clones.Input
{
    public class JoystickDirectionHandler : MonoBehaviour, IDragHandler, IStopable
    {
        [SerializeField] private RectTransform _handleBackground;

        private PlayerInput _input;
        private bool _isActivated;

        public static event Action Activated;
        public static event Action Deactivated;

        public static Vector2 Direction { get; private set; }

        private void OnEnable()
        {
            _input = new PlayerInput();
            _input.Enable();

            _input.Player.Touch.performed += ctx => OnUpTouch();
        }

        private void Update()
        {
            if (_input.Player.Touch.phase == UnityEngine.InputSystem.InputActionPhase.Started)
                OnDownTouch();
        }

        private void OnDisable()
        {
            _input.Player.Touch.performed -= ctx => OnUpTouch();

            _input.Disable();
        }

        public void Stop() =>
            OnUpTouch();

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
            if (Direction != Vector2.zero && _isActivated == false)
            {
                _isActivated = true;
                Activated?.Invoke();
            }
        }

        private void OnUpTouch()
        {
            _isActivated = false;
            Direction = Vector2.zero;
            Deactivated?.Invoke();
        }
    }
}
