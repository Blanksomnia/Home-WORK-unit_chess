using System.Collections.Generic;
using UnityEngine;

public class LockDoorLevers : InteractiveItem
{
    [SerializeField] List<Lever> levers = new List<Lever>();
    [SerializeField] private Vector3 rotateTurn;
    [SerializeField] private float speed;
    Door door;

    bool closed = true;

    [SerializeField] AudioClip closeDoor;
    [SerializeField] AudioClip openDoor;

    [SerializeField] Transform doorTransf;

    private void Awake()
    {
        door = new Door(doorTransf, rotateTurn, speed, closeDoor, openDoor, GetComponent<AudioSource>());
    }

    private bool CheckLevers()
    {
        int activated = 0;
        for (int i = 0; i < levers.Count; i++)
            if (levers[i]._activated == true)
              activated++;

        if (activated == levers.Count)
            return true;
        else
        {
            print("levers activated: " + activated + "/ " + levers.Count);
            return false;
        }
    }

    public override void Updata(float deltaTime)
    {
        door.Updata(deltaTime);
    }

    public override void Activate(Character character)
    {
        if (CheckLevers() && closed)
        {
            closed = false;
            door.OpenDoor();
        }
        else if (!closed)
        {
            closed = true;
            door.CloseDoor();
        }
    }

}
