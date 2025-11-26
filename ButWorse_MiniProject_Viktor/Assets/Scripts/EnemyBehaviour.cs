using UnityEngine;  
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] float currentHealth;
    [SerializeField] float maxHealth = 100;
    [SerializeField] float moveSpeed = 5f;


    NavMeshAgent agent;
    Transform target;
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        target = GameObject.Find("Player").transform;
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
            // 2. This single line handles pathfinding, rotation, and movement
            agent.SetDestination(target.position);
        }
    }

    

    public void TakeDamage(float damageAmount)
    {
        Debug.Log($"Damage amount: {damageAmount}");
        currentHealth -= damageAmount;
        Debug.Log($"Health is now: {currentHealth}");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // Notify round manager that this enemy was killed
        if (RoundManager.Instance != null)
        {
            RoundManager.Instance.EnemyKilled();
        }
         RoundManager.Instance.GivePoints();

        Destroy(gameObject);
    }
}