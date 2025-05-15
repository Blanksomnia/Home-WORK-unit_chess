using UnityEngine;

public class Lever : InteractiveItem
{
    [SerializeField] Vector3 activatedRotate;
    Quaternion rotateStarted;

    [SerializeField] Transform lever;

    AudioSource source;

    bool activated = false;
    public bool _activated => false;

    private void Start()
    {
        source = GetComponent<AudioSource>();
        rotateStarted = lever.rotation;
    }
    public override void Activate(Character character)
    {
        if (activated)
        {
            lever.rotation = rotateStarted;
            activated = false;
            source.Play();
        }
        else
        {
            lever.rotation = Quaternion.Euler(activatedRotate);
            activated = true;
            source.Play();
        }
    }

}
