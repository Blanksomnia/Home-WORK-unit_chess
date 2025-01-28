using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Xml.Serialization;
using System.Xml;
using Unity.VisualScripting.FullSerializer;
using System.Runtime.CompilerServices;
using System;

public class Init : MonoBehaviour
{
    [SerializeField] PlayerMemory player;
    [SerializeField] Transform ParentBonuses;
    private XMLFile<Config> config = new XMLFile<Config>("save");
    List<ActivateBonus> activities = new List<ActivateBonus>();

    private void Awake()
    {
        foreach(ActivateBonus act in ParentBonuses.GetComponentsInChildren<ActivateBonus>())
        {
            activities.Add(act);
        }
        Load();
    }

    public void Save()
    {
        Config conf = new Config();

        for(int i = 0; i < activities.Count; i++)
        {
            conf.ActiveBonus.Add(activities[i]);
        }
        
        conf.Health = player._health;
        conf.Coins = player._coins;
        conf.pos = transform.position;
        conf.Speed = player._speed;
        conf.rot = transform.rotation;

        config.Save(conf);
    }

    public void Load()
    {
        Config conf = new Config();
        config.Load(out conf);
        for (int i = 0; i < activities.Count; i++)
        {
            activities[i].active = conf.ActiveBonus[i];
        }
        player.GetLoad(conf.Health, conf.Coins, conf.Speed, conf.pos, conf.rot);
    }

    public void NewGame()
    {
        Config conf = new Config();
        for (int i = 0; i < activities.Count; i++)
        {
            conf.ActiveBonus.Add(true);
        }
        config.Save(conf);
    }
}

 

public sealed class XMLFile<T> where T : class
{

    public string path;
    private XmlSerializer serializer;

    public XMLFile(string path)
    {
        this.path = string.Format("{0}/{1}.xml", Application.dataPath, path);
        serializer = new XmlSerializer(typeof(T));
    }

    public void Load(out T instance)
    {
        var stream = new FileStream(path, FileMode.Open);
        instance = (T)serializer.Deserialize(stream);

    }

    public void Save(T instance)
    {
        var stream = new FileStream(path, FileMode.Open);
        serializer.Serialize(stream, instance);
        stream.Close();
    }

}


public sealed class Config
{
    public List<bool> ActiveBonus = new List<bool>();
    public int Health = 100;
    public int Coins = 0;
    public float Speed = 300;
    public Vector3 pos = new Vector3(-2.06F, 0.2F, -1.3F);
    public Quaternion rot = new Quaternion();
}




