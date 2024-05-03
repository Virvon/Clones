namespace Clones.Audio
{
    public class MusicSwithcer : AudioSwitcherSlider
    {
        private void Start() =>
           SetAudioVolume(Progress.Progress.Settings.MusicVolume);

        protected override void SetProgress(int volume) =>
            Progress.Progress.Settings.MusicVolume = volume;
    }
}