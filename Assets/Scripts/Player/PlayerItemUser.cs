using Items;
using UnityEngine;

namespace Player
{
    public class PlayerItemUser : MonoBehaviour
    {
        private Item currentItem;
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
            if (currentObj.TryGetComponent(out Item item))
            {
                currentTag = item.itemTag;
                currentItem = item;
            }
            else
            {
                return;
            }
            if(currentItem.CheckTag(currentTag) && currentItem.hasEffect) currentItem.OnUse();
        }
    }
}