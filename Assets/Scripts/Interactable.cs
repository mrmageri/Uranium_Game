using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class Interactable : MonoBehaviour
{
    [SerializeField] private float reachDistance = 2f;
    private Player.Player player;
    [SerializeField] private UnityEvent onDownEvent;
    [SerializeField] private UnityEvent onUpEvent;
    private bool isOn;

    private void Awake()
    {
        player = Player.Player.instancePlayer;
    }
    private void OnMouseDown()
    {
        if ((transform.position - player.transform.position).magnitude - reachDistance <= 0)
        {
            if (!isOn)
            {
                OnDown();
            }
            else
            {
                OnUp();
            }
        }
    }

    private void OnDown()
    {
        onDownEvent.Invoke();
        isOn = true;
    }

    private void OnUp()
    {
        onUpEvent.Invoke();
        isOn = false;
    }
}
