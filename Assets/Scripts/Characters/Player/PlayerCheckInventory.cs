using UnityEngine;

public class CharacterInventory
{
    BoxCollider itemsCheck;
    Character character;
    Characters manager;
    LayerMask mask;

    public CharacterInventory(Character character, LayerMask layerItems, BoxCollider itemsCheck, Characters chracters)
    {
        this.itemsCheck = itemsCheck;
        this.character = character;
        manager = chracters;
        mask = layerItems;
    }

    public void CheckAddToInventory()
    {
        if (!character.inventory.isBusy)
        {
            Vector3 currentCenter = itemsCheck.transform.position;
            Vector3 currentSize = itemsCheck.size;
            Collider[] colliders = Physics.OverlapBox(currentCenter, currentSize, character.transform.rotation, mask);
            if (colliders.Length > 0)
            {
                for (int i = 0; i < colliders.Length; i++)
                    manager.AddToInventory(character.inventory, colliders[i].transform);
            }
        }  
    }
}
