using UnityEngine;

namespace Clones.Progression
{
    public class ComplexityCoefficientCounter : MonoBehaviour
    {
        [SerializeField] private MonoBehaviour _spawnerComplexitybleBehavior;
        [SerializeField] private MonoBehaviour _questComplexitybleBehavior;
        [SerializeField] private Wallet _wallet;

        public float Coefficient { get; private set; }

        private IComplexityble _spawnerComplexityble;
        private IComplexityble _questComplexityble;

        public void OnEnable()
        {
            _spawnerComplexityble = (IComplexityble)_spawnerComplexitybleBehavior;
            _questComplexityble = (IComplexityble)_questComplexitybleBehavior;

            Coefficient = 1;
        }

        public void OnDisable()
        {
            Debug.Log("finish coefficient " + UpdateCoefficient());
        }

        private void OnValidate()
        {
            if (_spawnerComplexitybleBehavior && _spawnerComplexitybleBehavior is not IComplexityble)
            {
                Debug.LogError(nameof(_spawnerComplexitybleBehavior) + " needs to implement " + nameof(IComplexityble));
                _spawnerComplexitybleBehavior = null;
            }
            else if (_questComplexitybleBehavior && _questComplexitybleBehavior is not IComplexityble)
            {
                Debug.LogError(nameof(_questComplexitybleBehavior) + " needs to implement " + nameof(IComplexityble));
                _questComplexitybleBehavior = null;
            }
        }

        private float UpdateCoefficient()
        {
            float waveCoefficient = _spawnerComplexityble.Complexity / 10f;
            float questCoefficient = _questComplexityble.Complexity / 10f;
            float moneyCoefficient = _wallet.Money / 50f;
            float dnaCoefficient = _wallet.DNA / 10f;

            return waveCoefficient * waveCoefficient + questCoefficient * moneyCoefficient * dnaCoefficient;
        }
    }
}
