using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class Unit : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
{
    MovingGroupAgent movingGroupAgent;
    public NavMeshAgent navMeshAgent;
    public Rigidbody rb;
    MeshRenderer meshRenderer;

    private void Start()
    {
        movingGroupAgent = transform.parent.GetComponent<MovingGroupAgent>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        meshRenderer = GetComponent<MeshRenderer>();
        rb = GetComponent<Rigidbody>();
    }

    public void ChangeMat(Material mat)
    {
        if(mat == null)
        {
            meshRenderer.material = movingGroupAgent.exit;
        }
            meshRenderer.material = mat;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (movingGroupAgent.selected != this)
            ChangeMat(movingGroupAgent.enter);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(movingGroupAgent.selected != null)
        {
            movingGroupAgent.selected.ChangeMat(movingGroupAgent.exit);
        }

         ChangeMat(movingGroupAgent.select);
        movingGroupAgent.selected = this;

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if(movingGroupAgent.selected != this)
        ChangeMat(movingGroupAgent.exit);
    }

}
