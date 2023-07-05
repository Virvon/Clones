using System;
using UnityEngine;

public class MiningFacility : MonoBehaviour, IDamageble, IRewardle 
{
    [SerializeField] private float _health;
    public Vector3 Position => transform.position;
    public Vector3 Scale => transform.localScale;
    public float Health => _health;

    public event Action<IDamageble> Died;

    public void Accept(IVisitor visitor) => visitor.Visit(this);

    public void TakeDamage(float damage)
    {
        _health -= damage;

        if(_health <= 0)
        {
            Died?.Invoke(this);
            Destroy(gameObject);
        }
    }
}
