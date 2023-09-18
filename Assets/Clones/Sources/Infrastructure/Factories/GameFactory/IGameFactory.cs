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
        void CreateHud(IQuestsCreator questsCreator);
        void CreateWorldGenerator();
        GameObject CreateTile(BiomeType type, Vector3 position, Quaternion rotation, Transform parent);
        void CreateVirtualCamera();
        void CreatePreyResource(PreyResourceType type, Vector3 position, Quaternion rotation, Transform parent);
        GameObject CreateItem(CurrencyItemType type, Vector3 position);
        GameObject CreateItem(QuestItemType type, Vector3 position);
        GameObject CreateQuestView(Quest quest, Transform parent);
    }
}