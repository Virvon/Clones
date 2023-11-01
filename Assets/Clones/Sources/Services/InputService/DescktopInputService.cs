using Clones.Infrastructure;
using System;
using UnityEngine;

namespace Clones.Services
{
    public class DescktopInputService : IInputService
    {
        public Vector2 Direction => throw new NotImplementedException();

        public string ControlPath => AssetPath.DesktopInput;

        public event Action Activated;
        public event Action Deactivated;
    }
}