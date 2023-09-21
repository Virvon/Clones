using Clones.GameLogic;
using Clones.Services;
using UnityEngine;

namespace Clones.Infrastructure
{
    public interface IGameFacotry : IService
    {
        GameObject CreatePlayer(IItemsCounter itemsCounter);
        WorldGenerator CreateWorldGenerator();
        void CreateVirtualCamera();
        EnemiesSpawner CreateEnemiesSpawner(ICurrentBiome currentBiome);
    }
}