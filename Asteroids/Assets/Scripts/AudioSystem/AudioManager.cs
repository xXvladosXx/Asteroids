using System;
using UnityEngine;

namespace AudioSystem
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance;

        [SerializeField] private AudioSource _musicSource, _effectsSource;
        [SerializeField] private AudioClip _startMusic;
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            PlayMusicSound(_startMusic);
        }

        public void PlayEffectSound(AudioClip audioClip)
        {
            _effectsSource.PlayOneShot(audioClip);
        }
    
        public void PlayMusicSound(AudioClip audioClip)
        {
            _musicSource.PlayOneShot(audioClip);
            _musicSource.loop = true;
        }

        public void ChangeEffectsSound(float value)
        {
            _effectsSource.volume = value;
        }
        public void ChangeMusicSound(float value)
        {
            _musicSource.volume = value;
        }
    }
}
