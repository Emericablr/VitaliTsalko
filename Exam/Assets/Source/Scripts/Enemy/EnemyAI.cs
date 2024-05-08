using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public Animator animator; 
    [Header("Attack Config")] 
    public float Cooldown;
    public float AttackRange;
    public int Damage;
    public float hitChance = 50;
    [Header("Patrolling Config")]
    public float lookingRange; 
    public LayerMask playerMask;
    [Space(10)]
    public NavMeshAgent agent;
    public float walkRadius;
    public float distanceTreshold;
    [Header("Debug")]
    public Transform player;

    EnemyState currentState;

    const string ENABLE_ANIMATOR = "EnableAnimator"; 
    private void Start()
    {
        ChangeState(new PatrolState()); 

        Invoke(ENABLE_ANIMATOR, UnityEngine.Random.value);
        animator.enabled = false; 
    }

    private void EnableAnimator()
    {
        animator.enabled = true;
    }

    private void Update()
    {
        UpdateState();
    }

    
    public void ChangeState(EnemyState newState)
    {
        if (currentState != null)
        {
            currentState.ExitState();
        }

        currentState = newState; 
        currentState.AI = this;

        currentState.EnterState(); 
    }

    
    public void UpdateState()
    {
        if (currentState != null)
        {
            currentState.UpdateState();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, walkRadius);
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(transform.position, lookingRange);
        Gizmos.color = Color.yellow;
        Vector3 position = transform.position;
        position.y += 1;
        Gizmos.DrawWireSphere(position, AttackRange);
    }
}

public class ChaseState : EnemyState
{
    float lookingRange; 
    int Damage; 
    float AttackRange;
    float coolDown; 
    float elapsedTime;
    float hitChance; 

    Animator animator; 

    NavMeshAgent agent; 
    Transform player;

    void SetVariables()
    {
        animator = AI.animator;
        lookingRange = AI.lookingRange;
        agent = AI.agent;
        hitChance = AI.hitChance;
        Damage = AI.Damage;
        AttackRange = AI.AttackRange;
        coolDown = AI.Cooldown;
        player = AI.player;
    }

    public override void EnterState()
    {
        SetVariables();
        agent.isStopped = false;
    }

    public override void UpdateState()
    {
        agent.destination = player.position; 

        Vector3 position = AI.transform.position;
        position.y += 1;
        Vector3 playerPosition = player.position;

        elapsedTime += Time.deltaTime;

        if (elapsedTime >= coolDown)
        {
            if (Vector3.Distance(position, playerPosition) <= AttackRange)
            {
                agent.isStopped = true;
                animator.SetTrigger("Attack"); 

                var colliders = Physics.OverlapSphere(position, AttackRange, AI.playerMask); 

                if (colliders.Length > 0) 
                {
                    if (colliders[0].transform.TryGetComponent(out IDamageable damageable)) 
                    {
                        if (UnityEngine.Random.Range(0f, 100f) < hitChance) 
                            damageable.Hit(Damage); 
                    }
                }
            }

            agent.isStopped = false; 
            elapsedTime = 0; 
        }

        if (Vector3.Distance(AI.transform.position, AI.player.position) > lookingRange) 
        {
            AI.ChangeState(new PatrolState()); 
        }
    }

    public override void ExitState()
    {
        player = null;
    }
}

public class PatrolState : EnemyState
{
    float lookingRange;
    LayerMask playerMask;
    NavMeshAgent agent; 
    float walkRadius; 
    float distanceTreshold; 

    Vector3 randomDirection;

    void SetVariables()
    {
        lookingRange = AI.lookingRange;
        playerMask = AI.playerMask;
        agent = AI.agent;
        walkRadius = AI.walkRadius;
        distanceTreshold = AI.distanceTreshold;
    }

    public override void EnterState()
    {
        SetVariables();
        agent.isStopped = false; 

        randomDirection = UnityEngine.Random.insideUnitSphere * walkRadius;
        randomDirection += AI.transform.position;
        NavMeshHit hit;
        NavMesh.SamplePosition(randomDirection, out hit, walkRadius, 1);
        Vector3 finalPosition = hit.position;

        agent.destination = finalPosition; 
    }

    public override void UpdateState()
    {
        if (Vector3.Distance(AI.transform.position, agent.destination) <= distanceTreshold) 
        {
            randomDirection += AI.transform.position;
            NavMeshHit hit;
            NavMesh.SamplePosition(randomDirection, out hit, walkRadius, 1);
            Vector3 finalPosition = hit.position;

            agent.destination = finalPosition; 
        }

        var player = Physics.OverlapSphere(AI.transform.position, lookingRange, playerMask); 

        if (player.Length != 0) 
        {
            AI.player = player[0].transform; 
            agent.isStopped = true; 
            AI.ChangeState(new ChaseState()); 
        }
    }

    public override void ExitState()
    {

    }
}
