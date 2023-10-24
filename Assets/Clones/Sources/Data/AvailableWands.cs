using Clones.StaticData;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Clones.Data
{
    [Serializable]
    public class AvailableWands
    {
        public WandType SelectedWand;
        public List<WandData> Wands;

        public event Action SelectedWandChanged;
        public event Action SelectedWandUpgraded;

        public AvailableWands()
        {
            SelectedWand = WandType.Undefined;
            Wands = new();
        }

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
            Wands.Where(data => data.Type == SelectedWand).FirstOrDefault();
    }
}