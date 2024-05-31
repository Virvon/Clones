using Clones.Input;
using System;
using UnityEngine;

namespace Clones.Services
{
    public class DescktopInputService : IInputService
    {
        public DescktopInputService()
        {
            DesktopDirectionHandler.Activated += () => Activated?.Invoke();
            DesktopDirectionHandler.Deactivated += () => Deactivated?.Invoke();
        }

        ~DescktopInputService()
        {
            DesktopDirectionHandler.Activated -= () => Activated?.Invoke();
            DesktopDirectionHandler.Deactivated -= () => Deactivated?.Invoke();
        }

        public event Action Activated;
        public event Action Deactivated;

        public Vector2 Direction => DesktopDirectionHandler.Direction;
        public string ControlPath => AssetPath.DesktopInput;
    }
}