using UnityEngine;

namespace Player
{
    public class Player : MonoBehaviour
    {
        public PlayerMovement playerMovement;
        public PlayerRotation playerRotation;

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