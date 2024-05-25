using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Items
{
    public class Box : Item
    {
        [SerializeField] private GameObject[] possibleItems;
        [SerializeField] private float punchForce;
        [SerializeField] private GameObject particle;

        public override void OnUse()
        {
            Instantiate(particle, transform.position, quaternion.identity);
            GameObject currentObj = Instantiate(possibleItems[Random.Range(0, possibleItems.Length)], transform.position, Quaternion.identity);
            if (currentObj.TryGetComponent(out Rigidbody rb))
            {
                Vector3 force = transform.forward * punchForce;
                rb.AddForce(force, ForceMode.VelocityChange);
            }
            Player.Player.instancePlayer.playerGraber.DestroyItem();
        }
    }
}
