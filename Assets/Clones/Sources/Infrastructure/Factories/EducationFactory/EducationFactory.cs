using Clones.EducationLogic;
using Clones.Services;
using Clones.StaticData;
using UnityEngine;

namespace Clones.Infrastructure
{
    public class EducationFactory : IEducationFactory
    {
        private readonly IPartsFactory _partsFacotry;
        private readonly IGameStaticDataService _gameStaticDataService;

        public EducationFactory(IPartsFactory partsFacotry, IGameStaticDataService gameStaticDataService)
        {
            _partsFacotry = partsFacotry;
            _gameStaticDataService = gameStaticDataService;
        }

        public EducationPreyResourcesSpawner CreateSpawner()
        {
            EducationPreyResourcesSpawnerStaticData spawnerStaticData = _gameStaticDataService.GetEducationPreyResourcesSpawner();
            EducationPreyResourcesSpawner spawner = Object.Instantiate(spawnerStaticData.Prefab);

            spawner.Init(_partsFacotry, spawnerStaticData.PreyResourceCells);

            return spawner;
        }
    }
}