using System.Collections.Generic;
using UnityEngine;

public class ManagerUpdates : MonoBehaviour
{
    public Characters characters;
    public InteractivitiesItems interactivities;
    [SerializeField] CreatorEffects creatorEffects;
    [SerializeField] PoolManager pool;
    [SerializeField] Arms arms;
    List<PlatformMove> platforms = new List<PlatformMove>();

    private void Awake()
    {
        List<Character> characters = new List<Character>();
        List<InteractiveItem> interactivitiesItems = new List<InteractiveItem>();
        List<ItemObject> items = new List<ItemObject>();

        GameObject[] allGameobjects = FindObjectsOfType<GameObject>();
        foreach (GameObject hit in allGameobjects)
        {
            InteractiveItem itemInteractive = hit.GetComponent<InteractiveItem>();
            Character character = hit.GetComponent<Character>();
            ItemObject item = hit.GetComponent<ItemObject>();
            PlatformMove platform = hit.GetComponent<PlatformMove>();

            if (itemInteractive != null)
                interactivitiesItems.Add(itemInteractive);
            if(character != null)
                characters.Add(character);
            if(item != null)
                items.Add(item);
            if(platform != null)
                platforms.Add(platform);

        }

        this.characters = new Characters(characters, items);
        interactivities = new InteractivitiesItems(interactivitiesItems);
        pool.characters = this.characters;
        arms.characters = this.characters;

        pool.Start();

    }

    private void Update()
    {
        characters.Update(Time.deltaTime);
        interactivities.Update(Time.deltaTime);
        pool.Updata(Time.deltaTime);
        creatorEffects.Updata(Time.deltaTime);

        for (int i = 0;i < platforms.Count;i++)
            platforms[i].Updata(Time.deltaTime);

    }

}

