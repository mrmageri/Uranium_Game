using UnityEditor;
using UnityEngine;

public enum ItemTag
{
    Null,
    BucketIce,
    BucketEmpty,
    MugEmpty,
    MugCoffee
}
public class Item : MonoBehaviour
{
    public ItemTag itemTag;
}
