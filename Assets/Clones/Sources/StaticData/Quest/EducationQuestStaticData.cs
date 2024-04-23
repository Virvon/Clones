using Clones.GameLogic;
using Clones.Services;
using Clones.Types;
using System;
using UnityEngine;

namespace Clones.StaticData
{
    [CreateAssetMenu(fileName = "New Education Quest", menuName = "Data/Quests/Create new education quest", order = 51)]
    public class EducationQuestStaticData : ScriptableObject
    {
        public int Reward;
        public int RewardIncrease;

        [SerializeField] private QuestsList[] _allQuestsInfos;

        public Quest[][] GetAllQuests(IGameStaticDataService staticDataService, string isoLanguage)
        {
            Quest[][] allQuests = new Quest[_allQuestsInfos.Length][];

            for(var i = 0; i < allQuests.Length; i++)
            {
                allQuests[i] = new Quest[_allQuestsInfos[i].Quests.Length];

                for(var j = 0; j < allQuests[i].Length; j++)
                {
                    QuestInfo questInfo = _allQuestsInfos[i].Quests[j];
                    QuestItemStaticData itemStaticData = staticDataService.GetItem(questInfo.Type);

                    string itemName = itemStaticData.GetLocalizedName(isoLanguage);

                    allQuests[i][j] = new Quest(itemStaticData, questInfo.ItemsCount, itemName);
                }
            }

            return allQuests;
        }

        [Serializable]
        private class QuestsList
        {
            public QuestInfo[] Quests;
        }

        [Serializable]
        private class QuestInfo
        {
            public QuestItemType Type;
            public int ItemsCount;
        }
    }
}