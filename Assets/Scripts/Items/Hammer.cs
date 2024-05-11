using Unity.Mathematics;
using UnityEngine;

namespace Items
{
    public class Hammer : Item
    {
        [SerializeField] private GameObject particleObj;
        [SerializeField] private string triggerName;
        [SerializeField] private float hitRange;
        private Animator animator;
        private Transform playerGraberTransform;

        private void Awake()
        {
            animator = TryGetComponent(out Animator anima) ? anima : null;
            playerGraberTransform = Player.Player.instancePlayer.playerGraber.transform;
        }
        public override void OnUse()
        {
            //animator.SetTrigger(triggerName);
            if (Physics.Raycast(playerGraberTransform.position, playerGraberTransform.forward,out var hit,hitRange))
            {
                Instantiate(particleObj, hit.point, quaternion.identity);
            }
        }
    }
}
