using UnityEditor;
using UnityEngine;

namespace Machines
{
    public abstract class Machine : MonoBehaviour
    {
        [HideInInspector] public bool isBroken;
        protected Player.Player player;
        protected PlayerGraber playerGraber;
        [SerializeField] protected ItemTag requiredTag;

        private void Awake()
        {
            player = Player.Player.instancePlayer;
            playerGraber = player.playerGraber;
        }
        public bool GetState()
        {
            return isBroken;
        }

        public abstract void OnClick();

        public abstract void OnTick();
        
        protected void SetWorking()
        {
            isBroken = false;
            Computer.instanceComputer.UpdateWorkingMachinesNumber();
        }

        protected void SetBroken()
        {
            isBroken = true;
            Computer.instanceComputer.UpdateWorkingMachinesNumber();
        }
    }
    
}