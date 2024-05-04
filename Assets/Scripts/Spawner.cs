using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject spawnItem;
    [SerializeField] private Transform spawnTransform;

    public void Spawn()
    {
        Instantiate(spawnItem, spawnTransform.position, quaternion.identity);
    }
}
