using UnityEngine;

namespace Clones.StaticData
{
    [CreateAssetMenu(fileName = "New ScoreCounter", menuName = "Data/Create new score counter", order = 51)]
    public class ScoreCounterStaticData : ScriptableObject
    {
        public int ScorePerItem;
        public int ScorePerQuest;
        public int ScorePerKill;
    }
}
