using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoShoot : MonoBehaviour
{
    [SerializeField] float LimitDistance = 20;
    private CharacterAiming charct;
    private ActiveWeapon weapon;
    [SerializeField] Transform Enemies;
    private List<Transform> targets;

    private void Awake()
    {
        charct = GetComponent<CharacterAiming>();
        weapon = GetComponent<ActiveWeapon>();
        foreach(Transform t in Enemies.GetComponentInChildren<Transform>())
        {
                targets.Add(t);
        }

    }

    void LateUpdate()
    {
        if (weapon.GetWeapon(weapon.activeWeaponIndex).ammoCount > 0)
        {
            int st = 5050;

            if(targets.Count > 0)
            for (int i = 0; targets.Count > i; i++)
            {
                float Distance = Vector3.Distance(transform.position, targets[i].transform.position);
                Debug.DrawRay(transform.position, targets[i].transform.position, Color.red);
                if (!Physics.Raycast(transform.position, targets[i].transform.position, Distance, 0))
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

            if (st != 5050 && Vector3.Distance(transform.position, targets[st].transform.position) <= LimitDistance)
            {
                print("detect");
                charct.enabled = false;
                weapon.GetWeapon(weapon.activeWeaponIndex).StartFiring();
                transform.LookAt(targets[st].transform);
            }
            else
            {
                charct.enabled = true;
            }
        }
        else
        {
            charct.enabled = true;
        }


    }
}
