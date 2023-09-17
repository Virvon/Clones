using System;

namespace Clones.Data
{
    [Serializable]
    public class PlayerProgress
    {
        public Wallet Wallet;

        public PlayerProgress()
        {
            Wallet = new();
        }
    }
}
