using Clones.GameLogic;
using Clones.Services;
using Clones.StaticData;
using System;
using UnityEngine;

namespace Clones.Infrastructure
{
    public interface IPartsFactory : IService
    {
        event Action<IDroppable> DroppableCreated;

        void CreatePreyResource(PreyResourceType type, Vector3 position, Quaternion rotation, Transform parent);
        GameObject CreateItem(CurrencyItemType type, Vector3 position);
        GameObject CreateItem(QuestItemType type, Vector3 position);
        void CreateEnemy(EnemyType type, Vector3 position, Quaternion rotation, Transform parent, out float weight, GameObject playerObject);
        GameObject CreateTile(BiomeType type, Vector3 position, Quaternion rotation, Transform parent);
    }
}