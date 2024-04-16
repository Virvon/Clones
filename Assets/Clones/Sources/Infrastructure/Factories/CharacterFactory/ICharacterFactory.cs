using Clones.GameLogic;
using Clones.Services;
using UnityEngine;

namespace Clones.Infrastructure
{
    public interface ICharacterFactory : IService
    {
        GameObject CreateCharacter(IPartsFactory partsFactory, IItemsCounter itemsCounter);
        GameObject CreateWand(Transform bone);
        GameObject CreateWandModel();
        GameObject CreateCloneModel(Transform parent);
    }
}