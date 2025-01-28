using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetProperties : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] GameObject PrefabPont;
    [SerializeField] Transform Points;

    void Awake()
    {
        foreach(GetOnMapLocate point in gameObject.GetComponentsInChildren<GetOnMapLocate>())
        {
            point.CreatePoint(player, PrefabPont, Points);
        }
    }


}
