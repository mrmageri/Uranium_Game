using System;
using Machines;
using UnityEngine;

namespace DefaultNamespace
{
    public class TickManager : MonoBehaviour
    {
        private const float tickTimerMax = 1f;
        private int tick;
        private float tickTimer;
        [SerializeField] private Machine[] machines;

        private void Awake()
        {
            tick = 0;
        }

        private void Update()
        {
            tickTimer += Time.deltaTime;
            if (tickTimer >= tickTimerMax)
            {
                tickTimer -= tickTimerMax;
                tick++;
                OnTick();
            }
        }

        private void OnTick()
        {
            foreach (var elem in machines)
            {
                elem.OnTick();
            }
        }
    }
}