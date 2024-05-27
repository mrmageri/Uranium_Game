using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Items
{
    public class Hammer : Item
    {
        [Header("Hammer Data")]
        [SerializeField] private GameObject particleObj;
        [SerializeField] private float hitRange;
        private Animator animator;
        private Transform playerGraberTransform;
        [SerializeField] private new AudioClip[] audioClips;
        private AudioSource audioSource;

        private void Awake()
        {
            if (TryGetComponent(out AudioSource audioS)) audioSource = audioS;
            audioSource.clip = audioClips[Random.Range(0,audioClips.Length)];
            animator = TryGetComponent(out Animator anima) ? anima : null;
            playerGraberTransform = Player.Player.instancePlayer.playerGraber.transform;
        }
        public override void OnUse()
        {
            //animator.SetTrigger(triggerName);
            if (Physics.Raycast(playerGraberTransform.position, playerGraberTransform.forward,out var hit,hitRange))
            {
                audioSource.clip = audioClips[Random.Range(0,audioClips.Length)];
                audioSource.Play();
                Instantiate(particleObj, hit.point, quaternion.identity);
            }
        }
    }
}
