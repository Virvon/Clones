using System;
using System.Collections;
using UnityEngine;

public class Freezing : MonoBehaviour
{
    public float FreezPrecent { get; private set; }

    private Coroutine _coroutine;

    public event Action FreezingPrecentChanged;

    public void Freez(float targetFreezPrecent, float speed)
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        if (targetFreezPrecent < FreezPrecent)
            speed = speed * FreezPrecent;
        else
            speed = speed - speed * FreezPrecent;

        _coroutine = StartCoroutine(FreezController(targetFreezPrecent, speed));
    }

    private IEnumerator FreezController(float targetFreezPrecent, float freezingSpeed)
    {
        float time = 0;
        float startFreezPrecent = FreezPrecent;

        while(FreezPrecent != targetFreezPrecent)
        {
            time += Time.deltaTime;

            FreezPrecent = Mathf.Lerp(startFreezPrecent, targetFreezPrecent, time / freezingSpeed);
            
            FreezingPrecentChanged?.Invoke();

            yield return null;
        }
    }
}
