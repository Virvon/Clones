﻿using Clones.Types;
using UnityEngine;

namespace Clones.StaticData
{
    [CreateAssetMenu(fileName = "New MainMenu", menuName = "Data/Create new main menu", order = 51)]
    public class MainMenuStaticData : ScriptableObject
    {
        public GameObject Prefab;
        public CloneType[] CloneTypes;
        public WandType[] WandTypes;
    }
}
