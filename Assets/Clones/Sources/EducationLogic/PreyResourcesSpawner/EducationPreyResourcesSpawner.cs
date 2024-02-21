using Clones.Infrastructure;
using UnityEngine;

namespace Clones.EducationLogic
{
    public class EducationPreyResourcesSpawner : MonoBehaviour
    {
        private readonly int[] _rotations = new int[] { 0, 90, 180, 270 };

        private IPartsFactory _partsFactory;
        private PreyResourceCell[] _preyResourceCells;

        public void Init(IPartsFactory partsFactory, PreyResourceCell[] preyResourceCells)
        {
            _partsFactory = partsFactory;
            _preyResourceCells = preyResourceCells;
        }

        public void Create()
        {
            foreach(var cell in _preyResourceCells)
                _partsFactory.CreatePreyResource(cell.Type, cell.Position, GetRotation(), transform);
        }

        private Quaternion GetRotation() =>
            Quaternion.Euler(0, _rotations[Random.Range(0, _rotations.Length)], 0);
    }
}
