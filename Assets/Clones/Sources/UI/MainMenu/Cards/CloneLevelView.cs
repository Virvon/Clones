using Clones.Data;
using Clones.Services;
using Clones.StaticData;
using System.Linq;
using TMPro;
using UnityEngine;

namespace Clones.UI
{
    public class CloneLevelView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _levelValue;

        private IPersistentProgressService _persistentProgress;
        private CloneData _targetCloneData;

        public void Init(IPersistentProgressService persistentProgress, CloneType targetCloneType)
        {
            _persistentProgress = persistentProgress;
            _targetCloneData = _persistentProgress.Progress.AvailableClones.Clones.First(data => data.Type == targetCloneType);
            _targetCloneData.Upgraded += UpdateLevelValue;

            UpdateLevelValue();
        }

        private void UpdateLevelValue()
        {
            _levelValue.text = _targetCloneData.Level.ToString();
        }
    }
}