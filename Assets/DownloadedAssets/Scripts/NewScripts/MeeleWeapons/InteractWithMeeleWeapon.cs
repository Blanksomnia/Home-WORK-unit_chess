using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractWithMeeleWeapon : MonoBehaviour
{
    private Animator _anim;
    private MeeleWeaponParametres _weapon;
    [SerializeField] float Interval = 2f;
    private WaitForSeconds _wait;
    private WaitForSeconds _interval;
    [SerializeField] private LayerMask character;
    [SerializeField] private GameObject _unit;
    private Transform _posStart;
    private Transform _posEnd;

    bool _active = true;

    private void Start()
    {
        _weapon = GetComponent<MeeleWeaponParametres>();
        _interval = new WaitForSeconds(Interval);
        _wait = new WaitForSeconds(1.7f);
        
        _anim = _unit.GetComponent<Animator>();
    }

    public void Attack()
    {
        if(_active)
        {
             _active = false;
            _anim.Play("Slash");
            Target();
            StartCoroutine(PlayToAnim());
            StartCoroutine(IntervalToAttack());
        }
     
    }

    IEnumerator PlayToAnim()
    {
        yield return _wait;
        _anim.Play("Blend Tree");
    }

    IEnumerator IntervalToAttack()
    {
        yield return _interval;
        _active = true;
    }
    private void DamageUnits(Health _unit)
    {
        _unit.TakeDamage(_weapon._damage, _unit.transform.position - transform.position);
    }

    private void Target()
    {
            Collider[] rangeChecks = Physics.OverlapSphere(transform.position, 5, character);

            if (rangeChecks.Length > 0)
            {
                print(rangeChecks[0].gameObject.name);
                if (Physics.Linecast(_posStart.position, rangeChecks[0].transform.position, character))
                {

                    DamageUnits(rangeChecks[0].gameObject.GetComponent<HitBox>().health);
                }


            }
       
    }

}
