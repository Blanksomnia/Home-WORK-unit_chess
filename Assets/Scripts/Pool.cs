using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool : MonoBehaviour
{
    public Vector3 MoveTo;

    private void Update()
    {
        transform.position += Vector3.Lerp(transform.position, MoveTo, 1 * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != 2)
        {
            Destroy(gameObject);
        }
    }
}
