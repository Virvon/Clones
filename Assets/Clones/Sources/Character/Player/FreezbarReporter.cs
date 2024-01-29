using UnityEngine;

public class FreezbarReporter : MonoBehaviour
{
    private Freezbar _freezbar;
    private Freezing _freezing;

    private void OnDestroy()
    {
        if (_freezing != null)
            _freezing.FreezPercentChanged -= OnFreezPercentChanged;
    }

    public void Init(Freezbar freezbar) => 
        _freezbar = freezbar;

    public void SetFreezing(Freezing freezing)
    {
        _freezing = freezing;

        _freezing.FreezPercentChanged += OnFreezPercentChanged;
    }

    private void OnFreezPercentChanged(float percent) => 
        _freezbar.SetFreezPercent(percent);
}
