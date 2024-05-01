using UnityEngine;
using Random = UnityEngine.Random;

namespace Machines
{
    public class Cooler : Machine
    {
        [SerializeField] private string animIntName;
        [SerializeField] private GameObject[] smokeObjs;
        [SerializeField] private GameObject emptyBucket;


        private Animator animator;

        public int maxPercent = 1000;
        public int chance = 5;
        private int heatUpLevel = 1;
        private int maxHeatUpLevel = 4;
        private bool _isOpen = false;

        private new void Awake()
        {
            if(TryGetComponent(out Animator anim_tor))
            {
                animator = anim_tor;
            }
            playerGraber = Player.Player.instancePlayer.playerGraber;
        }

        public override void OnTick()
        {
            if (Random.Range(0, maxPercent) <= chance) HeatUp();
        }

        public override void OnClick()
        {
            _isOpen = _isOpen == false ? true : false;
            if (playerGraber.heldObj != null && playerGraber.heldObj.TryGetComponent(out Item item) && item.itemTag == requiredTag)
            {
                if (heatUpLevel > 1)
                {
                    playerGraber.DestroyItem();
                    playerGraber.CreatItem(emptyBucket);
                }
                HeatDown();
            }
        }

        public void HeatDown()
        {
            if(heatUpLevel == 1 && !isBroken) return;
            heatUpLevel--;
            if (heatUpLevel < 3)
            {
                foreach (var elem in smokeObjs)
                {
                    elem.SetActive(false);   
                }
            }
            animator.SetInteger(animIntName,heatUpLevel);
            if (heatUpLevel < 4) SetWorking();
        }

        private void HeatUp()
        {
            if (heatUpLevel == maxHeatUpLevel && isBroken) return;
            heatUpLevel++;
            animator.SetInteger(animIntName,heatUpLevel);
            if (heatUpLevel >= 3)
            {
                foreach (var elem in smokeObjs)
                {
                    elem.SetActive(true);   
                }
            }

            if (heatUpLevel == maxHeatUpLevel)
            {
                SetBroken();
            }
        }

        private void SetWorking()
        {
            isBroken = false;
            Computer.instanceComputer.UpdateWorkingMachinesNumber();
        }

        private void SetBroken()
        {
            isBroken = true;
            Computer.instanceComputer.UpdateWorkingMachinesNumber();
        }
        
    }
}
