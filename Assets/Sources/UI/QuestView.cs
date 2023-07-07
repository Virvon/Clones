using TMPro;
using UnityEngine;

public class QuestView : MonoBehaviour
{
    [SerializeField] private Quest _quest;
    [SerializeField] private TMP_Text _value;
    [SerializeField] private TMP_Text _questValue;
    [SerializeField] private TMP_Text _description;

    private void OnEnable()
    {
        _questValue.text = _quest.TargetResourcesCount.ToString();
        _quest.ResourcesCountChanged += OnResourcesCountChanged;
    }

    private void Start() => _description.text = _quest.s_MiningFacilityType.ToString();

    private void OnDisable() => _quest.ResourcesCountChanged -= OnResourcesCountChanged;

    private void OnResourcesCountChanged()
    {
        _value.text = _quest.ResourcesCount.ToString();
        _description.text = _quest.s_MiningFacilityType.ToString();
    }
}
