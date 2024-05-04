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
                if (tick == 100) tick = 0;
                OnTick();
            }
        }

        private void OnTick()
        {
            Player.Player.instancePlayer.DecreaseCoffee(tick);
            foreach (var elem in machines)
            {
                elem.OnTick();
            }
        }
    }
}