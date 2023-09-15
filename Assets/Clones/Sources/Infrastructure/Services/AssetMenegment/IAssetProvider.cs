using UnityEngine;

namespace Clones.Infrastructure
{
    public interface IAssetProvider : IService
    {
        GameObject Instantiate(string path);
    }
}