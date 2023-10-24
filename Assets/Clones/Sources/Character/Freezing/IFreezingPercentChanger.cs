using System;

public interface IFreezingPercentChanger
{
    float FreezingPercent { get; }

    event Action FreezingPercentChanged;
}