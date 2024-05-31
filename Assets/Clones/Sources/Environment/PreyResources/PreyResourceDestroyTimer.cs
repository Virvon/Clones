using Clones.DestroySystem;
using UnityEngine;

namespace Clones.Environment
{
    public class PreyResourceDestroyTimer : DestroyTimer
    {
        [SerializeField] private Collider _collider;
        [SerializeField] private GameObject _model;

        public override void Destroy()
        {
            _collider.enabled = false;
            _model.SetActive(false);
            base.Destroy();
        }
    }
}