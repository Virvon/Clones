using Clones.Services;
using System;
using UnityEngine;

namespace Clones.GameLogic
{
    public class Complexity
    {
        private readonly IPersistentProgressService _persistentProgress;
        private readonly float _playerSkillCoefficient;
        private readonly int _cloneLevel;

        public Complexity(IPersistentProgressService persistentProgress, int targetPlayTime, int cloneLevel)
        {
            _persistentProgress = persistentProgress;
            _cloneLevel = cloneLevel;

            _playerSkillCoefficient = GetPlayerSkillCoefficient(targetPlayTime);
        }

        public float GetComplexity(int currentWave)
        {
            float waveWeight = 0.95f + (float)Math.Pow(currentWave, 1.5) / 20;
            float complexity = _playerSkillCoefficient * waveWeight * _cloneLevel;

            complexity = complexity >= 1 ? complexity : 1;

            return complexity;
        }

        private float GetPlayerSkillCoefficient(int targetPlayTime)
        {
            if (_persistentProgress.Progress.AveragePlayTime.TryGetAveragePlayTime(out int averagePlayTime) && averagePlayTime >= 1)
            {
                return averagePlayTime / (float)targetPlayTime;
            }
            else
                return 1;
        }
    }
}
