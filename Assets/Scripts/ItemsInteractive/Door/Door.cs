using UnityEngine;

public class Door
{
    bool rotate = false;
    private float speed;
    private Vector3 rotateTurn;

    private Quaternion _rotateClosedDoor;
    public Quaternion rotateClosedDoor => _rotateClosedDoor;
    public Quaternion rotateDone;
    private Vector3 turn;
    Rigidbody rb;
    Transform transform;

    public AudioSource source;
    AudioClip close;
    AudioClip open;

    public Door(Transform door, Vector3 rotateTurn, float speed, AudioClip close, AudioClip open, AudioSource source)
    {
        this.speed = speed;
        this.rotateTurn = rotateTurn;
        transform = door;
        _rotateClosedDoor = transform.rotation;
        rb = transform.GetComponent<Rigidbody>();
        rb.isKinematic = true;
        this.close = close;
        this.open = open;
        this.source = source;
    }

    public void Updata(float deltaTime)
    {
        if (rotate)
        {
            float dist = Vector3.Distance(transform.forward, rotateDone*Vector3.forward);
            rb.AddTorque(turn * speed * rb.mass * 1000);

            if (dist <= 0.5f)
            {
                transform.rotation = rotateDone;
                rotate = false;
                rb.isKinematic = true;
            }
        }
    }

    public void OpenDoor()
    {
        turn = rotateTurn;
        rotateDone = rotateClosedDoor * Quaternion.Euler(turn.x, turn.y * 90, turn.z);
        rotate = true;
        rb.isKinematic = false;
        source.clip = open;
        source.Play();
    }

    public void CloseDoor()
    {
        turn = -rotateTurn;
        rotateDone = rotateClosedDoor;
        rotate = true;
        rb.isKinematic = false;
        source.clip = close;
        source.Play();
    }


}
