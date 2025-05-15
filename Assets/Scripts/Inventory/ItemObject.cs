using System;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour
{
    [SerializeField] EmptyItem item;
    [SerializeField] CreatorItems creatorItems;
    BoxCollider box;

    bool createdItem = false;

    [HideInInspector] public MeshFilter meshFilter;
    [HideInInspector] public MeshRenderer meshRenderer;

    public Action<ItemObject> refresh;


    public Vector3 sizeItem => item.scale;
    public string nameItem => item._name;
    public List<Effect> effects => item.effects;
    public bool canUseItem => item.canUse;

    private void Awake()
    {
        meshFilter = GetComponent<MeshFilter>();
        meshRenderer = GetComponent<MeshRenderer>();
        box = GetComponent<BoxCollider>();
    }

    private void Start()
    {
        ChangeItem(creatorItems.item(item));
        createdItem = true;
    }

    public void CleanItem()
    {
        if (!createdItem)
        {
            ChangeItem(creatorItems.item(item));
            createdItem = true;
        }


        gameObject.SetActive(false);
    }

    public void ActivateItem(Character character, AudioSource audio)
    {
        item.Activate(character, audio);
    }

    public void DropItem(Vector3 pos)
    {
        transform.position = pos;
        gameObject.SetActive(true);
    }

    public void ChangeItem(EmptyItem item)
    {
        this.item = item;

        if(meshFilter != null)
        meshFilter.mesh = item.mesh;
        if(meshRenderer != null)
        meshRenderer.material = item.material;

        if (refresh != null)
        refresh(this);
    }
}

