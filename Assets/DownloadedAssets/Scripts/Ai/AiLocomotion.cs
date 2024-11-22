using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AiLocomotion : MonoBehaviour
{
    NavMeshAgent agent;
    Animator animator;
    AudioSource audio;

    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (agent.hasPath) {
            animator.SetFloat("speed", agent.velocity.magnitude);
            if (agent.velocity.magnitude > 0)
            {
                audio.mute = false;  
            }
            else
            {
                audio.mute = true;
            }
        } else {
            animator.SetFloat("speed", 0);
            audio.mute = true;
        }
    }
}
