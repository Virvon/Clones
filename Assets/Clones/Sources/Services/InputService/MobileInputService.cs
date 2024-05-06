using Clones.Input;
using System;
using UnityEngine;

namespace Clones.Services
{
    public class MobileInputService : IInputService
    {
        public Vector2 Direction => JoysticDirectionHandler.Direction;
        public string ControlPath => AssetPath.Joystick;

        public event Action Activated;
        public event Action Deactivated;

        public MobileInputService()
        {
            JoysticDirectionHandler.Activated += () => Activated?.Invoke();
            JoysticDirectionHandler.Deactivated += () => Deactivated?.Invoke();
        }
    }
}