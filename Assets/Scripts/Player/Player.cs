using System;
using UnityEngine;

public class Player : Entity.Entity
{
    [SerializeField] private Rigidbody2D rigidBody;
    [SerializeField] private Animator animator;
    [SerializeField] private Transform followPoint;
    
    [SerializeField] private LayerMask damageableLayerMask;
    [SerializeField] private Inventory inventory;

    [SerializeField] public AudioSource walkSoundLoop;
    [SerializeField] public AudioSource hurtSound;
    [SerializeField] public AudioSource deathSound;

    public static Player Instance;

    public Inventory Inventory => inventory;
    
    //TODO put new animation in attack without the FX

    public Transform FollowPoint => followPoint;
    public Animator Animator => animator;
    private PlayerState activeState;
    private InputManager.InputActions currentInputAction = InputManager.InputActions.NONE;
    private Camera mainCam;
    private bool attackInputHeld = false;

    public Vector2 lastMovement;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError($"more than one player in scene");
        }
        Instance = this;
    }

    private void Start()
    {
        mainCam = Camera.main;
        ChangeState(new PlayerIdleState());
        InputManager.Instance.OnAttackPerformed += InputManager_OnAttackPerformed;
        // InputManager.Instance.OnAttackReleased += InputManager_OnAttackReleased;
        InputManager.Instance.OnInventory += InputManager_OnInventoryPressed;
        stats.Initialize();
    }

    private void InputManager_OnInventoryPressed(object sender, EventArgs e)
    {
        InventoryUIController.Instance.TogglePlayerInventory(this);
    }

    private void InputManager_OnAttackPerformed(object sender, EventArgs e)
    {
        attackInputHeld = true;
        currentInputAction = InputManager.InputActions.ATTACK; 
        PlayerState newState = activeState.Input(currentInputAction);
        if (newState != null) ChangeState(newState);
    }
    
    // private void InputManager_OnAttackReleased(object sender, EventArgs e)
    // {
    //     attackInputHeld = false;
    // }

    private void Update()
    {
        // HandleFlip(mainCam!.ScreenToWorldPoint(Input.mousePosition));
        StateInput();
        StateUpdate();
    }

    private void FixedUpdate()
    {
        StateFixedUpdate();
    }

    private void StateFixedUpdate()
    {
        PlayerState newState = activeState.FixedUpdate(Time.deltaTime);
        if(newState != null) ChangeState(newState);
    }

    private void ChangeState(PlayerState newState)
    {
        activeState?.Exit();
        activeState = newState;
        activeState.Enter(this);
    }

    private void StateInput()
    {
        //this only check movement states, as other states will be instantly overwritten by an event call.
        // if (attackInputHeld)
        // {
        //     currentInputAction = InputManager.InputActions.ATTACK;
        // }
        if (InputManager.Instance.GetMovementDirection().Equals(Vector2.zero))
        {
            currentInputAction = InputManager.InputActions.NONE;
        }
        else
        {
            currentInputAction = InputManager.InputActions.MOVE;
        }

        
        PlayerState newState = activeState.Input(currentInputAction);
        if(newState != null) ChangeState(newState);
    }

    private void StateUpdate()
    {
        PlayerState newState = activeState.Update(Time.deltaTime);
        if(newState != null) ChangeState(newState);
    }

    public void Move(Vector2 movementDirection)
    {
        Vector2 newPosition = rigidBody.position + movementDirection * (stats.speed * Time.fixedDeltaTime);
        rigidBody.MovePosition(newPosition);
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        hurtSound.pitch = UnityEngine.Random.Range(0.8f, 1f);
        hurtSound.Play();
    }

    protected override void Die()
    {
        base.Die();
        ChangeState(new PlayerDeathState());
        Debug.Log("Player died");
    }
}
