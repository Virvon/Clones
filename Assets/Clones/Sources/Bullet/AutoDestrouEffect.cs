﻿using System.Collections;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class AutoDestrouEffect : MonoBehaviour
{
    private void Start() => 
        StartCoroutine(Timer());

    private IEnumerator Timer()
    {
        var particleSystem = GetComponent<ParticleSystem>();

        yield return new WaitWhile(particleSystem.IsAlive);

        Destroy(gameObject);
    }
}