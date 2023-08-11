using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Player), typeof(Freezing))]
public class FreezingDamage : MonoBehaviour
{
    [SerializeField] private float DamagePrecent;
    [SerializeField] private float DamageCooldown;

    private Freezing _freezing;
    private Player _player;
    private Coroutine _damager;
    private float _damage => _player.MaxHealth * (DamagePrecent / 100);

    private void OnEnable()
    {
        _freezing = GetComponent<Freezing>();
        _player = GetComponent<Player>();

        _freezing.FreezingPrecentChanged += OnFreezingPrecentChanged;
    }

    private void OnDisable() => _freezing.FreezingPrecentChanged -= OnFreezingPrecentChanged;
    
    private void OnFreezingPrecentChanged()
    {
        if (_freezing.FreezPrecent < 1)
            return;

        if (_damager != null)
            StopCoroutine(_damager);

        _damager = StartCoroutine(Damager());
    }

    private IEnumerator Damager()
    {
        Debug.Log("startDamage");
        WaitForSeconds delay = new (DamageCooldown);

        while(_freezing.FreezPrecent == 1)
        {
            _player.TakeDamage(_damage);

            yield return delay;
        }
    }
}
