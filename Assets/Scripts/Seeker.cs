using UnityEngine;

public class Seeker : MonoBehaviour
{
    [SerializeField] private Transform targetTransform;
    public float moveSpeed = 10f;
    public float rotationSpeed = 5f;

    

    private void FixedUpdate()
    {
        Quaternion _lookRotation = Quaternion.LookRotation((targetTransform.position - transform.position).normalized);
        transform.rotation = Quaternion.Slerp(transform.rotation, _lookRotation, Time.deltaTime * rotationSpeed);
        transform.position = Vector3.MoveTowards(transform.position, targetTransform.position, moveSpeed * Time.deltaTime);
    }
}
