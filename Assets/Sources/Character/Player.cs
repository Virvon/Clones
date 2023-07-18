using UnityEngine;

public class Player : Clone
{
    [SerializeField] private float _attackRadius;
    [SerializeField] private float _lookRotationSpeed;
    [SerializeField] private CharacterAttack _characterAttack;

    public float AttackRadius => _attackRadius;
    public float LookRotationSpeed => _lookRotationSpeed;
    public CharacterAttack CharacterAttack => _characterAttack;
}
