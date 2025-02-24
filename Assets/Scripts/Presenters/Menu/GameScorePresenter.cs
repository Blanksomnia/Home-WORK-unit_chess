using Models.Interfaces;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Presenters.Menu
{
    public sealed class GameScorePresenter : MonoBehaviour
    {
        [SerializeField] private TMP_Text _currentScore;
        [SerializeField] private TMP_Text _StarsScore;
        [SerializeField] private TMP_Text _HearthScore;
        [FormerlySerializedAs("_maxScore")] [SerializeField] private TMP_Text _bestScore;

        [Inject]
        private void Inject(IGameScoreModel model, IBonusModel modelB)
        {
            model.CurrentScore.Subscribe(CurrentScoreUpdated).AddTo(this);
            model.BestScore.Subscribe(BestScoreUpdated).AddTo(this);
            _bestScore.text = model.BestScore.Value.ToString();
            int Stars = 0;
            int Hearths = 0;


            if(modelB.CollectedBonuses != null)
            {
                for (int i = 0; i < modelB.CollectedBonuses.Count; i++)
                {
                    if (modelB.CollectedBonuses[i] == Bonuses.Star)
                    {
                        Stars++;
                    }

                    if (modelB.CollectedBonuses[i] == Bonuses.Hearth)
                    {
                        Hearths++;
                    }
                }
            }

            _StarsScore.text = Stars.ToString();
            _HearthScore.text = Hearths.ToString();
        }

        private void CurrentScoreUpdated(int score) => _currentScore.text = score.ToString();
        
        private void BestScoreUpdated(int score) => _bestScore.text = score.ToString();
    }
}