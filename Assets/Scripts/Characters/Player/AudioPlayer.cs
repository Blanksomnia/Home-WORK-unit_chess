using UnityEngine;

public class AudioPlayer
{
    AudioSource source;
    AudioSource sourceJump;
    AudioClip getDamage;
    AudioClip die;
    AudioClip movement;

    public AudioPlayer(AudioSource source, AudioSource sourceJump)
    {
        this.source = source;
        this.sourceJump = sourceJump;
        string path = "Audio/";
        getDamage = Resources.Load(path + "Get damage") as AudioClip;
        die = Resources.Load(path + "Die") as AudioClip;
        movement = Resources.Load(path + "Movement") as AudioClip;
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
    public void Jump() => sourceJump.Play();
    public void Dead() => GetSourceClip(die);

    private void GetSourceClip(AudioClip clip)
    {
        source.clip = clip;
        source.Play();
    }
}
