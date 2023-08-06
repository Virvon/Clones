using UnityEngine;

namespace Clones.Progression
{
    public class ComplexityCounter : MonoBehaviour
    {
        [SerializeField] private MonoBehaviour _spawnerComplexitybleBehavior;
        [SerializeField] private MonoBehaviour _questComplexitybleBehavior;

        public float Complexity { get; private set; } = 1;

        private IComplexityble _spawnerComplexityble;
        private IComplexityble _questComplexityble;

        private void OnEnable()
        {
            _spawnerComplexityble = (IComplexityble)_spawnerComplexitybleBehavior;
            _questComplexityble = (IComplexityble)_questComplexitybleBehavior;

            _spawnerComplexityble.ComplexityIncreased += OnSpawnerComplexityIncreased;
            _questComplexityble.ComplexityIncreased += OnQuestComplexityIncreased;
        }

        private void OnDisable()
        {
            _spawnerComplexityble.ComplexityIncreased -= OnSpawnerComplexityIncreased;
            _questComplexityble.ComplexityIncreased -= OnQuestComplexityIncreased;
        }

        private void OnValidate()
        {
            if(_spawnerComplexitybleBehavior && _spawnerComplexitybleBehavior is not IComplexityble)
            {
                Debug.LogError(nameof(_spawnerComplexitybleBehavior) + " needs to implement " + nameof(IComplexityble));
                _spawnerComplexitybleBehavior = null;
            }
            else if(_questComplexitybleBehavior && _questComplexitybleBehavior is not IComplexityble)
            {
                Debug.LogError(nameof(_questComplexitybleBehavior) + " needs to implement " + nameof(IComplexityble));
                _questComplexitybleBehavior = null;
            }
        }

        private void OnSpawnerComplexityIncreased()
        {
            Complexity *= 1.2f;
        }

        private void OnQuestComplexityIncreased()
        {
            Complexity *= 1.2f;
        }
    }
}
