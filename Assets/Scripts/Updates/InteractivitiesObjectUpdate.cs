using System.Collections.Generic;
using UnityEngine;

public class InteractivitiesItems
{
    List<InteractiveItem> interactivitiesItems = new List<InteractiveItem>();
    Dictionary<Transform, Character> activateItems = new Dictionary<Transform, Character>();

    public InteractivitiesItems(List<InteractiveItem> interactivitiesItems) 
    {
        this.interactivitiesItems = interactivitiesItems;
    }

    public void ActivateItem(Transform item, Character charact)
    {
        activateItems.Add(item, charact);
    }

    public void Update(float deltaTime)
    {
        bool isDirtyActivateItem = false;

        for(int i = 0; i < interactivitiesItems.Count; i++)
        {
            interactivitiesItems[i].Updata(deltaTime);
            if(activateItems.Count > 0 && isDirtyActivateItem == false)
            {
                i = 0;
                isDirtyActivateItem = true;
            }

            if(isDirtyActivateItem)
                foreach(var (key, value) in activateItems)
                    if(key == interactivitiesItems[i].transform)
                        interactivitiesItems[i].Activate(value);
        }


        if(activateItems.Count > 0)
            activateItems.Clear();

    }


}
