using Clones.Data;
using Clones.Services;
using Clones.Types;
using System.Linq;
using TMPro;
using UnityEngine;

namespace Clones.UI
{
    public class WandLevelView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _levelValue;

        private IPersistentProgressService _persistentProgress;
        private WandData _targetWandData;

        public void Init(IPersistentProgressService persistentProgress, WandType targetWandType)
        {
            _persistentProgress = persistentProgress;
            _targetWandData = _persistentProgress.Progress.AvailableWands.Wands.First(data => data.Type == targetWandType);
            _targetWandData.Upgraded += UpdateLevelValue;

            UpdateLevelValue();
        }

        private void UpdateLevelValue() =>
            _levelValue.text = _targetWandData.Level.ToString();
    }
}