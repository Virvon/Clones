using Clones.Infrastructure;
using Clones.StaticData;
using UnityEngine;

public class PreyResourcesSpawner : MonoBehaviour
{
    private IPartsFactory _partsFactory;
    private Point[] _spawnPoints;
    private PreyResourceType[] _preyResourcesTemplates;
    private int _percentageFilled;

    public void Init(IPartsFactory partsFactory, PreyResourceType[] preyResourcesTemplates, int percentageFilled)
    {
        _partsFactory = partsFactory;
        _preyResourcesTemplates = preyResourcesTemplates;
        _percentageFilled = percentageFilled;

        _spawnPoints = GetComponentsInChildren<Point>();

        Spawn();
    }

    private void Spawn()
    {
        for(var i = 0; i < _spawnPoints.Length; i++)
        {
            if (CanSpawned())
            {
                PreyResourceType type = _preyResourcesTemplates[Random.Range(0, _preyResourcesTemplates.Length)];

                _partsFactory.CreatePreyResource(type, _spawnPoints[i].transform.position, Quaternion.identity, transform);
            }
        }
    }

    private bool CanSpawned() => 
        Random.Range(0, 101) <= _percentageFilled;
}