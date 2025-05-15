using System.Collections.Generic;
using UnityEngine;

public class Characters
{
    List<Character> characters = new List<Character>();
    List<ItemObject> items = new List<ItemObject>();
    Dictionary<Transform, int> characterGetDamage = new Dictionary<Transform, int>();
    Dictionary<Transform, Inventory> itemAddToInventory = new Dictionary<Transform, Inventory>();

    public Characters(List<Character> characters, List<ItemObject> items) 
    {
        this.characters = characters;
        this.items = items;
    }

    public void AddToInventory(Inventory inventory, Transform item)
    {
        itemAddToInventory.Add(item, inventory);
    }

    public void GetDamage(Transform character, int damage)
    {
        characterGetDamage.Add(character, damage);
    }

    public void Update(float deltaTime)
    {
        bool isDirtyDamage = false;

        for (int i = 0; i < characters.Count; i++)
        {
            if (characters[i].tag != "Dead")
            characters[i].Updata(deltaTime);

            if (characterGetDamage.Count > 0 && isDirtyDamage == false)
            {
                i = 0;
                isDirtyDamage = true;
            }

            if(isDirtyDamage)
                foreach (var (key, value) in characterGetDamage)
                    if (key == characters[i].transform)
                        characters[i].health -= value;
        }

        if(itemAddToInventory.Count > 0)
            foreach (var (key, value) in itemAddToInventory)
                for(int i = 0;i < items.Count; i++)
                    if(key == items[i].transform)
                        value.Add(items[i]);

        if(characterGetDamage.Count > 0)
        characterGetDamage.Clear();
        if(itemAddToInventory.Count > 0)
            itemAddToInventory.Clear();
    }
}
