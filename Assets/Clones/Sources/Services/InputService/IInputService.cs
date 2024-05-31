using System;
using UnityEngine;

namespace Clones.Services
{
    public interface IInputService : IService
    {
        event Action Activated;
        event Action Deactivated;

        Vector2 Direction { get; }
        string ControlPath { get; }
    }
}