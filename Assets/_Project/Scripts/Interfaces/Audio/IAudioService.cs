using UnityEngine;

namespace Project.Interfaces.Audio
{
    public interface IAudioService
    {
        void PlaySound(AudioClip clip);
        void PlayMusic(AudioClip clip);
    }
}
