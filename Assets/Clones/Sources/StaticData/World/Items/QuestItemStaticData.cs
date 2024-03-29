using Clones.Types;
using System;
using System.Linq;
using UnityEngine;

namespace Clones.StaticData
{
    [CreateAssetMenu(fileName = "New Quest Item", menuName = "Data/Items/Create new quest item", order = 51)]
    public class QuestItemStaticData : ItemStaticData
    {
        public QuestItemType Type;

        [SerializeField] private PreyResourceLocalizedName[] _localizedNames;
        [SerializeField] private string _defaultName;

        public string GetLocalizedName(string isoLanguage)
        {
            PreyResourceLocalizedName preyResourceLocalizedName = _localizedNames.Where(localizedName => localizedName.IsoLanguage == isoLanguage).FirstOrDefault();

            return preyResourceLocalizedName != null ? preyResourceLocalizedName.Name : _defaultName;
        }

        [Serializable]
        private class PreyResourceLocalizedName
        {
            public string IsoLanguage;
            public string Name;
        }
    }
}
