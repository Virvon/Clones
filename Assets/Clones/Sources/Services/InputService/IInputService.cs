using System;
using UnityEngine;

namespace Clones.Services
{
    public interface IInputService : IService
    {
        Vector2 Direction { get; }
        string ControlPath { get; }

        event Action Activated;
        event Action Deactivated;
    }

}