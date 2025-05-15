using UnityEngine;

public class Player : Character
{
    [SerializeField] BoxCollider checkAir;
    [SerializeField] BoxCollider checkClimb;
    [SerializeField] BoxCollider checkItems;
    public Rigidbody rb;

    [SerializeField] int _damage;

    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioSource audioSourceJump;

    public ManagerUpdates updates;

    public float speedClimb = 0.2f;
    public float powerJump;
    public float distanceToGetDamageOnGround = 10f;

    public LayerMask layerGround;
    public LayerMask layerClimb;
    public LayerMask layerCharacter;
    public LayerMask layerInteractiveItems;
    public LayerMask layerItems;

    public AnimationCharacter animations;
    public AudioPlayer sound;
    public PlayerMode mode;
    public ColliderChecks checks;
    public CharacterInventory checkInventory;
    public TimerManager timerManager = new TimerManager();
    StateMachineCharacter<StatePlayer> stateMachine;

    public bool lockJump = false;
    public bool lockMove = false;
    public bool lockAttack = false;

    public Vector2 turnMove;
    public Vector2 turnToJump;

    public Transform enemy;
    public int damage => _damage;

    public bool debug = false;

    private void Start()
    {
        checks = new ColliderChecks(checkClimb, checkAir, this);
        checkInventory = new CharacterInventory(this, layerItems, checkItems, updates.characters);
        sound = new AudioPlayer(audioSource, audioSourceJump);
        animations = new AnimationCharacter(GetComponent<Animator>());
        mode = new PlayerMode(this);
        stateMachine = new StateMachineCharacter<StatePlayer>();
        stateMachine.AddState(new IdleState(this));
        stateMachine.AddState(new MovementState(this));
        stateMachine.AddState(new JumpState(this));
        stateMachine.AddState(new ClimbState(this));
        stateMachine.AddState(new AttackState(this));
        stateMachine.AddState(new InteractiveItemState(this));
        stateMachine.AddState(new DeadState(this));

        inventory.selectAction += StartUseItem;
        changeHealthAction += GetDamage;
    }

    private void GetDamage(int value)
    {
        if (health > value)
        {
            animations.GetDamage();
            sound.GetDamage();
        }
    }

    private void StartUseItem(string select, int num)
    {
        if (!mode.onTheEdgeOfTheWall)
            animations.ChangeArm(select, num);
    }

    public override void Updata(float deltaTime)
    {
        if(enabled == false)
            stateMachine.state = StatePlayer.Idle;
        else
        {
            if (health == 0)
                stateMachine.state = StatePlayer.Dead;
            else if (checks.EnemyChecks(out enemy) && !mode.onTheEdgeOfTheWall && !lockAttack)
            {
                powerJump /= 2;
                stateMachine.state = StatePlayer.AttackInAir;
                stateMachine.state = StatePlayer.Jump;
                powerJump *= 2;
            }
            else if (turnMove == Vector2.zero)
                stateMachine.state = StatePlayer.Idle;
            else if (!mode.onTheEdgeOfTheWall && !lockMove)
                stateMachine.state = StatePlayer.Movement;
            else if (mode.onTheEdgeOfTheWall && !lockMove)
                stateMachine.state = StatePlayer.Climb;

            timerManager.Updata(deltaTime);
            checkInventory.CheckAddToInventory();
            mode.Update();
            animations.Update(deltaTime);
            stateMachine.Update(deltaTime);
        }    
    }

    public void InteractiveItem()
    {
        if(!mode.onTheEdgeOfTheWall)
        stateMachine.state = StatePlayer.InteractiveItem;
    }

    public void Move(Vector2 turn)
    {
        turnMove = turn;
    }

    public void Jump()
    {
        if (!lockJump)
            stateMachine.state = StatePlayer.Jump;
    }

    private void OnDrawGizmos()
    {
        if (debug)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawRay(checkAir.transform.position - transform.up * 0.2f, -transform.up);
            Gizmos.color = Color.white;
            Gizmos.DrawCube(checkAir.transform.position, checkAir.size*2.5f);
            Gizmos.color = Color.white;
            Gizmos.DrawCube(checkClimb.transform.position, checkClimb.size * 2.5f);
            Gizmos.color = Color.red;
            Gizmos.DrawCube(checkClimb.transform.position - transform.forward * 0.5f + new Vector3(0, 0.4f, 0), checkClimb.size * 2.5f);
            Gizmos.color = Color.red;
            Gizmos.DrawCube(checkClimb.transform.position + transform.forward * checkClimb.size.z*2.5f + new Vector3(0, checkClimb.size.y*2.5f, 0), checkClimb.size*2.5f);
            Gizmos.color = Color.white;
            Gizmos.DrawCube(checkClimb.transform.position + (transform.right * 1.3f), checkClimb.size * 2.5f);
            Gizmos.DrawCube(checkClimb.transform.position + (-transform.right * 1.3f), checkClimb.size * 2.5f);
            Gizmos.color = Color.red;
            Vector3 currentCenter = transform.position + new Vector3(0, 4f, 0) + transform.forward * 1.5f + new Vector3(0, 3f, 0);
            Vector3 currentSize = new Vector3(1.2f, 1.2f, 1f);
            Gizmos.DrawCube(currentCenter, currentSize);

        }
    }
}
