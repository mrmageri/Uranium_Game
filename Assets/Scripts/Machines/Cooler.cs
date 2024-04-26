using UnityEngine;
using Random = UnityEngine.Random;

namespace Machines
{
    public class Cooler : Machine
    {
        [SerializeField] private string animIntName;
        [SerializeField] private GameObject[] smokeObjs;


        private Animator animator;

        private int maxPercent = 100;
        private int heatUpChance = 2;
        private int heatUpLevel = 1;
        private int maxHeatUpLevel = 4;
        private bool _isOpen = false;

        private void Awake()
        {
            if(TryGetComponent(out Animator anim_tor))
            {
                animator = anim_tor;
            }
            playerGraber = Player.Player.instancePlayer.playerGraber;
        }

        public override void OnTick()
        {
            if (Random.Range(0, maxPercent) <= heatUpChance) HeatUp();
        }

        public override void OnClick()
        {
            _isOpen = _isOpen == false ? true : false;
            if (playerGraber.heldObj != null && playerGraber.heldObj.TryGetComponent(out Item item) && item.itemTag == requiredTag)
            {
                if(heatUpLevel > 1) playerGraber.DestroyItem();
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
            if (heatUpLevel == 1) isBroken = false;
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
            if (heatUpLevel == maxHeatUpLevel) isBroken = true;
        }
        
    }
}
