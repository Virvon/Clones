using System;

namespace Clones.UI
{
    public interface IBuyable
    {
        bool CanBuy { get; }

        event Action BuyTried;
    }
}