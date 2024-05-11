using Items;
using UnityEngine;
using UnityEngine.Events;

namespace Machines
{
    public class Hamster : Machine
    {
        public GameObject lightsObj;

        [SerializeField] private GameObject deadHamster;
        [SerializeField] private UnityEvent onClose;
        [SerializeField] private UnityEvent onOpen;
        
        public int maxPercent = 500;
        public int chance = 1;

        private bool isOpen = false;
        private bool hamsterIsDead = false;
        private Animator animator;


        public override void OnClick()
        {
            //We need this to prevent block from closing than player takes the hamster
            if (hamsterIsDead && isOpen && isBroken)
            {
                player.playerGraber.GiveItem(deadHamster);
                hamsterIsDead = false;
                return;
            }

            if (isOpen && isBroken)
            {
                if (player.playerGraber.heldObj != null && player.playerGraber.heldObj.TryGetComponent(out Item item) && item.itemTag == requiredTag)
                {
                    //TODO add alive hamster animation
                    SetWorking();
                    lightsObj.SetActive(true);
                    player.PlayerLight(false);
                    playerGraber.DestroyItem();
                    return;
                }
            }
            isOpen = !isOpen;
            if (isOpen)
            {
                onOpen.Invoke();
            }
            else
            {
                onClose.Invoke();
            }
        }

        public override void OnTick()
        {
            if (Random.Range(0, maxPercent) <= chance && !isBroken)
            {
                SetBroken();
                hamsterIsDead = true;
                lightsObj.SetActive(false);
                player.PlayerLight(true);

                //TODO add dead hamster animation
            }
        }
    }
}
