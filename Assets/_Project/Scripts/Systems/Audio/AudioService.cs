using Project.Interfaces.Audio;
using UnityEngine;

namespace Project.Systems.Audio
{
    public class AudioService : MonoBehaviour, IAudioService
    {
        [SerializeField] private AudioSource _musicSource;
        [SerializeField] private AudioSource _soundSource;
        [SerializeField] private AudioSource _ambientSource;

        private void Awake()
        {
            _musicSource.loop = true;
            _ambientSource.loop = true;
        }

        public void PlaySound(AudioClip clip)
        {
            if (clip == null)
                return;

            _soundSource.PlayOneShot(clip);
        }

        public void PlayMusic(AudioClip clip)
        {
            _musicSource.clip = clip;
            _musicSource.Play();
        }

        public void PlayAmbient(AudioClip clip)
        {
            _ambientSource.clip = clip;
            _ambientSource.Play();
        }

        public void MuteAudio()
        {
            _soundSource.mute = true;
            _musicSource.mute = true;
            _ambientSource.mute = true;
        }

        public void UnmuteAudio()
        {
            _soundSource.mute = false;
            _musicSource.mute = false;
            _ambientSource.mute = false;
        }

        public void PauseAudio()
        {
            AudioListener.pause = true;
            AudioListener.volume = 0f;
        }

        public void UnpauseAudio()
        {
            AudioListener.pause = false;
            AudioListener.volume = 1f;
        }
    }
}