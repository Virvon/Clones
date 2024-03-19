namespace Clones.Audio
{
    public class MusicSwithcer : AudioSwitcher
    {
        private void Start() =>
           SetAudioVolume(Progress.Progress.Settings.MusicVolume);

        protected override void SetProgress(float volume) =>
            Progress.Progress.Settings.MusicVolume = volume;
    }
}