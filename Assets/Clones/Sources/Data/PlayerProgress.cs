using System;
using System.Collections.Generic;

namespace Clones.Data
{
    [Serializable]
    public class PlayerProgress
    {
        public Wallet Wallet;
        public List<CloneData> CloneDatas;
        public List<WandData> WandDatas;

        public PlayerProgress()
        {
            Wallet = new();
            CloneDatas = new();
            WandDatas = new();
        }
    }
}