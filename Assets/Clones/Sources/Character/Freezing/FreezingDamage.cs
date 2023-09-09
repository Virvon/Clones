using System.Collections;
using UnityEngine;

[RequireComponent(typeof(PlayerHealth), typeof(Freezing))]
public class FreezingDamage : MonoBehaviour
{
    [SerializeField] private float DamagePrecent;
    [SerializeField] private float DamageCooldown;

    private Freezing _freezing;
    private PlayerHealth _playerHealth;
    private Coroutine _damager;
    private float _damage => _playerHealth.MaxHealth * (DamagePrecent / 100);

    private void OnEnable()
    {
        _freezing = GetComponent<Freezing>();
        _playerHealth = GetComponent<PlayerHealth>();

        _freezing.FreezingPercentChanged += OnFreezingPrecentChanged;
    }

    private void OnDisable() => _freezing.FreezingPercentChanged -= OnFreezingPrecentChanged;
    
    private void OnFreezingPrecentChanged()
    {
        if (_freezing.FreezingPercent < 1)
            return;

        if (_damager != null)
            StopCoroutine(_damager);

        _damager = StartCoroutine(Damager());
    }

    private IEnumerator Damager()
    {
        WaitForSeconds delay = new (DamageCooldown);

        while(_freezing.FreezingPercent == 1)
        {
            _playerHealth.TakeDamage(_damage);

            yield return delay;
        }
    }
}
