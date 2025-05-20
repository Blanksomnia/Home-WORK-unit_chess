using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    ItemObject[] items;
    MeshFilter filter;
    MeshRenderer render;
    AudioSource source;
    int _indexSelected = 0;
    public int indexSelected { get { return _indexSelected; } set { Select(value); } }

    public bool isBusy = false;
    public Action<string, int> selectAction;
    public Action<List<Effect>, string> activateSelectedAction;

    public Inventory(int maxLength, Transform arm)
    {
        items = new ItemObject[maxLength];
        filter = arm.GetComponent<MeshFilter>();
        render = arm.GetComponent<MeshRenderer>();
        source = arm.GetComponent<AudioSource>();
    }

    private void Select(int index)
    {
        if (index >= 0 && index < items.Length)
        {
            _indexSelected = index;

            if (selectAction != null)
            {
                if (items[index] != null)
                    selectAction(items[index].nameItem, index);
                else
                    selectAction(null, index);
            }


            
            if (items[index] != null)
            {
                render.material = items[index].meshRenderer.material;
                filter.mesh = items[index].meshFilter.mesh;
                render.transform.localScale = items[index].sizeItem;
                render.enabled = true;
            }
            else
                render.enabled = false;
        }
       
    }

    public ItemObject GetSelectedItem()
    {
        return items[_indexSelected];
    }



    private bool IsBusy(out int index)
    {
        bool isBusy = true;
        index = -1;
        for (int i = 0; i < items.Length; i++)
            if (items[i] == null && isBusy == true)
            {
                isBusy = false;
                index = i; break;
            }

        this.isBusy = isBusy;
        return isBusy;
    }

    public void Add(ItemObject itemObject)
    {

        if (!IsBusy(out int i))
        {
            items[i] = itemObject;
            items[i].CleanItem();
            items[i].refresh += RefreshItem;

            if (_indexSelected == -1)
                Select(0);
            else if (i == _indexSelected)
                Select(_indexSelected);

        }    

    }

    public void DropSelected(Vector3 pos)
    {
        if (items[_indexSelected] != null)
        {
            items[_indexSelected].DropItem(pos);
            items[_indexSelected].refresh -= RefreshItem;
            items[_indexSelected] = null;

            if (selectAction != null)
            {
                if (items[_indexSelected] != null)
                    selectAction(items[_indexSelected].nameItem, _indexSelected);
                else
                    selectAction(null, _indexSelected);
            }

            render.enabled = false;
        }
    }

    public void ActivateItem(Character character)
    {
        if (items[_indexSelected] != null)
            if (items[_indexSelected].canUseItem)
            {
                if (activateSelectedAction != null)
                    activateSelectedAction(items[_indexSelected].effects, items[_indexSelected].nameItem);

                items[_indexSelected].ActivateItem(character, source);
            }

    }

    private void RefreshItem(ItemObject item)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] != null && items[i] == item && i == _indexSelected)
            {
                render.material = items[i].meshRenderer.material;
                filter.mesh = items[i].meshFilter.mesh;
                render.transform.localScale = items[i].sizeItem;
                if (selectAction != null)
                {
                    if (items[_indexSelected] != null)
                        selectAction(items[_indexSelected].nameItem, _indexSelected);
                    else
                        selectAction(null, _indexSelected);
                }
                break;
            }
        }
    }
}
