
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;

namespace Machines
{
    public class CoffeeMachine : Machine
    {
        [SerializeField] private Transform spawnPoint;
        [SerializeField] private GameObject currentItem;
        [SerializeField] private GameObject mugCoffee;
        [SerializeField] private Animator animator;
        [SerializeField] private UnityEvent onStart;
        
        private new void Awake()
        {
            if(TryGetComponent(out Animator anim_tor))
            {
                animator = anim_tor;
            }
            player = Player.Player.instancePlayer;
            playerGraber = player.playerGraber;
        }

        public override void OnTick() { }

        public override void OnClick()
        {
            if (playerGraber.heldObj != null && playerGraber.heldObj.TryGetComponent(out Item item) && item.itemTag == requiredTag)
            {
                currentItem = playerGraber.heldObj;
                playerGraber.heldObj.layer = 0;
                playerGraber.DestroyItem();
                currentItem = Instantiate(currentItem, spawnPoint.position, quaternion.identity);
                if (currentItem.TryGetComponent(out Collider coll))
                {
                    coll.enabled = false;
                }
                onStart.Invoke();
                animator.SetTrigger("Work");
            }
        }

        public void OnActionEnd()
        {
            Destroy(currentItem);
            currentItem = null;
            Instantiate(mugCoffee, spawnPoint.position, quaternion.identity);
        }
    }
}
