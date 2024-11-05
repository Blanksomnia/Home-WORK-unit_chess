using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RBKinematic
{
    public void RBIsKinematic(GameObject unit, bool k)
    {
        if(k == false)
        {
            unit.GetComponent<Animator>().enabled = false;
        }

        foreach (Rigidbody rb in unit.GetComponentsInChildren<Rigidbody>())
        {
         
            if(rb.gameObject.layer != 10 && rb.gameObject.layer != 11)
                rb.isKinematic = k;
        }

    }


}
