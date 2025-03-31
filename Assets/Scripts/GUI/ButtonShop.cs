using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ButtonShop : MonoBehaviour
{
    InputPlayer player;
    CameraMove cameraMove;
    [SerializeField] GameObject shop;

    [Inject]
    public void Construct(InputPlayer pl, CameraMove cam)
    {
        player = pl;
        cameraMove = cam;
    }


    public void OpenShop()
    {
        player.enabled = false;
        cameraMove.enabled = false;
        shop.SetActive(true);
    }

    public void CloseShop()
    {
        player.enabled = true;
        cameraMove.enabled = true;
        shop.SetActive(false);
    }
}
