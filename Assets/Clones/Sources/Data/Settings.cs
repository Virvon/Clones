using System;

namespace Clones.Data
{
    [Serializable]
    public class Settings
    {
        private const int DefaultVolume = 0;

        public float SoundVolume;
        public float MusicVolume;

        public Settings()
        {
            SoundVolume = DefaultVolume;
            MusicVolume = DefaultVolume;
        }
    }
}