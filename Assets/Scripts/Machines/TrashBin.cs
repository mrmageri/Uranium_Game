using System.Collections.Generic;
using UnityEngine;

namespace Machines
{
    public class TrashBin : MonoBehaviour
    {
        private List<GameObject> objects = new List<GameObject>();
        private void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.CompareTag("canPickUp")) objects.Add(other.gameObject);
        }

        private void OnTriggerExit(Collider other)
        {
            if(other.gameObject.CompareTag("canPickUp")) objects.Remove(other.gameObject);
        }

        public void Clean()
        {
            foreach (var elem in objects)
            {
                Destroy(elem);
            }
            objects.Clear();
        }
    }
}
