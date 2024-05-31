using UnityEngine;

namespace Clones.Character
{
    [RequireComponent(typeof(Rigidbody))]
    public class Knockback : MonoBehaviour
    {
        private Rigidbody _rigidbody;

        private void Start() =>
            _rigidbody = GetComponent<Rigidbody>();

        public void Knockbaked(Vector3 force) =>
            _rigidbody.AddForce(force);
    }
}