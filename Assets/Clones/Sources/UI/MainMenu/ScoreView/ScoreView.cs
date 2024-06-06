using Clones.Auxiliary;
using Clones.Data;
using Clones.Services;
using TMPro;
using UnityEngine;

namespace Clones.UI
{
    public class ScoreView : MonoBehaviour, IProgressReader
    {
        [SerializeField] private TMP_Text _bestScore;
        [SerializeField] private TMP_Text _lastScore;

        private IPersistentProgressService _persistentProgress;

        private void OnDestroy() => 
           Unsubscribe();

        public void Init(IPersistentProgressService persistentProgress)
        {
            _persistentProgress = persistentProgress;

            Subscribe();
        }

        public void UpdateProgress()
        {
            Unsubscribe();
            Subscribe();
        }

        private void UpdateScores()
        {
            CloneData selectedCloneData = _persistentProgress.Progress.AvailableClones.GetSelectedCloneData();

            _bestScore.text = NumberFormatter.DivideIntegerOnDigits(selectedCloneData.BestScore);
            _lastScore.text = NumberFormatter.DivideIntegerOnDigits(selectedCloneData.LastScore);
        }

        private void Subscribe() => 
            _persistentProgress.Progress.AvailableClones.SelectedCloneChanged += UpdateScores;

        private void Unsubscribe() => 
            _persistentProgress.Progress.AvailableClones.SelectedCloneChanged -= UpdateScores;
    }
}