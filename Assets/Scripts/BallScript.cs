using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject Ball1;
    [SerializeField] private GameObject Ball2;
    [SerializeField] private GameObject Ball;
    [SerializeField] private  float LiveTime;
    [SerializeField] private float TimeForCreate;
    [SerializeField] private float LimitStrength;
    [SerializeField] Score _score;
    [SerializeField] private float Strength;
    
    private AudioSource audioSource;
    private bool IsMoved;
    private Rigidbody _ball;
    private void Start()
    {
        audioSource = gameObject.transform.GetComponent<AudioSource>();
        CreateBall();
    }
    private void CreateBall()
    {

        var ball = Instantiate(Ball);
        IsMoved = false;
        _ball = ball.GetComponent<Rigidbody>();
    }
    private IEnumerator Reload()
    {
        yield return new WaitForSeconds(TimeForCreate);
        _score.attempts += 1;
        _score.GiveScore();
        CreateBall();
    }

    public void BallType(int choice)
    {
        if (choice == 0)
        {
           
            Ball = Ball2;            
        }
        if (choice == 1)
        {
            
            Ball = Ball1;

        }
        if (IsMoved == false)
        {
            Destroy(_ball.gameObject);
            CreateBall();
        }
    }

    private void Move()
    {
        IsMoved = true;
        Destroy(_ball.gameObject, LiveTime);
        _ball.isKinematic = false;
        _ball.velocity = transform.forward * -Strength;
        Strength = 0f;
        _ball = null;
        StartCoroutine(Reload());
    }

    void Update()
    {
        if (Input.GetMouseButton(1) && _ball != null && Strength < LimitStrength)
        {
            Strength += 1f;
        }

        if (Input.GetMouseButtonUp(1) && _ball != null)
        {
            if (audioSource != null)
            {
                audioSource.Play();
            }
            Move();
        }

    }
}
