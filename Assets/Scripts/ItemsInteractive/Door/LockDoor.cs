using UnityEngine;

public class LockDoor : InteractiveItem
{
    [SerializeField] ItemObject key;
    [SerializeField] private Vector3 rotateTurn;
    [SerializeField] private float speed;
    [SerializeField] Transform doorTransform;
    [SerializeField] AudioClip closeDoor;
    [SerializeField] AudioClip openDoor;
    bool unlock = true;
    bool closed = true;
    Door door;
    private void Awake()
    {
        door = new Door(doorTransform, rotateTurn, speed, closeDoor, openDoor, GetComponent<AudioSource>());
        if (key != null)
            unlock = false;
    }

    public override void Updata(float deltaTime)
    {
        door.Updata(deltaTime);
    }

    public override void Activate(Character character)
    {
        if(key != null)
        {
            ItemObject item = character.inventory.GetSelectedItem();

            if (item == key)
                unlock = true;
                
        }

        if (!unlock && closed)
            print("you need " + key.nameItem);

        if(unlock && closed)
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
