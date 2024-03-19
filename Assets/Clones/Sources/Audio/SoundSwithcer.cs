namespace Clones.Audio
{
    public class SoundSwithcer : AudioSwitcher
    {
        private void Start() =>
            SetAudioVolume(Progress.Progress.Settings.SoundVolume);

        protected override void SetProgress(float volume) => 
            Progress.Progress.Settings.SoundVolume = volume;
    }
}