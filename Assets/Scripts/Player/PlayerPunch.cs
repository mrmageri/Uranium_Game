using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPunch : MonoBehaviour
{
    public float punchPower;
    public float reachDistance;
    public LayerMask layer;
    [SerializeField] private Transform cameraTransform;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (Physics.Raycast(cameraTransform.position, cameraTransform.forward,out RaycastHit raycastHit ,reachDistance, layer))
            {
                if (raycastHit.transform.TryGetComponent(out Rigidbody objectRb))
                {
                    Vector3 vector3 = (objectRb.gameObject.transform.position - transform.position).normalized;
                    vector3 = new Vector3(vector3.x,vector3.y + 0.25f,vector3.z);
                    objectRb.AddForce(vector3 * punchPower, ForceMode.Impulse);
                }
            }
        }
    }
}
