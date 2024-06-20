using UnityEngine;

public class DevTools : MonoBehaviour
{
    public bool isActive;
    public Vector3 tpPoint = new Vector3(0,0,-30);
    private void Update()
    {
        if(!isActive) return;
        if (Input.GetKeyDown(KeyCode.BackQuote)) gameObject.transform.position = tpPoint;
    }
}
