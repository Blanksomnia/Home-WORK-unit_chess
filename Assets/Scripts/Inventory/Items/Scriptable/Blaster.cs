using Cinemachine.Utility;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Item/Blaster", order = 1)]
public class Blaster : EmptyItem
{
    public PoolManager pool;

    public Blaster(Blaster blaster) : base(blaster)
    {
        this.pool = blaster.pool;
    }

    public override void Use(Character character)
    {
        pool.Shoot(-character.item.right, character.transform, character.item.position);
    }

}
