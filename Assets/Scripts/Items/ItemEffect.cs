using System;
using UnityEngine;

public abstract class ItemEffect : MonoBehaviour
{
    [HideInInspector] public ItemTag requiredTag;

    public void Awake()
    {
        if (TryGetComponent(out Item item))
        {
            requiredTag = item.itemTag;
        }
    }

    public abstract void OnUse();

    public bool CheckTag(ItemTag currentTag)
    {
        return currentTag == requiredTag;
    }
}
