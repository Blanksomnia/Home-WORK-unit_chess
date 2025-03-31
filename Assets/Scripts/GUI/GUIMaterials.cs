using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Zenject;

public class GUIMaterials : MonoBehaviour
{
    MaterialsManager mat;
    [SerializeField] private TextMeshProUGUI valueS;
    [SerializeField] private TextMeshProUGUI valueC;
    [SerializeField] private TextMeshProUGUI valueG;

    [Inject]
    public void Construct(MaterialsManager mater)
    {
        mat = mater;
        UpdMaterlials();
    }

    public void UpdMaterlials()
    {
        valueS.text = mat.material(TypeMine.Stone)._value.ToString();
        valueC.text = mat.material(TypeMine.Coal)._value.ToString();
        valueG.text = mat.material(TypeMine.Gold)._value.ToString();
    }


}
