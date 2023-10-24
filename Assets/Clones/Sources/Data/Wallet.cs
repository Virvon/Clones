using System;

namespace Clones.Data
{
    [Serializable]
    public class Wallet
    {
        public int Money;
        public int Dna;

        public event Action CurrencyCountChanged;

        public void CollectMoney(int count)
        {
            if (count > 0)
            {
                Money += count;

                CurrencyCountChanged?.Invoke();
            }    
        }

        public bool TryTakeMoney(int count)
        {
            if(Money >= count)
            {
                Money -= count;
                CurrencyCountChanged?.Invoke();

                return true;
            }

            return false;
        }

        public bool TryTakeDna(int count)
        {
            if(Dna >= count)
            {
                Dna -= count;
                CurrencyCountChanged?.Invoke();

                return true;
            }

            return false;
        }

        public void CollectDna(int count)
        {
            if (count > 0)
            {
                Dna += count;

                CurrencyCountChanged?.Invoke();
            }
        }
    }
}
