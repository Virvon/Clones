using UnityEngine;

public class PlayerHealthbar : Healthbar
{
    [SerializeField] private PlayerHealth Health;

    private void Awake() => TakeHealthble(Health);
}
