using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class MapPointLocate : MonoBehaviour
{
    private BoxCollider boxCollider;
    public Transform target;
    public Transform player;

    public void AddPointOnMap(Transform en, Transform pl)
    {
        player = pl; target = en; 
        boxCollider = en.GetComponent<BoxCollider>();
    }

    public void DestroyedPoint() 
    {
        gameObject.SetActive(false);
        Destroy(gameObject);
    }


    public void Update()
    {
        if(boxCollider.enabled == true)
        {
            Vector3 fef = player.position - target.position;
            if (fef.x >= 100 || fef.x <= -100 || fef.z >= 100 || fef.z <= -100)
            {
                transform.localPosition = new Vector3(60, 60, 0);
            }
            else
            {

                transform.localPosition = new Vector3(fef.x * 400, fef.z * 400, 0);
            }
        }
        else
        {
            DestroyedPoint();
        }


    }
}
