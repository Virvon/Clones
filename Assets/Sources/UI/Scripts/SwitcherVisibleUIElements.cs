using System.Collections.Generic;
using UnityEngine;

public class SwitcherVisibleUIElements : MonoBehaviour
{
    [SerializeField] private List<GameObject> _objectsActivate = new List<GameObject>();
    [SerializeField] private List<GameObject> _objectsDeactivate = new List<GameObject>();

    public void SwitchVisiblePanels()
    {
        foreach (var obj in _objectsActivate)
            obj.SetActive(true);

        foreach (var obj in _objectsDeactivate)
            obj.SetActive(false);
    }
}
