using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Clones.Input
{
    public class DesktopDirectionHandler : MonoBehaviour, IDragHandler
    {
        private PlayerInput _input;
        private Player _player;

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

        public void Init(Player player)
        {
            _player = player;
        }

        public void OnDrag(PointerEventData eventData)
        {
            throw new NotImplementedException();
        }

        private void OnUpTouch()
        {
            
        }
    }
}
