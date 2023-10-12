using System.Collections.Generic;
using UnityEngine;

public class ToggleWindows : MonoBehaviour
{
    [SerializeField] private List<GameObject> _activateGameobjects = new List<GameObject>();
    [SerializeField] private List<GameObject> _deactivateGameobjects = new List<GameObject>();

    public void Invoke()
    {
        foreach (GameObject gameObject in _activateGameobjects)
            gameObject.SetActive(true);

        foreach (GameObject gameObject in _deactivateGameobjects)
            gameObject.SetActive(false);
    }
}
