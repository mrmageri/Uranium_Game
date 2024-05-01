using System;
using UnityEngine;

public class ReturnArea : MonoBehaviour
{
    [SerializeField] private Transform returnPoint;
    private void OnTriggerEnter(Collider other)
    {
        other.transform.position = returnPoint.position;
    }
}
