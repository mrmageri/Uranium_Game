using UnityEngine;
using Random = UnityEngine.Random;

namespace Machines
{
    public class VacuumCleaner : Machine
    {
        public int maxPercent = 1000;
        public int chance = 5;
        
        [SerializeField] private float stopDistance;
        [SerializeField] private float maxDistanceDelta;
        [SerializeField] private float rotateAngle;
        [SerializeField] private float raySpacing;

        [SerializeField] private GameObject dust;
        
        private bool startRotating = false;
        private Animator animator;
        
        private new void Awake()
        {
            player = Player.Player.instancePlayer;
            if (TryGetComponent(out Animator anim))
            {
                animator = anim;
            }
        }

        private void Update()
        {
            if (CheckHit())
            {
                startRotating = true;
            }
            else
            {
                var transform1 = transform;
                transform.position = Vector3.MoveTowards(transform1.position,transform1.position + transform1.forward, maxDistanceDelta);
            }

            if (startRotating)
            {
                transform.rotation = Quaternion.AngleAxis(transform.rotation.eulerAngles.y + rotateAngle, new Vector3(0, 1, 0));
                startRotating = false;
            } 
        }

        private bool CheckHit()
        {
            return Physics.Raycast(transform.position, transform.forward, stopDistance) ||
                   Physics.Raycast(transform.position + transform.right * raySpacing,transform.forward, stopDistance) ||
                   Physics.Raycast(transform.position + transform.right * -raySpacing,transform.forward, stopDistance);
        }

        public override void OnClick()
        {
            if (isBroken)
            {
                player.playerGraber.GiveItem(dust);
                isBroken = false;
            }
        }

        public override void OnTick()
        {
            if ((Random.Range(0, maxPercent) <= chance) && !isBroken)
            {
                SetBroken();
                //animator.SetTrigger(brokeAnimTrigger);
            }
        }
    }
}
