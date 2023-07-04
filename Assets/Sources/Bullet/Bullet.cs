using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    [SerializeField] private float _force;
    [SerializeField] private float _damage;
    [SerializeField] private float _lifeTime = 5;

    private Vector3 _direction;

    private void Start()
    {
        GetComponent<Rigidbody>().velocity = _direction.normalized * _force;

        StartCoroutine(LifiTimer());
    }

    public void Init(Vector3 direction) => _direction = direction;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IDamageble iDamageble))
        {
            iDamageble.TakeDamage(_damage);
            Destroy(gameObject);
        }
    }

    private IEnumerator LifiTimer()
    {
        yield return new WaitForSeconds(_lifeTime);

        Destroy(gameObject);
    }
}
