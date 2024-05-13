
using UnityEngine;
using UnityEngine.Events;

public class TriggerArea : MonoBehaviour
{
    public LayerMask requiredLayer;
    public UnityEvent onEnter;
    private void OnTriggerEnter(Collider other)
    {
        if(((1<<other.gameObject.layer) & requiredLayer) != 0) onEnter.Invoke();
    }
}
