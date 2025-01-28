using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdGUI : MonoBehaviour
{
    [SerializeField] AudioSource audio;
    [SerializeField] AudioClip Win;
    [SerializeField] AudioClip Lose;
    Interfaces interfaces;
    PlayerMemory player;

    void Awake()
    {
        interfaces = GetComponent<Interfaces>();
        player = GetComponent<PlayerMemory>();
        UpdateInterface();
    }

    public void UpdateInterface()
    {
        interfaces.healthBaR.value = (float)player._health / player._healthMax * 100;
        interfaces.textCoins.text = player._coins + "/" + player._coinsMax;

        if(player._health == 0)
        {
            interfaces.gameOver.SetActive(true);
            audio.clip = Lose;
            audio.Play();
        }

        if(player._coins == player._coinsMax)
        {
            interfaces.WellDone.SetActive(true);
            audio.clip = Win;
            audio.Play();
        }
    }



}
