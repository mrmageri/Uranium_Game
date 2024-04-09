using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class Turnip : MonoBehaviour
{
    [SerializeField] private Transform targetTransform;
    [SerializeField] private Transform playerTransform;
    public float moveSpeed = 10f;
    public float rotationSpeed = 5f;

    

    private void FixedUpdate()
    {
        Quaternion _lookRotation = Quaternion.LookRotation((playerTransform.position - transform.position).normalized);
        transform.rotation = Quaternion.Slerp(transform.rotation, _lookRotation, Time.deltaTime * rotationSpeed);
        transform.position = Vector3.MoveTowards(transform.position, targetTransform.position, moveSpeed * Time.deltaTime);
    }
}
