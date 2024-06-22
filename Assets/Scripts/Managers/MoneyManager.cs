using System;
using TMPro;
using UnityEngine;

namespace Managers
{
    public class MoneyManager : MonoBehaviour
    {
        [Header("Money")] 
        public int money;
        [SerializeField] private int dayIncome;
        [SerializeField] private int workIncome;

        [Header("UI")] 
        public string currencySymbol;
        [SerializeField] private TMP_Text moneyText;
        
        public static MoneyManager instanceMoneyManager;
        
        MoneyManager()
        {
            instanceMoneyManager = this;
        }

        private void Awake()
        {
            UpdateText();
        }

        public void PayForDay()
        {
            money += dayIncome;
            UpdateText();
        }

        public void PayForWork(int income)
        {
            money += income;
            UpdateText();
        }
        
        public void PayForWork()
        {
            money += workIncome;
            UpdateText();
        }

        public void DecreaseMoney(int cost)
        {
            if (!CheckMoney(cost)) return;
            money -= cost;
            UpdateText();
        }

        public bool CheckMoney(int cost)
        {
            return money - cost >= 0;
        }
        

        private void UpdateText()
        {
            moneyText.text = money + currencySymbol;
        }
    }
}
