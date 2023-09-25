using Clones.Services;
using Clones.StaticData;
using UnityEngine;

namespace Clones.Infrastructure
{
    public interface IPartsFactory : IService
    {
        void CreatePreyResource(PreyResourceType type, Vector3 position, Quaternion rotation, Transform parent);
        GameObject CreateItem(CurrencyItemType type, Vector3 position);
        GameObject CreateItem(QuestItemType type, Vector3 position);
        void CreateBoost(BoostType type, Vector3 position, Quaternion rotation, Transform parent);
        void CreateEnemy(EnemyType type, Vector3 position, Quaternion rotation, Transform parent, out float weight, GameObject playerObject);
        GameObject CreateTile(BiomeType type, Vector3 position, Quaternion rotation, Transform parent);
    }
}