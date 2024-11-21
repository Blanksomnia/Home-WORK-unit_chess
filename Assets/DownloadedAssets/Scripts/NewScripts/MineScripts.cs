using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineScripts : MonoBehaviour
{
    [SerializeField] float Radius = 5f;
    [SerializeField] LayerMask unit;
    public int damage = 15;
    private AudioSource audioSource;
    private bool Activator = true;
    private GameObject explosionParticle;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        explosionParticle = Resources.Load("Prefabs/Particles/Explosion", typeof(GameObject)) as GameObject;
    }
    private void Activate()
    {
        if(Activator)
        {
            Activator = false;
            audioSource.Play();
            Collider[] rangeChecks = Physics.OverlapSphere(transform.position, Radius, unit);

            if (rangeChecks.Length > 0)
            {
                for (int i = 0; i < rangeChecks.Length; i++)
                {
                    var hit = rangeChecks[i].transform.GetComponent<Health>();
                    if (hit)
                    {
                        hit.TakeDamage(damage, rangeChecks[i].transform.position - transform.position);
                    }
                }
            }

            Instantiate(explosionParticle, transform.position, transform.rotation).AddComponent<ParticleScript>().Activate(1);
            StartCoroutine(timetoDelete());
        }
       
    }
    IEnumerator timetoDelete()
    {
        yield return new WaitForSeconds(1);
        gameObject.SetActive(false);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        print(other.gameObject.layer);
        Activate();
    }
}
