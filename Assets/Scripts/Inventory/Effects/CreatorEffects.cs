using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/creatorEffects", order = 1)]
public class CreatorEffects : ScriptableObject
{
    public TimerManager managerTimer = new TimerManager();
    TimerDelegate endTime;

    public void ActivateEffect(Effect effect, Character character)
    {     
        if(effect != null)
        {
            effect.GetEffect(character);
            if (effect.second > 0)
            {
                endTime += effect.EndTimer;
                Timer timer = new Timer(effect.second, null ,endTime);
                endTime -= effect.EndTimer;
                managerTimer.Add(timer);
            }

        }

    }

    public void Updata(float deltaTime)
    {
        managerTimer.Updata(deltaTime);
    }

}
