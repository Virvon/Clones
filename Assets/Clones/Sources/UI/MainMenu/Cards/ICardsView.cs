using System;

namespace Clones.UI
{
    public interface ICardsView
    {
        Card CurrentCard { get; }
        event Action CardSelected;
    }
}