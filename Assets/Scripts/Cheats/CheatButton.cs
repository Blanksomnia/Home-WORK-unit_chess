using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class CheatButton : MonoBehaviour
{

    private MaterialsManager materials;

    [Inject]
    public void Construct(MaterialsManager mat)
    {
        materials = mat;
    }

    public void Click()
    {
        materials.AddValue(TypeMine.Stone, 10);
        materials.AddValue(TypeMine.Coal, 10);
        materials.AddValue(TypeMine.Gold, 10);
    }
}
