using UnityEngine;

public enum ItemTag
{
    Null = 0,
    IceBag = 1,
    EmptyIceBag = 2
}
public class Item : MonoBehaviour
{
    public ItemTag itemTag;
}