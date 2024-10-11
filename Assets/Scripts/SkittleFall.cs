using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkittleFall : MonoBehaviour
{
    public skittle _skittle;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        _skittle.IsFellen = false;
    }

    private void OnCollisionExit(Collision collision)
    {
        _skittle.IsFellen = true;
    }
}
