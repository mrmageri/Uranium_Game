using Items;
using UnityEngine;

namespace Machines
{
    public class Crusher : Machine
    {
        public int maxPercent = 1000;
        public int chance = 5;
        [Header("Fix Chance")]
    
        public int maxFixChance = 100;
        public int minFixChance = 10;
        [Header("Getting Item")]
        public int maxItemChance = 100;
        public int minxItemChance = 5;
        [SerializeField] private float punchForce = 2f;
        [SerializeField] private Transform spawnTransform;
        [SerializeField] private GameObject[] possibleItems;
    
        [SerializeField] private string brokeAnimTrigger;
        private Animator animator;

        private new void Awake()
        {
            player = Player.Player.instancePlayer;
            if (TryGetComponent(out Animator anim))
            {
                animator = anim;
            }
        }
    
        public override void OnClick()
        {
            if(!isBroken) return;
            if (player.playerGraber.heldObj != null && player.playerGraber.heldObj.TryGetComponent(out Item item) &&
                item.itemTag == requiredTag)
            {
                if(Random.Range(0, maxFixChance) <= minFixChance) SetWorking();
                if (Random.Range(0, maxItemChance) <= minxItemChance)
                {
                    GameObject currentObj = Instantiate(possibleItems[Random.Range(0, possibleItems.Length)], spawnTransform.position, Quaternion.identity);
                    if (currentObj.TryGetComponent(out Rigidbody rb))
                    {
                        Vector3 force = new Vector3(0f,punchForce/2,punchForce);
                        rb.AddForce(force, ForceMode.VelocityChange);
                    }
                }
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

        public override void Reset()
        {
            SetWorking();
        }

        public override void ResetBroken()
        {
            SetBroken();
        }
    }
}
