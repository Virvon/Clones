using UnityEngine;

namespace Clones.Progression
{
    [RequireComponent(typeof(ComplexityCounter), typeof(ComplexityCoefficientCounter))]
    public class Complexity : MonoBehaviour
    {
        private ComplexityCounter _complexityCounter;
        private ComplexityCoefficientCounter _complexityCoefficientCounter;

        public float Value
        {
            get
            {
                float value = _complexityCounter.Complexity * _complexityCoefficientCounter.Coefficient;

                return value > 1 ? value : 1;
            }
        }

        private void Awake()
        {
            _complexityCounter = GetComponent<ComplexityCounter>();
            _complexityCoefficientCounter = GetComponent<ComplexityCoefficientCounter>();
        }
    }
}
