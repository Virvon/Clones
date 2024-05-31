using System;

namespace Clones.UI
{
    public interface IBuyable
    {
        event Action BuyTried;

        bool CanBuy { get; }
    }
}