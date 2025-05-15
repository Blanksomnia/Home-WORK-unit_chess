using System;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    [SerializeField] int _health = 1;
    [SerializeField] int maxhealth = 1;

    [SerializeField] float _speed = 1f;

    [SerializeField] int maxCellsInventory = 10;
    [SerializeField] Transform _item;

    Inventory _inventory;

    public Action<int> changeHealthAction = null;
    public Action<float> changeSpeedAction = null;
    public Inventory inventory => _inventory;
    public Transform item => _item;
    public int health { get { return _health; } set { _health = CheckHealth(value);  } }
    public float speed { get { return _speed;} set { _speed = GiveValueSpeed(value); } }

    public void Awake()
    {
        if(maxCellsInventory > 0)
        _inventory = new(maxCellsInventory, _item);
    }

    private int CheckHealth(int health)
    {
        int current = health;

        if(current > maxhealth)
            current = maxhealth;

        if(current <= 0)
            current = 0;

        if(changeHealthAction != null)
        changeHealthAction(current);

        return current;
    }

    private float GiveValueSpeed(float value)
    {
        if (changeSpeedAction != null)
            changeSpeedAction(value);

        return value;
    }

    public virtual void Updata(float deltaTime)
    {

    }

}
