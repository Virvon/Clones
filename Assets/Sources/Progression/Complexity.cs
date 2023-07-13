using UnityEngine;

namespace Clones.Progression
{
    [RequireComponent(typeof(ComplexityCounter), typeof(ComplexityCoefficientCounter))]
    public class Complexity : MonoBehaviour
    {
        private ComplexityCounter _complexityCounter;
        private ComplexityCoefficientCounter _complexityCoefficientCounter;

        public float ResultComplexity => _complexityCounter.Complexity * _complexityCoefficientCounter.Coefficient;

        private void Awake()
        {
            _complexityCounter = GetComponent<ComplexityCounter>();
            _complexityCoefficientCounter = GetComponent<ComplexityCoefficientCounter>();
        }
    }
}
