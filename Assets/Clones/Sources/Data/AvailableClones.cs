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
        public event Action SelectedCloneUpgraded;

        public AvailableClones()
        {
            SelectedClone = CloneType.Undefined;
            Clones = new();
        }

        public void SetSelectedClone(CloneType type)
        {
            CloneData selectedCloneData = GetSelectedCloneData();

            if(selectedCloneData != null)
                selectedCloneData.Upgraded -= SelectedCloneUpgraded;

            SelectedClone = type;

            GetSelectedCloneData().Upgraded += SelectedCloneUpgraded;

            SelectedCloneChanged?.Invoke();
        }

        public CloneData GetSelectedCloneData() =>
            Clones.Where(data => data.Type == SelectedClone).FirstOrDefault();
    }
}