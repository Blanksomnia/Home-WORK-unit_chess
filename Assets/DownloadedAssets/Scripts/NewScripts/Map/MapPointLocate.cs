using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class MapPointLocate : MonoBehaviour
{

    Transform enemy;
    Transform player;

    public void AddPointOnMap(Transform en, Transform pl)
    {
        player = pl; enemy = en; 

    }

    public void DestroyedPoint() 
    {
        gameObject.SetActive(false);
        Destroy(gameObject);
    }


    public void Update()
    {
        if(enemy != null)
        {
            Vector3 fef = player.position - enemy.position;
            if (fef.x >= 100 || fef.x <= -100 || fef.z >= 100 || fef.z <= -100)
            {
                transform.localPosition = new Vector3(60, 60, 0);
            }
            else
            {

                transform.localPosition = new Vector3(fef.x, fef.z, 0);
            }
        }
        else
        {
            DestroyedPoint();
        }


    }
}
