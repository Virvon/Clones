﻿using Clones.Character.Bars;
using UnityEngine;

namespace Clones.Character.Player
{
    public class FreezbarReporter : MonoBehaviour
    {
        private Freezbar _freezbar;
        private Freezing _freezing;

        public void Init(Freezbar freezbar) =>
            _freezbar = freezbar;

        private void OnDestroy()
        {
            if (_freezing != null)
                _freezing.FreezPercentChanged -= OnFreezPercentChanged;
        }

        public void SetFreezing(Freezing freezing)
        {
            if (_freezing != null)
                _freezing.FreezPercentChanged -= OnFreezPercentChanged;

            _freezing = freezing;
            _freezing.FreezPercentChanged += OnFreezPercentChanged;
        }

        private void OnFreezPercentChanged(float percent) =>
            _freezbar.SetFreezPercent(percent);
    }
}