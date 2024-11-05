using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPosit : MonoBehaviour
{
    [SerializeField] private GameObject PosFirsFace;
    [SerializeField] private GameObject PosThirtFace;

    bool F = true;

    private void Start()
    {
        PosThirtFace.SetActive(true);
        PosFirsFace.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F5))
        {
            if (F)
            {
                PosFirsFace.SetActive(true);
                PosThirtFace.SetActive(false);
                F = false;
            }
            else
            {
                F = true;
                PosThirtFace.SetActive(true);
                PosFirsFace.SetActive(false);
                
            }
        }
    }
}
