using UnityEngine;

public class PlayerHealthbar : Healthbar
{
    [SerializeField] private Player _player;

    private void Awake() => TakeHealthble(_player);
}
