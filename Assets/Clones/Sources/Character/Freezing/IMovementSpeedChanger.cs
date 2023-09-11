using System;

public interface IMovementSpeedChanger
{
    float MovementSpeed { get; }

    event Action MovementSpeedChanged;
}