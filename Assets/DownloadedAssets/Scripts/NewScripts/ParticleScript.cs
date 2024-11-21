using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleScript : MonoBehaviour
{
    private WaitForSeconds _wait;
    public void Activate(float Time)
    {
        _wait = new WaitForSeconds(Time);
        StartCoroutine(Delete());        
    }

    IEnumerator Delete()
    {
        yield return _wait;
        gameObject.SetActive(false);
        Destroy(gameObject);
    }
}
