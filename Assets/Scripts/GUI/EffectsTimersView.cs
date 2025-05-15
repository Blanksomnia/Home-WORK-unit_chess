using System.Collections.Generic;
using UnityEngine;

public class EffectsTimersView : MonoBehaviour
{
    public GameObject tableTimerPrefab;
    public Transform parentEffectsCollect;
    public Sprite Speed;
    public Sprite Blaster;
    public Sprite unknown;

    public void ActivateEffect(List<Effect> effects, TimerManager manager)
    {
        for (int i = 0; i < effects.Count; i++)
        {
            if (effects[i].second > 0) 
            {
                EffectsParemetresGUI effect = Instantiate(tableTimerPrefab, parentEffectsCollect).GetComponent<EffectsParemetresGUI>();
                effect.nameEffect.text = effects[i]._name;
                effect.icon.sprite = GetSprite(effects[i]._name);
                manager.Add(new Timer(effects[i].second, effect.timerEverySecond, effect.timerEnd));
            }
        }
    }

    private Sprite GetSprite(string name)
    {
        switch(name)
        {
            case "Speed": return Speed;
            case "Blaster": return Blaster;
            default: return unknown;
        }
    }

}
