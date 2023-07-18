using UnityEngine;

public class PlayerHealthbar : Healthbar
{
    [SerializeField] private Player _player;

    protected override IHealthble Healthble => _player;

    protected override void Start() => base.Start();
}
