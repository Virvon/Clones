using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderLocation : MonoBehaviour
{
    private const string _plaerTag = "Player";

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == _plaerTag)
            print("����������� ������, ������� ������� UI � � �� ���������� ����� ShowCurrentLocation(string locationName)");
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == _plaerTag)
            print("����������� ������, ������� ������� UI � � �� ���������� ����� ShowCurrentLocation(string locationName) - �������");
    }
}
