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

        public AvailableWands()
        {
            SelectedWand = WandType.Undefined;
            Wands = new();
        }

        public void SetSelectedWand(WandType type)
        {
            SelectedWand = type;
            SelectedWandChanged();
        }

        public WandData GetSelectedWandData() =>
            Wands.Where(data => data.Type == SelectedWand).FirstOrDefault();
    }
}