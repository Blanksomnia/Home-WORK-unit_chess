using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatePatrol : CreateBuild
{
    public override void successfullCreate()
    {
        _manager.GetPatrol(_builds[0].transform);
    }
}
