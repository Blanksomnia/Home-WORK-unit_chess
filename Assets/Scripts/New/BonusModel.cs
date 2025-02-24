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

namespace Models
{
    public sealed class BonusModel : IBonusModel
    {
        //Score bonuses
        private const string BonusesStarsKey = "StarKey";
        private const string BonusesHearthsKey = "HearthKey";
        public IReadOnlyReactiveCollection<Bonuses> CollectedBonuses => BonusesScore;

        private IReactiveCollection<Bonuses> BonusesScore;

        private readonly ISaveLoadDataHandler _saveLoadDataHandler;
        public BonusModel(ISaveLoadDataHandler saveLoadDataHandler) => _saveLoadDataHandler = saveLoadDataHandler;

        public void SaveBonuses()
        {
            int Stars = 0;
            int Hearths = 0;

            for (int i = 0; i < BonusesScore.Count; i++)
            {
                if (BonusesScore[i] == Bonuses.Star)
                {
                    Stars++;
                }

                if (BonusesScore[i] == Bonuses.Hearth)
                {
                    Hearths++;
                }

            }

            _saveLoadDataHandler.SaveInt(BonusesStarsKey, Stars);
            _saveLoadDataHandler.SaveInt(BonusesHearthsKey, Hearths);
        }

        private void LoadBonuses()
        {
            ReactiveProperty<int> Stars = new ReactiveProperty<int>(_saveLoadDataHandler.TryLoadInt(BonusesStarsKey, out var ScoreS) ? ScoreS : 0);
            ReactiveProperty<int> Hearths = new ReactiveProperty<int>(_saveLoadDataHandler.TryLoadInt(BonusesHearthsKey, out var ScoreH) ? ScoreH : 0);

            for (int i = 0; i < Stars.Value; i++)
            {
                BonusesScore.Add(Bonuses.Star);
            }

            for (int i = 0; i < Hearths.Value; i++)
            {
                BonusesScore.Add(Bonuses.Hearth);
            }

        }

        public void Initialize()
        {
            LoadBonuses();
        }
        //


        //ModelGameobject

        GameObject StarGM;
        GameObject HearthGM;
        Bonuses TypeBonus;

        public BonusModel([Inject(Id = "Star")] GameObject starB, [Inject(Id = "Hearth")] GameObject hearthB)
        {
            StarGM = starB;
            HearthGM = hearthB;
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

        public GameObject CreateBonus(Bonuses type, Vector2 to)
        {
            TypeBonus = type;
            return UnityEngine.Object.Instantiate(BonusType(type), to, Quaternion.identity);
        }

        public void ToCollect()
        {
            BonusesScore.Add(TypeBonus);
        }

        public void ResetCollect()
        {
            BonusesScore.Clear();
        }

        //
    }
}
