using UnityEngine;


[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Item/Item", order = 1)]
public class EmptyItem : Item
{
    TimerDelegate timerEnd;

    public bool canUse = true;

    public EmptyItem(EmptyItem item) 
    {
        timerEnd += canActivate;
        this._name = item._name;
        this.material = item.material;
        this.mesh = item.mesh;
        this.effects = item.effects;
        this.duration = item.duration;
        this.audioActivate = item.audioActivate;
        this.creatorEffects = item.creatorEffects;
        this.scale = item.scale;
    }


    public override void Activate(Character character, AudioSource audio)
    {
        if (canUse)
        {
            if(duration > 0)
            {
                canUse = false;
                creatorEffects.managerTimer.Add(new Timer(duration, null, timerEnd));
            }

            if (audioActivate != null)
            {
                audio.clip = audioActivate;
                audio.Play();
            }

            if (effects.Count > 0)
                for (int i = 0; i < effects.Count; i++)
                    creatorEffects.ActivateEffect(effects[i], character);

            Use(character);
        }
        
    }



    public virtual void Use(Character character)
    {

    }

    private void canActivate()
    {
        canUse = true;
    }
}
