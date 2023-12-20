public class Freezbar : Bar
{
    public void SetFreezPercent(float percent)
    {
        if (percent / 100f <= 0)
            Slider.gameObject.SetActive(false);
        else if (percent / 100f > 0 && Slider.IsActive() == false)
            Slider.gameObject.SetActive(true);

        StartAnimation(percent / 100f);
    }
}
