using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Effect/Speed", order = 1)]
public class Speed : Effect
{
    Character character;
    public float value;

    public override void GetEffect(Character character)
    {
        this.character = character;
        character.speed += value;
    }

    public override void EndTimer()
    {
       character.speed -= value;
    }

    
}
