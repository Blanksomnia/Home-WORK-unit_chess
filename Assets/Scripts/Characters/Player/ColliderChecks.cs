using UnityEngine;

public class ColliderChecks
{
    BoxCollider colliderClimb;
    BoxCollider colliderAir;
    Player player;

    public ColliderChecks(BoxCollider colliderClimb, BoxCollider colliderAir, Player player)
    {
        this.colliderClimb = colliderClimb;
        this.colliderAir = colliderAir;
        this.player = player;
    }

    public bool CheckAir()
    {
        if (!Physics.CheckBox(colliderAir.transform.position, colliderAir.size*2.5f, Quaternion.identity, player.layerGround))
            return true;
        else
            return false;
    }

    public bool CheckClimb()
    {
        if (!Physics.CheckBox(colliderClimb.transform.position, colliderClimb.size * 2.5f, Quaternion.identity, player.layerClimb))
            return false;
        else
        {
            if (!Physics.CheckBox(colliderClimb.transform.position + player.transform.forward * colliderClimb.size.z * 2.5f + new Vector3(0, colliderClimb.size.y * 2.5f, 0), colliderClimb.size * 2.5f, Quaternion.identity, player.layerGround) &&
                !Physics.CheckBox(colliderClimb.transform.position - player.transform.forward * 0.5f + new Vector3(0, -0.5f, 0), colliderClimb.size * 2.5f, Quaternion.identity, player.layerGround))
                return true;
            else
                return false;
        }
    }

    public bool EnemyChecks(out Transform enemy)
    {
        RaycastHit hit;
        enemy = null;

        if (Physics.Raycast(colliderAir.transform.position - player.transform.up * 0.2f, -player.transform.up, out hit, 1f, player.layerCharacter))
        {
            if (hit.transform.tag != "Dead")
                enemy = hit.transform;
        }

        if(enemy == null)
            return false;
        else
            return true;
    }

    public bool ClimbRightCheck()
    {
        if (Physics.CheckBox(colliderClimb.transform.position + (player.transform.right * 1.3f), colliderClimb.size * 2.5f, Quaternion.identity, player.layerClimb))
            return true;
        else
            return false;
    }

    public bool ClimbLeftCheck()
    {
        if (Physics.CheckBox(colliderClimb.transform.position - (player.transform.right * 1.3f), colliderClimb.size * 2.5f, Quaternion.identity, player.layerClimb))
            return true;
        else
            return false;
    }

    public bool ClimbUpCheck()
    {
        Vector3 currentCenter = player.transform.position + new Vector3(0, 4f, 0) + player.transform.forward * 1.5f + new Vector3(0, 3f, 0);
        Vector3 currentSize = new Vector3(1.2f, 1.2f, 1f);

        if (!Physics.CheckBox(currentCenter, currentSize, Quaternion.identity, player.layerGround))
            return true;
        else
            return false;
    }

}
