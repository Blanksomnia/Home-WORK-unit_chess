using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject.SpaceFighter;

public class Bonus : MonoBehaviour
{
    ActivateBonus act;
    public PlayerMemory player;
    GameObject Child;
    AudioSource audio;
    BoxCollider box;
    private void Start()
    {
        Child = transform.GetChild(0).gameObject;
        box = GetComponent<BoxCollider>();
        audio = GetComponent<AudioSource>();
        act = GetComponent<ActivateBonus>();
        if (act.active == false)
        {
            Delete();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 3)
        {
            act.active = false;
            audio.Play();
            Detect();
            box.enabled = false;
            Child.SetActive(false);
            StartCoroutine(TimeToDelete());
        }
    }

    public virtual void Detect()
    {

    }

    private IEnumerator TimeToDelete()
    {
        yield return new WaitForSeconds(2);
        Delete();
    }

    private void Delete()
    {
        gameObject.SetActive(false);
        Destroy(gameObject);
    }
}
