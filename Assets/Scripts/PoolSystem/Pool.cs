using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Item/pool", order = 1)]
public class PoolManager : ScriptableObject
{
    public Characters characters;
    public int countTrashToDelete;
    public float speed;
    public int damage;
    public float distance;
    public GameObject poolPrefab;
    public LayerMask unit;
    public LayerMask obstical;

    List<PoolStruct> poolList = new List<PoolStruct>();
    List<PoolStruct> trash = new List<PoolStruct>();

    public void Start()
    {
        poolList.Clear();
        trash.Clear();
    }

    public void Updata(float deltaTime)
    {
        poolsMove(deltaTime);
        CheckTrash();
    }

    private void CheckTrash()
    {
        if (trash.Count > countTrashToDelete)
        {
            for (int i = 0; i < trash.Count; i++)
                Destroy(trash[i].pool);

            trash.Clear();
        }
    }
    private void poolsMove(float deltaTime)
    {
        for (int i = 0; i < poolList.Count; i++)
        {
            poolList[i].pool.transform.position += poolList[i].direction * deltaTime * speed;

            if (Vector3.Distance(poolList[i].pool.transform.position, poolList[i].end) < 0.3f)
            {
                poolList[i].pool.SetActive(false);
                trash.Add(poolList[i]);
                poolList.RemoveAt(i);
            }
        }
    }

    public void Shoot(Vector3 direction, Transform owner, Vector3 startPos)
    {
        poolList.Add(CreatePool(direction, owner, startPos));
    }

    private PoolStruct CreatePool(Vector3 direction, Transform owner, Vector3 startPos)
    {
        PoolStruct pool = new PoolStruct();
        pool.pool = Instantiate(poolPrefab);
        pool.pool.transform.position = startPos;
        pool.direction = direction;
        pool.owner = owner;

        if (Physics.Raycast(startPos, direction, out RaycastHit hit, distance, unit))
        {
            if (!Physics.Raycast(startPos, direction, out RaycastHit obs, Vector3.Distance(hit.point, startPos), obstical) && hit.transform != pool.owner)
            {
                characters.GetDamage(hit.transform, damage);
                pool.end = hit.point;
            }
            else
                pool.end = obs.point;
        }
        else
            pool.end = startPos + direction * distance;

        return pool;

    }

}
