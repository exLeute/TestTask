using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace UserInterface
{
    [Serializable]
    public struct SoundLibrary
    {
        [Serializable]
        public struct Controls
        {
            public AudioClip hoverClick;
        }        
        
        [Serializable]
        public struct Music
        {
            public AudioClip mainMenu;
        }
    }
    
    public class SoundOrganizer : MonoBehaviour
    {
        [SerializeField] private bool autoPlayMusic = true;
        
        [Header("Audio sources")]
        public AudioSource musicAudioSource;
        public AudioSource uiAudioSource;
        
        [Header("Sound Library")]
        public SoundLibrary.Controls controls;
        public SoundLibrary.Music music;

        private void Start()
        {
            musicAudioSource.clip = music.mainMenu;
            if(autoPlayMusic) musicAudioSource.Play();
        }

        private static void PlaySound(AudioClip sound, AudioSource audioSource)
        {
            if (audioSource.clip != sound) audioSource.clip = sound;
            if(audioSource.isPlaying) audioSource.Stop();
            audioSource.Play();
        }

        public void AssignSoundToUI<TEventType>(AudioClip sound, VisualElement element)
            where TEventType : EventBase<TEventType>, new()
        {
            element.RegisterCallback<TEventType>(_ => PlaySound(sound, uiAudioSource));
        }
    }
}