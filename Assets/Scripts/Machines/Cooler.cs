using Items;
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

        public int maxPercent = 200;
        public int chance = 1;
        private int heatUpLevel = 1;
        private int maxHeatUpLevel = 4;
        private bool _isOpen = false;

        private new void Awake()
        {
            if(TryGetComponent(out Animator anim_tor))
            {
                animator = anim_tor;
            }
            player = Player.Player.instancePlayer;
        }

        public override void OnTick()
        {
            if (Random.Range(0, maxPercent) <= chance) HeatUp();
        }

        public override void OnClick()
        {
            _isOpen = _isOpen == false ? true : false;
            if (player.playerGraber.heldObj != null && player.playerGraber.heldObj.TryGetComponent(out Item item) && item.itemTag == requiredTag)
            {
                if (heatUpLevel > 1)
                {
                    player.playerGraber.DestroyItem();
                    player.playerGraber.ReplaceItem(emptyBucket);
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
        
        
    }
}
