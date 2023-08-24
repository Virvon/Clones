using UnityEngine;
using Clones.SaveUtility;
using System.Collections.Generic;
using System;

namespace Clones.Progression
{
    public class ComplexityCoefficientCounter : MonoBehaviour
    {
        [SerializeField] private MonoBehaviour _spawnerComplexitybleBehavior;
        [SerializeField] private MonoBehaviour _questComplexitybleBehavior;
        [SerializeField] private Wallet _wallet;
        [SerializeField] private float _savedCoefficientsCount;
        [SerializeField] private Player _player;

        public float Coefficient { get; private set; }

        private IComplexityble _spawnerComplexityble;
        private IComplexityble _questComplexityble;

        private const string SaveKey = "ComplexityCoefficientCounter";
        private const char Separator = '*';

        public void OnEnable()
        {
            _spawnerComplexityble = (IComplexityble)_spawnerComplexitybleBehavior;
            _questComplexityble = (IComplexityble)_questComplexitybleBehavior;

            Coefficient = GetCoefficient();

            _player.Died += OnPlayerDied;
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

        private void OnPlayerDied(IDamageable damageable)
        {
            UpdateCoefficient();

            _player.Died -= OnPlayerDied;
        }

        private void UpdateCoefficient()
        {
            float waveCoefficient = _spawnerComplexityble.QuestLevel / 10f;
            float questCoefficient = _questComplexityble.QuestLevel / 10f;
            float moneyCoefficient = _wallet.Money / 50f;
            float dnaCoefficient = _wallet.DNA / 10f;

            float currentCoefficient = (waveCoefficient + waveCoefficient + questCoefficient + moneyCoefficient + dnaCoefficient) / 4;

            Queue<float> coefficients = LoadCoefficients();

            if (coefficients == null)
                coefficients = new();

            if (coefficients.Count >= _savedCoefficientsCount)
                coefficients.Dequeue();

            coefficients.Enqueue(currentCoefficient);

            SaveCoefficients(coefficients);
        }

        private float GetCoefficient()
        {
            Queue<float> coefficients = LoadCoefficients();

            if (coefficients == null || coefficients.Count == 0)
                return 1;

            float coefficientsSum = 0;

            foreach (var coefficient in coefficients)
                coefficientsSum += coefficient;

            Debug.Log("count " + coefficients.Count + " value " + coefficientsSum / coefficients.Count);

            return coefficientsSum / coefficients.Count;
        }

        private void SaveCoefficients(Queue<float> coefficients)
        {
            string stringCoefficients = FloatsQueueToString(coefficients);

            SaveSystem.Save(SaveKey, new ComplexityProfile { Coefficients = stringCoefficients });
        }

        private Queue<float> LoadCoefficients()
        {
            string coefficients = SaveSystem.Load<ComplexityProfile>(SaveKey).Coefficients;

            return StringToFloatsQueue(coefficients);
        }

        private string FloatsQueueToString(Queue<float> values)
        {
            if (values == null || values.Count == 0)
                return null;

            string result = "";

            foreach (var value in values)
            {
                result += value;
                result += Separator;
            }

            result = result.Remove(result.Length - 1);

            return result;
        }

        private Queue<float> StringToFloatsQueue(string value)
        {
            if (value == null || value.Length == 0)
                return null;

            string[] loadedStrings = value.Split(Separator);

            Queue<float> result = new Queue<float>();

            foreach(var loadedString in loadedStrings)
                result.Enqueue(float.Parse(loadedString));

            return result;
        }
    }
}