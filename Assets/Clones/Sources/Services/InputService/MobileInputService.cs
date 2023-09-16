using System;
using UnityEngine;

namespace Clones.Services
{
    public class MobileInputService : IInputService
    {
        public Vector2 Direction => DirectionHandler.Direction;

        public event Action Activated;
        public event Action Deactivated;

        public MobileInputService()
        {
            DirectionHandler.Activated += () => Activated?.Invoke();
            DirectionHandler.Deactivated += () => Deactivated?.Invoke();
        }
    }
}