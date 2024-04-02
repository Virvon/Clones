using Cinemachine;
using Clones.GameLogic;
using Clones.Services;
using UnityEngine;

namespace Clones.Infrastructure
{
    public interface IGameFacotry : IService
    {
        WorldGenerator CreateWorldGenerator(GameObject player);
        CinemachineVirtualCamera CreateVirtualCamera(GameObject player);
        EnemiesSpawner CreateEnemiesSpawner(ICurrentBiome currentBiome, Complexity complexity, GameObject player);
        GameMusic CreateMusic(ICurrentBiome currentBiome);
        void CreateFreezingScreen(GameObject player);
    }
}