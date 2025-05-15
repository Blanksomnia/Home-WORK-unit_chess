using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class AudioEnemy
{
    AudioSource source;
    AudioSource sourceNoise;
    AudioClip getDamage;
    AudioClip die;
    AudioClip movement;
    List<AudioClip> zombieNoise = new List<AudioClip>();
    Random rand = new Random();

    public AudioEnemy(AudioSource source, AudioSource noiseZombie)
    {
        this.source = source;
        sourceNoise = noiseZombie;
        string path = "Audio/";
        getDamage = Resources.Load(path + "Get damage") as AudioClip;
        die = Resources.Load(path + "Die") as AudioClip;
        movement = Resources.Load(path + "Movement") as AudioClip;
        zombieNoise.Add(Resources.Load(path + "Zombie noise0") as AudioClip);
        zombieNoise.Add(Resources.Load(path + "Zombie noise1") as AudioClip);
    }

    public void StartMove()
    {
        source.loop = true;
        GetSourceClip(movement);
    }

    public void Stop()
    {
        source.loop = false;
        source.Stop();
    }

    public void GetDamage() => GetSourceClip(getDamage);
    public void ZombieNoise()
    {
        int index = rand.Next(0, zombieNoise.Count);
        sourceNoise.clip = zombieNoise[index];
        sourceNoise.Play();
    }

    public void Dead() => GetSourceClip(die);

    private void GetSourceClip(AudioClip clip)
    {
        source.clip = clip;
        source.Play();
    }
}
