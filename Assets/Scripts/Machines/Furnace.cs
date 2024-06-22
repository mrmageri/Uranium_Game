using Managers;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class Furnace : DayMachine
{
    [SerializeField] private int minPrice;
    [SerializeField] private int maxPrice;
    [SerializeField] private string itemTag;
    [SerializeField] private GameObject particle;
    private int _currentPrice = 1;

    [Header("UI")] 
    [SerializeField]private TMP_Text priceText;

    private MoneyManager moneyManager;

    private void Start()
    {
        moneyManager = MoneyManager.instanceMoneyManager;
        _currentPrice = 1;
        priceText.text = _currentPrice + moneyManager.currencySymbol;
    }
    
    public override void onDaySwitch()
    {
        SetRandomPrice();
    }

    private void SetRandomPrice()
    {
        _currentPrice = Random.Range(minPrice, maxPrice + 1);
        priceText.text = _currentPrice + moneyManager.currencySymbol;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(itemTag))
        {
            moneyManager.PayForWork(_currentPrice);
            Instantiate(particle, other.transform.position, quaternion.identity);
            Destroy(other.gameObject);
        }
    }
}
