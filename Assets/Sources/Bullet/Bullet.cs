using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    [SerializeField] private float _force;
    [SerializeField] private float _lifeTime = 5;

    private Vector3 _direction;

    public event Action<IDamageable> s_Hitted;

    private void Start()
    {
        GetComponent<Rigidbody>().velocity = _direction.normalized * _force;

        StartCoroutine(LifiTimer());
    }

    public void Shoot(Vector3 direction, Action<IDamageable> Hitted = null)
    {
        _direction = direction;
        s_Hitted = Hitted;
    } 

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IDamageable iDamageble))
        {
            s_Hitted?.Invoke(iDamageble);
            Destroy(gameObject);
        }
    }

    private IEnumerator LifiTimer()
    {
        yield return new WaitForSeconds(_lifeTime);

        Destroy(gameObject);
    }
}
