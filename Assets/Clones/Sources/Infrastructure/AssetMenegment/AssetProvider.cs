using UnityEngine;

namespace Clones.Infrastructure
{
    public class AssetProvider : IAssetProvider
    {
        public GameObject Instantiate(string path)
        {
            var prefab = Resources.Load<GameObject>(path);

            return Object.Instantiate(prefab);
        }
    }
}