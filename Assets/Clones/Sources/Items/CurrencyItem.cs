using Clones.GameLogic;
using UnityEngine;

namespace Clones.Items
{
    public class CurrencyItem : MonoBehaviour, IItem
    {
        public void Accept(IItemVisitor visitor) =>
            visitor.Visit(this);
    }
}