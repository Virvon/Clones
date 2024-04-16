using Clones.Infrastructure;
using UnityEngine;

namespace Clones.UI
{
    public class CloneModelCreator : MonoBehaviour
    {
        [SerializeField] private WandModelCreator _wandModelCreator;

        private ClonesCardsView _cloneCardsView;
        private ICharacterFactory _characterFactory;

        private GameObject _currentClone;

        public void Init(ClonesCardsView clonesCardsView, ICharacterFactory characterFactory)
        {
            _cloneCardsView = clonesCardsView;
            _characterFactory = characterFactory;

            _cloneCardsView.CardSelected += OnCardSelected;
        }

        private void OnCardSelected()
        {
            if (_currentClone != null)
                Destroy(_currentClone);

            _currentClone = _characterFactory.CreateCloneModel(transform);
            _wandModelCreator.Create();
        }
    }
}