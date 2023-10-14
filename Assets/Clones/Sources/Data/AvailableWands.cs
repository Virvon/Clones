using Clones.StaticData;
using System;
using System.Collections.Generic;

namespace Clones.Data
{
    [Serializable]
    public class AvailableWands
    {
        public WandType SelectedWand;
        public Dictionary<WandType, WandData> Wands;

        public AvailableWands()
        {
            Wands = new();
        }
    }
}