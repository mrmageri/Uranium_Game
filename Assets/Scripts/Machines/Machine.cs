using System;
using Items;
using Managers;
using Player;
using UnityEngine;

namespace Machines
{
    public abstract class Machine : MonoBehaviour
    {
        [HideInInspector] public bool isBroken;
        public int income = 0;
        protected Player.Player player;
        protected PlayerGraber playerGraber;
        [SerializeField] protected ItemTag requiredTag;
        private MoneyManager moneyManager;
        

        protected void Awake()
        {
            player = Player.Player.instancePlayer;
            playerGraber = player.playerGraber;
            moneyManager = MoneyManager.instanceMoneyManager;
            TickManager.instanceTickManager.AddMachine(this);
        }

        protected void Start()
        {
            TickManager.instanceTickManager.AddMachine(this);
        }

        public bool GetState()
        {
            return isBroken;
        }

        public abstract void OnClick();

        public abstract void OnTick();

        public abstract void Reset();

        public abstract void ResetBroken();

        public void SetWorking()
        {
            isBroken = false;
            Computer.instanceComputer.UpdateWorkingMachinesNumber();
            if (income == 0)
            {
                moneyManager.PayForWork();
            }
            else
            {
                moneyManager.PayForWork(income);
            }
        }

        public void SetBroken()
        {
            isBroken = true;
            Computer.instanceComputer.UpdateWorkingMachinesNumber();
        }
    }
    
}