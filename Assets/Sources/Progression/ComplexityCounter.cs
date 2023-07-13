using UnityEngine;

namespace Clones.Progression
{
    public class ComplexityCounter : MonoBehaviour
    {
        [SerializeField] private MonoBehaviour _complexibleBehavior;

        private IComplexityble _complexityble;
        public int complexity { get; private set; }

        private void OnEnable()
        {
            _complexityble = (IComplexityble)_complexibleBehavior;
            _complexityble.ComplexityIncreased += OnComplexityIncreased;
        }

        private void OnDisable() => _complexityble.ComplexityIncreased += OnComplexityIncreased;

        private void OnValidate()
        {
            if(_complexibleBehavior && _complexibleBehavior is not IComplexityble)
            {
                Debug.LogError(nameof(_complexibleBehavior) + " needs to implement " + nameof(IComplexityble));
                _complexibleBehavior = null;
            }
        }

        public void OnComplexityIncreased()
        {
            complexity++;
        }
    }
}
