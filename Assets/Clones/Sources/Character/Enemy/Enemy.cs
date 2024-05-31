using UnityEngine;

namespace Clones.Character.Enemy
{
    public class Enemy : MonoBehaviour
    {
        public GameObject Target { get; private set; }

        public void Init(GameObject target) =>
            Target = target;
    }
}