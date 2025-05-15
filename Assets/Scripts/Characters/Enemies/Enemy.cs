using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Character
{
    public ManagerUpdates updates;
    public TimerManager timer = new TimerManager();
    TimerDelegate randomZombieNoise;

    public Transform spine;
    [SerializeField] ItemObject weapon;
    public float distanceLookAtPlayer = 10;
    public float distanceToAttack;
    public float cornerLook = 90;
    public float durationMoveToPoints = 20;

    [SerializeField] AudioSource source;
    [SerializeField] AudioSource sourceForZombieNoise;

    StateMachineCharacter<StateEnemy> stateMachine;
    public AnimationCharacter animations;
    public NavMeshAgent agent;
    public CheckTarget checkPlayer;
    public AudioEnemy sound;

    public LayerMask unit;
    public LayerMask obstical;
    public CapsuleCollider capsule;
    public BoxCollider box;

    public List<Transform> pointsToMove = new List<Transform>();


    public Vector3 moveTo = Vector3.zero;
    public Transform target;

    private void Start()
    {
        capsule = GetComponent<CapsuleCollider>();
        box = GetComponent<BoxCollider>();
        sound = new AudioEnemy(source, sourceForZombieNoise);
        animations = new AnimationCharacter(GetComponent<Animator>());
        checkPlayer = new CheckTarget(this);
        stateMachine = new StateMachineCharacter<StateEnemy>();
        stateMachine.AddState(new IdleStateEnemy(this));
        stateMachine.AddState(new MoveStateEnemy(this));
        stateMachine.AddState(new AttackStateEnemy(this));
        stateMachine.AddState(new DeadStateEnemy(this));

        inventory.selectAction += animations.ChangeArm;
        inventory.Add(weapon);
        inventory.activateSelectedAction += animations.ActivateItem;
        changeHealthAction += GetDamage;
        changeSpeedAction += ChangeSpeed;
        randomZombieNoise = NoiseZombie;
    }

    private void NoiseZombie()
    {
        sound.ZombieNoise();
        timer.Add(new Timer(30, null, randomZombieNoise));
    }

    private void GetDamage(int value)
    {
        if (health > value)
        {
            animations.GetDamage();
            sound.GetDamage();
        }
    }

    private void ChangeSpeed(float value)
    {
        if(agent.speed != 0)
        agent.speed = value;
    }

    public override void Updata(float deltaTime)
    {
        timer.Updata(deltaTime);
        stateMachine.Update(deltaTime);
        checkPlayer.Check();
        CheckState();
    }


    private void CheckState()
    {
        if (health <= 0)
            stateMachine.state = StateEnemy.Dead;
        else if (target != null && moveTo == Vector3.zero)
            stateMachine.state = StateEnemy.Attack;
        else if (moveTo == Vector3.zero)
            stateMachine.state = StateEnemy.Idle;
        else if (moveTo != Vector3.zero)
            stateMachine.state = StateEnemy.Move;
    }

}
