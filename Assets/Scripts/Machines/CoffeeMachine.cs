
using Items;
using TMPro;
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

        public int delayMin;
        public int delaySec;
        [SerializeField] private TMP_Text timeText;
        private int minLast = 0;
        private int secLast = 0;
        private string min;
        private string sec;

        
        private new void Awake()
        {
            if(TryGetComponent(out Animator anim_tor))
            {
                animator = anim_tor;
            }
            player = Player.Player.instancePlayer;
            playerGraber = player.playerGraber;
            minLast = delayMin;
            secLast = delaySec;
            SetZero();
        }

        public override void OnTick()
        {
            if (isBroken)
            {
                CountSec();
                if(minLast == 0 && secLast == 0) SetWorking();
            }
        }

        public override void OnClick()
        {
            if(isBroken) return;
            if (playerGraber.heldObj != null && playerGraber.heldObj.TryGetComponent(out Item item) && item.itemTag == requiredTag)
            {
                    currentItem = playerGraber.heldObj;
                    playerGraber.heldObj.layer = 0;
                    for (int i = 0; i < playerGraber.heldObj.transform.childCount; i++)
                    {
                        playerGraber.heldObj.transform.GetChild(i).gameObject.layer = 0;
                    }
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

        public override void Reset()
        {
            SetWorking();
        }

        private void SetTime()
        {
            minLast = delayMin;
            secLast = delaySec;
            timeText.text = min + ":" + sec;
        }

        private void SetZero()
        {
            min = "00";
            sec = "00";
            timeText.text = min + ":" + sec;
        }

        private void CountSec()
        {
            secLast -= 1;
            if (minLast - 1 >= 0 && secLast < 0)
            {
                minLast--; 
                secLast = 59;
            }
            min = minLast.ToString();
            sec = secLast.ToString();
            if (minLast < 10) min = "0" + minLast;
            if (secLast < 10) sec = "0" + secLast;
            timeText.text = min + ":" + sec;
        }
        
        public override void ResetBroken()
        {
            SetBroken();
        }

        public void OnActionEnd()
        {
            Destroy(currentItem);
            currentItem = null;
            Instantiate(mugCoffee, spawnPoint.position, quaternion.identity);
            SetBroken();
            SetTime();
        }
    }
}
