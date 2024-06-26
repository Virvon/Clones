﻿using Clones.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Clones.Data
{
    [Serializable]
    public class AvailableWands
    {
        public WandType SelectedWand;
        public List<WandData> Wands;

        public AvailableWands()
        {
            SelectedWand = WandType.Undefined;
            Wands = new();
        }

        public event Action SelectedWandChanged;
        public event Action SelectedWandUpgraded;

        public void SetSelectedWand(WandType type)
        {
            WandData selectedWandData = GetSelectedWandData();

            if(selectedWandData != null)
                selectedWandData.Upgraded -= SelectedWandUpgraded;

            SelectedWand = type;

            GetSelectedWandData().Upgraded += SelectedWandUpgraded;

            SelectedWandChanged?.Invoke();
        }

        public WandData GetSelectedWandData() => 
            Wands.Find(data => data.Type == SelectedWand);
    }
}