using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionWithTools : MonoBehaviour
{
    ParametresUnit _parametresUnit;
    Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
       _parametresUnit =  gameObject.GetComponent<ParametresUnit>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject Tool = collision.gameObject;
        ToolParametres toolParametres = Tool.GetComponent<ToolParametres>();

        if (toolParametres)
        {

            if (Tool.layer == 6 && _parametresUnit._health + toolParametres._health <= _parametresUnit._maxHealth)
            {
                _parametresUnit.GiveHealth(toolParametres._health);
                Destroy(Tool);
            }

            if (Tool.layer == 7)
            {

                if (_parametresUnit._gun == null)
                {
                    _animator.Play("Gathering Objects");

                    _parametresUnit.GiveGun(toolParametres);
                    
                }
            }
        }

        

    }

}
