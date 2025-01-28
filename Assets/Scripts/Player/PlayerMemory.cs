using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject.SpaceFighter;

public class PlayerMemory : MonoBehaviour
{
    UpdGUI inter;
    InputPlayer player;
    [SerializeField] int Health = 100;
    [SerializeField] int HealthMax = 100;

    [SerializeField] float Speed = 1;

    [SerializeField] int Coins = 0;
    [SerializeField] int CoinsMax = 15;

    public int _health => Health;
    public int _healthMax => HealthMax;
    public float _speed => Speed;
    public int _coins => Coins;
    public int _coinsMax => CoinsMax;

    private void Awake()
    {
        inter = GetComponent<UpdGUI>();
        player = GetComponent<InputPlayer>();
    }

    public void GetCoin(int F)
    {
        if (F < 0) return;
        Coins += F;

        if(Coins == CoinsMax) { player.enabled = false; };

        inter.UpdateInterface();
    }

    public void GetSpeed(float F)
    {
        if (F < 0) return;
        Speed += F;
    }

    public void GetDamage(int F)
    {
        if(F < 0) return;

        Health -= F;
        if(Health <= 0)
        {
            Health = 0;   
            player.enabled = false;
        }

        inter.UpdateInterface();
    }

    public void GetHealth(int F)
    {
        if (F < 0) return;

        Health += F;

        if (Health > HealthMax)
        {
            Health = HealthMax;
        }

        inter.UpdateInterface();
    }

    public void GetLoad(int H, int C, float S, Vector3 pos, Quaternion rot)
    {
        Health = H;
        Coins = C;
        Speed = S;
        transform.position = pos;
        transform.rotation = rot;
        if(inter != null)
        inter.UpdateInterface();
    }

}
