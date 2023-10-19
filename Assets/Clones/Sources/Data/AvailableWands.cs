using Clones.StaticData;
using System;
using System.Collections.Generic;

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
    }
}