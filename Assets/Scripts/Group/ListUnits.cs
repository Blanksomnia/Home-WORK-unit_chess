using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IListUnits
{
    public List<List<IStateUnitBehaviour>> _activities();
    public List<MinePoint> _minePoints();
    public Transform _posBase();
    public MaterialsManager _materials();
    public List<Transform> _patrols();

}
