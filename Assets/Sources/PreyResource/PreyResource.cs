using Clones.Data;
using System;
using UnityEngine;

public class PreyResource : MonoBehaviour, IDamageable, IDropble 
{
    [SerializeField] private float _health;

    public PreyResourceData Data { get; private set; }
    public bool IsAlive => _isAlive;

    private bool _isAlive;

    public event Action<IDamageable> Died;


    public void Init(PreyResourceData data)
    {
        _isAlive = true;

        if(Data == null)
            Data = data;
    }

    public void Accept(IDropVisitor visitor) => visitor.Visit(this);

    public void TakeDamage(float damage)
    {
        _health -= damage;

        if(_health <= 0)
        {
            _isAlive = false;
            Died?.Invoke(this);
            Destroy(gameObject);
        }
    }
}
