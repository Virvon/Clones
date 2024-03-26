using Clones.UI;
using UnityEngine;

namespace Clones.EducationLogic
{
    public class SpawnFirstWaveHandler : EducationHandler
    {
        private const float WaitingTime = 3;

        private readonly EducationEnemiesSpawner _spawner;
        private readonly DialogPanel _dialogPanel;
        private readonly Waiter _waiter;

        public SpawnFirstWaveHandler(EducationEnemiesSpawner educationEnemiesSpawner, DialogPanel dialogPanel, Waiter waiter)
        {
            _spawner = educationEnemiesSpawner;
            _dialogPanel = dialogPanel;
            _waiter = waiter;
        }

        public override void Handle()
        {
            _dialogPanel.Open();
            _spawner.Spawn();

            _waiter.Wait(WaitingTime, callback: OnCallback);
        }

        private void OnCallback()
        {
            _dialogPanel.Close();
            Successor.Handle();
        }
    }
}
