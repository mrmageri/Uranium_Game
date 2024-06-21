using Managers;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;

namespace Shop
{
    public class Shop : MonoBehaviour
    {
        [Header("Products")]
        [SerializeField] private ProductScrObj[] products;
        [SerializeField] private Transform productsLayout;
        [SerializeField] private GameObject productObj;
        [Header("Spawn")]
        [SerializeField] private Transform spawnPoint;
        private MoneyManager moneyManager;

        private void Awake()
        {
            moneyManager = MoneyManager.instanceMoneyManager;
            for (int i = 0; i < products.Length; i++)
            {
                GameObject obj = Instantiate(productObj, productsLayout.position, productsLayout.rotation, productsLayout);
                obj.TryGetComponent(out Product pr);
                pr.SetUp(products[i].productName,products[i].cost, products[i].icon, products[i].item, this, i);
            }
        }

        public void Click(int num)
        {
            int cost = products[num].cost;
            if (moneyManager.CheckMoney(cost))
            {
                moneyManager.DecreaseMoney(cost);
                Instantiate(products[num].item, spawnPoint.position, quaternion.identity);
            }
        }
    }
}
