using Clones.Services;
using System;

namespace Clones.GameLogic
{
    public class Complexity
    {
        private readonly IPersistentProgressService _persistentProgress;
        private readonly float _playerSkillCoefficient;

        public Complexity(IPersistentProgressService persistentProgress, int targetPlayTime)
        {
            _persistentProgress = persistentProgress;

            _playerSkillCoefficient = GetPlayerSkillCoefficient(targetPlayTime);
        }

        public float GetComplexity(int currentWave)
        {
            float waveWeight = 0.5f + (float)Math.Pow(currentWave, 1.5) / 20;
            float complexity = _playerSkillCoefficient * waveWeight;

            return complexity;
        }

        private float GetPlayerSkillCoefficient(int targetPlayTime) => 
            _persistentProgress.Progress.AveragePlayTime.GetAveragePlayTime() / targetPlayTime;
    }
}
