using Clones.Services;

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

        private float GetPlayerSkillCoefficient(int targetPlayTime) => 
            _persistentProgress.Progress.AveragePlayTime.GetAveragePlayTime() / targetPlayTime;
    }
}
