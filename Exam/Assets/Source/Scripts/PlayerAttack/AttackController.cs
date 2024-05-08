using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class AttackController : MonoBehaviour
{
    [SerializeField] float delay = 0.5f; 
    Animator animator; 
    float elapsedTime;
    float attackTime; 
    [Header("Axe Head Config")] 
    [SerializeField] Transform axeHead;
    [SerializeField] float radius; 
    [Header("Enemies Config")] 
    [SerializeField] LayerMask enemyMask; 
    [SerializeField] LayerMask treeMask;
    [Header("Damage Config")] 
    public int EnemyDamage; 
    public int TreeDamage; 
    Color currentGizmoColor = Color.white; 

    [Space]
    public UnityEvent onAttack; 

    const string ATTACK_ANIMATOR_TRIGGER = "Chop"; 

    private void Update()
    {
        elapsedTime += Time.deltaTime; 

        if (Input.GetMouseButton(0) && elapsedTime >= delay) 
        {
            Attack(); 
            elapsedTime = 0; 
        }
    }
    private void Start()
    {
        animator = GetComponent<Animator>(); 
    }

    public void IncreaseTreeDamage(int amount)
    {
        TreeDamage += amount; 
    }

    public void Attack()
    {
        animator.SetTrigger(ATTACK_ANIMATOR_TRIGGER); 
        StartCoroutine(attack()); 
        onAttack.Invoke(); 
    }

    IEnumerator attack()
    {
        while (attackTime < delay) 
        {
            attackTime += Time.deltaTime; 

            currentGizmoColor = Color.red; 

            Collider[] colliders = Physics.OverlapSphere(axeHead.transform.position, radius, enemyMask);

            if (colliders.Length > 0)
            {
                if (colliders[0].TryGetComponent(out IDamageable damageable)) 
                {
                    damageable.Hit(EnemyDamage); 
                    yield break; 
                }
            }

            Collider[] treeColliders = Physics.OverlapSphere(axeHead.transform.position, radius, treeMask);

            if (treeColliders.Length > 0)
            {
                if (treeColliders[0].TryGetComponent(out IDamageable damageable)) 
                {
                    damageable.Hit(TreeDamage); 
                    yield break; 
                }
            }

            yield return null; 

        }

        attackTime = 0; 
        currentGizmoColor = Color.white; 
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = currentGizmoColor;
        Gizmos.DrawSphere(axeHead.transform.position, radius); 
    }
}
