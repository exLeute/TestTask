using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

namespace UserInterface
{
    [RequireComponent(typeof(AudioSource))]
    public class UiSoundOrganizer : MonoBehaviour
    {
        private AudioSource _audioSource;
        [SerializeField] private AudioClip audioClip;

        public void Init()
        {
            _audioSource = GetComponent<AudioSource>();
            _audioSource.clip = audioClip;
        }
        
        public void PlayUiHoverSound()
        {
            if(_audioSource.isPlaying) _audioSource.Stop();
            _audioSource.Play();
        }

        public void AssignSoundTo<TEventType>(VisualElement element)
            where TEventType : EventBase<TEventType>, new()
        {
            element.RegisterCallback<TEventType>(_ => PlayUiHoverSound());
        }
    }
}