using System;
using UnityEngine;

public class MobileInputService : IInputService
{
    public Vector2 Direction => DirectionHandler.Direction;

    public event Action Activated;
    public event Action Deactivated;

    public MobileInputService()
    {
        DirectionHandler.Activated += X;
        DirectionHandler.Deactivated += Y;
    }

    private void X()
    {
        Activated?.Invoke();
    }

    private void Y()
    {
        Deactivated?.Invoke();
    }
}