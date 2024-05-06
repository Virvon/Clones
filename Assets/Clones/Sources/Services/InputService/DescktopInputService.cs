using Clones.Input;
using System;
using UnityEngine;

namespace Clones.Services
{
    public class DescktopInputService : IInputService
    {
        public Vector2 Direction => DesktopDirectionHandler.Direction;
        public string ControlPath => AssetPath.DesktopInput;

        public event Action Activated;
        public event Action Deactivated;

        public DescktopInputService()
        {
            DesktopDirectionHandler.Activated += () => Activated?.Invoke();
            DesktopDirectionHandler.Deactivated += () => Deactivated?.Invoke();
        }
    }
}