using UnityEngine;
using UnityEngine.Events;

public class MonsterInteraction : MonoBehaviour
{
   public UnityEvent onInteraction;

   public void InvokeInteraction()
   {
      onInteraction.Invoke();
   }
}
