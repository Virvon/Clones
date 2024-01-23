using Cinemachine;
using Clones.GameLogic;
using Clones.Services;
using UnityEngine;

namespace Clones.Infrastructure
{
    public interface IGameFacotry : IService
    {
        GameObject CreatePlayer(IPartsFactory partsFactory, IItemsCounter itemsCounter);
        WorldGenerator CreateWorldGenerator();
        CinemachineVirtualCamera CreateVirtualCamera();
        EnemiesSpawner CreateEnemiesSpawner(ICurrentBiome currentBiome, Complexity complexity);
        void CreateMusic(ICurrentBiome currentBiome);
    }
}