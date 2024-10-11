using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UIElements;
using static UnityEditor.FilePathAttribute;

public class skittle : MonoBehaviour
{
    [SerializeField] Score _score;
    private bool GiveScore = true;
    Quaternion rotate = new Quaternion();
    Vector3 Pos;
    Rigidbody rigidbody;
    [HideInInspector]
    public bool Reset;
    public bool IsFellen = false;
    public int Rotation;

    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        Pos = gameObject.transform.position;
        rotate = transform.rotation;

        audioSource = gameObject.transform.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Reset == true)
        {
            rigidbody.isKinematic = true;
            gameObject.transform.position = Pos;
            gameObject.transform.rotation = rotate;
            rigidbody.isKinematic = false;
            IsFellen = false;
            GiveScore = true;
            Reset = false;
        }
        if (GiveScore == true && IsFellen == true)
        {
            _score._score += 1;
            GiveScore = false;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(audioSource != null)
        {
            audioSource.Play();
        }
    }
}

