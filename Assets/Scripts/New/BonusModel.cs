using Core.Pools;
using Core.SaveLoad;
using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Pool;
using Zenject;

public struct StarsUpd { }

public struct HearthsUpd { }



namespace Models
{
    public sealed class BonusModel : IBonusModel
    {
        //Score bonuses
        private const string BonusesHearthsKeyBest = "HearthKeyBest";
        private const string BonusesStarsKeyBest = "StarKeyBest";
        public ReactiveProperty<int> stars => _stars;
        public ReactiveProperty<int> starsBest => _starsBest;
        public ReactiveProperty<int> hearths => _hearths;
        public ReactiveProperty<int> hearthsBest => _hearthsBest;


        private ReactiveProperty<int> _stars = new ReactiveProperty<int>(0);
        private ReactiveProperty<int> _starsBest = new ReactiveProperty<int>(0);
        private ReactiveProperty<int> _hearths = new ReactiveProperty<int>(0);
        private ReactiveProperty<int> _hearthsBest = new ReactiveProperty<int>(0);

        private readonly ISaveLoadDataHandler _saveLoadDataHandler;

        private List<Bonuses> collect = new();
        public BonusModel(ISaveLoadDataHandler saveLoadDataHandler) 
        {
            _saveLoadDataHandler = saveLoadDataHandler;
        }

        public void SaveBonuses()
        {
            if (_stars.Value > _starsBest.Value) { _starsBest.Value = _stars.Value; }
            if (_hearths.Value > _hearthsBest.Value) { _hearthsBest.Value = _hearths.Value; }
            _saveLoadDataHandler.SaveInt(BonusesHearthsKeyBest, _hearthsBest.Value);
            _saveLoadDataHandler.SaveInt(BonusesStarsKeyBest, _starsBest.Value);
        }

        private void LoadBonuses()
        {
            _stars = new ReactiveProperty<int>(0);
            _hearths = new ReactiveProperty<int>(0);
            _starsBest = new ReactiveProperty<int>(_saveLoadDataHandler.TryLoadInt(BonusesStarsKeyBest, out var ScoreHB) ? ScoreHB : 0);
            _hearthsBest = new ReactiveProperty<int>(_saveLoadDataHandler.TryLoadInt(BonusesHearthsKeyBest, out var ScoreSB) ? ScoreSB : 0);

        }

        public void Initialize()
        {
            LoadBonuses();
        }

        public void ToCollect(Bonuses type)
        {
            if(type == Bonuses.Star) { _stars.Value++; }
            if(type == Bonuses.Hearth) { _hearths.Value++; }
            collect.Add(type);
        }

        public void ResetCollect(bool ResetAll)
        {
            if(ResetAll)
            {
                _stars = new ReactiveProperty<int>(0);
                _hearths = new ReactiveProperty<int>(0);
                collect.Clear();

            }
            else 
            { 
                Bonuses type = collect[collect.Count - 1];
                collect.RemoveAt(collect.Count - 1);
                if(type == Bonuses.Hearth)
                {
                    _hearths.Value--;
                }

                if(type == Bonuses.Star)
                {
                    _stars.Value--;
                }
            }
        }


    }
}
