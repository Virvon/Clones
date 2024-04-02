using Clones.Infrastructure;
using Clones.UI;
using UnityEngine;

public class WandCreator : MonoBehaviour
{
    private WandsCardsView _wandsCardsView;
    private ICharacterFactory _characterFactory;
    private Transform _wandBone;

    private GameObject _currentWand;

    public void Init(WandsCardsView wandsCardsView, ICharacterFactory characterFactory, Transform wandBone)
    {
        _wandsCardsView = wandsCardsView;
        _characterFactory = characterFactory;   
        _wandBone = wandBone;

        _wandsCardsView.CardSelected += OnCardSelected;
    }

    private void OnDestroy() => 
        _wandsCardsView.CardSelected -= OnCardSelected;

    private void OnCardSelected()
    {
        if(_currentWand != null)
            Destroy(_currentWand);

        _currentWand = _characterFactory.CreateWandModel(_wandBone);
    }
}
