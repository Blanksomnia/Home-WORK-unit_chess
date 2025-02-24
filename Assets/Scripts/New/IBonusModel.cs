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
    IReadOnlyReactiveCollection<Bonuses> CollectedBonuses { get; }

    GameObject CreateBonus(Bonuses type, Vector2 to);

    void SaveBonuses();

    void ToCollect();

    void ResetCollect();

}
