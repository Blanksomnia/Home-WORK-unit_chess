using Core.Pools;
using Models;
using Presenters;
using UnityEngine;
using Zenject;

namespace Installers
{
    public sealed class GameInstaller : MonoInstaller
    {
        [SerializeField] private GameObject _building;
        [SerializeField] private GameObject _player;
        [SerializeField] private GameObject _stick;
        [SerializeField] private GameObject _hearth;
        [SerializeField] private GameObject _star;
        
        public override void InstallBindings()
        {
            BindGameObjects();
            BindModels();
            BindPresenters();
        }

        private void BindGameObjects()
        {
            Container.BindInstance(_building).WithId(Constants.BuildingIdentifier);
            Container.BindInstance(_player).WithId(Constants.PlayerIdentifier);
            Container.BindInstance(_stick).WithId(Constants.StickIdentifier);
            Container.BindInstance(_star).WithId(Constants.StarIdentifier);
            Container.BindInstance(_hearth).WithId(Constants.HearthIdentifier);
        }

        private void BindModels()
        {
            Container.Bind<IPool<GameObject>>().To<GameObjectPool>().FromInstance(new GameObjectPool()).NonLazy();
            Container.BindInterfacesAndSelfTo<BuildingModel>().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerModel>().AsSingle();
            Container.BindInterfacesAndSelfTo<StickModel>().AsSingle();
            Container.BindInterfacesAndSelfTo<TimeModel>().AsSingle();
            
        }

        private void BindPresenters()
        {
            Container.BindInterfacesAndSelfTo<InputPresenter>().AsSingle();
            Container.BindInterfacesAndSelfTo<StickBuildPresenter>().AsSingle();
            Container.BindInterfacesAndSelfTo<GameLoopPresenter>().AsSingle();
            Container.BindInterfacesAndSelfTo<BonusGeneratorPresenter>().AsSingle();
        }
    }
}