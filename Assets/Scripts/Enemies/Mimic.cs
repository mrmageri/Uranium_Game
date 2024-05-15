using System;
using System.Collections;
using Items;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.AI;

namespace Enemies
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class Mimic : MonoBehaviour
    {
        [SerializeField] private int health;
        [SerializeField] private LayerMask targetMask;
        [SerializeField] private float toTargetDelta;
        [SerializeField] private float hitDelay;
        [SerializeField] private int damage;
        [SerializeField] private GameObject deathParticle;
        private GameObject playerGameObject;
        private Player.Player player;
        private int currentPoint;

        [Header("Player Check Sphere")] 
        [SerializeField] private float sphereRadius;
        private NavMeshAgent agent;
        private bool isAngry = false;
        private bool hitOnDelay = false;

        private void Awake()
        {
            if (TryGetComponent(out NavMeshAgent navMeshAgent)) agent = navMeshAgent;
            player = Player.Player.instancePlayer;
            playerGameObject = player.gameObject;
            agent.isStopped = true;
        }

        private void Update()
        {
            if (!CheckPlayerAround()) SetSleepy();

            if (isAngry)
            {
                agent.destination = playerGameObject.transform.position;
            }

            if ((transform.position - playerGameObject.transform.position).magnitude <= toTargetDelta && !hitOnDelay && isAngry)
                StartCoroutine(Hit());
        }

        public void Damaged()
        {
            if (player.playerGraber.heldObj != null)
            {
                if (player.playerGraber.heldObj.TryGetComponent(out Item itemHeld) && itemHeld.isWeapon)
                {
                    health -= itemHeld.damage;
                    agent.speed /= 2;
                    Instantiate(deathParticle, transform.position, quaternion.identity);
                    if(health <= 0) Destroy(gameObject);
                }
            }
        }

        public void SetAngry()
        {
            if(isAngry) return; 
            isAngry = true;
            agent.isStopped = false;
        }
        
        private void SetSleepy()
        {
            isAngry = false;
            agent.isStopped = true;
        }

        private IEnumerator Hit()
        {
            player.DecreaseCoffee(damage);
            hitOnDelay = true;
            yield return  new WaitForSeconds(hitDelay);
            hitOnDelay = false;
        }
        private bool CheckPlayerAround()
        {
            Collider[] rangeChecks = Physics.OverlapSphere(transform.position, sphereRadius, targetMask);
            return (rangeChecks.Length > 0);
        }
    }
}
