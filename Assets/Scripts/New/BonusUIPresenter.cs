using Messages;
using Models;
using Models.Interfaces;
using Presenters.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;


namespace Presenters.Menu
{
    public class BonusUIPresenter : MonoBehaviour
    {
        private IBonusModel _model;
        [SerializeField] private TMP_Text _scoreStars;
        [SerializeField] private TMP_Text _scoreHearths;
        [SerializeField] private TMP_Text _scoreBestHearths;
        [SerializeField] private TMP_Text _scoreBestStars;
        IMessageBroker _messageBroker;

        [Inject]
        private void Inject(IBonusModel bonusModel, IMessageBroker _message) { _model = bonusModel; _messageBroker = _message; }

        private void Start()
        {
            _messageBroker.Receive<MoveSuccessfulMessage>().Subscribe(OnMoveSuccessfull).AddTo(this);
            UpdateTitle();

        }
        private void UpdateTitle()
        {
            _scoreHearths.text = Convert.ToString(_model.hearths);
            _scoreStars.text = Convert.ToString(_model.stars);

            _scoreBestHearths.text = Convert.ToString(_model.hearthsBest);
            _scoreBestStars.text = Convert.ToString(_model.starsBest);
        }

        private void OnMoveSuccessfull(MoveSuccessfulMessage message) { UpdateTitle(); }
    }
}
