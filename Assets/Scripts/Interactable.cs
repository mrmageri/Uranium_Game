using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Interactable : MonoBehaviour
{
    [SerializeField] private float reachDistance;
    [SerializeField] private PlayerPunch playerPunch;
    [SerializeField] private UnityEvent onDownEvent;
    [SerializeField] private UnityEvent onUpEvent;
    private bool _isDown;

    private void Awake()
    {
    }

    private void OnMouseDown()
    {
        if ((transform.position - playerPunch.transform.position).magnitude - reachDistance <= 0)
        {
            if (!_isDown)
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
        _isDown = true;
    }

    private void OnUp()
    {
        onUpEvent.Invoke();
        _isDown = false;
    }
}
