using UnityEditor;
using UnityEngine;

namespace Machines
{
    public abstract class Machine : MonoBehaviour
    {
        [HideInInspector] public bool isBroken;
        protected PlayerGraber playerGraber;
        [SerializeField] protected ItemTag requiredTag;

        private void Awake()
        {
            playerGraber = Player.Player.instancePlayer.playerGraber;
        }
        public bool GetState()
        {
            return isBroken;
        }

        public abstract void OnClick();

        public abstract void OnTick();
    }
    
}