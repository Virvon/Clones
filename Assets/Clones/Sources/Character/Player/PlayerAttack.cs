using UnityEngine;

public class PlayerAttack : MonoBehaviour, IAttackble
{
    [SerializeField] private int _damage;
    [SerializeField] private float _attackSpeed;

    public int Damage => _damage;

    public float AttackSpeed => _attackSpeed;
}
