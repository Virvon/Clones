using UnityEngine;
using UnityEngine.AI;

namespace Clones.GameLogic
{
    public class NavMeshZone : MonoBehaviour
    {
        private NavMeshSurface _navMeshSurface;
        private Transform _player;
        private float _delta;

        public void Init(NavMeshSurface navMehsSurface, Transform player)
        {
            _navMeshSurface = navMehsSurface;
            _player = player;

            _delta = GetComponent<BoxCollider>().size.x / 4;

            _navMeshSurface.BuildNavMesh();
        }

        private void Update()
        {
            if (_player == null)
                return;

            if (Vector3.Distance(transform.position, _player.transform.position) > _delta)
                MoveZone();
        }

        private void MoveZone()
        {
            Vector3 positon = _player.transform.position;
            positon.y = transform.position.y;

            transform.position = positon;

            _navMeshSurface.UpdateNavMesh(_navMeshSurface.navMeshData);
        }
    }
}