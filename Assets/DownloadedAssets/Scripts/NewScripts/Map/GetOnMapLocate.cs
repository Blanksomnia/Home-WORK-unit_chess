using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetOnMapLocate : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] GameObject PrefabPont;
    [SerializeField] Transform Points;

    private void Start()
    {
        for (int i = 0; transform.childCount > i; i++)
        {
            Instantiate(PrefabPont, Points).AddComponent<MapPointLocate>().AddPointOnMap(transform.GetChild(i), player);
        }
    }


}
