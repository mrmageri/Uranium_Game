using UnityEngine;

namespace Items
{
    public class Coffee : Item
    {
        [SerializeField]private GameObject emptyMug;

        public override void OnUse()
        {
            Player.Player.instancePlayer.IncreaseCoffee();
            Player.Player.instancePlayer.playerGraber.ReplaceItem(emptyMug);
        }
    }
}
