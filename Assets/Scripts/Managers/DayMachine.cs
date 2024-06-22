using UnityEngine;

namespace Managers
{
    public abstract class DayMachine : MonoBehaviour
    {
        protected void Awake()
        {
            GameManager.instanceGameManager.AddDayMachine(this);
        }

        public abstract void onDaySwitch();
    }
}