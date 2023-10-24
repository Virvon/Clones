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
            CloneData cloneData;

            if(TryGetSelectedCloneData(out cloneData))
                cloneData.Upgraded -= SelectedCloneUpgraded;

            SelectedClone = type;
            
            if(TryGetSelectedCloneData(out cloneData))
                cloneData.Upgraded += SelectedCloneUpgraded;

            SelectedCloneChanged?.Invoke();
        }

        public bool TryGetSelectedCloneData(out CloneData cloneData)
        {
            cloneData = Clones.Where(data => data.Type == SelectedClone).FirstOrDefault();

            return cloneData != null;
        }

        public CloneData GetSelectedCloneData() =>
            Clones.Where(data => data.Type == SelectedClone).FirstOrDefault();

        public bool TryGetFirstDisuse(out CloneType type)
        {
            CloneData disuseClone = Clones.Where(data => data.IsUsed == false).FirstOrDefault();

            if(disuseClone != null)
            {
                type = disuseClone.Type;
                return true;
            }
            else
            {
                type = CloneType.Undefined;
                return false;
            }
        }
    }
}