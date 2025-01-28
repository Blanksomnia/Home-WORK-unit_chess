using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetOnMapLocate : MonoBehaviour
{

    public void CreatePoint(Transform pl, GameObject pointPr, Transform Parent)
    {
        Instantiate(pointPr, Parent).AddComponent<MapPointLocate>().AddPointOnMap(transform, pl);
    }


}
