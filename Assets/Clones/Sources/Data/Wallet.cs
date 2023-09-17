using System;

namespace Clones.Data
{
    [Serializable]
    public class Wallet
    {
        public int Money;
        public int Dna;

        public event Action CurrencyCountChanged;

        public void CollectMoney(int money)
        {
            if (money > 0)
            {
                Money += money;

                CurrencyCountChanged?.Invoke();
            }    
        }

        public void CollectDna(int dna)
        {
            if (dna > 0)
            {
                Dna += dna;

                CurrencyCountChanged?.Invoke();
            }
        }
    }
}
