using UnityEngine;

public class PlayerMode
{
    Player player;
    private Vector3 pos = Vector3.zero;
  
    private bool _inAir = true;
    private bool _onTheEdgeOfTheWall = false;

    public bool lockChangeClimb = false;
    public bool inAir { get { return _inAir; } set { ChangeInAir(value); } }
    public bool onTheEdgeOfTheWall { get { return _onTheEdgeOfTheWall; } set { ChangeCanClimb(value); } }



    public PlayerMode(Player player)
    {
        this.player = player;
    }

    public void Update()
    {
        inAir = player.checks.CheckAir();

        if (inAir)
            if(player.checks.CheckClimb())
                onTheEdgeOfTheWall = true;

    }

    private void ChangeInAir(bool current)
    {
        if (_inAir != current && !onTheEdgeOfTheWall)
        {
            _inAir = current;
            if (_inAir == true)
            {
                if (player.turnMove != Vector2.zero)
                    player.sound.Stop();

                pos = player.transform.position;

                player.sound.Jump();
                player.speed -= 0.3f;
                player.animations.StartJump();
                player.lockJump = true;
            }
            else
            {
                if (pos != Vector3.zero && pos.y - player.transform.position.y >= player.distanceToGetDamageOnGround)
                    player.health -= 3;

                if (player.turnMove != Vector2.zero)
                    player.sound.StartMove();

                player.sound.Jump();
                player.speed += 0.3f;

                player.animations.EndJump();
                player.lockJump = false;
                lockChangeClimb = false;
            }
        }

    }

    private void ChangeCanClimb(bool current)
    {
        if(current != _onTheEdgeOfTheWall && !lockChangeClimb)
        {
            _onTheEdgeOfTheWall = current;
            if (_onTheEdgeOfTheWall == true)
            {
                player.animations.StartClimb();
                Rotation();
                player.rb.isKinematic = true;
            }
            else
            {
                ItemObject item = player.inventory.GetSelectedItem();
                if (item != null)
                player.animations.ChangeArm(player.inventory.GetSelectedItem().nameItem, 0);
                else
                    player.animations.ChangeArm(null, 0);

                player.rb.isKinematic = false;
            }
        }

    }

    private void Rotation()
    {
        Vector3 posPlayer = player.transform.position;
        Vector3 rightEndDirection = player.transform.forward + (player.transform.right * 0.2f);
        Vector3 leftEndDirection = player.transform.forward - (player.transform.right * 0.2f);
        Vector3 hitLeft = Vector3.zero;
        Vector3 hitRight = Vector3.zero;

        if (Physics.Raycast(posPlayer, leftEndDirection, out RaycastHit L, 5, player.layerGround))
            hitLeft = L.point;
        if (Physics.Raycast(posPlayer, rightEndDirection, out RaycastHit R, 5, player.layerGround))
            hitRight = R.point;

        Quaternion rotationForward = Quaternion.Euler(new Vector3(0, -180, 0) + Quaternion.FromToRotation(Vector3.right, hitLeft - hitRight).eulerAngles);
        Vector3 start = (rotationForward * Vector3.back) + hitRight;

        if (hitLeft != Vector3.zero && hitRight != Vector3.zero)
            player.animations.LookAt(hitRight - start + player.transform.position, 200);

    }
}
