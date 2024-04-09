using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class SpecialButton : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private PlayerPunch playerPunch;
    [SerializeField] private UnityEvent onDownEvent;

    private void Awake()
    {
        if(TryGetComponent(out Animator anim))
        {
            animator = anim;
        }
    }

    private void OnMouseDown()
    {
        if ((transform.position - playerPunch.transform.position).magnitude - playerPunch.reachDistance <= 0)
        {
            animator.SetTrigger("Down");
            OnDown();
        }
    }

    private void OnDown()
    {
        onDownEvent.Invoke();
    }
}
