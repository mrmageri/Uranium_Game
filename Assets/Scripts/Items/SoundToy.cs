using System;
using UnityEngine;

namespace Items
{
    public class SoundToy : Item
    {
        [SerializeField] private new AudioClip audio;
        private AudioSource audioSource;

        private void Awake()
        {
            if (TryGetComponent(out AudioSource audioS)) audioSource = audioS;
            audioSource.clip = audio;
        }

        public override void OnUse()
        {
            audioSource.Play();
        }
    }
}
