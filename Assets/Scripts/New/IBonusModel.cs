using System.Collections;
using System.Collections.Generic;
using UniRx;
using Zenject;
using UnityEngine;

public enum Bonuses
{
    Star = 0,
    Hearth = 1
}

public interface IBonusModel : IInitializable
{
    ReactiveProperty<int> stars { get; }
    ReactiveProperty<int> starsBest { get; }
    ReactiveProperty<int> hearths { get; }
    ReactiveProperty<int> hearthsBest { get; }


    void SaveBonuses();

    void ToCollect(Bonuses type);

    void ResetCollect(bool ResetAll);

}
