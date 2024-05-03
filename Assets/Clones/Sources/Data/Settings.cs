using System;

namespace Clones.Data
{
    [Serializable]
    public class Settings
    {
        private const int DefaultVolume = 0;

        public int SoundVolume;
        public int MusicVolume;

        public Settings()
        {
            SoundVolume = DefaultVolume;
            MusicVolume = DefaultVolume;
        }
    }
}