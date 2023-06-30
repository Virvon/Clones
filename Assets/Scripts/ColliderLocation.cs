using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderLocation : MonoBehaviour
{
    private const string _plaerTag = "Player";

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == _plaerTag)
            print("Выполняется скрипт, который находит UI и в нём использует метод ShowCurrentLocation(string locationName) => Лес");
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == _plaerTag)
            print("Выполняется скрипт, который находит UI и в нём использует метод ShowCurrentLocation(string locationName) => пустошь");
    }
}
