using System;
using UnityEngine;

public interface IInputService
{
    Vector2 Direction { get; }

    event Action Activated;
    event Action Deactivated;
}
