using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class SceneInstaller : MonoInstaller
{
    [SerializeField] ManagerUnits manage;
    [SerializeField] MaterialsManager manageMaterials;
    [SerializeField] InventoryUnits inventory;
    [SerializeField] InputActionAsset input;
    [SerializeField] CameraMove cameraMove;
    [SerializeField] Camera camera;
    [SerializeField] ListUnitSpawner spawner;
    [SerializeField] InputPlayer inputPlayer;
    [SerializeField] CreateBase build;

    public override void InstallBindings()
    {
        this.Container
            .Bind<ManagerUnits>()
            .FromInstance(manage)
            .AsSingle();

        this.Container
            .Bind<InventoryUnits>()
            .FromInstance(inventory)
            .AsSingle();

        this.Container
           .Bind<InputActionAsset>()
           .FromInstance(input)
           .AsSingle();

        this.Container
           .Bind<CameraMove>()
           .FromInstance(cameraMove)
           .AsSingle();

        this.Container
           .Bind<Camera>()
           .FromInstance(camera)
           .AsSingle();

        this.Container
         .Bind<ListUnitSpawner>()
         .FromInstance(spawner)
         .AsSingle();

        this.Container
            .Bind<MaterialsManager>()
            .FromInstance(manageMaterials)
            .AsSingle();

        this.Container
            .Bind<InputPlayer>()
            .FromInstance(inputPlayer)
            .AsSingle();

        this.Container
           .Bind<CreateBase>()
           .FromInstance(build)
           .AsSingle();
    }
}
