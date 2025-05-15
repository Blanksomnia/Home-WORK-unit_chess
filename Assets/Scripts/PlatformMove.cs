using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class PlatformMove : MonoBehaviour
{
    [SerializeField] Vector3 endPos;
    [SerializeField] float speed = 5;
    Vector3 pos;
    bool onStart = true;
    [SerializeField] Transform player;
    bool playerOnPlatform = false;

    private void Awake()
    {
        pos = transform.position;
    }

    public void Updata(float deltatime)
    {
        if (onStart)
        {

            if (Vector3.Distance(endPos, transform.position) <= 1f)
                onStart = false;
            else
            {
                Vector3 move = Vector3.Lerp(transform.position, endPos, speed * deltatime);
                Vector3 difference = player.transform.position - transform.position;
                transform.position = move;

                if(playerOnPlatform)
                        player.position = transform.position + difference;
            }
        }

        else
        {

            if (Vector3.Distance(pos, transform.position) <= 1f)
                onStart = true;
            else
            {
                Vector3 move = Vector3.Lerp(transform.position, pos, speed * deltatime);
                Vector3 difference = player.transform.position - transform.position;
                transform.position = move;

                if (playerOnPlatform)
                    player.position = transform.position + difference;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.transform == player.transform)
        {
            playerOnPlatform = true;
        }
            
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.transform == player.transform)
        {
            playerOnPlatform = false;
        }
            
    }
}
