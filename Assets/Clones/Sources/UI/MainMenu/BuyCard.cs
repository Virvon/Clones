using Clones.Max;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BuyCard : MonoBehaviour
{
    [SerializeField] private Button _selectCardButton;
    [Space]
    [SerializeField] private int _buyPrice;
    [SerializeField] private GameObject _buyButton;
    [SerializeField] private GameObject _cantBuyVisuals;
    [SerializeField] private TMP_Text _textPrice;
    [SerializeField] private Wallet _wallet;
    [Space]
    [SerializeField] private GameObject _lockVisuals;
    [SerializeField] private GameObject _unlockVisuals;

    private bool _canBuy => _wallet.Coins >= _buyPrice;

    public UnityEvent Purchased = new UnityEvent();

    private void Start()
    {
        _selectCardButton.interactable = false;
        _wallet.ValuesChanged.AddListener(CheckCanBuy);
        _textPrice.text = NumberFormatter.DivideIntegerOnDigits(_buyPrice);
        CheckCanBuy();
    }

    public void CheckCanBuy()
    {
        _cantBuyVisuals.SetActive(_canBuy == false);
    }

    public void Invoke()
    {
        if (_canBuy)
        {
            _buyButton.SetActive(false);
            _lockVisuals.SetActive(false);
            _unlockVisuals.SetActive(true);
            _wallet.ChangeCoinsCount(-_buyPrice);
            _selectCardButton.interactable = true;
            Purchased?.Invoke();
            Destroy(this);
        }
    }
}
