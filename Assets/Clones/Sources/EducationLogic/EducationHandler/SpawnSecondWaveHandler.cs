using Clones.GameLogic;

namespace Clones.EducationLogic
{
    public class SpawnSecondWaveHandler : EducationHandler
    {
        private readonly EducationEnemiesSpawner _enemiesSpawner;
        private readonly IQuestsCreator _questsCreator;

        public SpawnSecondWaveHandler(EducationEnemiesSpawner enemiesSpawner, IQuestsCreator questsCreator)
        {
            _enemiesSpawner = enemiesSpawner;
            _questsCreator = questsCreator;
        }

        public override void Handle() =>
            _questsCreator.Updated += OnQuestUpdated;

        private void OnQuestUpdated(Quest obj)
        {
            _questsCreator.Updated -= OnQuestUpdated;
            _enemiesSpawner.Spawn();
        }
    }
}