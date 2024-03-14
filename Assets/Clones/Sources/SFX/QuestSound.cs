using Clones.GameLogic;
using UnityEngine;

namespace Clones.SFX
{
    public class QuestSound : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource;

        private IQuestsCreator _questsCreator;

        public void Init(IQuestsCreator questsCreator)
        {
            _questsCreator = questsCreator;

            _questsCreator.Completed += OnCompleted;
        }

        private void OnDestroy() => 
            _questsCreator.Completed -= OnCompleted;

        private void OnCompleted() => 
            _audioSource.Play();
    }
}
