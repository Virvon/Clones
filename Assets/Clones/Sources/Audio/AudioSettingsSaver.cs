using Clones.Services;
using UnityEngine;

namespace Clones.Audio
{
    public class AudioSettingsSaver : MonoBehaviour
    {
        private ISaveLoadService _saveLoadService;

        public void Init(ISaveLoadService saveLoagService) =>
            _saveLoadService = saveLoagService;

        private void OnDisable() => 
            _saveLoadService.SaveProgress();
    }
}