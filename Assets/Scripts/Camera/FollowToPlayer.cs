using UnityEngine;

public class FollowToPlayer : MonoBehaviour
{
    [SerializeField] private Transform target;
    float Y;

    private void Awake()
    {
        Y = transform.position.y - target.position.y;
    }

    void Update()
    {
        Vector3 current = target.position;
        current.z = transform.position.z;
        current.y += Y;
        transform.position = current;
    }
}
