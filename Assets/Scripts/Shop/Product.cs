using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Shop
{
    public class Product : MonoBehaviour
    {
        [SerializeField] private TMP_Text nameText;
        [SerializeField] private Image image;
        [SerializeField] private GameObject item;
        [SerializeField] private Interactable interactable;
        
        public void SetUp(string productName, int cost, Sprite sprite, GameObject newItem,Shop thisShop, int num)
        {
            Shop shop = thisShop;
            nameText.text = productName + " " + cost + "$";
            image.sprite = sprite;
            item = newItem;
            interactable.onDownEvent.AddListener(() => { shop.Click(num);});
            interactable.onUpEvent.AddListener(() => { shop.Click(num);});
        }
    }
}