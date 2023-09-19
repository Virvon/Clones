using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardClone : Card
{
    [Space]
    [Header("Характеристики клона")]
    [SerializeField] private float _helath;
    [SerializeField] private float _damage;

    private GameObject _wandPrefab;

    public float Helath => _helath;
    public float Damage => _damage;

    public CardClone ReturnCard() => this;

    public void SetWand(GameObject wandPrefab, GameObject clonePrefab)
    {
        Destroy(_wandPrefab?.gameObject);
        _wandPrefab = Instantiate(wandPrefab, clonePrefab.GetComponent<ClonePrafab>().WandPrefabPlace);
    }
}
