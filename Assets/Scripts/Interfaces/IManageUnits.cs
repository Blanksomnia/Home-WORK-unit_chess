using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IManageUnits
{
    public void CreateUnit(TypeUnits type, int count);

    public void addUnit(Vector3 pos, TypeUnits type);
    public void KillUnit(IStateUnitBehaviour unit);
    public void SelectUnit(IStateUnitBehaviour unit);

    public void SelectGroup();
    public void KillSelectedUnits();
    public void ClearSelected();
   
    public void UnitMoveTo();


}
