using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public interface IStateUnitBehaviour
{
    public void GetManager(IListUnits units);

    public StateUnit _state();
    public bool _onPoint();
    public Vector3 _pos();

    public Transform _transform();
    public TypeUnits _type();
    public void GetDamage(int damage);

    public void OnPointerEnter();
    public void OnPointerExit();
    public void Select();
    public void Unselect();

    public void StateUpdate();
    public void WakeUp();
    public void Stay();
    public void Move(Vector3 posit);
    public void CollectResources(MinePoint mineP, Transform posBase, MaterialsManager material);
    public void AngryToUnits();

    public void IsDead();
}
