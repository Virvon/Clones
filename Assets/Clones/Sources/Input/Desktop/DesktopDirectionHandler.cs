using System;
using UnityEngine;

namespace Clones.Input
{
    public class DesktopDirectionHandler : MonoBehaviour
    {
        [SerializeField] private RectTransform _background;

        private PlayerInput _input;
        private Player _player;

        public static Vector2 Direction { get; private set; }

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

        public void Init(Player player)
        {
            _player = player;
        }

        private void OnDownTouch()
        {
            Vector3 mousePosition = UnityEngine.Input.mousePosition;

            if (RectTransformUtility.RectangleContainsScreenPoint(_background, new Vector2(mousePosition.x, mousePosition.y)))
            {
                Vector3 PlayerScreenPosition = Camera.main.WorldToScreenPoint(_player.transform.position);
                Vector2 direction = new Vector2(mousePosition.x, mousePosition.y) - new Vector2(PlayerScreenPosition.x, PlayerScreenPosition.y);

                Direction = direction.normalized;
            }

            if (Direction != Vector2.zero)
                Activated?.Invoke();
        }

        private void OnUpTouch()
        {
            Direction = Vector2.zero;
            Deactivated?.Invoke();
        }
    }
}
