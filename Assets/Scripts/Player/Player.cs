using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Player
{
    public class Player : MonoBehaviour
    {
        public int coffeeDecreaseDelay = 10;
        
        private int maxCoffee = 15;
        [SerializeField] int currentCoffee = 15;
        [SerializeField] private new GameObject light;
        [SerializeField] private char coffeeSymbol;
        [SerializeField] private TMP_Text coffeeBar;
        
        public PlayerMovement playerMovement;
        public PlayerRotation playerRotation;
        public PlayerGraber playerGraber;
        public PlayerItemUser playerItemUser;
        
        public static Player instancePlayer;

        Player()
        {
            instancePlayer = this;
        }

        private void Awake()
        {
            coffeeBar.text = "";
            for (int i = 0; i < maxCoffee; i++)
            {
                coffeeBar.text += coffeeSymbol;
            }
        }

        public void DecreaseCoffee(int tick)
        {
            if (tick % coffeeDecreaseDelay != 0) return;
            currentCoffee--;
            UpdateCoffeeData();
            if (currentCoffee <= 0)
            {
                //Death
            }
        }

        public void IncreaseCoffee()
        {
            if (currentCoffee + 5 >= maxCoffee)
            {
                currentCoffee = maxCoffee;
            }
            else
            {
                currentCoffee+=5;
            }
            UpdateCoffeeData();
        }

        public void ResetCoffee()
        {
            currentCoffee = maxCoffee;
            UpdateCoffeeData();
        }
        public void PlayerLight(bool activate)
        {
            light.SetActive(activate);
        }

        public void StopPlayer()
        {
            playerRotation.enabled = false;
            playerMovement.enabled = false;
        }
        
        public void ActivatePlayer()
        {
            playerRotation.enabled = true;
            playerMovement.enabled = true;
        }

        private void UpdateCoffeeData()
        {
            string coffee = "";
            for (int i = 0; i < currentCoffee; i++)
            {
                coffee += coffeeSymbol;
            }
            coffeeBar.text = coffee;
        }
    }
}