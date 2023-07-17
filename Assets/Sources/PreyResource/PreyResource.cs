using System;
using UnityEngine;

public class PreyResource : MonoBehaviour, IDamageable, IDropble 
{
    [SerializeField] private float _health;
    [SerializeField] private PreyResourceType _type;
    public Vector3 Position => transform.position;
    public Vector3 Scale => transform.localScale;
    public float Health => _health;
    public PreyResourceType Type => _type;

    public event Action<IDamageable> Died;

    public void Accept(IDropVisitor visitor) => visitor.Visit(this);

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
