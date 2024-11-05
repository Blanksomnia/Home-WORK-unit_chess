using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngineInternal;
using static Cinemachine.CinemachineTargetGroup;
using static UnityEngine.GraphicsBuffer;

public class InteractWithGun : MonoBehaviour
{
    [SerializeField] int LayerTarget;
    [SerializeField] GameObject PrefabPool;
    [SerializeField] Transform posHead;
    Animator _anim;
    [SerializeField] float _timeToShoot;
    [SerializeField] float _limitDistance;
    ParametresUnit _unit;
    private bool _toShoot = true;
    private WaitForSeconds _wait;

    private void Start()
    {
        _anim = GetComponent<Animator>();
        _unit = gameObject.GetComponent<ParametresUnit>();
        _wait = new WaitForSeconds(_timeToShoot);
    }


    public void Shooting(Vector3 posPool)
    {
        if(_toShoot && _unit._gun != null)
        {

            _anim.Play("Gunplay");
           _toShoot = false;
           Vector3 posUnit = posHead.position;
           Vector3 Direction = (posPool - posUnit).normalized;

           RaycastHit[] f = Physics.RaycastAll(posUnit, Direction);
            Debug.DrawLine(posUnit, posPool, Color.red);
            int st = 0;
            GameObject target;
           for (int i = 0; i < f.Length; i++)
           {
                float Distance = Vector3.Distance(f[i].transform.position, posUnit);
                float Distst = Vector3.Distance(f[st].transform.position, posUnit);
                if (f[i].collider != null)
                {
                    

                    if (i != 0)
                    {
                        if (Distance < Distst && f[i].collider.gameObject.layer != 2)
                        {
                            st = i;
                        }
                    }
                    else
                    {
                        st = 0;
                    }
                }
               
           }
           target = f[st].collider.gameObject;

            if (target.layer == LayerTarget)
            {
                target.GetComponent<ParametresUnit>().GiveHealth(-_unit.Gun._damage);
            }

            StartCoroutine(ShootTime());
            
        }
    }
    IEnumerator ShootTime()
    {
        yield return _wait;
        _toShoot = true;
    }



    

}
