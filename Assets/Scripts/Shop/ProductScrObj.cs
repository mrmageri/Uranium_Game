using UnityEngine;

namespace Shop
{
    [CreateAssetMenu(fileName = "Product", menuName = "ScriptableObject/Products", order = 0)]
    public class ProductScrObj : ScriptableObject
    {
        public string productName;
        public Sprite icon;
        public int cost;
        public GameObject item;
    }
}