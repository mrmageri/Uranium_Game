using UnityEngine;

public class IKFootSolver : MonoBehaviour
{
    
    [SerializeField] private Transform body;
    [SerializeField] private float footSpacingX;
    [SerializeField] private float footSpacingZ;
    [SerializeField] private float stepDistance;
    [SerializeField] private float stepHeight;
    
    [SerializeField] private float lerp = 0;
    [SerializeField] private float speed;
    private Vector3 newPosition;
    private Vector3 currentPosition;
    private Vector3 oldPosition;
    private void Update()
    {
        
        transform.position = currentPosition;
        Ray ray = new Ray(body.position + (body.right * footSpacingX) + (body.forward * footSpacingZ), Vector3.down);
        if (Physics.Raycast(ray, out RaycastHit info, 10))
        {
            if (Vector3.Distance(newPosition, info.point) > stepDistance)
            {
                lerp = 0;
                newPosition = info.point;
            }
            //transform.position = info.point;
        }
        if (lerp < 1)
        {
            Vector3 footPosition = Vector3.Lerp(oldPosition, newPosition, lerp);
            footPosition.y += Mathf.Sin(lerp * Mathf.PI) * stepHeight;

            currentPosition = footPosition;
            lerp += Time.deltaTime * speed;
        }
        else
        {
            oldPosition = newPosition;
        }
    }
}
