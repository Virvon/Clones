using Clones.Infrastructure;
using Clones.StaticData;
using UnityEngine;

public class PreyResourcesSpawner : MonoBehaviour
{
    private IGameFactory _gameFactory;
    private Point[] _spawnPoints;
    private PreyResourceType[] _preyResourcesTemplates;
    private int _percentageFilled;
   

    public void Init(IGameFactory gameFactory, PreyResourceType[] preyResourcesTemplates, int percentageFilled)
    {
        _gameFactory = gameFactory;
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

                _gameFactory.CreatePreyResource(type, _spawnPoints[i].transform.position, Quaternion.identity, transform);
            }
        }
    }

    private bool CanSpawned() => 
        Random.Range(0, 101) <= _percentageFilled;
}