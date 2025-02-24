using Core.SaveLoad;
using Messages;
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
        private readonly Vector3 _startPos;
        private readonly CompositeDisposable _compositeDisposable = new();

        GameObject bonusObj;
        Vector2 PosBonus;

        public void Initialize()
        {
            Subscribe();
            _bonuses.ResetCollect();
        }

        public BonusGeneratorPresenter(IMessageBroker messageBroker, CompositeDisposable compositeDisposable, IBonusModel bonuses, [Inject(Id = "Start")] Transform startPosition)
        {
            _messageBroker = messageBroker;
            _compositeDisposable = compositeDisposable;
            _bonuses = bonuses;
            _startPos = startPosition.position;
        }

        private void Subscribe()
        {
            _messageBroker.Receive<StickFallCompletedMessage>().Subscribe(OnStickFallCompleted).AddTo(_compositeDisposable);
            _messageBroker.Receive<MoveFailedMessage>().Subscribe(OnMoveFailed).AddTo(_compositeDisposable);
            _messageBroker.Receive<MoveSuccessfulMessage>().Subscribe(OnMoveSuccessfull).AddTo(_compositeDisposable);
        }

        private void OnStickFallCompleted(StickFallCompletedMessage message)
        {

        }

        private void OnMoveFailed(MoveFailedMessage message)
        {

        }

        private void OnMoveSuccessfull(MoveSuccessfulMessage message)
        {
            if (bonusObj != null)
            {
                _bonuses.ToCollect();
            }
            _bonuses.SaveBonuses();
            CreateBonus();
        }


        private void CreateBonus()
        {
            Random random = new Random();
            Bonuses bonus;
            if (random.Next(0, 1) == 0)
            {
                bonus = Bonuses.Star;
            }
            else
            {
                bonus = Bonuses.Hearth;
            }

            bonusObj = _bonuses.CreateBonus(bonus, PosBonus);
        }

        private void DeleteBonus()
        {
            bonusObj.SetActive(false);
            UnityEngine.Object.Destroy(bonusObj);
            bonusObj = null;

        }

        public void Dispose() => _compositeDisposable.Dispose();

    }
}
