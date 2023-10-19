using Clones.StaticData;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Clones.Data
{
    [Serializable]
    public class AvailableClones
    {
        public CloneType SelectedClone;
        public List<CloneData> Clones;

        public event Action SelectedCloneChanged;

        public AvailableClones()
        {
            SelectedClone = CloneType.Undefined;
            Clones = new();
        }

        public void SetSelectedClone(CloneType type)
        {
            SelectedClone = type;
            SelectedCloneChanged?.Invoke();
        }

        public CloneData GetSelectedCloneData() =>
            Clones.Where(data => data.Type == SelectedClone).FirstOrDefault();
    }
}