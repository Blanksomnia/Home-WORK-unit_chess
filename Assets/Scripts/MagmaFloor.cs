using UnityEngine;

public class MagmaFloor : MonoBehaviour
{
    [SerializeField] Character player;
    [SerializeField] private int damage = 5;
    TimerManager timerManager = new TimerManager();
    AudioSource _audio;
    private bool canDamage = true;
    TimerDelegate timerDelegate;

    public void Awake()
    {
        timerDelegate += ActivateDamage;
        _audio = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        _audio.Play();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.transform == player.transform && other.tag != "Dead")
        {
            timerManager.Updata(Time.fixedDeltaTime);
            if (canDamage)
            {
                player.health -= damage;
                canDamage = false;
                Timer timer = new Timer(1, null, timerDelegate);
                timerManager.Add(timer);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.transform == player.transform) 
        {
            canDamage = true;
            timerManager.Clear();
        }

        _audio.Play();
    }


    private void ActivateDamage()
    {
        canDamage = true;
    }

}
