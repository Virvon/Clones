using Clones.Services;
using Clones.StaticData;
using UnityEngine;

namespace Clones.Infrastructure
{
    public interface IMainMenuFactory : IService
    {
        GameObject CreateMainMenu();
        void CreateCardClone(CardCloneType type);
    }
}