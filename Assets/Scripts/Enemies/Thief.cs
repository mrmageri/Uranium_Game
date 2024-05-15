using Items;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace Enemies
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class Thief : MonoBehaviour
    {
        [SerializeField] private float reachDistance = 1f;
        [SerializeField] private GameObject deathParticle;
        
        [SerializeField] private Transform[] targetPoints;
        [SerializeField] private Transform holdingItemTransform;
        [Header("View")]
        [SerializeField] private LayerMask targetMask;
        [SerializeField] private Vector3 searchCubeSize;
        
        private GameObject currentItem;
        private Transform currentTarget;
        private NavMeshAgent agent;
        private bool foundItem;
        private bool hasItem = false;
        private int targetNumber = 0;
        private void Start()
        {
            currentTarget = targetPoints[targetNumber];
            if (TryGetComponent(out NavMeshAgent ag))
            {
                agent = ag;
            }
            SetTarget();
        }

        private void Update()
        {
            FieldOfItemsCheck();
            if ((transform.position - currentTarget.position).magnitude <= reachDistance)
            {
                if (!foundItem)
                {
                    targetNumber = Random.Range(0, targetPoints.Length);
                    currentTarget = targetPoints[targetNumber];
                    SetTarget();
                }
                else
                {
                    currentItem = currentTarget.gameObject;
                    currentItem.transform.SetParent(holdingItemTransform,false);
                    currentItem.transform.position = holdingItemTransform.transform.position;
                    if (currentItem.TryGetComponent(out Rigidbody rb)) rb.isKinematic = true;
                    if (currentItem.TryGetComponent(out Item item))
                    {
                        item.enabled = false;
                    }
                    if (currentItem.TryGetComponent(out Collider coll)) coll.enabled = false;
                    currentTarget = targetPoints[targetNumber];
                    SetTarget();
                    foundItem = false;
                    hasItem = true;
                }
            }
        }

        public void Death()
        {
            if (Player.Player.instancePlayer.playerGraber.heldObj != null)
            {
                if (Player.Player.instancePlayer.playerGraber.heldObj.TryGetComponent(out Item itemHeld) && itemHeld.isWeapon)
                {
                    
                    if (currentItem != null)
                    {
                        if (currentItem.TryGetComponent(out Rigidbody rb)) rb.isKinematic = false;
                        if (currentItem.TryGetComponent(out Collider coll)) coll.enabled = true;
                        if (currentItem.TryGetComponent(out Item item)) item.enabled = true;
                        currentItem.transform.SetParent(null,false);
                        currentItem.transform.position = transform.position;
                    }

                    hasItem = false;
                    agent.speed /= 2;
                    Instantiate(deathParticle, transform.position, quaternion.identity);
                    Destroy(gameObject);
                }
            }
        }

        private void FieldOfItemsCheck()
        {
            Collider[] rangeChecks = Physics.OverlapBox(transform.position,searchCubeSize,quaternion.identity,targetMask);

            if (rangeChecks.Length != 0)
            {
                if (hasItem || foundItem) return;
                foundItem = true;
                currentTarget = rangeChecks[0].transform;
                SetTarget();
                agent.speed *= 2;
            }
            else if(!hasItem && foundItem)
            {
                currentTarget = targetPoints[targetNumber];
                SetTarget();
                agent.speed /= 2;
                foundItem = false;
            }
        } 
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out MonsterInteraction inter))
            {
                inter.InvokeInteraction();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out MonsterInteraction inter))
            {
                inter.InvokeInteraction();
            }
        }

        private void SetTarget()
        {
            agent.destination = currentTarget.position;
        }
    }
}
