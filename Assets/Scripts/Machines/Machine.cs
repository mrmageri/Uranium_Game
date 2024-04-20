using UnityEngine;

namespace Machines
{
    public abstract class Machine : MonoBehaviour
    {
        public bool isBroken;

        public bool GetState()
        {
            return isBroken;
        }

        public abstract void OnTick();
    }
}