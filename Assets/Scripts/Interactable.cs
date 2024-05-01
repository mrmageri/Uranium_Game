using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Interactable : MonoBehaviour
{
    [SerializeField] private float reachDistance = 2f;
    private Player.Player player;
    [SerializeField] private UnityEvent onDownEvent;
    [SerializeField] private UnityEvent onUpEvent;
    private bool _isDown;

    private void Awake()
    {
        player = Player.Player.instancePlayer;
    }

    private void OnMouseDown()
    {
        if ((transform.position - player.transform.position).magnitude - reachDistance <= 0)
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
