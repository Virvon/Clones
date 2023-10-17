using System.Collections.Generic;
using UnityEngine;

public class ToggleWindows : MonoBehaviour
{
    private List<GameObject> _activateGameobjects;
    private List<GameObject> _deactivateGameobjects;

    public void Init(List<GameObject> activateGameobjects, List<GameObject> deactivateGameobjects)
    {
        _activateGameobjects = activateGameobjects;
        _deactivateGameobjects = deactivateGameobjects;
    }

    public void Invoke()
    {
        foreach (GameObject gameObject in _activateGameobjects)
            gameObject.SetActive(true);

        foreach (GameObject gameObject in _deactivateGameobjects)
            gameObject.SetActive(false);
    }
}
