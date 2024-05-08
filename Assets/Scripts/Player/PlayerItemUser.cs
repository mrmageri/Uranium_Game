using System;
using UnityEngine;

namespace Player
{
    public class PlayerItemUser : MonoBehaviour
    {
        private ItemEffect currentItemEffect;
        private ItemTag currentTag;
        private GameObject currentObj;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Mouse0)) UseCurrentItem();
        }

        private void UseCurrentItem()
        {
            currentObj = Player.instancePlayer.playerGraber.heldObj;
            if (currentObj == null) return;
            if (currentObj.TryGetComponent(out Item item) && currentObj.TryGetComponent(out ItemEffect effect))
            {
                currentTag = item.itemTag;
                currentItemEffect = effect;
            }
            else
            {
                return;
            }
            if(currentItemEffect.CheckTag(currentTag)) currentItemEffect.OnUse();
        }
    }
}