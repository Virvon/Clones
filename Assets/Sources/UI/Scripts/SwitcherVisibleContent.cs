using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitcherVisibleContent : MonoBehaviour
{
    [SerializeField] private List<GameObject> _firstGroup;
    [SerializeField] private List<GameObject> _secondGroup;

    private bool _isActivFirstGroup = false;

    private void Start()
    {
        Invoke();
    }

    public void Invoke()
    {
        _isActivFirstGroup = !_isActivFirstGroup;

        foreach (var obj in _firstGroup)
            obj.SetActive(_isActivFirstGroup);

        foreach (var obj in _secondGroup)
            obj.SetActive(!_isActivFirstGroup);
    }
}
