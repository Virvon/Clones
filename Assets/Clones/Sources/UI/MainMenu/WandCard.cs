using UnityEngine;

public class WandCard : Card
{
    [SerializeField] private GameObject _unlock;

    public WandCard ReturnCard() => this;

    protected override void Unlock()
    {
        SelectButton.interactable = true;
        _unlock.SetActive(true);
    }
}
