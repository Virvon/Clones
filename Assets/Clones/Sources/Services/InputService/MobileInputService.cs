using Clones.Input;
using System;
using UnityEngine;

namespace Clones.Services
{
    public class MobileInputService : IInputService
    {
        public MobileInputService()
        {
            JoystickDirectionHandler.Activated += () => Activated?.Invoke();
            JoystickDirectionHandler.Deactivated += () => Deactivated?.Invoke();
        }

        ~MobileInputService()
        {
            JoystickDirectionHandler.Activated -= () => Activated?.Invoke();
            JoystickDirectionHandler.Deactivated -= () => Deactivated?.Invoke();
        }

        public event Action Activated;
        public event Action Deactivated;

        public Vector2 Direction => JoystickDirectionHandler.Direction;
        public string ControlPath => AssetPath.Joystick;
    }
}