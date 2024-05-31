using Cinemachine;
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
        private readonly IAssetProvider _assets;

        public EducationFactory(IPartsFactory partsFacotry, IGameStaticDataService gameStaticDataService, IAssetProvider assetProvider)
        {
            _partsFacotry = partsFacotry;
            _gameStaticDataService = gameStaticDataService;
            _assets = assetProvider;
        }

        public DirectionMarker CreateDirectionMarker(Transform player)
        {
            DirectionMarker directionMarker = _assets.Instantiate(AssetPath.DirectionMarker, player).GetComponent<DirectionMarker>();
            directionMarker.Init(player);

            return directionMarker;
        }

        public EducationEnemiesSpawner CreateEnemiesSpawner(GameObject playerObject)
        {
            EducationEnemiesSpawnerStaticData spawnerStaticData = _gameStaticDataService.GetEducationEnemiesSpawner();
            EducationEnemiesSpawner spawner = Object.Instantiate(spawnerStaticData.Prefab);

            spawner.Init(spawnerStaticData.WaveInfos, _partsFacotry, playerObject);

            return spawner;
        }

        public EducationPreyResourcesSpawner CreatePreyResourcesSpawner()
        {
            EducationPreyResourcesSpawnerStaticData spawnerStaticData = _gameStaticDataService.GetEducationPreyResourcesSpawner();
            EducationPreyResourcesSpawner spawner = Object.Instantiate(spawnerStaticData.Prefab);

            spawner.Init(_partsFacotry, spawnerStaticData.PreyResourceCells);

            return spawner;
        }

        public CinemachineVirtualCamera CreateVirtualCamera() =>
            _assets.Instantiate(AssetPath.EducationVirtualCamera).GetComponent<CinemachineVirtualCamera>();

        public Transform CreateFirstDirectionMarkerTarget() => 
            _assets.Instantiate(AssetPath.FirstDirectionMarkerTarget).transform;

        public Transform CreateSecondDirectionMarkerTarget() =>
            _assets.Instantiate(AssetPath.SecondDirectionMarketTarget).transform;
    }
}