using UnityEngine;
using UnityEngine.AI;

namespace AI
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class Thief : MonoBehaviour
    {
        [SerializeField] private float reachDistance = 1f;
        
        [SerializeField] private Transform[] targetPoints;
        private Transform currentTarget;
        private NavMeshAgent agent;
        private bool foundItem;
        private int targetNumber = 0;
        private void Start()
        {
            currentTarget = targetPoints[targetNumber];
            if (TryGetComponent(out NavMeshAgent ag))
            {
                agent = ag;
            }
            agent.destination = currentTarget.position;
        }

        private void Update()
        {
            if ((transform.position - currentTarget.position).magnitude <= reachDistance)
            {
                if (!foundItem)
                {
                   /* if (targetNumber + 1 >= targetPoints.Length)
                    {
                        targetNumber = 0;
                    }
                    else
                    {
                        targetNumber++;
                    }*/
                    targetNumber = ((targetNumber + 1) >= targetPoints.Length) ? 0 : targetNumber + 1;
                    currentTarget = targetPoints[targetNumber];
                    SetTarget();
                }
                else
                {
                    Destroy(currentTarget.gameObject);
                }
            }
        }

        private void SetTarget()
        {
            agent.destination = currentTarget.position;
        }
    }
}
