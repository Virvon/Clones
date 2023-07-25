using TMPro;
using UnityEngine;

public class QuestView : MonoBehaviour
{
    [SerializeField] private TMP_Text _value;
    [SerializeField] private TMP_Text _questValue;
    [SerializeField] private TMP_Text _description;

    private QuestCell _cell;

    public void Init(QuestCell questCell)
    {
        _cell = questCell;
        _description.text = questCell.Type.name;

        UpdateInfo();
    }

    public void UpdateInfo()
    {
        _value.text = _cell.CurrentCount.ToString();
        _questValue.text = _cell.MaxCount.ToString();
    }
}
