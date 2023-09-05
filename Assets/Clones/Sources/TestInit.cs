using Agava.YandexGames;
using System;
using System.Collections;
using UnityEngine;

public class TestInit : MonoBehaviour
{
    [SerializeField] private SceneTransitionManager _x;


    private IEnumerator Start()
    {
#if !UNITY_WEBGL || UNITY_EDITOR
        _x.TryTransit();
        yield break;
#endif

        yield return YandexGamesSdk.Initialize();

        if (YandexGamesSdk.IsInitialized == false)
            throw new ArgumentNullException(nameof(YandexGamesSdk), "Yandex SDK didn't initialized correctly");

        YandexGamesSdk.CallbackLogging = true;

        _x.TryTransit();
    }
}
