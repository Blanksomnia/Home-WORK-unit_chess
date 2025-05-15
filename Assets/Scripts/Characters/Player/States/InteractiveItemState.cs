using UnityEngine;

public class InteractiveItemState : State<StatePlayer>
{
    Player player;

    public InteractiveItemState(Player player)
    {
        this.player = player;
        state = StatePlayer.InteractiveItem;
    }

    public override void Enter()
    {
        InteractiveItemsChecks();
    }

    private void InteractiveItemsChecks()
    {
        RaycastHit hit;

        if (Physics.Raycast(player.rb.transform.position + new Vector3(0, 2F, 0), player.rb.transform.forward, out hit, 3.5f, player.layerInteractiveItems))
            player.updates.interactivities.ActivateItem(hit.transform, player);

    }

}


