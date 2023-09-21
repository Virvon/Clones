using Clones.Biomes;
using Clones.GameLogic;
using Clones.Services;
using Clones.StaticData;
using System;
using UnityEngine;

namespace Clones.Infrastructure
{
    public interface IGameFactory : IService
    {
        event Action<IDroppable> DroppableCreated;

        GameObject CreatePlayer(IItemsCounter itemsCounter);
        GameObject CreateHud(IQuestsCreator questsCreator);
        WorldGenerator CreateWorldGenerator();
        GameObject CreateTile(BiomeType type, Vector3 position, Quaternion rotation, Transform parent);
        void CreateVirtualCamera();
        void CreatePreyResource(PreyResourceType type, Vector3 position, Quaternion rotation, Transform parent);
        GameObject CreateItem(CurrencyItemType type, Vector3 position);
        GameObject CreateItem(QuestItemType type, Vector3 position);
        GameObject CreateQuestView(Quest quest, Transform parent);
        void CreateEnemy(EnemyType type, Vector3 position, Quaternion rotation, out float weight);
    }
}