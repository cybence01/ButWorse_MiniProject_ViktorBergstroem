using UnityEngine;  
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] float currentHealth;
    [SerializeField] float maxHealth = 100;
    [SerializeField] float moveSpeed = 5f;

    [Header("Attack Settings")]
    [SerializeField] float attackDamage = 20f;
    [SerializeField] float attackRange = 2f;
    [SerializeField] float timeBetweenAttacks = 1.5f;
    private float nextAttackTime = 0f;


    NavMeshAgent agent;
    Transform target;
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        currentHealth = maxHealth;

        if (agent != null)
        {
            agent.speed = moveSpeed;
        }   
    }

    void Update()
    {
        if (target != null)
        {
            // Move to target
            agent.SetDestination(target.position);

            float distanceToPlayer = Vector3.Distance(transform.position, target.position);

            if (distanceToPlayer <= attackRange && Time.time >= nextAttackTime)
            {
                AttackPlayer();
                nextAttackTime = Time.time + timeBetweenAttacks;
            }
        }
    }

    void AttackPlayer()
    {
        PlayerHealth playerHealth = target.GetComponent<PlayerHealth>();

        if (playerHealth != null)
        {
            playerHealth.TakeDamage(attackDamage);
            Debug.Log("Enemy Attacked Player!");
        }
    }

    

    public void TakeDamage(float damageAmount)
    {
       
        currentHealth -= damageAmount;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        if (RoundManager.Instance != null)
        {
            RoundManager.Instance.EnemyKilled();
        }
         RoundManager.Instance.GivePoints();

        Destroy(gameObject);
    }
}