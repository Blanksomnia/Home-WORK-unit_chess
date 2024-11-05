using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class ToolParametres : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] CanvasInterfaces _canvasInterfaces;
    [SerializeField] public GameObject _gunmodel;
    [SerializeField] int Health;
    [SerializeField] string Name;
    [SerializeField] int Damage;
    [SerializeField] int Ammo;
    public int _health => Health;
    public string _name => Name;
    public int _damage => Damage;
    public int _ammo => Ammo;

    private InfoEdit _infoEdit = new InfoEdit();
    public void OnPointerEnter(PointerEventData eventData)
    {
        if(_canvasInterfaces != null)
        {
            _infoEdit.OpenInfo(_canvasInterfaces._tableInfo.gameObject);
            _infoEdit.GiveInfoMain(Name, _canvasInterfaces._mainInfoTab);
            _infoEdit.GiveInfoDescription(Damage, Health, Ammo, _canvasInterfaces._descriptionInfoTab);
        }
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        if (_canvasInterfaces != null)
        {
            _infoEdit.CloseInfo(_canvasInterfaces._tableInfo.gameObject);
        }
    }

}
