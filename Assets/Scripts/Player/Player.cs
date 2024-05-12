using System;
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
        [SerializeField] private Slider coffeeBar;
        
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
            coffeeBar.maxValue = maxCoffee;
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
            if (currentCoffee + 5 >+ maxCoffee) return;
            
            currentCoffee++;
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
            coffeeBar.value = currentCoffee;
        }
    }
}