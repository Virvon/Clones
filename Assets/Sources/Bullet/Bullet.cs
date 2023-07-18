using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    [SerializeField] private float _force;
    [SerializeField] private float _lifeTime = 5;

    private Vector3 _direction;
    private IDamageable _selfDamageable;

    public event Action<IDamageable> s_Hitted;

    private void Start()
    {
        GetComponent<Rigidbody>().velocity = _direction.normalized * _force;

        StartCoroutine(LifiTimer());
    }

    public void Shoot(Vector3 direction, IDamageable selfDamageable, Action<IDamageable> Hitted = null)
    {
        _direction = direction;
        _selfDamageable = selfDamageable;
        s_Hitted = Hitted;
    } 

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IDamageable damageable) && damageable != _selfDamageable)
        {
            s_Hitted?.Invoke(damageable);
            Destroy(gameObject);
        }
    }

    private IEnumerator LifiTimer()
    {
        yield return new WaitForSeconds(_lifeTime);

        Destroy(gameObject);
    }
}
