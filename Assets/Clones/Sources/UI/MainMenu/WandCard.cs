using UnityEngine;

public class WandCard : Card
{
    //[Space]
    //[Header("Характеристики палочки")]
    //[SerializeField] private int _damage;
    //[SerializeField] private float _cooldown;
    [SerializeField] private GameObject _unlock;

    //public int Damage => _damage;
    ///public float Cooldown => _cooldown;

    public WandCard ReturnCard() => this;

    public override void Select()
    {
        base.Select();

        //PlayerView.SelectCard(this);
    }

    protected override void Unlock()
    {
        SelectButton.interactable = true;
        _unlock.SetActive(true);
    }
}
