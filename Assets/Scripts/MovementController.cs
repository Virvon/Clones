using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    [SerializeField] private string _controlledTag;
    [SerializeField] private float moveSpeed = 5f;
    
    private GameObject _controlledObject;

    private void Awake()
    {
        if (_controlledObject == null)
        {
            if (_controlledTag == "")
                _controlledTag = "Player";

            _controlledObject = GameObject.FindGameObjectWithTag(_controlledTag);

            if (_controlledObject == null)
                throw new NullReferenceException("The MovementController does not have a controlled object!");
        }
    }

    private void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0f, moveVertical);

        movement.Normalize();

        _controlledObject.transform.position += movement * moveSpeed * Time.deltaTime;
    }
}
