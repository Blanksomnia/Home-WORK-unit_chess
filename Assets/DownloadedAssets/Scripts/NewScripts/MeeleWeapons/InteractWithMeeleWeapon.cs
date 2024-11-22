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
            StartCoroutine(IntervalToAttack());
        }
     
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
            Collider[] rangeChecks = Physics.OverlapSphere(transform.position, 1.5f, character);

            if (rangeChecks.Length > 0)
            {
                print(rangeChecks[0].gameObject.name);
                Health healthchar = rangeChecks[0].gameObject.GetComponent<Health>();

                if(healthchar.currentHealth > 0)
                DamageUnits(healthchar);

            }
       
    }

}
