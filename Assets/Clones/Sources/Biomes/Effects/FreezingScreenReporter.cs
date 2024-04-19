using UnityEngine;

public class FreezingScreenReporter : MonoBehaviour
{
    private FreezingScreen _freezingScreen;
    private Freezing _freezing;

    public void Init(FreezingScreen freezingScreen) => 
        _freezingScreen = freezingScreen;

    private void OnDestroy()
    {
        if (_freezing != null)
            _freezing.FreezPercentChanged -= OnFreezPercentChanged;
    }

    public void SetFreezing(Freezing freezing)
    {
        if(_freezing != null)
            _freezing.FreezPercentChanged -= OnFreezPercentChanged;
        _freezing = freezing;

        _freezing.FreezPercentChanged += OnFreezPercentChanged;
    }

    private void OnFreezPercentChanged(float percent) => 
        _freezingScreen.SetFreezPercent(percent);
}