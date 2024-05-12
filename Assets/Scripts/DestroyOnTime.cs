using System.Collections;
using UnityEngine;

public class DestroyOnTime : MonoBehaviour
{
    public float time;

    private void Start()
    {
        StartCoroutine(DestroyDelay());
    }

    private IEnumerator DestroyDelay()
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
