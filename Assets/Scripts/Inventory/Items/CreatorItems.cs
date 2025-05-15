using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/creatorItems", order = 1)]
public class CreatorItems : ScriptableObject
{
    public List<EmptyItem> items;
    public List<Arms> arms;
    public List<Blaster> blasters;


    public EmptyItem item(EmptyItem create)
    {
        EmptyItem item = null;

        for (int i = 0; i < items.Count; i++)
            if(create == items[i])
            {
                item = new EmptyItem(items[i]);
                break;
            }

        if(item == null)
            for(int i = 0;i < arms.Count;i++)
                if (create == arms[i])
                {
                    item = new Arms(arms[i]);
                    break;
                }

        if(item == null)
            for (int i = 0; i < blasters.Count; i++)
                if (create == blasters[i])
                {
                    item = new Blaster(blasters[i]);
                    break;
                }

        return item;
    }
}
