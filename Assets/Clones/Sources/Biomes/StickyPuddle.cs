using UnityEngine;

namespace Clones.Biomes
{
    [RequireComponent(typeof(Collider))]
    public class StickyPuddle : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            //if (other.TryGetComponent(out Player player))
            //    player.MovementStats.SlowDown(0.5f);
        }

        private void OnTriggerExit(Collider other)
        {
            //if (other.TryGetComponent(out Player player))
            //    player.MovementStats.SlowDown(0);
        }
    }
}
