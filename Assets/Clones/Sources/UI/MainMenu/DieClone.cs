using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class DieClone : MonoBehaviour
{
    //[SerializeField] private Button _selectedCloneButton;
    //[SerializeField] private GameObject _unlockVisuals;
    [SerializeField] private GameObject _dieVisuals;
    //[SerializeField] private int _secondsToRestore;
    [SerializeField] private TMP_Text _timeToRestoreText;

    private const float Delay = 1;

    private DateTime _disuseEndDate;

    public bool IsUsed { get; private set; }

    public event Action Disused;
    private UnityEvent CloneDied = new UnityEvent();

    private void Start()
    {
        //CloneDied.AddListener(Die);
    }
    private void OnEnable()
    {
        Wait();
    }

    public void Init(DateTime disuseEndDate)
    {
        _disuseEndDate = disuseEndDate;

        Wait();
    }

    public void Invoke()
    {
        CloneDied.Invoke();
    }

    private void Die()
    {
        //_selectedCloneButton.interactable = false;
        //_unlockVisuals.SetActive(false);
        //_dieVisuals.SetActive(true);
        //StartCoroutine(ActivateTimer());
    }

    private void Restore()
    {
        //_selectedCloneButton.interactable = true;
        //_unlockVisuals.SetActive(true);
        _dieVisuals.SetActive(false);
    }

    private void Wait()
    {
        TimeSpan timeLeft = _disuseEndDate - DateTime.Now;

        if (timeLeft > TimeSpan.Zero)
            Use();
        else
            EndUse();
    }

    private void EndUse()
    {
        IsUsed = false;
        _dieVisuals.SetActive(false);

        Disused?.Invoke();
    }

    private void Use()
    {
        IsUsed = true;
        _dieVisuals.SetActive(true);

        StartCoroutine(Timer());
    }

    private IEnumerator Timer()
    {
        var delay = new WaitForSeconds(Delay);
        bool isTimeUp = false;
        TimeSpan timeLeft;

        while (isTimeUp == false)
        {
            timeLeft = _disuseEndDate - DateTime.Now;

            if (timeLeft > TimeSpan.Zero)
                _timeToRestoreText.text = NumberFormatter.ConvertSecondsToTimeString((float)timeLeft.TotalSeconds);
            else
                isTimeUp = true;

            yield return delay;
        }

        EndUse();
    }
}
