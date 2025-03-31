using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInventoryUnits
{
    public void GetSelect(CellUnit s);
    public void AddUnit(int value);
    public void DeleteUnit();
}
