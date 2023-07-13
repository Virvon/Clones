using UnityEngine;

public class UIPanelsManager : MonoBehaviour
{
    [SerializeField] private GameObject _panel;

    public void SetVisiblePanel(bool isActive) => _panel.SetActive(isActive);
}
