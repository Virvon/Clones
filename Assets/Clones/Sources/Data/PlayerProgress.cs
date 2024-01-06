﻿using System;

namespace Clones.Data
{
    [Serializable]
    public class PlayerProgress
    {
        public Wallet Wallet;
        public AvailableClones AvailableClones;
        public AvailableWands AvailableWands;
        public AveragePlayTime AveragePlayTime;

        public PlayerProgress()
        {
            Wallet = new();
            AvailableClones = new();
            AvailableWands = new();
            AveragePlayTime = new();
        }
    }
}