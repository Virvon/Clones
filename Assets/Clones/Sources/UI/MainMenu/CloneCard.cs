using TMPro;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(DieClone))]
public class CloneCard : Card
{
    [SerializeField] private TMP_Text _levelText;
    [SerializeField] private GameObject _unlock;

    private GameObject _wandPrefab;
    private int _level = 1;
    private DieClone _dieClone;

    public UnityEvent Selected = new UnityEvent();

    public void SetWand(GameObject wandPrefab, GameObject clonePrefab)
    {
        Destroy(_wandPrefab?.gameObject);
        _wandPrefab = Instantiate(wandPrefab, clonePrefab.GetComponent<ClonePrafab>().WandPrefabPlace);
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
}
