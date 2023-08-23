using System;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

[RequireComponent(typeof(NavMeshAgent))]
public class NavMeshRandomDistance : MonoBehaviour
{
    [SerializeField] private float _maxDistance;
    [SerializeField] private float _minDistance;

    private void Awake() => GetComponent<NavMeshAgent>().stoppingDistance = (float)Math.Round(Random.Range(_minDistance, _maxDistance + 1), 2);
}
