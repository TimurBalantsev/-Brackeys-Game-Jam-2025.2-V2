using HitBox;
using System.Collections;
using UnityEngine;

public class Enemy : Entity.Entity, AttackHitBoxSource
{
    [SerializeField] private Rigidbody2D rigidBody;
    [SerializeField] private Animator animator;
    [SerializeField] private EnemyFOV enemyFOV;
    [SerializeField] private AttackHitBox attackHitBox;
    [SerializeField] private LayerMask obstacleMask;

    [SerializeField] private float targetLostDelay = 5f;
    [SerializeField] public float attackStartRange = 0.1f;
    [SerializeField] public int maxPatrolDistance = 1;
    [SerializeField] public float maxIdleTime = 5f;
    [SerializeField] public float patrolDuration = 5f;
    [SerializeField] private float minDistance = 0.1f;

    private EnemyState activeState;
    public Animator Animator => animator;
    public LayerMask ObstacleMask => obstacleMask;

    public Entity.Entity Target { get; private set; }

    private Coroutine loseTargetCoroutine;
    private Vector2 lastMovementDirection;

    private void OnEnable()
    {
        enemyFOV.OnTargetSpotted += EnemyFOV_OnTargetSpotted;
        enemyFOV.OnTargetLost += EnemyFOV_OnTargetLost;
    }

    private void OnDisable()
    {
        enemyFOV.OnTargetSpotted -= EnemyFOV_OnTargetSpotted;
        enemyFOV.OnTargetLost -= EnemyFOV_OnTargetLost;
    }

    private void EnemyFOV_OnTargetSpotted(Entity.Entity target)
    {
        if (loseTargetCoroutine != null)
        {
            StopCoroutine(loseTargetCoroutine);
            loseTargetCoroutine = null;
        }

        Target = target;
        EnemyState newState = activeState.Input(target);
        if (newState != null)
        {
            ChangeState(newState);
        }
        else
        {
        }
    }

    private void EnemyFOV_OnTargetLost()
    {
        if (loseTargetCoroutine != null)
        {
            StopCoroutine(loseTargetCoroutine);
        }

        loseTargetCoroutine = StartCoroutine(LoseTargetAfterDelay());
    }

    private IEnumerator LoseTargetAfterDelay()
    {
        yield return new WaitForSeconds(targetLostDelay);

        Target = null;
        EnemyState newState = activeState.Input(null);
        if (newState != null)
        {
            ChangeState(newState);
        }
        else
        {
        }

        loseTargetCoroutine = null;
    }

    private void Start()
    {
        ChangeState(new EnemyIdleState());
    }

    private void Update()
    {
        StateUpdate();

        UpdateFOVRotation(lastMovementDirection);
        UpdateAttackHitboxRotation(lastMovementDirection);
    }

    private void FixedUpdate()
    {
        StateFixedUpdate();
    }

    private void StateFixedUpdate()
    {
        EnemyState newState = activeState.FixedUpdate(Time.deltaTime);
        if (newState != null) ChangeState(newState);
    }

    private void ChangeState(EnemyState newState)
    {
        activeState?.Exit();
        activeState = newState;
        activeState.Enter(this);
    }

    private void StateUpdate()
    {
        EnemyState newState = activeState.Update(Time.deltaTime);
        if (newState != null) ChangeState(newState);
    }

    public void Move(Vector2 movementDirection)
    {
        float distance = Vector2.Distance(transform.position, Target.transform.position);
        if (distance < minDistance)            return;
        Vector2 newPosition = rigidBody.position + movementDirection * (stats.speed * Time.fixedDeltaTime);
        rigidBody.MovePosition(newPosition);
        lastMovementDirection = movementDirection;
    }

    private void UpdateFOVRotation(Vector2 movementDirection)
    {
        float angle = Mathf.Atan2(movementDirection.y, movementDirection.x) * Mathf.Rad2Deg;

        enemyFOV.transform.rotation = Quaternion.Euler(0, 0, angle - 90f);
    }

    private void UpdateAttackHitboxRotation(Vector2 movementDirection)
    {
        float angle = Mathf.Atan2(movementDirection.y, movementDirection.x) * Mathf.Rad2Deg;
        attackHitBox.transform.rotation = Quaternion.Euler(0, 0, angle - 90f);
    }

    protected override void Die()
    {
        Debug.Log($"Enemy {name} died");
    }
}
