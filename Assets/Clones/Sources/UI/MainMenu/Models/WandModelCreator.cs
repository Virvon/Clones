using Clones.Infrastructure;
using UnityEngine;

namespace Clones.UI
{
    public class WandModelCreator : MonoBehaviour
    {
        private WandsCardsView _wandsCardsView;
        private ICharacterFactory _characterFactory;

        private GameObject _currentWand;

        public void Init(WandsCardsView wandsCardsView, ICharacterFactory characterFactory)
        {
            _wandsCardsView = wandsCardsView;
            _characterFactory = characterFactory;

            _wandsCardsView.CardSelected += Create;
        }

        private void OnDestroy() =>
            _wandsCardsView.CardSelected -= Create;

        public void Create()
        {
            if (_currentWand != null)
                Destroy(_currentWand);

            _currentWand = _characterFactory.CreateWandModel();
        }
    }
}