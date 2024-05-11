using Items;
using UnityEngine;

namespace Machines
{
    public class Freezer : MonoBehaviour
    {
        [SerializeField] private ItemTag requiredTag;
        [SerializeField] private GameObject fullIceBucket;
        private PlayerGraber playerGraber;
        private void Awake()
        {
            playerGraber = Player.Player.instancePlayer.playerGraber;
        }
        
        private bool _isOpen = false;

        public  void OnClick()
        {
            _isOpen = _isOpen == false ? true : false;
            if (playerGraber.heldObj != null && playerGraber.heldObj.TryGetComponent(out Item item) && item.itemTag == requiredTag)
            {
                playerGraber.DestroyItem();
                playerGraber.GiveItem(fullIceBucket);
            }
        }
    }
}