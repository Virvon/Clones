using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CharacterAttack))]
public class AttackShake : MonoBehaviour
{
    [SerializeField] private float _amplitudeGain;
    [SerializeField] private float _frequencyGain;
    [SerializeField] private float _delay;

    private CharacterAttack _attack;
    private Coroutine _shaker;

    private void OnEnable()
    {
        _attack = GetComponent<CharacterAttack>();

        _attack.Attacked += OnAttacked;
    }

    private void OnDisable() => _attack.Attacked -= OnAttacked;

    private void OnAttacked()
    {
        if (_shaker != null)
            StopCoroutine(_shaker);

        _shaker = StartCoroutine(Shaker(_amplitudeGain, _frequencyGain, _delay));
    }

    private IEnumerator Shaker(float amplitudeGain, float frequencyGain, float delay) 
    {
        CameraShake.SetShake(amplitudeGain, frequencyGain);

        yield return new WaitForSeconds(delay);

        CameraShake.SetShake(0, 0);
    }
}
