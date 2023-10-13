using Clones.StaticData;
using UnityEngine;

namespace Clones.UI
{
    public class MainMenu : MonoBehaviour
    {
        public CardCloneType[] CardCloneTypes { get; private set; }

        public void Init(CardCloneType[] cardCloneTypes)
        {
            CardCloneTypes = cardCloneTypes;
        }
    }
}
