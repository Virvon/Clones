using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TestController : MonoBehaviour
{
    [SerializeField] private Vector3 _offset;

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
            transform.position += _offset;
    }
}
