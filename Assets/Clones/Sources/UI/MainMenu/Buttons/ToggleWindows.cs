using System;
using System.Collections.Generic;
using UnityEngine;

namespace Clones.UI
{
    public class ToggleWindows : MonoBehaviour
    {
        private List<GameObject> _activateGameobjects;
        private List<GameObject> _deactivateGameobjects;

        public event Action WindowToggled;

        public void Init(List<GameObject> activateGameobjects, List<GameObject> deactivateGameobjects)
        {
            _activateGameobjects = activateGameobjects;
            _deactivateGameobjects = deactivateGameobjects;
        }

        public void Toogle()
        {
            SetActiveRange(_activateGameobjects, true);
            SetActiveRange(_deactivateGameobjects, false);

            WindowToggled?.Invoke();
        }

        private void SetActiveRange(List<GameObject> range, bool value)
        {
            foreach (var gameObject in range)
                gameObject.SetActive(value);
        }
    }
}