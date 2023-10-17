using Clones.StaticData;
using System;
using System.Collections.Generic;

namespace Clones.Data
{
    [Serializable]
    public class AvailableClones
    {
        public CloneType SelectedClone;
        public List<CloneData> Clones;

        public AvailableClones()
        {
            Clones = new();
        }
    }
}