using UnityEngine;

public class Box : InteractiveItem
{
    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public override void Activate(Character character)
    {
        rb.AddForce(-character.transform.forward * 0.6f * 1000 * rb.mass);
    }
}
