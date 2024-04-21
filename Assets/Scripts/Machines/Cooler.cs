using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Machines
{
    public class Cooler : Machine
    {
        [SerializeField] private string animIntName;
        [SerializeField] private GameObject[] smokeObjs;


        private Animator animator;

        private float maxPercent = 1;
        private float heatUpChance = 0.00625f;
        private int heatUpLevel = 1;
        private int maxHeatUpLevel = 4;

        private void Awake()
        {
            if(TryGetComponent(out Animator anim_tor))
            {
                animator = anim_tor;
            }
        }

        public override void OnTick()
        {
            if (Random.Range(0, maxPercent) <= heatUpChance) HeatUp();
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
