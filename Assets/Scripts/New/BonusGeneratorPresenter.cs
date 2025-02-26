using Core.SaveLoad;
using Messages;
using Models.Interfaces;
using Presenters.Interfaces;
using System;
using UniRx;
using UnityEngine;
using Zenject;
using Random = System.Random;


namespace Presenters
{
    [Serializable]
    public sealed class BonusGeneratorPresenter : IGameLoopPresenter
    {
        private readonly ISaveLoadDataHandler _saveLoadDataHandler;

        private readonly IMessageBroker _messageBroker;
        private readonly IBonusModel _bonuses;
        private readonly IBuildingModel _building;
        private readonly CompositeDisposable _compositeDisposable = new();
        Bonuses type;

        GameObject bonusObj;
        GameObject StarGM;
        GameObject HearthGM;
        public void Initialize()
        {
            Subscribe();
            _bonuses.ResetCollect(true);
            CreateBonus();
        }

        public BonusGeneratorPresenter(IMessageBroker messageBroker, IBonusModel bonuses, IBuildingModel buildingModel, [Inject(Id = "Star")] GameObject starB, [Inject(Id = "Hearth")] GameObject hearthB)
        {
            _messageBroker = messageBroker;
            _bonuses = bonuses;
            StarGM = starB;
            HearthGM = hearthB;
            _building = buildingModel;
        }

        private void Subscribe()
        {
            _messageBroker.Receive<MoveFailedMessage>().Subscribe(OnMoveFailed).AddTo(_compositeDisposable);
            _messageBroker.Receive<MoveSuccessfulMessage>().Subscribe(OnMoveSuccessfull).AddTo(_compositeDisposable);
        }

        private void OnMoveFailed(MoveFailedMessage message)
        {
            _bonuses.SaveBonuses();
        }

        private void OnMoveSuccessfull(MoveSuccessfulMessage message)
        {
            _bonuses.ToCollect(type);
            DeleteBonus();
            CreateBonus();
        }

        private GameObject BonusType(Bonuses type)
        {
            switch (type)
            {
                case Bonuses.Star: return StarGM;
                case Bonuses.Hearth: return HearthGM;
                default: return null;
            }

        }

        private void CreateBonus()
        {
            Random random = new Random();
            int result = random.Next(0, 2);
            Bonuses bonus;
            
            if (result == 0)
            {
                bonus = Bonuses.Star;
            }
            else
            {
                bonus = Bonuses.Hearth;
            }
            type = bonus;
            bonusObj = UnityEngine.Object.Instantiate(BonusType(bonus), new Vector3(2f + _building.GetPositionForPlayer(true).x, 0, 0), Quaternion.identity);
        }

        private void DeleteBonus()
        {
            if(_bonuses != null)
            {
                bonusObj.SetActive(false);
                UnityEngine.Object.Destroy(bonusObj);
                bonusObj = null;
            }

        }

        public void Dispose() => _compositeDisposable.Dispose();

    }
}
