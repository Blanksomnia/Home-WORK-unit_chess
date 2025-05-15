using System.Collections.Generic;
using UnityEngine;

public class LockDoorButtons : InteractiveItem
{
    [SerializeField] List<Button> buttons = new List<Button>();
    [SerializeField] private Vector3 rotateTurn;
    [SerializeField] private float speed;
    Door door;

    bool closed = true;

    [SerializeField] AudioClip closeDoor;
    [SerializeField] AudioClip openDoor;

    [SerializeField] Transform doorTransform;

    private void Awake()
    {
        door = new Door(doorTransform, rotateTurn, speed, closeDoor, openDoor, GetComponent<AudioSource>());
    }

    private bool CheckLevers()
    {
        int activated = 0;

        for (int i = 0; i < buttons.Count; i++)
            if (buttons[i].activated == true)
                activated++;

        if (activated == buttons.Count)
            return true;
        else
        {
            print("buttons activated: " +  activated + "/ " + buttons.Count);
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
            door.OpenDoor();
            closed = false;
        }
        else if (!closed)
        {
            door.CloseDoor();
            closed= true;
        }
    }
}
