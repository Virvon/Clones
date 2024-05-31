using System;

namespace Clones.UI
{
    public interface ICardsView
    {
        event Action CardSelected;

        Card CurrentCard { get; }
    }
}