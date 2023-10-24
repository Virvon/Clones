using UnityEngine;

namespace Clones.UI
{
    public class WandCard : Card
    {
        [SerializeField] private GameObject _unlock;

        protected override void Unlock()
        {
            SelectButton.interactable = true;
            _unlock.SetActive(true);
        }
    }
}