using UnityEngine;

namespace Items
{
    public enum ItemTag
    {
        Null,
        BucketIce,
        BucketEmpty,
        MugEmpty,
        MugCoffee,
        DeadHamster,
        Hamster,
        Hammer,
        BucketUranium
    }
    public abstract class Item : MonoBehaviour
    {
        public ItemTag itemTag;
        public bool hasEffect;
    
        public abstract void OnUse();

        public bool CheckTag(ItemTag currentTag)
        {
            return currentTag == itemTag;
        }
    }
}