using Clones.Data;
using Clones.Services;
using TMPro;
using UnityEngine;

namespace Clones.UI
{
    public class ScoreView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _bestScore;
        [SerializeField] private TMP_Text _lastScore;

        private IPersistentProgressService _persistentProgress;

        public void Init(IPersistentProgressService persistentProgress)
        {
            _persistentProgress = persistentProgress;

            _persistentProgress.Progress.AvailableClones.SelectedCloneChanged += UpdateScores;
        }

        private void OnDestroy() => 
            _persistentProgress.Progress.AvailableClones.SelectedCloneChanged -= UpdateScores;

        private void UpdateScores()
        {
            CloneData selectedCloneData = _persistentProgress.Progress.AvailableClones.GetSelectedCloneData();

            _bestScore.text = selectedCloneData.BestScore.ToString();
            _lastScore.text = selectedCloneData.LastScore.ToString();
        }
    }
}