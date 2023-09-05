using System;
using System.Collections;
using UnityEngine;

public class Freezing : MonoBehaviour
{
    public float FreezingPercent { get; private set; }

    private Coroutine _coroutine;

    public event Action FreezingPercentChanged;

    public void Freez(float targetFreezPrecent, float speed)
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        if (targetFreezPrecent < FreezingPercent)
            speed = speed * FreezingPercent;
        else
            speed = speed - speed * FreezingPercent;

        _coroutine = StartCoroutine(FreezController(targetFreezPrecent, speed));
    }

    private IEnumerator FreezController(float targetFreezPrecent, float freezingSpeed)
    {
        float time = 0;
        float startFreezPrecent = FreezingPercent;

        while(FreezingPercent != targetFreezPrecent)
        {
            time += Time.deltaTime;

            FreezingPercent = (float)Math.Round(Mathf.Lerp(startFreezPrecent, targetFreezPrecent, time / freezingSpeed), 3);
            
            FreezingPercentChanged?.Invoke();

            yield return null;
        }
    }
}
