using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Return : MonoBehaviour
{
    private NavMeshAgent _navMeshAsent;
    Animator _animator;
    bool _activate = false;
    bool Finished = false;
    // Start is called before the first frame update
    void Start()
    {
        _navMeshAsent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
    }

    public void Activate()
    {
        _animator.Play("Blend Tree");
        _activate = true;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (_activate == true && Finished == false)
        {
            _animator.SetFloat("RUN", _navMeshAsent.velocity.magnitude);
            _navMeshAsent.destination = new Vector3(0, 0.062f, 0);
            if(_navMeshAsent.stoppingDistance != 0 )
            {
                _navMeshAsent.stoppingDistance = 0;
            }
            if (transform.position.x <= 0.1f && transform.position.y <= 0.1f)
            {
                _animator.Play("Joyful");
                Finished = true;
            }
        }

    }
}
