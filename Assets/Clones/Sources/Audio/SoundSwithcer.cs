namespace Clones.Audio
{
    public class SoundSwithcer : AudioSwitcherSlider
    {
        private void Start() =>
            SetAudioVolume(Progress.Progress.Settings.SoundVolume);

        protected override void SetProgress(int volume) => 
            Progress.Progress.Settings.SoundVolume = volume;
    }
}