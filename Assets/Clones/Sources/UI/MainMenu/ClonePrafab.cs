using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClonePrafab : MonoBehaviour
{
    [SerializeField] private Transform _wandPrefabPlace;

    public Transform WandPrefabPlace => _wandPrefabPlace;
}
