using Clones.StaticData;
using System;
using System.Collections.Generic;

namespace Clones.Data
{
    [Serializable]
    public class AvailableClones
    {
        public CardCloneType SelectedClone;
        public Dictionary<CardCloneType, CloneData> Clones;

        public AvailableClones()
        {
            Clones = new();
        }
    }
}