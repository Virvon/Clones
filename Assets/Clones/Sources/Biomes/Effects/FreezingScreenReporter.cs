using UnityEngine;

public class FreezingScreenReporter : MonoBehaviour
{
    private FreezingScreen _freezingScreen;
    private Freezing _freezing;

    public void Init(FreezingScreen freezingScreen) => 
        _freezingScreen = freezingScreen;

    public void SetFreezing(Freezing freezing)
    {
        _freezing = freezing;

        _freezing.FreezPercentChanged += OnFreezPercentChanged;
    }

    private void OnFreezPercentChanged(float percent) => 
        _freezingScreen.SetFreezPercent(percent);
}