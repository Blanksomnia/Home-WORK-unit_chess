using UnityEngine;


[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Effect/ChangeItem", order = 1)]
public class ChangeItem : Effect
{
    public EmptyItem itemStart = null;
    public EmptyItem itemEnd = null;
    ItemObject itemObject;
    public CreatorItems creatorItems;

    public override void GetEffect(Character character)
    {
       itemObject = character.inventory.GetSelectedItem();

        if(itemStart != null)
        itemObject.ChangeItem(creatorItems.item(itemStart));
    }

    public override void EndTimer()
    {
        if (itemEnd != null)
            itemObject.ChangeItem(creatorItems.item(itemEnd));
    }
}
