using UnityEngine;

namespace Clones.UI
{
    [RequireComponent(typeof(DieClone))]
    public class CloneCard : Card
    {
        [SerializeField] private GameObject _unlock;

        private DieClone _dieClone;

        protected override void Unlock()
        {
            _dieClone = GetComponent<DieClone>();

            if (_dieClone.IsUsed)
            {
                SelectButton.interactable = false;
                _dieClone.Disused += OnDisused;
            }
            else
            {
                ActiveVisuals();
            }
        }

        private void OnDisused()
        {
            _dieClone.Disused -= OnDisused;
            ActiveVisuals();
        }

        private void ActiveVisuals()
        {
            SelectButton.interactable = true;
            _unlock.SetActive(true);
        }
    }
}