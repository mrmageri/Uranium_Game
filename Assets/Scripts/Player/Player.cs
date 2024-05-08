using UnityEngine;

namespace Player
{
    public class Player : MonoBehaviour
    {
        public int coffeeDecreaseDelay = 10;
        
        private int maxCoffee = 15;
        [SerializeField] int currentCoffee = 15;
        [SerializeField] private GameObject light;
        
        public PlayerMovement playerMovement;
        public PlayerRotation playerRotation;
        public PlayerGraber playerGraber;
        public PlayerItemUser playerItemUser;
        
        public static Player instancePlayer;

        Player()
        {
            instancePlayer = this;
        }

        public void DecreaseCoffee(int tick)
        {
            if (tick % coffeeDecreaseDelay != 0) return;
            currentCoffee--;
            if (currentCoffee <= 0)
            {
                //Death
            }
        }

        public void IncreaseCoffee()
        {
            if (currentCoffee + 1 >+ maxCoffee) return;
            
            currentCoffee++;
            
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
    }
}