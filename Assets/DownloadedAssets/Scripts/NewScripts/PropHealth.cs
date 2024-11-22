using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropHealth : Health
{
    public GameObject particleDamage;
    AudioSource audioCrash;

    protected override void OnStart()
    {
        audioCrash = GetComponent<AudioSource>();
        particleDamage = Resources.Load("Prefabs/Particles/Prop", typeof(GameObject)) as GameObject;
    }

    protected override void OnDeath(Vector3 direction)
    {
        audioCrash.Play();
        StartCoroutine(TimeToDelete());
    }

    IEnumerator TimeToDelete()
    {
        yield return new WaitForSeconds(2);
        gameObject.SetActive(false);
        Destroy(gameObject);
    }

    protected override void OnDamage(Vector3 direction)
    {
        Instantiate(particleDamage, transform.position, transform.rotation).AddComponent<ParticleScript>().Activate(2);
    }
}
