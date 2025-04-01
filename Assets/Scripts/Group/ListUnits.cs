using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IListUnits
{
    public List<List<IStateUnitBehaviour>> _activities();
}
