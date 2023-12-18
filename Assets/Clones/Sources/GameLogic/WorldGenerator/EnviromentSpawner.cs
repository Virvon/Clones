using Clones.Infrastructure;
using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Clones.GameLogic
{
    public abstract class EnviromentSpawner<TEnviroment> : MonoBehaviour where TEnviroment : Enum
    {
        [SerializeField] private Transform[] _spawnPoints;

        private IPartsFactory _partsFactory;
        private TEnviroment[] _enviromentsTypes;
        private int _percentageFilled;

        public void Init(IPartsFactory partsFactory, TEnviroment[] enviromentsTypes, int percentageFilled)
        {
            _partsFactory = partsFactory;
            _enviromentsTypes = enviromentsTypes;
            _percentageFilled = percentageFilled;

            if (_enviromentsTypes.Length > 0)
                Spawn();
        }
        protected abstract void CreateEnviromentPart(IPartsFactory partsFactory, TEnviroment enviromentType, Vector3 position);

        private void Spawn()
        {
            foreach(Transform point in _spawnPoints)
            {
                if (CanSpawned())
                    CreateEnviromentPart(_partsFactory, _enviromentsTypes[Random.Range(0, _enviromentsTypes.Length)], point.transform.position);
            }
        }

        private bool CanSpawned() =>
            Random.Range(0, 101) <= _percentageFilled;

    }
}