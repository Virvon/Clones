using UnityEngine;

namespace Clones.Infrastructure
{
    public interface IGameFactory : IService
    {
        GameObject CreatePlayer();
        void CreateHud();
    }
}