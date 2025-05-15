using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupscribeGUIPlayer : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] InventoryView inventoryView;
    [SerializeField] EffectsTimersView timers;
    [SerializeField] HealthbarView healthbar;
    [SerializeField] GameObject gameOver;

    private void Start()
    {
        player.inventory.selectAction += inventoryView.SelectItem;
        player.inventory.activateSelectedAction += ActivateEffect;
        player.changeHealthAction += healthbar.ChangeValue;
        player.changeHealthAction += CheckHealth;
        healthbar.ChangeValue(player.health);
    }

    private void ActivateEffect(List<Effect> effects, string name)
    {
        if(effects.Count > 0)
        timers.ActivateEffect(effects, player.timerManager);
    }

    private void CheckHealth(int value)
    {
        if(value <= 0)
            gameOver.SetActive(true);
    }
}
