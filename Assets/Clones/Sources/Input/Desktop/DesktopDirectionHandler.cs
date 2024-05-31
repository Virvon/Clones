﻿using Clones.Character.Player;
using System;
using UnityEngine;

namespace Clones.Input
{
    public class DesktopDirectionHandler : MonoBehaviour, IStopable
    {
        private const float Delta = 0;

        [SerializeField] private RectTransform _background;

        private PlayerInput _input;
        private Player _player;
        private bool _isActivated;

        public static event Action Activated;
        public static event Action Deactivated;

        public static Vector2 Direction { get; private set; }

        private void OnEnable()
        {
            _isActivated = false;
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

        public void Init(Player player) =>
            _player = player;

        public void Stop() => 
            OnUpTouch();

        private void OnDownTouch()
        {
            Vector3 mousePosition = UnityEngine.Input.mousePosition;

            if (RectTransformUtility.RectangleContainsScreenPoint(_background, new Vector2(mousePosition.x, mousePosition.y)))
            {
                Vector3 PlayerScreenPosition = Camera.main.WorldToScreenPoint(_player.transform.position);
                Vector2 direction = new Vector2(mousePosition.x, mousePosition.y) - new Vector2(PlayerScreenPosition.x, PlayerScreenPosition.y);

                if(direction.sqrMagnitude > Delta)
                    Direction = direction.normalized;
            }

            if (_isActivated == false)
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
