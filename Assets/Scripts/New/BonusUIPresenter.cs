using Models;
using Models.Interfaces;
using Presenters.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UniRx;
using UnityEngine;
using Zenject;


namespace Presenters.Menu
{
    public class BonusUIPresenter : MonoBehaviour
    {
        private IBonusModel _model;
        [SerializeField] private TMP_Text _scoreStars;
        [SerializeField] private TMP_Text _scoreHearths;


        [Inject]
        private void Inject(IBonusModel bonusModel) => _model = bonusModel;

        private void Start()
        {
            int Stars = 0;
            int Hearths = 0;

            Debug.Log(_model.CollectedBonuses);

            if(_model.CollectedBonuses != null)
            {
                for (int i = 0; i < _model.CollectedBonuses.Count; i++)
                {
                    if (_model.CollectedBonuses[i] == Bonuses.Star) { Stars++; }
                    if (_model.CollectedBonuses[i] == Bonuses.Hearth) { Hearths++; }
                }
            }

            _scoreHearths.text = Convert.ToString(Hearths);
            _scoreStars.text = Convert.ToString(Stars);
        }
    }
}
