using UnityEngine;

namespace Clones.SFX
{
    public class CharacterShootSound : ShootSound
    {
        public void Init(AudioClip audioClip, float volume)
        {
            AudioSource.clip = audioClip;
            AudioSource.volume = volume;
        }
    }
}
