using Clones.Services;
using UnityEngine;

namespace Clones.Infrastructure
{
    public interface IAssetProvider : IService
    {
        GameObject Instantiate(string path);
        GameObject Instantiate(string path, Transform parent);
    }
}