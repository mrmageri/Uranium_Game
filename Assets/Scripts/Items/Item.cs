using UnityEditor;
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
        [Header("Every Item")]
        public ItemTag itemTag;
        public bool hasEffect;
        public bool isWeapon;
        [Header("If is Weapon")]
        public int damage;

        public abstract void OnUse();

        public bool CheckTag(ItemTag currentTag)
        {
            return currentTag == itemTag;
        }
    }

}