using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateBase : MonoBehaviour
{
    [SerializeField] GameObject build;
    GameObject baseBuild;
    public Transform _baseBuild => baseBuild.transform;
    [SerializeField] Material canCreateMat;
    [SerializeField] Material noCanCreateMat;
    [SerializeField] Material defaultMat;
    BoxCollider boxCollider;
    MeshRenderer renderer;
    [SerializeField] ManagerUnits managerUnits;
    int count = 0;
    int maxCount = 1;
    private bool canCreateUnits = false;
    private bool select = false;
    public bool _canCreateUnits => canCreateUnits;

    private void Awake()
    {
        boxCollider = build.GetComponent<BoxCollider>();
        baseBuild = Instantiate(build);
        baseBuild.SetActive(false);
        foreach(MeshRenderer render in baseBuild.GetComponentsInChildren<MeshRenderer>())
        {
            renderer = render;
        }
    }

    private void SelectBuild(CreateBase b)
    {
        select = true;
    }

    public void Click()
    {
        if(count < maxCount)
        {
            SelectBuild(this);
            baseBuild.SetActive(true);
        }
    }

    public bool Create(Vector3 vec)
    {
        if (CanCreate(vec))
        {
            renderer.material = defaultMat;
            baseBuild.transform.position = vec;
            count++;
            canCreateUnits = true;
            this.enabled = false;
            return true;
        }
        else
        {
            return false;
        }
    }

    private bool CanCreate(Vector3 vec)
    {
        if(count < maxCount && vec != Vector3.zero)
        {
            Collider[] _obstical = Physics.OverlapBox(boxCollider.center + vec, boxCollider.size, Quaternion.identity, managerUnits.obsticalGround);

            if (_obstical.Length == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }

    }

    public void Selected(Vector3 vec)
    {
        baseBuild.transform.position = vec;
        if(CanCreate(vec))
        {
            renderer.material = canCreateMat;
        }
        else
        {
            renderer.material = noCanCreateMat;
        }
    }

    public void addBuild(Vector3 pos)
    {
        if (select)
        {
            if (Create(pos))
            {
                canCreateUnits = true;
                select = false;
            }
        }
        else
        {
            Debug.Log("You Need Create Base!!!");
        }
    }

    private void Update()
    {
        if (select)
        {
            Selected(managerUnits.MousePoint(managerUnits.ground, managerUnits.obsticalGround));
        }
    }

}
