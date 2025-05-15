using UnityEngine;


[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Effect/Health", order = 1)]
public class Health : Effect
{
    Character character;
    public int value = 0;

    public override void GetEffect(Character character)
    {
        this.character = character;
        character.health += value;
    }

    public override void EndTimer()
    {
        character.health -= value;
    }
}
