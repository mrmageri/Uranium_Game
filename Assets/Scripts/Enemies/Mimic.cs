using UnityEngine;
using UnityEngine.AI;

namespace Enemies
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class Mimic : MonoBehaviour
    {
        [SerializeField] private LayerMask targetMask;
        [SerializeField] private float toTargetDelta;
        [SerializeField] private int moveChance;
        private GameManager gameManager;
        private Transform[] wanderingTransforms;
        private GameObject playerGameObject;
        private int currentPoint;

        [Header("Player Check Sphere")] 
        [SerializeField] private float sphereRadius;
        private NavMeshAgent agent;
        private bool isMoving = false;
        private bool isAngry = false;

        private void Awake()
        {
            if (TryGetComponent(out NavMeshAgent navMeshAgent)) agent = navMeshAgent;
            gameManager = GameManager.instanceGameManager;
            playerGameObject = Player.Player.instancePlayer.gameObject;
            wanderingTransforms = new Transform[gameManager.wanderingPoints.Length];
            for (int i = 0; i < gameManager.wanderingPoints.Length; i++)
            {
                wanderingTransforms[i] = gameManager.wanderingPoints[i];
            }
            
            agent.isStopped = true;
            SetRandomTarget();
        }

        private void Update()
        {
            
            if (isAngry)
            {
                agent.destination = playerGameObject.transform.position;
                return;
            }
            
            if (!isMoving)
            {
                isMoving = Random.Range(1, moveChance + 1) == 1;
                if (isMoving)
                {
                    SetRandomTarget();
                    agent.isStopped = false;
                }
            }
            else
            {
                if (CheckPlayerAround())
                {
                    if(agent.isStopped == false) SetRandomTarget();
                    agent.isStopped = true;
                    //return;
                }
                else
                {
                    agent.isStopped = false;
                }

                if ((transform.position - wanderingTransforms[currentPoint].position).magnitude <= toTargetDelta)
                {
                    agent.isStopped = true;
                    isMoving = false;
                }
            }
            //agent.isStopped = false;

        }

        public void SetAngry()
        {
            if(isAngry) return; 
            isAngry = true;
            agent.isStopped = false;
        }
        
        public void SetSleepy()
        {
            isAngry = false;
            agent.isStopped = true;
            isMoving = false;
        }

        private void SetRandomTarget()
        {
            currentPoint = Random.Range(0, wanderingTransforms.Length);
            agent.destination = wanderingTransforms[currentPoint].position;
        }
        private bool CheckPlayerAround()
        {
            Collider[] rangeChecks = Physics.OverlapSphere(transform.position, sphereRadius, targetMask);
            return (rangeChecks.Length > 0);
        }
    }
}
