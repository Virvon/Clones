using UnityEngine;

public class CardWand : Card
{
    [Space]
    [Header("�������������� �������")]
    [SerializeField] private int _damage;
    [SerializeField] private float _cooldown;

    public int Damage => _damage;
    public float Cooldown => _cooldown;

    public CardWand ReturnCard() => this;

    public void Init(int damage, float cooldown)
    {
        _damage = damage;
        _cooldown = cooldown;
    }

    public override void Select()
    {
        base.Select();

        //PlayerView.SelectCard(this);
    }
}
