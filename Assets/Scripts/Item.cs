using UnityEngine;

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
public class Item : MonoBehaviour
{
    public ItemTag itemTag;
}
