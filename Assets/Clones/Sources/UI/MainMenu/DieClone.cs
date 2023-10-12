using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DieClone : MonoBehaviour
{
    [SerializeField] private Button _selectedCloneButton;
    [SerializeField] private GameObject _unlockVisuals;
    [SerializeField] private GameObject _dieVisuals;
    [SerializeField] private int _secondsToRestore;
    [SerializeField] private TMP_Text _timeToRestoreText;

    private UnityEvent CloneDied = new UnityEvent();

    private void Start()
    {
        CloneDied.AddListener(Die);
    }

    public void Invoke()
    {
        CloneDied.Invoke();
    }

    private void Die()
    {
        _selectedCloneButton.interactable = false;
        _unlockVisuals.SetActive(false);
        _dieVisuals.SetActive(true);
        StartCoroutine(ActivateTimer());
    }

    private void Restore()
    {
        _selectedCloneButton.interactable = true;
        _unlockVisuals.SetActive(true);
        _dieVisuals.SetActive(false);
    }

    private IEnumerator ActivateTimer()
    {
        var waitForDelaySeconds = new WaitForSeconds(1f);

        float currentSecondsToRestore = _secondsToRestore;

        while (currentSecondsToRestore > 0)
        {
            _timeToRestoreText.text = NumberFormatter.ConvertSecondsToTimeString(currentSecondsToRestore--);
            yield return waitForDelaySeconds;
        }

        Restore();
    }
}
