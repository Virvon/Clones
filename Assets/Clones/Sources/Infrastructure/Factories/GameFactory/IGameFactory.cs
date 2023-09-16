using Clones.Services;
using Clones.StaticData;
using System;
using UnityEngine;

namespace Clones.Infrastructure
{
    public interface IGameFactory : IService
    {
        GameObject CreatePlayer();
        void CreateHud();
        void CreateWorldGenerator();
        GameObject CreateTile(BiomeType type, Vector3 position, Quaternion rotation, Transform parent);
        void CreateVirtualCamera();
        void CreatePreyResource(PreyResourceType type, Vector3 position, Quaternion rotation, Transform parent);
        GameObject CreateItem(ItemType type, Vector3 position);
        GameObject CreateQuestView(Quest quest, Transform parent);
    }
}