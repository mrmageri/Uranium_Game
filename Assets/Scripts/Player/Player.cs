using System;
using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Player
{
    public class Player : MonoBehaviour
    {
        public int coffeeDecreaseDelay = 3;
        
        public int currentCoffee = 15;
        private int maxCoffee = 15;
        [SerializeField] private new GameObject light;
        [SerializeField] private char coffeeSymbol;
        [SerializeField] private TMP_Text coffeeBar;
        
        public PlayerMovement playerMovement;
        public PlayerRotation playerRotation;
        public PlayerGraber playerGraber;
        public PlayerItemUser playerItemUser;
        
        public static Player instancePlayer;

        private GameManager gameManager;

        Player()
        {
            instancePlayer = this;
        }

        private void Awake()
        {
            gameManager = GameManager.instanceGameManager;
            coffeeBar.text = "";
            for (int i = 0; i < maxCoffee; i++)
            {
                coffeeBar.text += coffeeSymbol;
            }
        }

        public void DecreaseCoffeePerTick(int tick)
        {
            if (tick % coffeeDecreaseDelay != 0) return;
            //if tick / coffeeDecreaseDelay == 0 and player is sprinting, than decrease coffee
            if(currentCoffee - 1 >= 0) currentCoffee--;
            UpdateCoffeeData();
        }

        public void DecreaseCoffeeOnHit(int hits)
        {
            currentCoffee -= hits;
            if (currentCoffee < 0)
            {
                gameManager.EndGame();
            }
            UpdateCoffeeData();
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
            AchievementsManager.achievementsManager.OpenAchievement(1);
            AchievementsManager.achievementsManager.OpenAchievement(2);
            AchievementsManager.achievementsManager.OpenAchievement(3);
            AchievementsManager.achievementsManager.OpenAchievement(4);
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