using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoShoot : MonoBehaviour
{
    [SerializeField] float LimitDistance = 20;
    private ActiveWeapon weapon;
    private CameraControl camera;
    [SerializeField] Transform Enemies;
    private List<Transform> targets;

    private void Awake()
    {
        weapon = GetComponent<ActiveWeapon>();
        foreach(CameraControl cam in GetComponentsInChildren<CameraControl>())
        {
            camera = cam;
        }
        for (int i = 0; Enemies.childCount > i; i++)
        {
            targets.Add(Enemies.GetChild(i));
        }
    }

    void LateUpdate()
    {
        if (weapon.GetWeapon(weapon.activeWeaponIndex).ammoCount > 0)
        {
            
            int st = 5050;

            for (int i = 0; targets.Count > i; i++)
            {
                float Distance = Vector3.Distance(transform.position, targets[i].transform.position);
                if (!Physics.Raycast(transform.position, targets[i].transform.position, LimitDistance, 0))
                {
                    if (st == 5050)
                    {
                        st = i;
                    }
                    else
                    {
                        float DistanceST = Vector3.Distance(transform.position, targets[st].transform.position);
                        if (Distance < DistanceST)
                        {
                            st = i;
                        }

                    }
                }

            }

            if (st != 5050)
            {
                camera.CanUse = false;
                weapon.GetWeapon(weapon.activeWeaponIndex).StartFiring();
                transform.LookAt(targets[st].transform);
            }
            else
            {
                camera.CanUse = true;
            }
        }
        else
        {
            camera.CanUse = true;
        }
       

    }
}
