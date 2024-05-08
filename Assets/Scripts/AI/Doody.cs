using UnityEngine;

public class Doody : MonoBehaviour
{
    
    private Transform playerTransform;
    [SerializeField] private float maxDistance;
    [SerializeField] private float minDistance;
    [SerializeField] private float maxDistanceDelta = .01f;
    [SerializeField] private float maxHeight = 0.5f;

    private void Awake()
    {
        playerTransform = Player.Player.instancePlayer.transform;
    }


    private void Update()
    {
        
        Vector3 dir = playerTransform.position - transform.position;
        Quaternion lookRot = Quaternion.LookRotation(dir);
        lookRot.x = 0; lookRot.z = 0;
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRot, Mathf.Clamp01(3.0f * Time.maximumDeltaTime));

        if ((playerTransform.position - transform.position).magnitude > maxDistance)
        {
            Vector3 target = new Vector3(playerTransform.position.x,transform.position.y,playerTransform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, target, maxDistanceDelta);
        }

        if ((playerTransform.position - transform.position).magnitude < minDistance)
        {
            Vector3 target = new Vector3(-playerTransform.position.x,transform.position.y,-playerTransform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, target, maxDistanceDelta);
        }
        
    }

}
