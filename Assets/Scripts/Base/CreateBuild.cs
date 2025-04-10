using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;
using Zenject;

public class CreateBuild : MonoBehaviour
{
    [SerializeField] GameObject build;
    public string nameBuild = "Empty";
    List<MeshRenderer> renderer = new List<MeshRenderer>();
    List<GameObject> builds = new List<GameObject>();
    public List<GameObject> _builds => builds;
    [SerializeField] Material canCreateMat;
    [SerializeField] Material noCanCreateMat;
    [SerializeField] Material defaultMat;
    BoxCollider boxCollider;
    ManagerUnits managerUnits;
    public ManagerUnits _manager => managerUnits;
    public GameObject _build => build;
    int count = 0;
    [SerializeField] int maxCount = 1;

    [Inject]
    public void Construct(ManagerUnits manage)
    {
        managerUnits = manage;
    }

    private void Awake()
    {
        boxCollider = build.GetComponent<BoxCollider>();
        for (int i = 0; i < maxCount; i++)
        {
            GameObject baseBuild = Instantiate(build);
            builds.Add(baseBuild);
            baseBuild.SetActive(false);
            foreach (MeshRenderer render in baseBuild.GetComponentsInChildren<MeshRenderer>())
            {
                renderer.Add(render);
            }
        }
       
    }

    public void UnSelect()
    {
        builds[0].transform.position = Vector3.zero;
        builds[0].gameObject.SetActive(false);
    }
    public void Click()
    {
        if (count < maxCount)
        {
            managerUnits.SelectBuild(this);
            builds[0].SetActive(true);
        }
    }

    private bool Create(Vector3 vec)
    {
        if (CanCreate())
        {
            renderer[0].material = defaultMat;
            builds[0].transform.position = vec;
            successfullCreate();
            builds.RemoveAt(0);
            renderer.RemoveAt(0);
            count++;
            return true;
        }
        else
        {
            return false;
        }
    }

    public virtual void successfullCreate()
    {

    }

    private bool CanCreate()
    {
        if (count < maxCount)
        {
            Collider[] _obstical = Physics.OverlapBox(boxCollider.center + builds[0].transform.position, boxCollider.size * builds[0].transform.localScale.x, Quaternion.identity, managerUnits.obsticalGround);

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
        if(builds.Count > 0)
        {
            builds[0].transform.position = vec;
            if (CanCreate())
            {
                renderer[0].material = canCreateMat;
            }
            else
            {
                renderer[0].material = noCanCreateMat;
            }
        }

    }

    public void addBuild(Vector3 pos)
    {
        if (Create(pos))
        {
            managerUnits.KillSelectBuild();
        }

    }


}
